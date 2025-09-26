using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.TaskClass;
namespace ToDo.ManagerClasses
{
    public class TaskManager
    {
        private List<ToDo.TaskClass.Task> tasks;
        private List<Action> observers;

        public TaskManager()
        {
            tasks = new List<ToDo.TaskClass.Task>();
            observers = new List<Action>();
        }

        public List<ToDo.TaskClass.Task> Tasks => tasks;
        public void AddTask(ToDo.TaskClass.Task task)
        {
            if (task == null)
            {
                throw new ArgumentException(nameof(task));
            }
            tasks.Add(task);
            NotifyObservers();
        }

        public bool RemoveTask(Guid id)
        {
            ToDo.TaskClass.Task task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null && tasks.Remove(task) ){
                NotifyObservers();
                return true;
            }
            return false; 
        }

        public bool EditTask(Guid taskId, string title, Priority priority, Category category, DateTime deadline)
        {
            ToDo.TaskClass.Task task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                try
                {
                    task.Title = title;
                    task.Priority = priority;
                    task.Category = category;
                    task.DeadLine = deadline;

                    NotifyObservers();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool SubTaskCompletion(Guid taskId)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.isCompleted = !task.isCompleted;

                foreach (var subtask in task.SubTasks)
                {
                    subtask.isCompleted = task.isCompleted;
                }

                NotifyObservers();

                return true;
            }
            return false;
        }



        public void Subscribe(Action observer) => observers.Add(observer);
        public void Unsubscribe(Action observer) => observers.Remove(observer);
        private void NotifyObservers() => observers.ForEach(observer => observer?.Invoke());
    }
    
}
