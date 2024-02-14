using ForsythBarr.Server.Controllers.RequestModels;
using ForsythBarr.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Task = ForsythBarr.Server.Domain.Models.Task;

namespace ForsythBarr.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController(ITaskService taskService) : ControllerBase
{
    [HttpGet(Name = "GetAllTasks")]
    public IEnumerable<Task> Get()
    {
        return  taskService.GetAllTasks();
    }
    
    [HttpPost(Name = "CreateNewTask")]
    public async Task<IActionResult> Post([FromBody] CreateNewTaskRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid task data.");
        }

        var newTask = new Task()
        {
            Description = request.Description,
            Title = request.Title,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = request.Status
        };
        int newTaskId = await taskService.AddTask(newTask);
        return CreatedAtRoute("GetAllTasks", new { id = newTaskId }, newTask);
    }
}