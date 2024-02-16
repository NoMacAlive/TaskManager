using System;
using System.Collections.Generic;
using System.Linq;
using ForsythBarr.Server.Domain.Models;
using ForsythBarr.Server.Infrastructure;
using ForsythBarr.Server.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Task = ForsythBarr.Server.Domain.Models.Task;
using TaskStatus = ForsythBarr.Server.Domain.Models.TaskStatus;

namespace forsythbarr.test.Repositories
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private ApplicationDbContext _dbContext;
        private TaskRepository _taskRepository;

        [SetUp]
        public void SetUp()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _taskRepository = new TaskRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.DisposeAsync();
        }

        [Test]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new Task  {
                    Id = 50,
                    Title = "Complete Project Proposal",
                    Description = "Write and submit the project proposal by next week.",
                    DueDate = new DateTime(2024, 2, 20),
                    Priority = 2, 
                    Status = TaskStatus.Pending
                },
                new Task {
                    Id = 2,
                    Title = "Review Pull Request",
                    Description = "Review all 3 pull requests.",
                    DueDate = new DateTime(2024, 2, 20),
                    Priority = 3, 
                    Status = TaskStatus.Pending
                },
            };
            _dbContext.Tasks.AddRange(tasks);
            _dbContext.SaveChanges();

            // Act
            var result = _taskRepository.GetAllTasks();
            Console.WriteLine(_dbContext.Tasks);
            // Assert
            Assert.AreEqual(tasks.Count, result.Count());
        }

        [Test]
        public void GetTaskById_ExistingId_ShouldReturnTask()
        {
            // Arrange
            var task = new Task
            {
                Id = 3,
                Title = "Review Pull Request",
                Description = "Review all 3 pull requests.",
                DueDate = new DateTime(2024, 2, 20),
                Priority = 3,
                Status = TaskStatus.Pending
            };
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();

            // Act
            var result = _taskRepository.GetTaskById(3);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(task.Id, result.Id);
        }
        
        [Test]
        public void AddTask_ValidTask_ShouldAddToDatabase()
        {
            // Arrange
            var newTask = new Task
            {
                Title = "New Task",
                Description = "Sample description",
                DueDate = DateTime.Now.AddDays(7),
                Priority = 2,
                Status = TaskStatus.Pending
            };

            // Act
            _taskRepository.AddTask(newTask);

            // Assert
            var addedTask = _dbContext.Tasks.Find(newTask.Id);
            Assert.IsNotNull(addedTask);
            Assert.AreEqual(newTask.Title, addedTask.Title);
        }

        [Test]
        public void UpdateTask_ExistingTask_ShouldUpdateProperties()
        {
            // Arrange
            var existingTask = new Task
            {
                Id = 10,
                Title = "Existing Task",
                Description = "Old description",
                DueDate = DateTime.Now.AddDays(3),
                Priority = 1,
                Status = TaskStatus.InProgress
            };
            _dbContext.Tasks.Add(existingTask);
            _dbContext.SaveChanges();

            // Modify the task
            existingTask.Title = "Updated Task";
            existingTask.Description = "New description";
            existingTask.DueDate = DateTime.Now.AddDays(5);
            existingTask.Priority = 3; // Low priority
            existingTask.Status = TaskStatus.Completed;

            // Act
            _taskRepository.UpdateTask(existingTask);

            // Assert
            var updatedTask = _dbContext.Tasks.Find(existingTask.Id);
            Assert.IsNotNull(updatedTask);
            Assert.AreEqual(existingTask.Title, updatedTask.Title);
            Assert.AreEqual(existingTask.Description, updatedTask.Description);
            Assert.AreEqual(existingTask.DueDate, updatedTask.DueDate);
            Assert.AreEqual(existingTask.Priority, updatedTask.Priority);
            Assert.AreEqual(existingTask.Status, updatedTask.Status);
        }

        [Test]
        public void DeleteTask_ExistingId_ShouldRemoveFromDatabase()
        {
            // Arrange
            var taskToDelete = new Task
            {
                Id = 13,
                Title = "Existing Task",
                Description = "Old description",
                DueDate = DateTime.Now.AddDays(3),
                Priority = 1,
                Status = TaskStatus.InProgress
            };
            _dbContext.Tasks.Add(taskToDelete);
            _dbContext.SaveChanges();

            // Act
            _taskRepository.DeleteTask(taskToDelete.Id);

            // Assert
            var deletedTask = _dbContext.Tasks.Find(taskToDelete.Id);
            Assert.IsNull(deletedTask);
        }
    }
}