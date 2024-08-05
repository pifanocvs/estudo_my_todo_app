using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyToDoApp.Models;
using MyTodoApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace MyTodoApp
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDo
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToDos.ToListAsync());
        }

        // GET: ToDo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // GET: ToDo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Done,Created,LastUpdated,User")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                // adicionando o usuario logado na criação do todo
                toDoModel.User = User.Identity.Name;
                _context.Add(toDoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoModel);
        }

        // GET: ToDo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDos.FindAsync(id);
            if (toDoModel == null)
            {
                return NotFound();
            }
            return View(toDoModel);
        }

        // POST: ToDo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Done,Created,LastUpdated,User")] ToDoModel toDoModel)
        {
            if (id != toDoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoModelExists(toDoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoModel);
        }

        // GET: ToDo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.ToDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // POST: ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoModel = await _context.ToDos.FindAsync(id);
            if (toDoModel != null)
            {
                _context.ToDos.Remove(toDoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoModelExists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
