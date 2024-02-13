using ForsythBarr.Server.Infrastructure;
using ForsythBarr.Server.Infrastructure.EventHandlers;
using ForsythBarr.Server.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task = ForsythBarr.Server.Domain.Models.Task;
using TaskStatus = ForsythBarr.Server.Domain.Models.TaskStatus;

namespace ForsythBarr.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITaskService _taskService;
        private readonly IMediator _mediator;

        public WeatherForecastController(ApplicationDbContext context, ILogger<WeatherForecastController> logger, ITaskService taskService, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _taskService = taskService;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _taskService.AddTask(new Task()
            {
                Description = "TestDescription",
                DueDate = DateTime.Now,
                Priority = 3,
                Title = "TestTitle",
                Status = TaskStatus.Pending
            });
            List<Task> tasks = _taskService.GetAllTasks().ToList();
            
            foreach(var task in tasks)
            {
                Console.WriteLine($"Task title is: {task.Title}");
            }
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
