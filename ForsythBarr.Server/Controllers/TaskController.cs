using ForsythBarr.Server.Controllers.RequestModels;
using ForsythBarr.Server.Controllers.Validators;
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
        var validator = new CreateNewTaskRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
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
        return CreatedAtRoute("CreateNewTask", new { id = newTaskId }, newTask);
    }
    
    [HttpPut(Name = "UpdateTask")]
    public async Task<IActionResult> Put([FromBody] UpdateTaskRequest request)
    {
        var validator = new UpdateTaskRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var newTask = new Task()
        {
            Id = request.Id,
            Description = request.Description,
            Title = request.Title,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = request.Status
        };
        await taskService.UpdateTask(newTask);
        return NoContent();
    }
    
    [HttpDelete(Name = "DeleteTask")]
    public IActionResult Delete([FromRoute] int id)
    {
        taskService.DeleteTask(id);
        return Ok();
    }
}