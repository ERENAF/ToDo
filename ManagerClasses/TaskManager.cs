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
        private SortManager sortManager;


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


        // Методы сортировки через SortManager
        public IEnumerable<ToDo.TaskClass.Task> SortByTitle() =>
            sortManager.Sort(tasks, SortCategory.Title);

        public IEnumerable<ToDo.TaskClass.Task> SortByPriority() =>
            sortManager.Sort(tasks, SortCategory.Priority);

        public IEnumerable<ToDo.TaskClass.Task> SortByCategory() =>
            sortManager.Sort(tasks, SortCategory.Category);

        public IEnumerable<ToDo.TaskClass.Task> SortByDeadline() =>
            sortManager.Sort(tasks, SortCategory.DateOfDeadLine);

        public IEnumerable<ToDo.TaskClass.Task> SortByCompletion() =>
            sortManager.Sort(tasks, SortCategory.Completion);

        // Универсальный метод сортировки
        public IEnumerable<ToDo.TaskClass.Task> Sort(SortCategory sortCategory) =>
            sortManager.Sort(tasks, sortCategory);

        // Методы фильтрации
        public IEnumerable<ToDo.TaskClass.Task> FilterByCategory(Category category) =>
            tasks.Where(t => t.Category == category);

        public IEnumerable<ToDo.TaskClass.Task> GetDueSoonTasks(int daysThreshold = 3) =>
            tasks.Where(t => !t.IsCompleted && t.DeadLine >= DateTime.Today && t.DeadLine <= DateTime.Today.AddDays(daysThreshold));

        public IEnumerable<ToDo.TaskClass.Task> GetCompletedTasks() =>
            tasks.Where(t => t.IsCompleted);
        
        public IEnumerable<ToDo.TaskClass.Task> GetPendingTasks() =>
            tasks.Where(t => !t.IsCompleted);

        public IEnumerable<ToDo.TaskClass.Task> SearchTasks(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return tasks;

            return tasks.Where(t =>
                t.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }
        // Наблюдатель
        public void Subscribe(Action observer) => observers.Add(observer);
        public void Unsubscribe(Action observer) => observers.Remove(observer);
        private void NotifyObservers() => observers.ForEach(observer => observer?.Invoke());
    }
    
}
