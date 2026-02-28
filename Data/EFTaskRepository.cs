using Microsoft.EntityFrameworkCore;
using Mission08_Team5.Models;

namespace Mission08_Team5.Data;

public class EFTaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public EFTaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<TaskItem> Tasks => _context.Tasks.Include(t => t.Category);

    public TaskItem? GetTaskById(int taskId)
    {
        return _context.Tasks
            .Include(t => t.Category)
            .FirstOrDefault(t => t.TaskId == taskId);
    }

    public void AddTask(TaskItem task)
    {
        _context.Tasks.Add(task);
    }

    public void UpdateTask(TaskItem task)
    {
        _context.Tasks.Update(task);
    }

    public void DeleteTask(TaskItem task)
    {
        _context.Tasks.Remove(task);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}

