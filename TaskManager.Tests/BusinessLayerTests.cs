using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;
using TaskManager.Entities;

namespace TaskManager.Tests
{
    [TestFixture]
    public class BusinessLayerTests
    {
        [Test]
        public void GetTasks()
        {
            var mockObj = new Mock<ITaskRepository>();
            var businessLayer = new TaskApplication(mockObj.Object);
            var tasks = new List<Task_Table>();
            tasks.Add(new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 });

            mockObj.Setup(x => x.GetTasks()).Returns(tasks);
            //Assert
            var actualTasks = businessLayer.GetTasks();
            Assert.AreEqual(tasks.Count(), actualTasks.Count());
            Assert.AreEqual(tasks.FirstOrDefault().Task, actualTasks.FirstOrDefault().Task);
        }

        [Test]
        public void GetSpecificTask()
        {
            var mockObj = new Mock<ITaskRepository>();
            var businessLayer = new TaskApplication(mockObj.Object);
            var tasks = new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7) };
            mockObj.Setup(x => x.GetTaskByTaskId(It.IsAny<int>())).Returns(tasks);

            //Assert
            var actualTasks = businessLayer.GetTaskByTaskId(It.IsAny<int>());
            Assert.AreEqual(tasks.Task, actualTasks.Task);
        }

        [Test]
        public void EndTask()
        {
            var mockObj = new Mock<ITaskRepository>();
            var businessLayer = new TaskApplication(mockObj.Object);

            //Assert
            businessLayer.EndTask(5);
            mockObj.Verify(x => x.EndTask(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void UpdateTask()
        {
            var mockObj = new Mock<ITaskRepository>();
            var businessLayer = new TaskApplication(mockObj.Object);

            //Assert
            businessLayer.UpdateTask(new TaskModel() { TaskId = 5 });
            mockObj.Verify(x => x.AddTask(It.IsAny<Task_Table>()), Times.Once);
            mockObj.Verify(x => x.DeleteTask(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void AddTask()
        {
            var mockObj = new Mock<ITaskRepository>();
            var businessLayer = new TaskApplication(mockObj.Object);

            //Assert
            businessLayer.AddTask(new TaskModel() { TaskId = 5 });
            mockObj.Verify(x => x.AddTask(It.IsAny<Task_Table>()), Times.Once);
        }

        [Test]
        public void IsTaskExists()
        {
            var mockObj = new Mock<ITaskRepository>();
            var businessLayer = new TaskApplication(mockObj.Object);
            mockObj.Setup(x => x.IsTaskExists(It.IsAny<string>())).Returns(true);
            
            //Assert
            Assert.IsTrue(businessLayer.IsTaskExists("test"));
        }
    }
}