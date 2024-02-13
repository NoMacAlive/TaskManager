using Task = ForsythBarr.Server.Domain.Models.Task;
namespace ForsythBarr.Server.Infrastructure.Repositories;

public interface ITaskRepository
{
    public IEnumerable<Task> GetAllTasks();

    public Task? GetTaskById(int id);

    public void AddTask(Task task);

    public void UpdateTask(Task task);

    public void DeleteTask(int id);
}