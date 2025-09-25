namespace ToDo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NameApp = new Label();
            TitleLabel = new Label();
            textBoxTitle = new TextBox();
            textBoxDescribtion = new TextBox();
            DescribtionLabel = new Label();
            PriorityLabel = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            CategoryLabel = new Label();
            monthCalendar1 = new MonthCalendar();
            DeadlineLabel = new Label();
            dataGridView1 = new DataGridView();
            Title = new DataGridViewTextBoxColumn();
            Describtion = new DataGridViewTextBoxColumn();
            IsCompleted = new DataGridViewCheckBoxColumn();
            Priority = new DataGridViewComboBoxColumn();
            Category = new DataGridViewComboBoxColumn();
            Deadline = new DataGridViewTextBoxColumn();
            button1 = new Button();
            SortCategory = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // NameApp
            // 
            NameApp.AutoSize = true;
            NameApp.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 204);
            NameApp.Location = new Point(12, 9);
            NameApp.Name = "NameApp";
            NameApp.Size = new Size(224, 65);
            NameApp.TabIndex = 0;
            NameApp.Text = "ToDoList";
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TitleLabel.Location = new Point(12, 74);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(52, 25);
            TitleLabel.TabIndex = 1;
            TitleLabel.Text = "Title:";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(70, 75);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(100, 23);
            textBoxTitle.TabIndex = 2;
            // 
            // textBoxDescribtion
            // 
            textBoxDescribtion.Location = new Point(130, 104);
            textBoxDescribtion.Name = "textBoxDescribtion";
            textBoxDescribtion.Size = new Size(465, 23);
            textBoxDescribtion.TabIndex = 4;
            // 
            // DescribtionLabel
            // 
            DescribtionLabel.AutoSize = true;
            DescribtionLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DescribtionLabel.Location = new Point(12, 99);
            DescribtionLabel.Name = "DescribtionLabel";
            DescribtionLabel.Size = new Size(112, 25);
            DescribtionLabel.TabIndex = 3;
            DescribtionLabel.Text = "Describtion:";
            // 
            // PriorityLabel
            // 
            PriorityLabel.AutoSize = true;
            PriorityLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PriorityLabel.Location = new Point(176, 75);
            PriorityLabel.Name = "PriorityLabel";
            PriorityLabel.Size = new Size(77, 25);
            PriorityLabel.TabIndex = 5;
            PriorityLabel.Text = "Priority:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(249, 77);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 6;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(474, 79);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 23);
            comboBox2.TabIndex = 8;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // CategoryLabel
            // 
            CategoryLabel.AutoSize = true;
            CategoryLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CategoryLabel.Location = new Point(376, 75);
            CategoryLabel.Name = "CategoryLabel";
            CategoryLabel.Size = new Size(92, 25);
            CategoryLabel.TabIndex = 7;
            CategoryLabel.Text = "Category:";
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(607, 45);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 9;
            // 
            // DeadlineLabel
            // 
            DeadlineLabel.AutoSize = true;
            DeadlineLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DeadlineLabel.Location = new Point(607, 9);
            DeadlineLabel.Name = "DeadlineLabel";
            DeadlineLabel.Size = new Size(91, 25);
            DeadlineLabel.TabIndex = 10;
            DeadlineLabel.Text = "Deadline:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Title, Describtion, IsCompleted, Priority, Category, Deadline });
            dataGridView1.Location = new Point(12, 227);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(650, 222);
            dataGridView1.TabIndex = 11;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.Name = "Title";
            // 
            // Describtion
            // 
            Describtion.HeaderText = "Describtion";
            Describtion.Name = "Describtion";
            // 
            // IsCompleted
            // 
            IsCompleted.HeaderText = "IsCompleted";
            IsCompleted.Name = "IsCompleted";
            // 
            // Priority
            // 
            Priority.HeaderText = "Priority";
            Priority.Name = "Priority";
            // 
            // Category
            // 
            Category.HeaderText = "Category";
            Category.Name = "Category";
            // 
            // Deadline
            // 
            Deadline.HeaderText = "Deadline";
            Deadline.Name = "Deadline";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(12, 127);
            button1.Name = "button1";
            button1.Size = new Size(87, 31);
            button1.TabIndex = 12;
            button1.Text = "Sort";
            button1.UseVisualStyleBackColor = true;
            // 
            // SortCategory
            // 
            SortCategory.FormattingEnabled = true;
            SortCategory.Location = new Point(105, 133);
            SortCategory.Name = "SortCategory";
            SortCategory.Size = new Size(121, 23);
            SortCategory.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 461);
            Controls.Add(SortCategory);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(DeadlineLabel);
            Controls.Add(monthCalendar1);
            Controls.Add(comboBox2);
            Controls.Add(CategoryLabel);
            Controls.Add(comboBox1);
            Controls.Add(PriorityLabel);
            Controls.Add(textBoxDescribtion);
            Controls.Add(DescribtionLabel);
            Controls.Add(textBoxTitle);
            Controls.Add(TitleLabel);
            Controls.Add(NameApp);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label NameApp;
        private Label TitleLabel;
        private TextBox textBoxTitle;
        private TextBox textBoxDescribtion;
        private Label DescribtionLabel;
        private Label PriorityLabel;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label CategoryLabel;
        private MonthCalendar monthCalendar1;
        private Label DeadlineLabel;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Describtion;
        private DataGridViewCheckBoxColumn IsCompleted;
        private DataGridViewComboBoxColumn Priority;
        private DataGridViewComboBoxColumn Category;
        private DataGridViewTextBoxColumn Deadline;
        private Button button1;
        private ComboBox SortCategory;
    }
}
