using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services;

public class TaskService : ITaskService
{
    private static int _currentId = 1;
    private static readonly List<TaskItem> _tasks = new();
    
    public List<TaskItem> GetAll()
    {
        return _tasks;
    }
    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }
    public TaskItem Create(CreateTaskDto tdo)
    {
        var task = new TaskItem
        {
            Id = _currentId++,
            Title = tdo.Title,
            IsCompleted = tdo.IsCompleted
        };
        _tasks.Add(task);
        return task;
    }
    public bool Delete(int id)
    {
        var task = GetById(id);
        if (task == null) return false;
        _tasks.Remove(task);
        return true;
    }
    public TaskItem? Update(int id, UpdateTaskDto tdo)
    {
        var task = GetById(id);
        if (task == null) return null;
        task.Title = tdo.Title;
        task.IsCompleted = tdo.IsCompleted;
        return task;
    }
}
