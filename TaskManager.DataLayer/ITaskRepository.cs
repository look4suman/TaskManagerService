using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataLayer
{
    public interface ITaskRepository
    {
        void AddTask(Task_Table task);
        IList<Task_Table> GetTasks();
        Task_Table GetTaskByTaskId(int TaskId);
        void EndTask(int TaskId);
        void DeleteTask(int TaskId);
        bool IsTaskExists(string TaskName);
    }
}
