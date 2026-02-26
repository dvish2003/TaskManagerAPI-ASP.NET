using System.ComponentModel.DataAnnotations;
namespace TaskManagerAPI.Model;

public class TaskUser
{
    public int Id { get; set; }
    public required string  Title { get; set; }
    public required string Description { get; set; }
    public required string Email { get; set; }
    public required User User { get; set; }

}
