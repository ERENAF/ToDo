using ToDo.TaskClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Principal;

namespace ToDo.ManagerClasses
{
    public class FileManager
    {
        private readonly string _filePath;


        public FileManager(string fileName = "tasks.json")
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        private class TaskData
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Priority Priority { get; set; }
            public Category Category { get; set; }
            public DateTime DeadLine { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime CreationDate { get; set; }
            public List<TaskData> SubTasks { get; set; }
        }

        public void SaveTasks(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            try
            {
                List<TaskData> taskDataList = ConvertToTaskData(tasks);
                var json = JsonSerializer.Serialize(taskDataList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error of saving tasks: {ex.Message}", ex);
            }
        }

        public List<ToDo.TaskClass.Task> LoadTasks()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<ToDo.TaskClass.Task>();
                var json = File.ReadAllText(_filePath);
                var taskDataList = JsonSerializer.Deserialize<List<TaskData>>(json);
                return ConvertFromTaskData(taskDataList);
            }
            catch (Exception ex)
            {
                throw new Exception($"error of loading tasks: {ex.Message}", ex);
            }
        }
        private List<TaskData> ConvertToTaskData(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            List<TaskData> result = new List<TaskData>();

            foreach (var task in tasks)
            {
                var taskData = new TaskData
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority,
                    Category = task.Category,
                    DeadLine = task.DeadLine,
                    IsCompleted = task.IsCompleted,
                    CreationDate = task.CreateDate,
                    SubTasks = new List<TaskData>()
                };
                foreach (var subtask in task.Subtasks)
                {
                    taskData.SubTasks.Add(new TaskData
                    {
                        Id = subtask.Id,
                        Title = subtask.Title,
                        Description = subtask.Description,
                        Priority = subtask.Priority,
                        Category = subtask.Category,
                        DeadLine = subtask.DeadLine,
                        IsCompleted = subtask.IsCompleted,
                        CreationDate = subtask.CreateDate,
                        SubTasks = (subtask.Subtasks.Count > 0) ? ConvertToTaskData(subtask.Subtasks) : new List<TaskData>()
                    });
                }
                result.Add(taskData);
            }
            return result;
        }


        private List<ToDo.TaskClass.Task> ConvertFromTaskData(List<TaskData> taskDataList)
        {
            var tasks = new List<ToDo.TaskClass.Task>();

            if (taskDataList == null) return tasks;

            foreach (var taskData in taskDataList)
            {
                ToDo.TaskClass.Task task = new ToDo.TaskClass.Task(
                    taskData.Title,
                    taskData.Description,
                    taskData.Priority,
                    taskData.Category,
                    taskData.DeadLine)
                {
                    Id = taskData.Id,
                    IsCompleted = taskData.IsCompleted,
                    CreateDate = taskData.CreationDate

                };

                if (taskData.SubTasks != null)
                {
                    foreach (var subtaskData in taskData.SubTasks)
                    {
                        ToDo.TaskClass.Task subtask = new ToDo.TaskClass.Task(
                            subtaskData.Title,
                            subtaskData.Description,
                            subtaskData.Priority,
                            subtaskData.Category,
                            subtaskData.DeadLine)
                        {
                            Id = subtaskData.Id,
                            IsCompleted = subtaskData.IsCompleted,
                            CreateDate = subtaskData.CreationDate
                        };
                        if (subtaskData.SubTasks != null)
                        {
                            subtask._subtasks = ConvertFromTaskData(subtaskData.SubTasks);
                        }
                        else
                        {
                            subtask._subtasks = new List<ToDo.TaskClass.Task>();
                        }
                        task.AddSubtask(subtask);
                    }
                }
                tasks.Add(task);
            }
            return tasks;
        }
    }
}
