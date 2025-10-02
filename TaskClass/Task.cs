using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.TaskClass
{
    public class Task
    {
        private string _title;
        private string _description;
        public bool isCompleted;
        private DateTime _deadline;
        private DateTime createdate;
        public List<Task> _subtasks;


        public Task (string title, string describtion, Priority priority,
                     Category category, DateTime deadline)
        {
            Id = Guid.NewGuid(); 
            Title = title;
            Description = describtion;
            Priority = priority;
            Category = category;
            IsCompleted = false;
            CreateDate = DateTime.Now;
            DeadLine = deadline;
            _subtasks = new List<Task> ();
        }

        public Guid Id {  get; set; }
        public string Title { 
            get=>_title;
            set
            {   
                if (value == null)
                {
                    throw new InvalidOperationException("title is missing");
                }
                _title = value;
            } 
        }
        public string Description
        {
            get => _description;
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("describtion is missing");
                }
                _description = value;
            }
        }
        public Priority Priority { get; set; }
        public Category Category { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime DeadLine
        {
            get => _deadline;
            set
            {
                if (value < createdate.Date)
                {
                    _deadline = createdate;
                }
                else
                {
                    _deadline = value;
                }
            }
        }
        public DateTime CreateDate { get; set; }

        public List<Task> Subtasks => _subtasks;


        public void AddSubtask( Task subtask)
        {
            if (subtask.DeadLine > DeadLine)
            {
                subtask.DeadLine = DeadLine;
            }
            _subtasks.Add(subtask);
        }

        public bool RemoveSubtask(Guid id)
        {
            Task subtask = _subtasks.FirstOrDefault(t => t.Id == id);
            return subtask != null && _subtasks.Remove(subtask);
        }

        public void ClearSubtasks()
        {
            _subtasks.Clear();
        }

        public bool IsDeadlineClose (int days = 3)
        {
            return !isCompleted &&
                DeadLine >= DateTime.Today &&
                DeadLine <= DateTime.Today.AddDays(days);
        }
    }
}
