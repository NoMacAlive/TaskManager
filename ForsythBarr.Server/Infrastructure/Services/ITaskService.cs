﻿using Task = ForsythBarr.Server.Domain.Models.Task;
namespace ForsythBarr.Server.Infrastructure.Services;

public interface ITaskService
{
    IEnumerable<Task> GetAllTasks();
    Task GetTaskById(int id);
    System.Threading.Tasks.Task<int> AddTask(Task task);
    System.Threading.Tasks.Task UpdateTask(Task task);
    void DeleteTask(int id);
}
