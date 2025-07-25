using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services;

public interface ITaskService
{
    List<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Create(CreateTaskDto tdo);
    bool Delete(int id);
    TaskItem? Update(int id, UpdateTaskDto tdo);
}
