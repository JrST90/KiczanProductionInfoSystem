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
            this.barChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pieChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pieChart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.barChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart2)).BeginInit();
            this.SuspendLayout();
            // 
            // barChart1
            // 
            chartArea1.Name = "ChartArea1";
            this.barChart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.barChart1.Legends.Add(legend1);
            this.barChart1.Location = new System.Drawing.Point(47, 39);
            this.barChart1.Name = "barChart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.barChart1.Series.Add(series1);
            this.barChart1.Size = new System.Drawing.Size(1718, 1400);
            this.barChart1.TabIndex = 0;
            this.barChart1.Text = "barChart1";
            // 
            // pieChart1
            // 
            chartArea2.Name = "ChartArea1";
            this.pieChart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.pieChart1.Legends.Add(legend2);
            this.pieChart1.Location = new System.Drawing.Point(1797, 39);
            this.pieChart1.Name = "pieChart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.pieChart1.Series.Add(series2);
            this.pieChart1.Size = new System.Drawing.Size(700, 700);
            this.pieChart1.TabIndex = 1;
            this.pieChart1.Text = "pieChart1";
            // 
            // pieChart2
            // 
            chartArea3.Name = "ChartArea1";
            this.pieChart2.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.pieChart2.Legends.Add(legend3);
            this.pieChart2.Location = new System.Drawing.Point(1797, 746);
            this.pieChart2.Name = "pieChart2";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.pieChart2.Series.Add(series3);
            this.pieChart2.Size = new System.Drawing.Size(700, 700);
            this.pieChart2.TabIndex = 2;
            this.pieChart2.Text = "pieChart2";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(207)))), ((int)(((byte)(219)))));
            this.ClientSize = new System.Drawing.Size(2500, 1117);
            this.Controls.Add(this.pieChart2);
            this.Controls.Add(this.pieChart1);
            this.Controls.Add(this.barChart1);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart barChart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart2;
    }
}