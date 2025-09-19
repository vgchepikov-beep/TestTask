namespace TestTask
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
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            buttonAdditem = new Button();
            listBox1 = new ListBox();
            label4 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            textBoxSurname = new TextBox();
            label1 = new Label();
            textBoxName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 66);
            button1.Name = "button1";
            button1.Size = new Size(240, 39);
            button1.TabIndex = 0;
            button1.Text = "Выбрать xml-файл";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 33);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(240, 27);
            textBox1.TabIndex = 1;
            textBox1.Text = "Файл не выбран";
            // 
            // button2
            // 
            button2.Location = new Point(569, 372);
            button2.Name = "button2";
            button2.Size = new Size(203, 54);
            button2.TabIndex = 2;
            button2.Text = "Преобразовать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(270, 33);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(502, 317);
            dataGridView1.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonAdditem);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBoxSurname);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBoxName);
            groupBox1.Location = new Point(12, 122);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(240, 316);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Добавить item";
            // 
            // buttonAdditem
            // 
            buttonAdditem.Location = new Point(7, 269);
            buttonAdditem.Name = "buttonAdditem";
            buttonAdditem.Size = new Size(227, 35);
            buttonAdditem.TabIndex = 8;
            buttonAdditem.Text = "Добавить";
            buttonAdditem.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Items.AddRange(new object[] { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" });
            listBox1.Location = new Point(7, 225);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(227, 24);
            listBox1.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 202);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 6;
            label4.Text = "месяц";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 140);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 5;
            label3.Text = "сумма";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(6, 163);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(228, 27);
            textBox2.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 81);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 3;
            label2.Text = "фамилия";
            // 
            // textBoxSurname
            // 
            textBoxSurname.Location = new Point(6, 104);
            textBoxSurname.Name = "textBoxSurname";
            textBoxSurname.Size = new Size(228, 27);
            textBoxSurname.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 23);
            label1.Name = "label1";
            label1.Size = new Size(37, 20);
            label1.TabIndex = 1;
            label1.Text = "имя";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(6, 46);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(228, 27);
            textBoxName.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Label label2;
        private TextBox textBoxSurname;
        private Label label1;
        private TextBox textBoxName;
        private Button buttonAdditem;
        private ListBox listBox1;
        private Label label4;
        private Label label3;
        private TextBox textBox2;
    }
}
