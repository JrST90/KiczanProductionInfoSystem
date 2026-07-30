namespace KiczanProductionInfoSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            comboBox1 = new System.Windows.Forms.ComboBox();
            button1 = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            label2 = new System.Windows.Forms.Label();
            comboBox2 = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            updateRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteRecordArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            restoreRecordMainTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridView1.ColumnHeadersHeight = 46;
            dataGridView1.Location = new System.Drawing.Point(98, 233);
            dataGridView1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 25;
            dataGridView1.Size = new System.Drawing.Size(2031, 947);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Search by Due Date Range", "Search by Part Number", "Search by Operator Name", "Search by Fabrication Department", "Search by Machining Department", "Search by Part Number in Archive" });
            comboBox1.Location = new System.Drawing.Point(27, 49);
            comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(467, 40);
            comboBox1.TabIndex = 13;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.Green;
            button1.ForeColor = System.Drawing.Color.White;
            button1.Location = new System.Drawing.Point(1576, 28);
            button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(208, 65);
            button1.TabIndex = 14;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(770, 51);
            textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(275, 39);
            textBox1.TabIndex = 15;
            textBox1.Enter += textBox1_Enter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(765, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(150, 32);
            label1.TabIndex = 16;
            label1.Text = "Search Value";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(31, 106);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(0, 32);
            label2.TabIndex = 17;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(501, 49);
            comboBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(243, 40);
            comboBox2.TabIndex = 18;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(1091, 12);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(403, 198);
            label3.TabIndex = 19;
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(68, 154);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(0, 32);
            label4.TabIndex = 20;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.Blue;
            button2.ForeColor = System.Drawing.Color.White;
            button2.Location = new System.Drawing.Point(1791, 28);
            button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(208, 65);
            button2.TabIndex = 21;
            button2.Text = "Create Record";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.Purple;
            button3.ForeColor = System.Drawing.Color.White;
            button3.Location = new System.Drawing.Point(2005, 27);
            button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(208, 64);
            button3.TabIndex = 22;
            button3.Text = "Export";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(1731, 178);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(0, 32);
            label5.TabIndex = 23;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(1946, 178);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(0, 32);
            label6.TabIndex = 24;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(1985, 1190);
            button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(144, 59);
            button4.TabIndex = 25;
            button4.Text = "Next";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(1835, 1190);
            button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(143, 59);
            button5.TabIndex = 26;
            button5.Text = "Previous";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { updateRecordToolStripMenuItem, deleteRecordArchiveToolStripMenuItem, restoreRecordMainTableToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(385, 118);
            // 
            // updateRecordToolStripMenuItem
            // 
            updateRecordToolStripMenuItem.Name = "updateRecordToolStripMenuItem";
            updateRecordToolStripMenuItem.Size = new System.Drawing.Size(384, 38);
            updateRecordToolStripMenuItem.Text = "Update Record";
            updateRecordToolStripMenuItem.Click += updateRecordToolStripMenuItem_Click;
            // 
            // deleteRecordArchiveToolStripMenuItem
            // 
            deleteRecordArchiveToolStripMenuItem.Name = "deleteRecordArchiveToolStripMenuItem";
            deleteRecordArchiveToolStripMenuItem.Size = new System.Drawing.Size(384, 38);
            deleteRecordArchiveToolStripMenuItem.Text = "Delete Record (Archive)";
            deleteRecordArchiveToolStripMenuItem.Click += deleteRecordToolStripMenuItem_Click;
            // 
            // restoreRecordMainTableToolStripMenuItem
            // 
            restoreRecordMainTableToolStripMenuItem.Name = "restoreRecordMainTableToolStripMenuItem";
            restoreRecordMainTableToolStripMenuItem.Size = new System.Drawing.Size(384, 38);
            restoreRecordMainTableToolStripMenuItem.Text = "Restore Record (Main Table)";
            restoreRecordMainTableToolStripMenuItem.Click += restoreRecordMainTableToolStripMenuItem_Click;
            // 
            // button6
            // 
            button6.BackColor = System.Drawing.Color.Orange;
            button6.ForeColor = System.Drawing.Color.White;
            button6.Location = new System.Drawing.Point(1576, 101);
            button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(208, 65);
            button6.TabIndex = 27;
            button6.Text = "Dashboard";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(198, 207, 219);
            ClientSize = new System.Drawing.Size(1197, 704);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(comboBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updateRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRecordArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreRecordMainTableToolStripMenuItem;
        private System.Windows.Forms.Button button6;
    }
}

