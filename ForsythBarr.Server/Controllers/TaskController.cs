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
}