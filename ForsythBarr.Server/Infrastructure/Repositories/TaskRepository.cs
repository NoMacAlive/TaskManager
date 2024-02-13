using Task = ForsythBarr.Server.Domain.Models.Task;
namespace ForsythBarr.Server.Infrastructure.Repositories;

public class TaskRepository(ApplicationDbContext dbContext) : ITaskRepository
{
    public IEnumerable<Task> GetAllTasks()
    {
        return dbContext.Set<Task>().ToList();
    }

    public Task? GetTaskById(int id)
    {
        return dbContext.Set<Task>().Find(id);
    }

    public void AddTask(Task task)
    {
        dbContext.Set<Task>().Add(task);
        dbContext.SaveChanges();
    }

    public void UpdateTask(Domain.Models.Task task)
    {
        var existingTask = dbContext.Set<Task>().Find(task.Id);
        if (existingTask == null) return;
        
        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.Priority = task.Priority;
        existingTask.Status = task.Status;

        dbContext.SaveChanges();
    }

    public void DeleteTask(int id)
    {
        var task = dbContext.Set<Task>().Find(id);
        if (task == null) return;
        
        dbContext.Set<Task>().Remove(task);
        dbContext.SaveChanges();
    }
}