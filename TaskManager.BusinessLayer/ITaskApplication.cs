using System.Collections.Generic;
using TaskManager.Entities;

namespace TaskManager.BusinessLayer
{
    public interface ITaskApplication
    {
        void AddTask(TaskModel task);
        IList<TaskModel> GetTasks();
        TaskModel GetTaskByTaskId(int TaskId);
        void EndTask(TaskModel task);
        void UpdateTask(TaskModel task);
        bool IsTaskExists(string TaskName);
    }
}
