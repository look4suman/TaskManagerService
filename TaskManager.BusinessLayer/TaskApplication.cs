using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.DataLayer;
using TaskManager.Entities;

namespace TaskManager.BusinessLayer
{
    public class TaskApplication : ITaskApplication
    {
        ITaskRepository _repository;
        public TaskApplication() : this(new TaskRepository()) { }

        public TaskApplication(ITaskRepository repository)
        {
            this._repository = repository;
        }

        public void AddTask(TaskModel taskModel)
        {
            var task = new Task_Table()
            {
                Task = taskModel.Task
            };

            task.Parent_ID = taskModel.ParentTaskId;
            task.Priority = taskModel.Priority;
            task.Start_Date = Convert.ToDateTime(taskModel.StartDate);
            task.End_Date = Convert.ToDateTime(taskModel.EndDate);
            this._repository.AddTask(task);
        }

        public void UpdateTask(TaskModel task)
        {
            this._repository.DeleteTask(task.TaskId);
            this.AddTask(task);
        }

        public void EndTask(TaskModel task)
        {
            this._repository.EndTask(task.TaskId);
        }

        public TaskModel GetTaskByTaskId(int TaskId)
        {
            var task = this._repository.GetTaskByTaskId(TaskId);
            var taskList = this._repository.GetTasks();

            var taskModel = new TaskModel()
            {
                TaskId = task.Task_ID,
                Task = task.Task,
                StartDate = task.Start_Date.GetValueOrDefault().ToString(),
                EndDate = task.End_Date.GetValueOrDefault().ToString(),
                Priority = task.Priority.GetValueOrDefault(),
                ParentTaskId = task.Parent_ID,
                ParentTask = task.Parent_ID.HasValue ? (taskList.FirstOrDefault(x => x.Task_ID == task.Parent_ID).Task) : string.Empty
            };
            return taskModel;
        }

        public IList<TaskModel> GetTasks()
        {
            var taskList = new List<TaskModel>();
            var tasks = this._repository.GetTasks();

            foreach (var item in tasks)
            {
                var taskModel = new TaskModel()
                {
                    TaskId = item.Task_ID,
                    Task = item.Task,
                    Priority = item.Priority.GetValueOrDefault(),
                    StartDate = item.Start_Date.ToString(),
                    EndDate = item.End_Date.ToString(),
                    IsEditable = item.End_Date != null ? (item.End_Date.Value.Date > DateTime.Now.Date) : true,
                    ParentTaskId = item.Parent_ID,
                    ParentTask = item.Parent_ID.HasValue ? (tasks.FirstOrDefault(x => x.Task_ID == item.Parent_ID).Task) : string.Empty
                };
                taskList.Add(taskModel);
            }
            return taskList;
        }

        public bool IsTaskExists(string TaskName)
        {
            return this._repository.IsTaskExists(TaskName);
        }
    }
}
