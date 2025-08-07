using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(
    ITaskService taskService,
    ILogger<TaskController> logger
) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;
    private readonly ILogger<TaskController> _logger = logger;

    // GET: api/task
    [HttpGet]
    public ActionResult<List<TaskItem>> GetAll()
    {
        _logger.LogInformation("Listing all tasks");
        var tasks = _taskService.GetAll();
        return Ok(tasks);
    }

    // GET: api/task/{id}
    [HttpGet("{id}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = _taskService.GetById(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    // POST: api/task
    [HttpPost]
    public ActionResult<TaskItem> Create([FromBody] CreateTaskDto tdo)
    {
        _logger.LogInformation("Creating new task: {Title}", tdo.Title);

        var created = _taskService.Create(tdo);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/task/{id}
    [HttpPut("{id}")]
    public ActionResult<TaskItem> Update(int id, [FromBody] UpdateTaskDto tdo)
    {
        var updated = _taskService.Update(id, tdo);
        if (updated == null)
        {
            _logger.LogWarning("Task with ID {Id} not found for update", id);
            return NotFound();
        }
        _logger.LogInformation("Task with ID {Id} updated successfully", id);
        return Ok(updated);
    }

    // DELETE: api/task/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var success = _taskService.Delete(id);
        if (!success)
        {
            _logger.LogWarning("Task with ID {Id} not found for deletion", id);
            return NotFound();
        }
        _logger.LogInformation("Task with ID {Id} deleted successfully", id);
        return NoContent();
    }
}
