using System.Drawing;

namespace KiczanProductionInfoSystem
{
    partial class CreateRecord
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxQuantity = new System.Windows.Forms.TextBox();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelOperations = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.labelRecordStatus = new System.Windows.Forms.Label();
            this.labelPartNumber = new System.Windows.Forms.Label();
            this.textBoxPartNumber = new System.Windows.Forms.TextBox();
            this.errorProviderPartNumber = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelPO = new System.Windows.Forms.Label();
            this.textBoxPO = new System.Windows.Forms.TextBox();
            this.labelPartNumberError = new System.Windows.Forms.Label();
            this.labelQuantityError = new System.Windows.Forms.Label();
            this.labelOperationsError = new System.Windows.Forms.Label();
            this.labelPOError = new System.Windows.Forms.Label();
            this.operatorComboBox = new System.Windows.Forms.ComboBox();
            this.labelOperator = new System.Windows.Forms.Label();
            this.labelOperatorError = new System.Windows.Forms.Label();
            this.errorProviderOperator = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelCustomerError = new System.Windows.Forms.Label();
            this.labelCustomer = new System.Windows.Forms.Label();
            this.customerComboBox = new System.Windows.Forms.ComboBox();
            this.errorProviderCustomer = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxDateReceived = new System.Windows.Forms.TextBox();
            this.labelDateReceived = new System.Windows.Forms.Label();
            this.errorProviderDateReceived = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelDateReceivedError = new System.Windows.Forms.Label();
            this.labelDueDate = new System.Windows.Forms.Label();
            this.textboxDueDate = new System.Windows.Forms.TextBox();
            this.labelDueDateError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPartNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderOperator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderDateReceived)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(532, 808);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 44);
            this.button1.TabIndex = 8;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Laser",
            "Water Jet",
            "Punch",
            "Press Brake",
            "Lathe",
            "Mill"});
            this.checkedListBox1.Location = new System.Drawing.Point(786, 557);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(6);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(236, 200);
            this.checkedListBox1.TabIndex = 7;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Location = new System.Drawing.Point(786, 333);
            this.textBoxQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(236, 31);
            this.textBoxQuantity.TabIndex = 4;
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuantity.Location = new System.Drawing.Point(614, 338);
            this.labelQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(93, 26);
            this.labelQuantity.TabIndex = 2;
            this.labelQuantity.Text = "Quantity";
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // labelOperations
            // 
            this.labelOperations.AutoSize = true;
            this.labelOperations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOperations.Location = new System.Drawing.Point(614, 625);
            this.labelOperations.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOperations.Name = "labelOperations";
            this.labelOperations.Size = new System.Drawing.Size(118, 26);
            this.labelOperations.TabIndex = 6;
            this.labelOperations.Text = "Operations";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(803, 808);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 44);
            this.button2.TabIndex = 9;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Blue;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1068, 808);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 44);
            this.button3.TabIndex = 10;
            this.button3.Text = "Back to Menu";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelRecordStatus
            // 
            this.labelRecordStatus.AutoSize = true;
            this.labelRecordStatus.Location = new System.Drawing.Point(726, 763);
            this.labelRecordStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRecordStatus.Name = "labelRecordStatus";
            this.labelRecordStatus.Size = new System.Drawing.Size(0, 25);
            this.labelRecordStatus.TabIndex = 11;
            // 
            // labelPartNumber
            // 
            this.labelPartNumber.AutoSize = true;
            this.labelPartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPartNumber.Location = new System.Drawing.Point(614, 185);
            this.labelPartNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPartNumber.Name = "labelPartNumber";
            this.labelPartNumber.Size = new System.Drawing.Size(136, 26);
            this.labelPartNumber.TabIndex = 12;
            this.labelPartNumber.Text = "Part Number";
            // 
            // textBoxPartNumber
            // 
            this.textBoxPartNumber.Location = new System.Drawing.Point(786, 179);
            this.textBoxPartNumber.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPartNumber.Name = "textBoxPartNumber";
            this.textBoxPartNumber.Size = new System.Drawing.Size(236, 31);
            this.textBoxPartNumber.TabIndex = 2;
            // 
            // errorProviderPartNumber
            // 
            this.errorProviderPartNumber.ContainerControl = this;
            // 
            // labelPO
            // 
            this.labelPO.AutoSize = true;
            this.labelPO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPO.Location = new System.Drawing.Point(614, 260);
            this.labelPO.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPO.Name = "labelPO";
            this.labelPO.Size = new System.Drawing.Size(151, 26);
            this.labelPO.TabIndex = 15;
            this.labelPO.Text = "Order Number";
            // 
            // textBoxPO
            // 
            this.textBoxPO.Location = new System.Drawing.Point(786, 254);
            this.textBoxPO.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxPO.Name = "textBoxPO";
            this.textBoxPO.Size = new System.Drawing.Size(236, 31);
            this.textBoxPO.TabIndex = 3;
            // 
            // labelPartNumberError
            // 
            this.labelPartNumberError.AutoSize = true;
            this.labelPartNumberError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPartNumberError.Location = new System.Drawing.Point(144, 179);
            this.labelPartNumberError.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPartNumberError.Name = "labelPartNumberError";
            this.labelPartNumberError.Size = new System.Drawing.Size(0, 24);
            this.labelPartNumberError.TabIndex = 17;
            // 
            // labelQuantityError
            // 
            this.labelQuantityError.AutoSize = true;
            this.labelQuantityError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuantityError.Location = new System.Drawing.Point(144, 333);
            this.labelQuantityError.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelQuantityError.Name = "labelQuantityError";
            this.labelQuantityError.Size = new System.Drawing.Size(0, 24);
            this.labelQuantityError.TabIndex = 18;
            // 
            // labelOperationsError
            // 
            this.labelOperationsError.AutoSize = true;
            this.labelOperationsError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOperationsError.Location = new System.Drawing.Point(144, 626);
            this.labelOperationsError.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelOperationsError.Name = "labelOperationsError";
            this.labelOperationsError.Size = new System.Drawing.Size(0, 24);
            this.labelOperationsError.TabIndex = 19;
            // 
            // labelPOError
            // 
            this.labelPOError.AutoSize = true;
            this.labelPOError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPOError.Location = new System.Drawing.Point(144, 254);
            this.labelPOError.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPOError.Name = "labelPOError";
            this.labelPOError.Size = new System.Drawing.Size(0, 24);
            this.labelPOError.TabIndex = 20;
            // 
            // operatorComboBox
            // 
            this.operatorComboBox.FormattingEnabled = true;
            this.operatorComboBox.Location = new System.Drawing.Point(786, 106);
            this.operatorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.operatorComboBox.Name = "operatorComboBox";
            this.operatorComboBox.Size = new System.Drawing.Size(236, 33);
            this.operatorComboBox.TabIndex = 1;
            // 
            // labelOperator
            // 
            this.labelOperator.AutoSize = true;
            this.labelOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOperator.Location = new System.Drawing.Point(614, 106);
            this.labelOperator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOperator.Name = "labelOperator";
            this.labelOperator.Size = new System.Drawing.Size(97, 26);
            this.labelOperator.TabIndex = 22;
            this.labelOperator.Text = "Operator";
            // 
            // labelOperatorError
            // 
            this.labelOperatorError.AutoSize = true;
            this.labelOperatorError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOperatorError.Location = new System.Drawing.Point(144, 106);
            this.labelOperatorError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOperatorError.Name = "labelOperatorError";
            this.labelOperatorError.Size = new System.Drawing.Size(0, 24);
            this.labelOperatorError.TabIndex = 23;
            // 
            // errorProviderOperator
            // 
            this.errorProviderOperator.ContainerControl = this;
            // 
            // labelCustomerError
            // 
            this.labelCustomerError.AutoSize = true;
            this.labelCustomerError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomerError.Location = new System.Drawing.Point(144, 23);
            this.labelCustomerError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCustomerError.Name = "labelCustomerError";
            this.labelCustomerError.Size = new System.Drawing.Size(0, 24);
            this.labelCustomerError.TabIndex = 24;
            // 
            // labelCustomer
            // 
            this.labelCustomer.AutoSize = true;
            this.labelCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomer.Location = new System.Drawing.Point(614, 23);
            this.labelCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCustomer.Name = "labelCustomer";
            this.labelCustomer.Size = new System.Drawing.Size(107, 26);
            this.labelCustomer.TabIndex = 25;
            this.labelCustomer.Text = "Customer";
            // 
            // customerComboBox
            // 
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.Location = new System.Drawing.Point(786, 23);
            this.customerComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(236, 33);
            this.customerComboBox.TabIndex = 0;
            // 
            // errorProviderCustomer
            // 
            this.errorProviderCustomer.ContainerControl = this;
            // 
            // textBoxDateReceived
            // 
            this.textBoxDateReceived.Location = new System.Drawing.Point(786, 417);
            this.textBoxDateReceived.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDateReceived.Name = "textBoxDateReceived";
            this.textBoxDateReceived.Size = new System.Drawing.Size(236, 31);
            this.textBoxDateReceived.TabIndex = 5;
            // 
            // labelDateReceived
            // 
            this.labelDateReceived.AutoSize = true;
            this.labelDateReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateReceived.Location = new System.Drawing.Point(614, 423);
            this.labelDateReceived.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDateReceived.Name = "labelDateReceived";
            this.labelDateReceived.Size = new System.Drawing.Size(153, 25);
            this.labelDateReceived.TabIndex = 28;
            this.labelDateReceived.Text = "Date Received";
            // 
            // errorProviderDateReceived
            // 
            this.errorProviderDateReceived.ContainerControl = this;
            // 
            // labelDateReceivedError
            // 
            this.labelDateReceivedError.AutoSize = true;
            this.labelDateReceivedError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateReceivedError.Location = new System.Drawing.Point(144, 417);
            this.labelDateReceivedError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDateReceivedError.Name = "labelDateReceivedError";
            this.labelDateReceivedError.Size = new System.Drawing.Size(0, 24);
            this.labelDateReceivedError.TabIndex = 29;
            // 
            // labelDueDate
            // 
            this.labelDueDate.AutoSize = true;
            this.labelDueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDueDate.Location = new System.Drawing.Point(614, 501);
            this.labelDueDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDueDate.Name = "labelDueDate";
            this.labelDueDate.Size = new System.Drawing.Size(104, 26);
            this.labelDueDate.TabIndex = 30;
            this.labelDueDate.Text = "Due Date";
            // 
            // textboxDueDate
            // 
            this.textboxDueDate.Location = new System.Drawing.Point(786, 496);
            this.textboxDueDate.Margin = new System.Windows.Forms.Padding(6);
            this.textboxDueDate.Name = "textboxDueDate";
            this.textboxDueDate.Size = new System.Drawing.Size(236, 31);
            this.textboxDueDate.TabIndex = 6;
            // 
            // labelDueDateError
            // 
            this.labelDueDateError.AutoSize = true;
            this.labelDueDateError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDueDateError.Location = new System.Drawing.Point(144, 496);
            this.labelDueDateError.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDueDateError.Name = "labelDueDateError";
            this.labelDueDateError.Size = new System.Drawing.Size(0, 24);
            this.labelDueDateError.TabIndex = 32;
            // 
            // CreateRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.labelDueDateError);
            this.Controls.Add(this.textboxDueDate);
            this.Controls.Add(this.labelDueDate);
            this.Controls.Add(this.labelDateReceivedError);
            this.Controls.Add(this.labelDateReceived);
            this.Controls.Add(this.textBoxDateReceived);
            this.Controls.Add(this.customerComboBox);
            this.Controls.Add(this.labelCustomer);
            this.Controls.Add(this.labelCustomerError);
            this.Controls.Add(this.labelOperatorError);
            this.Controls.Add(this.labelOperator);
            this.Controls.Add(this.operatorComboBox);
            this.Controls.Add(this.labelPOError);
            this.Controls.Add(this.labelOperationsError);
            this.Controls.Add(this.labelQuantityError);
            this.Controls.Add(this.labelPartNumberError);
            this.Controls.Add(this.textBoxPO);
            this.Controls.Add(this.labelPO);
            this.Controls.Add(this.textBoxPartNumber);
            this.Controls.Add(this.labelPartNumber);
            this.Controls.Add(this.labelRecordStatus);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelOperations);
            this.Controls.Add(this.textBoxQuantity);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "CreateRecord";
            this.Text = "CreateRecord";
            this.Load += new System.EventHandler(this.CreateRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPartNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderOperator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderDateReceived)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.Label labelOperations;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelRecordStatus;
        private System.Windows.Forms.Label labelPartNumber;
        private System.Windows.Forms.TextBox textBoxPartNumber;
        private System.Windows.Forms.ErrorProvider errorProviderPartNumber;
        private System.Windows.Forms.TextBox textBoxPO;
        private System.Windows.Forms.Label labelPO;
        private System.Windows.Forms.Label labelPartNumberError;
        private System.Windows.Forms.Label labelPOError;
        private System.Windows.Forms.Label labelOperationsError;
        private System.Windows.Forms.Label labelQuantityError;
        private System.Windows.Forms.ComboBox operatorComboBox;
        private System.Windows.Forms.Label labelOperator;
        private System.Windows.Forms.Label labelOperatorError;
        private System.Windows.Forms.ErrorProvider errorProviderOperator;
        private System.Windows.Forms.ComboBox customerComboBox;
        private System.Windows.Forms.Label labelCustomer;
        private System.Windows.Forms.Label labelCustomerError;
        private System.Windows.Forms.ErrorProvider errorProviderCustomer;
        private System.Windows.Forms.Label labelDateReceived;
        private System.Windows.Forms.TextBox textBoxDateReceived;
        private System.Windows.Forms.ErrorProvider errorProviderDateReceived;
        private System.Windows.Forms.Label labelDateReceivedError;
        private System.Windows.Forms.TextBox textboxDueDate;
        private System.Windows.Forms.Label labelDueDate;
        private System.Windows.Forms.Label labelDueDateError;
    }
}
