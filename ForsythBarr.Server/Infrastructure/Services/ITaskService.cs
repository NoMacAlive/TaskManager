using Task = ForsythBarr.Server.Domain.Models.Task;
namespace ForsythBarr.Server.Infrastructure.Services;

public interface ITaskService
{
    IEnumerable<Task> GetAllTasks();
    Task GetTaskById(int id);
    System.Threading.Tasks.Task AddTask(Task task);
    void UpdateTask(Task task);
    void DeleteTask(int id);
}
