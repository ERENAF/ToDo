using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.TaskClass
namespace ToDo.ManagerClasses
{
    public class TaskManager
    {
        private readonly List<Task> tasks;

        public TaskManager()
        {
            tasks = new List<Task>();
        }

        public IReadOnlyList<Task> Tasks => tasks.AsReadOnly();
        public void AddTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentException(nameof(task));
            }
            tasks.Add(task);
        }

        public bool RemoveTask(Guid id)
        {
            Task task = tasks.FirstOrDefault(t => t.Id == id);
            return task != null && tasks.Remove(task);
        }
    }
}
