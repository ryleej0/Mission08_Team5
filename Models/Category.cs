using System.ComponentModel.DataAnnotations;

namespace Mission08_Team5.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}

