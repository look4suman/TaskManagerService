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
        public TaskModel Task(int taskId)
        {
            return _app.GetTaskByTaskId(taskId);
        }

        [HttpPost]
        public void AddTask(TaskModel task)
        {
            _app.AddTask(task);
        }

        [HttpPut]
        public void EndTask(TaskModel task)
        {
            _app.EndTask(task);
        }

        [HttpPost]
        public void UpdateTask(TaskModel task)
        {
            _app.UpdateTask(task);
        }
    }
}