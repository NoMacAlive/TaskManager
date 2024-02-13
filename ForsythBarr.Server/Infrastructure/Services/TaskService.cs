using Task = ForsythBarr.Server.Domain.Models.Task;
using ForsythBarr.Server.Infrastructure.Repositories;

namespace ForsythBarr.Server.Infrastructure.Services;

public class TaskService(ITaskRepository taskRepository) : ITaskService
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

    public void AddTask(Task task)
    {
        taskRepository.AddTask(task);
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