using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyToDoApp.Models;

namespace MyTodoApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {  
    }

    // Criado depois tendo o <> com o nome da classe dentro
     public DbSet<ToDoModel> ToDos {get; set;}
}
