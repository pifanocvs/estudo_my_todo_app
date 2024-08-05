using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// mudança de namespace por quickfix nao podia ser my_todo_app
namespace MyToDoApp.Models;

// nome da classe com model para incentivo de boa prática
public class ToDoModel
{
    public int Id {get; set;}

    /* parametros adicionais do campo
    public required string Title {get; set;} 
    - com o quick fix pro uso do required
    não é necessario o parametro pro usuario final 
    o build pediu mas pesquisando que colocar ? depois do type tira o aviso */
    [DisplayName("Título")]
    [Required(ErrorMessage = "Insira o título.")]
    public string? Title {get; set;}

    [DisplayName("Concluído")]
    public bool Done {get; set;} 

    [DisplayName ("Criado em")]
    public DateTime Created {get; set;} = DateTime.Now;

    [DisplayName ("Atualizado em")]
    public DateTime LastUpdated{get; set;} = DateTime.Now;
    [DisplayName ("Usuário")]
    public string? User {get; set;} 
}