using ForsythBarr.Server.Infrastructure.EventHandlers;
using Task = ForsythBarr.Server.Domain.Models.Task;
using ForsythBarr.Server.Infrastructure.Repositories;
using MediatR;

namespace ForsythBarr.Server.Infrastructure.Services;

public class TaskService(ITaskRepository taskRepository, IMediator mediator) : ITaskService
{
    public IEnumerable<Task> GetAllTasks()
    {
        return taskRepository.GetAllTasks();
    }

    public Task GetTaskById(int id)
    {
        var task = taskRepository.GetTaskById(id);
        if (task == null)
        {
            throw new InvalidOperationException($"Task with ID {id} not found.");
        }
        return task;
    }

    public async System.Threading.Tasks.Task AddTask(Task task)
    {
        taskRepository.AddTask(task);
        await mediator.Publish(new TaskCreatedEvent()
        {
            Id = task.Id,
            UserId = 0,
        });
    }

    public void UpdateTask(Task task)
    {
        if (taskRepository.GetTaskById(task.Id) == null)
        {
            throw new InvalidOperationException($"Task with ID {task.Id} not found.");
        }
        taskRepository.UpdateTask(task);
    }

    public void DeleteTask(int id)
    {
        taskRepository.DeleteTask(id);
    }
}