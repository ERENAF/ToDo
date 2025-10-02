using ToDo.ManagerClasses;
using ToDo.TaskClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ToDo.Forms
{
    public partial class MainForm : Form
    {
        private readonly TaskManager _taskManager;
        private readonly FileManager _fileService;
        private readonly NotifyIcon _notifyIcon;
        private readonly SortManager _sortManager;

        // Элементы управления
        private ListView lvTasks;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private ComboBox cmbPriority;
        private ComboBox cmbCategory;
        private DateTimePicker dtpDeadline;
        private Button btnAddTask;
        private Button btnDeleteTask;
        private CheckBox chbIsCompleted;
        private Button btnAddSubtask;
        private Button btnEditTask;
        private ComboBox cmbSortType;
        private Button btnApplySort;
        private Button btnlookSubTasks;
        private Button btnbackToTasks;
        private Label lleveloftasks;


        public MainForm()
        {
            InitializeComponent();

            _taskManager = new TaskManager();
            _fileService = new FileManager();
            _sortManager = new SortManager();

            // Настройка уведомлений
            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true,
                BalloonTipTitle = "Список задач",
                BalloonTipIcon = ToolTipIcon.Info
            };

            _taskManager.Subscribe(RefreshTaskList);
            LoadTasks();
            InitializeControls();
            CheckDueTasks();
        }

        private void InitializeComponent()
        {
            SetupForm();
            CreateControls();
            SetupLayout();
        }

        private void SetupForm()
        {
            SuspendLayout();

            // Настройка формы
            Text = "ToDo List Application";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += MainForm_FormClosing;


            ResumeLayout(false);
            PerformLayout();
        }

        private void CreateControls()
        {
            // ListView для отображения задач
            lvTasks = new ListView
            {
                Location = new Point(12, 150),
                Size = new Size(960, 450),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false
            };

            // Колонки
            lvTasks.Columns.AddRange(new[]
            {
                new ColumnHeader { Text = "Название", Width = 150 },
                new ColumnHeader { Text = "Описание", Width = 200 },
                new ColumnHeader { Text = "Приоритет", Width = 80 },
                new ColumnHeader { Text = "Категория", Width = 80 },
                new ColumnHeader { Text = "Срок", Width = 90 },
                new ColumnHeader { Text = "Выполнена", Width = 70 },
                new ColumnHeader { Text = "Подзадачи", Width = 70 },
                new ColumnHeader { Text = "Дата создания", Width = 90 }
            });

            // Поля ввода
            txtTitle = new TextBox
            {
                Location = new Point(12, 25),
                Size = new Size(200, 20),
                PlaceholderText = "Название задачи"
            };

            txtDescription = new TextBox
            {
                Location = new Point(218, 25),
                Size = new Size(200, 20),
                PlaceholderText = "Описание задачи"
            };

            cmbPriority = new ComboBox
            {
                Location = new Point(424, 25),
                Size = new Size(100, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbCategory = new ComboBox
            {
                Location = new Point(530, 25),
                Size = new Size(100, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            dtpDeadline = new DateTimePicker
            {
                Location = new Point(636, 25),
                Size = new Size(120, 20),
                Value = DateTime.Today.AddDays(1),
                MinDate = DateTime.Today
            };

            // Кнопки управления задачами
            btnAddTask = new Button
            {
                Location = new Point(9, 58),
                Size = new Size(150, 23),
                Text = "Добавить задачу"
            };

            btnEditTask = new Button
            {
                Location = new Point(420, 58),
                Size = new Size(100, 23),
                Text = "Редактировать"
            };

            btnAddSubtask = new Button
            {
                Location = new Point(160, 58),
                Size = new Size(160, 23),
                Text = "Добавить подзадачу"
            };

            btnDeleteTask = new Button
            {
                Location = new Point(320, 58),
                Size = new Size(100, 23),
                Text = "Удалить задачу"
            };

            chbIsCompleted = new CheckBox
            {
                Location = new Point(778, 10),
                Size = new Size(50,50)
            };

            cmbSortType = new ComboBox
            {
                Location = new Point(600, 60),
                Size = new Size(120, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnApplySort = new Button
            {
                Location = new Point(720, 58),
                Size = new Size(95, 28),
                Text = "Сортировать"
            };

            btnlookSubTasks = new Button
            {
                Location = new Point(12, 88),
                Size = new Size(135, 28),
                Text = "Смотреть подзадачи"
            };

            btnbackToTasks = new Button
            {
                Location = new Point(152, 88),
                Size = new Size(136, 28),
                Text = "Вернуться"
            };

            // Метки
            var labels = new[]
            {
                new Label { Location = new Point(12, 9), Text = "Название:", AutoSize = true },
                new Label { Location = new Point(218, 9), Text = "Описание:", AutoSize = true },
                new Label { Location = new Point(424, 9), Text = "Приоритет:", AutoSize = true },
                new Label { Location = new Point(530, 9), Text = "Категория:", AutoSize = true },
                new Label { Location = new Point(636, 9), Text = "Срок:", AutoSize = true },
                new Label { Location = new Point(758, 9), Text = "Выполнение:", AutoSize = true },
                new Label { Location = new Point(525, 62), Text = "Сортировка:", AutoSize = true }
            };

            lleveloftasks = new Label
            {
                Location = new Point(300, 93),
                Text = "Уровень",
                AutoSize = true
            };

            // Добавление элементов на форму
            Controls.Add(lvTasks);
            Controls.Add(txtTitle);
            Controls.Add(txtDescription);
            Controls.Add(cmbPriority);
            Controls.Add(cmbCategory);
            Controls.Add(dtpDeadline);
            Controls.Add(btnAddTask);
            Controls.Add(btnEditTask);
            Controls.Add(btnAddSubtask);
            Controls.Add(btnDeleteTask);
            Controls.Add(cmbSortType);
            Controls.Add(btnApplySort);
            Controls.AddRange(labels);
            Controls.Add(chbIsCompleted);
            Controls.Add(btnlookSubTasks);
            Controls.Add(btnbackToTasks);
            Controls.Add(lleveloftasks);
        }

        private void SetupLayout()
        {
            // Настройка обработчиков событий
            btnAddTask.Click += btnAddTask_Click;
            btnEditTask.Click += btnEditTask_Click;
            btnAddSubtask.Click += btnAddSubtask_Click;
            btnDeleteTask.Click += btnDeleteTask_Click;
            btnlookSubTasks.Click += btnLookSubTasks;
            btnbackToTasks.Click += btnBackToTasks;
            btnApplySort.Click += btnApplySort_Click;

            //настройка двойного клика
            lvTasks.DoubleClick += btnLookSubTasks;
        
        }

        private void InitializeControls()
        {
            // Заполнение комбобоксов
            cmbPriority.DataSource = Enum.GetValues(typeof(Priority));
            cmbCategory.DataSource = Enum.GetValues(typeof(Category));


            // Типы сортировки
            cmbSortType.Items.Add("По названию");
            cmbSortType.Items.Add("По приоритету");
            cmbSortType.Items.Add("По категории");
            cmbSortType.Items.Add("По сроку");
            cmbSortType.Items.Add("По выполнению");
            cmbSortType.SelectedIndex = 0;
        }

        private void LoadTasks()
        {
            try
            {
                var tasks = _fileService.LoadTasks();
                foreach (var task in tasks)
                {
                    _taskManager.AddTask(task);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки задач: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveTasks()
        {
            try
            {
                _taskManager.BackToFirstTask();
                _fileService.SaveTasks(_taskManager.Tasks);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения задач: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshTaskList()
        {
            lvTasks.Items.Clear();

            IEnumerable<ToDo.TaskClass.Task> tasks = _taskManager.Tasks;

            if (cmbSortType.SelectedIndex > 0)
            {
                var selectedCategory = (SortCategory)(cmbSortType.SelectedIndex - 1);
                tasks = _sortManager.Sort(tasks, selectedCategory);
            }
            lleveloftasks.Text = $"Уровень задач: {_taskManager.stacktask.Count + 1}";

            DisplayTasks(tasks);
        }

        private void DisplayTasks(IEnumerable<ToDo.TaskClass.Task> tasks)
        {
            lvTasks.Items.Clear();

            foreach (var task in tasks)
            {
                var item = new ListViewItem(task.Title);
                item.SubItems.Add(task.Description);
                item.SubItems.Add(task.Priority.ToString());
                item.SubItems.Add(task.Category.ToString());
                item.SubItems.Add(task.DeadLine.ToString("dd.MM.yyyy"));
                item.SubItems.Add(task.IsCompleted ? "Да" : "Нет");
                item.SubItems.Add(task.Subtasks?.Count.ToString() ?? "0");
                item.SubItems.Add(task.CreateDate.ToString("dd.MM.yyyy"));

                item.Tag = task.Id;
                item.BackColor = CategoryExtra.GetCategoryColor(task.Category);

                if (task.IsCompleted)
                {
                    item.Font = new Font(item.Font, FontStyle.Strikeout);
                    item.ForeColor = Color.Gray;
                }
                else if (task.IsDeadlineClose())
                {
                    item.ForeColor = Color.Red;
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }

                lvTasks.Items.Add(item);
            }
        }

        private void CheckDueTasks()
        {
            var dueSoonTasks = _sortManager.GetDueSoonTasks(_taskManager.Tasks);
            foreach (var task in dueSoonTasks)
            {
                _notifyIcon.BalloonTipText = $"Задача '{task.Title}' должна быть выполнена до {task.DeadLine:dd.MM.yyyy}";
                _notifyIcon.ShowBalloonTip(3000);
            }
        }

        // Обработчики событий
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Введите название задачи", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var task = new ToDo.TaskClass.Task(
                    title: txtTitle.Text.Trim(),
                    describtion: txtDescription.Text.Trim(),
                    priority: (Priority)cmbPriority.SelectedItem,
                    category: (Category)cmbCategory.SelectedItem,
                    deadline: dtpDeadline.Value)
                {
                    IsCompleted = chbIsCompleted.Checked
                };

                _taskManager.AddTask(task);
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления задачи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditTask_Click(object sender, EventArgs e)
        {
            if (lvTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите задачу для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var taskId = (Guid)lvTasks.SelectedItems[0].Tag;
                var task = _taskManager.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task != null)
                {
                    // Заполняем поля данными выбранной задачи
                    txtTitle.Text = task.Title;
                    txtDescription.Text = task.Description;
                    cmbPriority.SelectedItem = task.Priority;
                    cmbCategory.SelectedItem = task.Category;
                    dtpDeadline.Value = task.DeadLine;
                    chbIsCompleted.Checked = task.IsCompleted;
                    _taskManager.SubTaskCompletion(task.Id);
                    // Удаляем старую задачу и создаем новую с обновленными данными
                    _taskManager.RemoveTask(taskId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования задачи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddSubtask_Click(object sender, EventArgs e)
        {
            if (lvTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите основную задачу для добавления подзадачи", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var mainTaskId = (Guid)lvTasks.SelectedItems[0].Tag;
                var mainTask = _taskManager.Tasks.FirstOrDefault(t => t.Id == mainTaskId);

                if (mainTask != null)
                {
                    if (string.IsNullOrWhiteSpace(txtTitle.Text))
                    {
                        MessageBox.Show("Введите название подзадачи", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var subtask = new ToDo.TaskClass.Task(
                        title: txtTitle.Text.Trim(),
                        describtion: txtDescription.Text.Trim(),
                        priority: (Priority)cmbPriority.SelectedItem,
                        category: (Category)cmbCategory.SelectedItem,
                        deadline: dtpDeadline.Value > mainTask.DeadLine ? mainTask.DeadLine : dtpDeadline.Value
                    );

                    mainTask.AddSubtask(subtask);
                    RefreshTaskList();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления подзадачи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLookSubTasks (object sender, EventArgs e)
        {
            if (lvTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите задачу для просмотра подзадачи", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var mainTaskId = (Guid)lvTasks.SelectedItems[0].Tag;
                _taskManager.LookSubTasks(mainTaskId);
                RefreshTaskList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка  просмотра подзадачи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackToTasks(object sender, EventArgs e)
        {
            if (!_taskManager.BackToHeadTask())
            {
                MessageBox.Show("Нет задач для просмотра", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                RefreshTaskList();
            }
        }



        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (lvTasks.SelectedItems.Count > 0)
            {
                var result = MessageBox.Show("Удалить выбранную задачу?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var taskId = (Guid)lvTasks.SelectedItems[0].Tag;
                    _taskManager.RemoveTask(taskId);
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnApplySort_Click(object sender, EventArgs e)
        {
            var sortCategory = cmbSortType.SelectedIndex switch
            {
                0 => SortCategory.Title,
                1 => SortCategory.Priority,
                2 => SortCategory.Category,
                3 => SortCategory.DateOfDeadLine,
                4 => SortCategory.Completion,
                _ => SortCategory.Title
            };

            var sortedTasks = _sortManager.Sort(_taskManager.Tasks,sortCategory);
            DisplayTasks(sortedTasks);
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTaskList();
        }

        private void ClearInputs()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            cmbPriority.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            dtpDeadline.Value = DateTime.Today.AddDays(1);
            chbIsCompleted.Checked = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTasks();
            _notifyIcon.Dispose();
        }

    }
}