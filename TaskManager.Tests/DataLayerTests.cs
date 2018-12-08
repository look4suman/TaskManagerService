using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskManager.DataLayer;

namespace TaskManager.Tests
{
    [TestFixture]
    public class DataLayerTests
    {
        [Test]
        public void GetTasksData()
        {
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB" },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Task_Table>>();
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Task_Table).Returns(mockSet.Object);

            var service = new TaskRepository(mockContext.Object);
            var tasks = service.GetTasks();

            Assert.AreEqual(data.Count(), tasks.Count());
            Assert.AreEqual(data.First().Task, tasks.First().Task);
        }

        [Test]
        public void GetSpecificTasksData()
        {
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB", Task_ID = 5 },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Task_Table>>();
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();

            var service = new TaskRepository(mockContext.Object);
            mockContext.Setup(c => c.Task_Table).Returns(mockSet.Object);

            var task = service.GetTaskByTaskId(5);

            Assert.AreEqual(data.First().Task, task.Task);
        }

        [Test]
        public void AddTaskData()
        {
            var mockSet = new Mock<DbSet<Task_Table>>();

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(m => m.Task_Table).Returns(mockSet.Object);

            var service = new TaskRepository(mockContext.Object);
            service.AddTask(new Task_Table() { });

            mockSet.Verify(m => m.Add(It.IsAny<Task_Table>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void EndTaskData()
        {
            var mockSet = new Mock<DbSet<Task_Table>>();
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB", Task_ID=5 },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();

            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Task_Table).Returns(mockSet.Object);

            var service = new TaskRepository(mockContext.Object);
            service.EndTask(It.IsAny<int>());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateTaskData()
        {
            var mockSet = new Mock<DbSet<Task_Table>>();
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB", Task_ID=5 },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();

            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Task_Table).Returns(mockSet.Object);

            var service = new TaskRepository(mockContext.Object);
            service.DeleteTask(It.IsAny<int>());

            mockSet.Verify(m => m.Remove(It.IsAny<Task_Table>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
