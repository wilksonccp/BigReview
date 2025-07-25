namespace TaskManager.Api.DTOs;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
public class UpdateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
