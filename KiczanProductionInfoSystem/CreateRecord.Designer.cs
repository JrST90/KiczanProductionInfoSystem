using System.Drawing;

namespace KiczanProductionInformationSystem
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
            this.button1.Location = new System.Drawing.Point(355, 517);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 28);
            this.button1.TabIndex = 0;
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
            "Press Brake"});
            this.checkedListBox1.Location = new System.Drawing.Point(524, 375);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(159, 89);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBoxQuantity
            // 
            this.textBoxQuantity.Location = new System.Drawing.Point(524, 269);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(159, 22);
            this.textBoxQuantity.TabIndex = 3;
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuantity.Location = new System.Drawing.Point(419, 271);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(61, 17);
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
            this.labelOperations.Location = new System.Drawing.Point(419, 410);
            this.labelOperations.Name = "labelOperations";
            this.labelOperations.Size = new System.Drawing.Size(78, 17);
            this.labelOperations.TabIndex = 6;
            this.labelOperations.Text = "Operations";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(524, 517);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 28);
            this.button2.TabIndex = 9;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Blue;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(712, 517);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 28);
            this.button3.TabIndex = 10;
            this.button3.Text = "Back to Menu";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelRecordStatus
            // 
            this.labelRecordStatus.AutoSize = true;
            this.labelRecordStatus.Location = new System.Drawing.Point(483, 483);
            this.labelRecordStatus.Name = "labelRecordStatus";
            this.labelRecordStatus.Size = new System.Drawing.Size(0, 16);
            this.labelRecordStatus.TabIndex = 11;
            // 
            // labelPartNumber
            // 
            this.labelPartNumber.AutoSize = true;
            this.labelPartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPartNumber.Location = new System.Drawing.Point(419, 117);
            this.labelPartNumber.Name = "labelPartNumber";
            this.labelPartNumber.Size = new System.Drawing.Size(88, 17);
            this.labelPartNumber.TabIndex = 12;
            this.labelPartNumber.Text = "Part Number";
            // 
            // textBoxPartNumber
            // 
            this.textBoxPartNumber.Location = new System.Drawing.Point(524, 115);
            this.textBoxPartNumber.Name = "textBoxPartNumber";
            this.textBoxPartNumber.Size = new System.Drawing.Size(159, 22);
            this.textBoxPartNumber.TabIndex = 13;
            // 
            // errorProviderPartNumber
            // 
            this.errorProviderPartNumber.ContainerControl = this;
            // 
            // labelPO
            // 
            this.labelPO.AutoSize = true;
            this.labelPO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPO.Location = new System.Drawing.Point(419, 194);
            this.labelPO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPO.Name = "labelPO";
            this.labelPO.Size = new System.Drawing.Size(99, 17);
            this.labelPO.TabIndex = 15;
            this.labelPO.Text = "Order Number";
            // 
            // textBoxPO
            // 
            this.textBoxPO.Location = new System.Drawing.Point(524, 194);
            this.textBoxPO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPO.Name = "textBoxPO";
            this.textBoxPO.Size = new System.Drawing.Size(159, 22);
            this.textBoxPO.TabIndex = 16;
            // 
            // labelPartNumberError
            // 
            this.labelPartNumberError.AutoSize = true;
            this.labelPartNumberError.Location = new System.Drawing.Point(96, 118);
            this.labelPartNumberError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPartNumberError.Name = "labelPartNumberError";
            this.labelPartNumberError.Size = new System.Drawing.Size(0, 16);
            this.labelPartNumberError.TabIndex = 17;
            // 
            // labelQuantityError
            // 
            this.labelQuantityError.AutoSize = true;
            this.labelQuantityError.Location = new System.Drawing.Point(96, 271);
            this.labelQuantityError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelQuantityError.Name = "labelQuantityError";
            this.labelQuantityError.Size = new System.Drawing.Size(0, 16);
            this.labelQuantityError.TabIndex = 18;
            // 
            // labelOperationsError
            // 
            this.labelOperationsError.AutoSize = true;
            this.labelOperationsError.Location = new System.Drawing.Point(96, 411);
            this.labelOperationsError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOperationsError.Name = "labelOperationsError";
            this.labelOperationsError.Size = new System.Drawing.Size(0, 16);
            this.labelOperationsError.TabIndex = 19;
            // 
            // labelPOError
            // 
            this.labelPOError.AutoSize = true;
            this.labelPOError.Location = new System.Drawing.Point(96, 196);
            this.labelPOError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPOError.Name = "labelPOError";
            this.labelPOError.Size = new System.Drawing.Size(0, 16);
            this.labelPOError.TabIndex = 20;
            // 
            // operatorComboBox
            // 
            this.operatorComboBox.FormattingEnabled = true;
            this.operatorComboBox.Location = new System.Drawing.Point(524, 68);
            this.operatorComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.operatorComboBox.Name = "operatorComboBox";
            this.operatorComboBox.Size = new System.Drawing.Size(159, 24);
            this.operatorComboBox.TabIndex = 21;
            // 
            // labelOperator
            // 
            this.labelOperator.AutoSize = true;
            this.labelOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOperator.Location = new System.Drawing.Point(422, 68);
            this.labelOperator.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOperator.Name = "labelOperator";
            this.labelOperator.Size = new System.Drawing.Size(65, 17);
            this.labelOperator.TabIndex = 22;
            this.labelOperator.Text = "Operator";
            // 
            // labelOperatorError
            // 
            this.labelOperatorError.AutoSize = true;
            this.labelOperatorError.Location = new System.Drawing.Point(96, 70);
            this.labelOperatorError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOperatorError.Name = "labelOperatorError";
            this.labelOperatorError.Size = new System.Drawing.Size(0, 16);
            this.labelOperatorError.TabIndex = 23;
            // 
            // errorProviderOperator
            // 
            this.errorProviderOperator.ContainerControl = this;
            // 
            // labelCustomerError
            // 
            this.labelCustomerError.AutoSize = true;
            this.labelCustomerError.Location = new System.Drawing.Point(96, 20);
            this.labelCustomerError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCustomerError.Name = "labelCustomerError";
            this.labelCustomerError.Size = new System.Drawing.Size(0, 16);
            this.labelCustomerError.TabIndex = 24;
            // 
            // labelCustomer
            // 
            this.labelCustomer.AutoSize = true;
            this.labelCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomer.Location = new System.Drawing.Point(422, 20);
            this.labelCustomer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCustomer.Name = "labelCustomer";
            this.labelCustomer.Size = new System.Drawing.Size(68, 17);
            this.labelCustomer.TabIndex = 25;
            this.labelCustomer.Text = "Customer";
            // 
            // customerComboBox
            // 
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.Location = new System.Drawing.Point(524, 15);
            this.customerComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(159, 24);
            this.customerComboBox.TabIndex = 26;
            // 
            // errorProviderCustomer
            // 
            this.errorProviderCustomer.ContainerControl = this;
            // 
            // textBoxDateReceived
            // 
            this.textBoxDateReceived.Location = new System.Drawing.Point(524, 321);
            this.textBoxDateReceived.Name = "textBoxDateReceived";
            this.textBoxDateReceived.Size = new System.Drawing.Size(159, 22);
            this.textBoxDateReceived.TabIndex = 27;
            // 
            // labelDateReceived
            // 
            this.labelDateReceived.AutoSize = true;
            this.labelDateReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateReceived.Location = new System.Drawing.Point(409, 327);
            this.labelDateReceived.Name = "labelDateReceived";
            this.labelDateReceived.Size = new System.Drawing.Size(98, 16);
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
            this.labelDateReceivedError.Location = new System.Drawing.Point(96, 327);
            this.labelDateReceivedError.Name = "labelDateReceivedError";
            this.labelDateReceivedError.Size = new System.Drawing.Size(0, 16);
            this.labelDateReceivedError.TabIndex = 29;
            // 
            // CreateRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}
