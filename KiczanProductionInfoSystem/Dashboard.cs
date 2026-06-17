using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KiczanProductionInfoSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Text = "Kiczan: Dashboard";
            this.WindowState = FormWindowState.Normal;
            this.ClientSize = new Size(1270, 750);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //Event handler to load dashboard chart on form initialization.
        private void Dashboard_Load(object sender, EventArgs e)
        {
            PopulateCustomerChartData();
            PopulateOperatorChartData();
            PopulateDepartmentChartData();
            PopulateDepartmentGridView();
            PopulateVolumeChartData();
        }

        //Method to populate customer bar chart data.
        private void PopulateCustomerChartData()
        {
            DAO newDAO = new DAO();

            barChart1.Series.Clear();
            barChart1.ChartAreas.Clear();
            barChart1.Titles.Clear();

            ChartArea barChartArea = new ChartArea("barChartArea");
            barChart1.ChartAreas.Add(barChartArea);

            barChartArea.AxisX.IsReversed = true;
            barChartArea.AxisX.IsMarksNextToAxis = true;
            barChartArea.AxisX.Interval = 1;
            barChartArea.AxisX.IsLabelAutoFit = true;
            barChartArea.AxisX.LabelStyle.Enabled = true;
            barChartArea.AxisX.MajorGrid.Enabled = false;

            barChartArea.AxisY2.Enabled = AxisEnabled.True;
            barChartArea.AxisY2.MajorGrid.Enabled = false;
            barChartArea.AxisY.Enabled = AxisEnabled.False;
            barChartArea.AxisY.MajorGrid.Enabled = false;

            barChart1.Legends.Clear();

            barChart1.Titles.Add("Customer Parts Quantity in Last 6 Months");
            barChart1.Titles[0].Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);

            barChart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            barChart1.ChartAreas[0].AxisY2.LabelStyle.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);

            Series series = new Series("Quantity")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true,
                ["BarLabelStyle"] = "Center",
                Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold),
                XAxisType = AxisType.Primary,
                YAxisType = AxisType.Primary,
                XValueType = ChartValueType.String
            };

            barChart1.BackColor = Color.Transparent;
            barChart1.ChartAreas[0].BackColor = Color.Transparent;

            barChart1.Series.Add(series);
                        
            DataTable dt = newDAO.LoadCustomerChartData();

            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                string customerName = row["CUSTOMER_NAME"].ToString();

                int totalQuantity = Convert.ToInt32(row["TotalQuantity"]);

                int newIndex = barChart1.Series["Quantity"].Points.AddXY(i, totalQuantity);

                barChart1.Series["Quantity"].Points[newIndex].AxisLabel = customerName;

                i++;
            }
            
            Color[] dashboardColors = new Color[]
            {
                Color.FromArgb(52, 116, 181),
                Color.FromArgb(46, 139, 87),
                Color.FromArgb(139, 92, 246),
                Color.FromArgb(234, 179, 8),
                Color.FromArgb(249, 115, 22),
                Color.FromArgb(220, 38, 38),
                Color.FromArgb(13, 148, 136)
            };
            
            for (int j = 0; j < barChart1.Series["Quantity"].Points.Count; j++)
            {
                var point = barChart1.Series["Quantity"].Points[j];
                Color chosenColor = dashboardColors[j % dashboardColors.Length];
                point.Color = chosenColor;
                point.BorderColor = Color.White;
                point.BorderWidth = 1;
            }
        }
        
        //Method to populate operator pie chart data.
        private void PopulateOperatorChartData()
        {
            DAO newDAO = new DAO();

            pieChart1.Series.Clear();
            pieChart1.ChartAreas.Clear();
            pieChart1.Titles.Clear();

            ChartArea pieChartArea = new ChartArea("pieChartArea");
            pieChart1.ChartAreas.Add(pieChartArea);

            pieChart1.Titles.Add("Bending Operator Parts Quantity in Last 6 Months");
            pieChart1.Titles[0].Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);

            Series series = new Series("Quantity")
            {
                ChartType = SeriesChartType.Pie,
                XValueMember = "OPERATOR_NAME",
                YValueMembers = "TotalQuantity",
                IsValueShownAsLabel = true,
                Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold),
                ToolTip = "Quantity: #VALY"
            };

            series.Label = "#PERCENT{P1}";
            series.LegendText = "#VALX";

            pieChartArea.Area3DStyle.Enable3D = true;
            pieChartArea.Area3DStyle.Inclination = 45;

            pieChart1.Series.Add(series);

            Legend legend = new Legend("Main Legend")
            {
                Docking = Docking.Right,
                BackColor = Color.Transparent
            };

            pieChart1.Legends.Add(legend);
            pieChart1.Legends[0].Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            pieChart1.Legends[0].BackColor = Color.Transparent;
           
            pieChart1.DataSource = newDAO.LoadOperatorChartData();
            pieChart1.DataBind();

            pieChart1.BackColor = Color.Transparent;
            pieChart1.ChartAreas[0].BackColor = Color.Transparent;

            Color[] dashboardColors = new Color[]
            {
                Color.FromArgb(52, 116, 181),
                Color.FromArgb(46, 139, 87),
                Color.FromArgb(139, 92, 246),
                Color.FromArgb(234, 179, 8),
                Color.FromArgb(249, 115, 22),
                Color.FromArgb(220, 38, 38),
                Color.FromArgb(13, 148, 136)
            };

            for (int i = 0; i < pieChart1.Series["Quantity"].Points.Count; i++)
            {
                 var slice = pieChart1.Series["Quantity"].Points[i];
                 slice.Color = dashboardColors[i % dashboardColors.Length];
                 slice.BorderColor = Color.White;
                 slice.BorderWidth = 1;
            }
        }
        
        //Method to populate department pie chart data.
        private void PopulateDepartmentChartData()
        {
            DAO newDAO = new DAO();

            pieChart2.Series.Clear();
            pieChart2.ChartAreas.Clear();
            pieChart2.Titles.Clear();

            ChartArea pieChartArea = new ChartArea("pieChartArea");
            pieChart2.ChartAreas.Add(pieChartArea);

            pieChart2.Titles.Add("Department Work Load in Next 6 Months");
            pieChart2.Titles[0].Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);

            Series series = new Series("Quantity")
            {
                ChartType = SeriesChartType.Pie,
                XValueMember = "Department",
                YValueMembers = "TotalJobs",
                IsValueShownAsLabel = true,
                Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold),
                ToolTip = "Total Jobs: #VALY"
            };

            series.Label = "#PERCENT{P1}";
            series.LegendText = "#VALX";

            pieChartArea.Area3DStyle.Enable3D = true;
            pieChartArea.Area3DStyle.Inclination = 45;

            pieChart2.Series.Add(series);

            Legend legend = new Legend("Main Legend")
            {
                Docking = Docking.Right,
                BackColor = Color.Transparent
            };

            pieChart2.Legends.Add(legend);
            pieChart2.Legends[0].Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            pieChart2.Legends[0].BackColor = Color.Transparent;

            pieChart2.DataSource = newDAO.LoadDepartmentChartData();
            pieChart2.DataBind();

            pieChart2.BackColor = Color.Transparent;
            pieChart2.ChartAreas[0].BackColor = Color.Transparent;

            Color[] dashboardColors = new Color[]
            {
                Color.FromArgb(52, 116, 181),
                Color.FromArgb(46, 139, 87),
                Color.FromArgb(139, 92, 246),
                Color.FromArgb(234, 179, 8),
                Color.FromArgb(249, 115, 22),
                Color.FromArgb(220, 38, 38),
                Color.FromArgb(13, 148, 136)
            };

            for (int i = 0; i < pieChart2.Series["Quantity"].Points.Count; i++)
            {
                var slice = pieChart2.Series["Quantity"].Points[i];
                slice.Color = dashboardColors[i % dashboardColors.Length];
                slice.BorderColor = Color.White;
                slice.BorderWidth = 1;
            }
        }
        
        private void PopulateVolumeChartData()
        {
            DAO newDAO = new DAO();

            columnChart1.Series.Clear();
            columnChart1.Titles.Clear();
            columnChart1.ChartAreas[0].AxisX.Interval = 1;

            columnChart1.Titles.Add("Last Fiscal Year Quarterly Volume \n(Orders & Parts)");
            columnChart1.Titles[0].Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);

            float currentFontSize = columnChart1.ChartAreas[0].AxisX.LabelStyle.Font.Size;

            columnChart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font(new FontFamily("Arial"), currentFontSize, FontStyle.Bold);
            columnChart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font(new FontFamily("Arial"), currentFontSize, FontStyle.Bold);

            columnChart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;

            Series seriesVolume = new Series("Total Orders")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                ToolTip = "Orders: #VALY",
                Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold),
                Color = Color.FromArgb(139, 92, 246),
                BorderColor = Color.White,
                BorderWidth = 1,
                XValueType = ChartValueType.String
            };

            Series seriesItems = new Series("Physical Parts")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                ToolTip = "Total Units: #VALY",
                Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold),
                Color = Color.FromArgb(234, 179, 8),
                BorderColor = Color.White,
                BorderWidth = 1,
                XValueType = ChartValueType.String
            };

            columnChart1.BackColor = Color.Transparent;
            columnChart1.ChartAreas[0].BackColor = Color.Transparent;
            columnChart1.Legends[0].BackColor = Color.Transparent;
            columnChart1.Legends[0].Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);

            columnChart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            columnChart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            columnChart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            columnChart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            columnChart1.Series.Add(seriesVolume);
            columnChart1.Series.Add(seriesItems);

            /*
            columnChart1.DataSource = newDAO.LoadLastFiscalYearVolume();
            columnChart1.DataBind();
            */

            DataTable dt = newDAO.LoadLastFiscalYearVolume();

            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string fiscalQuarter  = row["Fiscal Quarter"].ToString();

                    int totalVolume = Convert.ToInt32(row["Total Volume"]);

                    int totalItems = Convert.ToInt32(row["Total Scheduled Items"]);

                    int p1 = seriesVolume.Points.AddXY(i, totalVolume);
                    int p2 = seriesItems.Points.AddXY(i, totalItems);

                    seriesVolume.Points[p1].AxisLabel = fiscalQuarter;
                    seriesItems.Points[p2].AxisLabel = fiscalQuarter;

                    i++;
                }
            }
        }
        private void PopulateDepartmentGridView()
        {
            DAO newDAO = new DAO();
            dataGridView1.DataSource = newDAO.LoadDepartmentGridViewData();
        }
    }
}
