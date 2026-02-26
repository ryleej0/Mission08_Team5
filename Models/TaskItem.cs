using System.ComponentModel.DataAnnotations;

namespace Mission08_Team5.Models;

public class TaskItem
{
    public int TaskId { get; set; }

    [Required]
    public string TaskDescription { get; set; } = string.Empty;

    public DateTime? DueDate { get; set; }

    [Required]
    [Range(1, 4)]
    public int Quadrant { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public bool Completed { get; set; } = false;
}

