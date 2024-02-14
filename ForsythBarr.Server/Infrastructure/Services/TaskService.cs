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

    public async Task<int> AddTask(Task task)
    {
        taskRepository.AddTask(task);
        await mediator.Publish(new TaskCreatedEvent()
        {
            Id = task.Id,
            UserId = 0,
        });

        return task.Id;
    }

    public async System.Threading.Tasks.Task UpdateTask(Task task)
    {
        var existingTask = taskRepository.GetTaskById(task.Id);
        if (existingTask == null)
        {
            throw new InvalidOperationException($"Task with ID {task.Id} not found.");
        }
        taskRepository.UpdateTask(task);

        if (existingTask.Priority != task.Priority)
        {
            await mediator.Publish(new TaskPriorityUpdatedEvent()
            {
                Id = task.Id,
                UserId = 0,
                OldPriority = existingTask.Priority,
                NewPriority = task.Priority
            });
        }
        
        if (existingTask.DueDate != task.DueDate)
        {
            await mediator.Publish(new TaskDueDateUpdatedEvent()
            {
                Id = task.Id,
                UserId = 0,
                OldDueDate = existingTask.DueDate,
                NewDueDate = task.DueDate
            });
        }
        
        if (existingTask.Status != task.Status)
        {
            await mediator.Publish(new TaskStatusUpdatedEvent()
            {
                Id = task.Id,
                UserId = 0,
                NewStatus = existingTask.Status,
                OldStatus = task.Status
            });
        }
    }

    public void DeleteTask(int id)
    {
        taskRepository.DeleteTask(id);
    }
}