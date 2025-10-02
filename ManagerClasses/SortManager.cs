using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.TaskClass;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ToDo.ManagerClasses
{
    public enum SortCategory
    {
        Title, 
        Priority,
        Category,
        DateOfDeadLine,
        Completion

    }
    public class SortManager
    {
        public IEnumerable<ToDo.TaskClass.Task> Sort(IEnumerable<ToDo.TaskClass.Task> tasks,SortCategory category)
        {
            switch (category)
            {
                case SortCategory.Title:
                    return SortByTitle(tasks);
                case SortCategory.Priority:
                    return SortByPriority(tasks);
                case SortCategory.Category:
                    return SortByCategory(tasks);
                case SortCategory.DateOfDeadLine:
                    return SortByDateOfDeadline(tasks);
                case SortCategory.Completion:
                    return SortByCompletion(tasks);
                default: return tasks;

            }
        }

        private IEnumerable<ToDo.TaskClass.Task> SortByTitle(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            return tasks.OrderBy(t => t.Title);
        }

        private IEnumerable<ToDo.TaskClass.Task> SortByPriority(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            return tasks.OrderByDescending(t => t.Priority);
        }

        private IEnumerable<ToDo.TaskClass.Task> SortByCategory(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            return tasks.OrderByDescending(t => t.Category);
        }

        private IEnumerable<ToDo.TaskClass.Task> SortByDateOfDeadline(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            return tasks.OrderBy(t => t.DeadLine);
        }

        private IEnumerable<ToDo.TaskClass.Task> SortByCompletion(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            return tasks.OrderBy(t => t.isCompleted);
        }

        public IEnumerable<ToDo.TaskClass.Task> GetDueSoonTasks(IEnumerable<ToDo.TaskClass.Task> tasks, int daysThreshold = 3)
        {
            return tasks.Where(t => !t.IsCompleted && t.DeadLine >= DateTime.Today && t.DeadLine <= DateTime.Today.AddDays(daysThreshold));
        }
    }
}
