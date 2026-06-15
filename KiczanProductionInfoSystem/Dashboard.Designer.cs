namespace KiczanProductionInfoSystem
{
    partial class Dashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.barChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pieChart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.columnChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartOrders = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.barChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // barChart1
            // 
            chartArea1.Name = "ChartArea1";
            this.barChart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.barChart1.Legends.Add(legend1);
            this.barChart1.Location = new System.Drawing.Point(24, 20);
            this.barChart1.Margin = new System.Windows.Forms.Padding(2);
            this.barChart1.Name = "barChart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.barChart1.Series.Add(series1);
            this.barChart1.Size = new System.Drawing.Size(859, 338);
            this.barChart1.TabIndex = 0;
            this.barChart1.Text = "barChart1";
            // 
            // pieChart2
            // 
            chartArea2.Name = "ChartArea1";
            this.pieChart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.pieChart2.Legends.Add(legend2);
            this.pieChart2.Location = new System.Drawing.Point(24, 364);
            this.pieChart2.Margin = new System.Windows.Forms.Padding(2);
            this.pieChart2.Name = "pieChart2";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.pieChart2.Series.Add(series2);
            this.pieChart2.Size = new System.Drawing.Size(325, 338);
            this.pieChart2.TabIndex = 2;
            this.pieChart2.Text = "pieChart2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(362, 364);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(520, 338);
            this.dataGridView1.TabIndex = 3;
            // 
            // columnChart1
            // 
            chartArea3.Name = "ChartArea1";
            this.columnChart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.columnChart1.Legends.Add(legend3);
            this.columnChart1.Location = new System.Drawing.Point(900, 325);
            this.columnChart1.Margin = new System.Windows.Forms.Padding(2);
            this.columnChart1.Name = "columnChart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.columnChart1.Series.Add(series3);
            this.columnChart1.Size = new System.Drawing.Size(364, 377);
            this.columnChart1.TabIndex = 4;
            this.columnChart1.Text = "columnChart1";
            // 
            // chartOrders
            // 
            chartArea4.Name = "ChartArea1";
            this.chartOrders.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartOrders.Legends.Add(legend4);
            this.chartOrders.Location = new System.Drawing.Point(900, 20);
            this.chartOrders.Name = "chartOrders";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartOrders.Series.Add(series4);
            this.chartOrders.Size = new System.Drawing.Size(364, 300);
            this.chartOrders.TabIndex = 5;
            this.chartOrders.Text = "chartOrders";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.ClientSize = new System.Drawing.Size(1260, 552);
            this.Controls.Add(this.chartOrders);
            this.Controls.Add(this.columnChart1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pieChart2);
            this.Controls.Add(this.barChart1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart barChart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart columnChart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOrders;
    }
}