using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TaskManager.BusinessLayer;
using TaskManager.Entities;

namespace TaskManager.Api.Controllers
{
    [RoutePrefix("api")]
    public class TaskController : ApiController
    {
        ITaskApplication _app;
        public TaskController() : this(new TaskApplication()) { }

        public TaskController(ITaskApplication app)
        {
            this._app = app;
        }

        [HttpGet]
        [Route("GetTask")]
        public List<TaskModel> GetTask()
        {
            return _app.GetTasks().ToList();
        }

        [HttpGet]
        [Route("Task/{taskId}")]
        public TaskModel Task(int taskId)
        {
            return _app.GetTaskByTaskId(taskId);
        }

        [HttpPost]
        [Route("AddTask")]
        public void AddTask(TaskModel task)
        {
            _app.AddTask(task);
        }

        [HttpDelete]
        [Route("EndTask/{taskId}")]
        public void EndTask(int taskId)
        {
            _app.EndTask(taskId);
        }

        [HttpPost]
        [Route("UpdateTask")]
        public void UpdateTask(TaskModel task)
        {
            _app.UpdateTask(task);
        }
    }
}