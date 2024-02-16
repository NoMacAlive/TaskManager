using ForsythBarr.Server.Domain.Models;
using ForsythBarr.Server.Infrastructure.EventHandlers;
using ForsythBarr.Server.Infrastructure.Repositories;
using ForsythBarr.Server.Infrastructure.Services;
using MediatR;
using Moq;
using Task = ForsythBarr.Server.Domain.Models.Task;


namespace forsythbarr.test.Services
{
    [TestFixture]
    public class TaskServiceTests
    {
        private Mock<ITaskRepository> _taskRepositoryMock;
        private Mock<IMediator> _mediatorMock;
        private TaskService _taskService;

        [SetUp]
        public void SetUp()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _mediatorMock = new Mock<IMediator>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _mediatorMock.Object);
        }

        [Test]
        public void GetTaskById_ExistingTask_ReturnsTask()
        {
            // Arrange
            var taskId = 42;
            var existingTask = new Task() { Id = taskId, Title = "Sample Task" };
            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns(existingTask);

            // Act
            var result = _taskService.GetTaskById(taskId);

            // Assert
            Assert.AreEqual(existingTask, result);
        }

        [Test]
        public void GetTaskById_NonExistingTask_ThrowsException()
        {
            // Arrange
            var taskId = 99;
            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns((Task)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _taskService.GetTaskById(taskId));
        }
        
        [Test]
        public async System.Threading.Tasks.Task AddTask_ValidTask_PublishesEventAndReturnsTaskId()
        {
            // Arrange
            var task = new Task { Id = 42, Title = "New Task" };

            // Act
            var result = await _taskService.AddTask(task);

            // Assert
            Assert.AreEqual(task.Id, result);

            // Verify that AddTask was called with the correct task
            _taskRepositoryMock.Verify(repo => repo.AddTask(task), Times.Once);

            // Verify that TaskCreatedEvent was published
            _mediatorMock.Verify(mediator => mediator.Publish(
                It.Is<TaskCreatedEvent>(e => e.Id == task.Id && e.UserId == 0),
                default), Times.Once);
        }

        [Test]
        public async System.Threading.Tasks.Task AddTask_InvalidTask_ThrowsException()
        {
            // Arrange
            var invalidTask = new Task { Id = 99, Title = "Invalid Task" };
            _taskRepositoryMock.Setup(repo => repo.AddTask(invalidTask)).Throws(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _taskService.AddTask(invalidTask));
        }
        
        [Test]
        public async System.Threading.Tasks.Task UpdateTask_ExistingTask_UpdatesAndPublishesEvents()
        {
            // Arrange
            var existingTask = new Task { Id = 42, Priority = 0, DueDate = DateTime.Now.AddDays(1) };
            var updatedTask = new Task { Id = 42, Priority = 1, DueDate = DateTime.Now.AddDays(2) };

            _taskRepositoryMock.Setup(repo => repo.GetTaskById(existingTask.Id)).Returns(existingTask);

            // Act
            await _taskService.UpdateTask(updatedTask);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.UpdateTask(updatedTask), Times.Once);

            _mediatorMock.Verify(mediator => mediator.Publish(
                It.Is<TaskPriorityUpdatedEvent>(e => e.Id == updatedTask.Id && e.NewPriority == updatedTask.Priority),
                default), Times.Once);

            _mediatorMock.Verify(mediator => mediator.Publish(
                It.Is<TaskDueDateUpdatedEvent>(e => e.Id == updatedTask.Id && e.NewDueDate == updatedTask.DueDate),
                default), Times.Once);
        }
        
        [Test]
        public void DeleteTask_ExistingTask_DeletesSuccessfully()
        {
            // Arrange
            var taskId = 42;
            var existingTask = new Task { Id = taskId };

            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns(existingTask);

            // Act & Assert
            Assert.DoesNotThrow(() => _taskService.DeleteTask(taskId));
            _taskRepositoryMock.Verify(repo => repo.DeleteTask(taskId), Times.Once);
        }

        [Test]
        public void DeleteTask_NonExistingTask_ThrowsException()
        {
            // Arrange
            var taskId = 99;
            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns((Task)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _taskService.DeleteTask(taskId));
        }
    }
}
