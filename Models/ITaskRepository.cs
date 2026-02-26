using System.Linq;

namespace Mission08_Team5.Models;

public interface ITaskRepository
{
    IQueryable<TaskItem> Tasks { get; }

    TaskItem? GetTaskById(int taskId);

    void AddTask(TaskItem task);

    void UpdateTask(TaskItem task);

    void DeleteTask(TaskItem task);

    void SaveChanges();
}

