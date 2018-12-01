using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataLayer
{
    public class TaskRepository : ITaskRepository
    {
        LocalDBEntities _entities;
        public TaskRepository() : this(new LocalDBEntities()) { }

        public TaskRepository(LocalDBEntities entities)
        {
            _entities = entities;
        }

        public void AddTask(Task_Table task)
        {
            _entities.Task_Table.Add(task);
            _entities.SaveChanges();
        }

        public void DeleteTask(int TaskId)
        {
            var task = _entities.Task_Table.FirstOrDefault(x => x.Task_ID == TaskId);
            if (task!=null)
            {
                _entities.Task_Table.Remove(task);
                _entities.SaveChanges();
            }
        }

        public void EndTask(int TaskId)
        {
            var task = _entities.Task_Table.FirstOrDefault(x => x.Task_ID == TaskId);
            if (task != null)
            {
                task.End_Date = DateTime.Now;
                _entities.SaveChanges();
            }
        }

        public Task_Table GetTaskByTaskId(int TaskId)
        {
            return _entities.Task_Table.FirstOrDefault(x => x.Task_ID == TaskId);
        }

        public IList<Task_Table> GetTasks()
        {
            return _entities.Task_Table.ToList();
        }

        public bool IsTaskExists(string TaskName)
        {
            return _entities.Task_Table.FirstOrDefault(x => x.Task == TaskName) == null ? false : true;
        }
    }
}
