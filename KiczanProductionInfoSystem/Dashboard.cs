using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.ML;
using Microsoft.ML.TimeSeries;




namespace KiczanProductionInfoSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Text = "Kiczan: Dashboard";
            this.WindowState = FormWindowState.Normal;
            this.ClientSize = new Size(1270, 600);
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
            PopulateOrdersChartData();

            this.AutoScroll = true;
        }
        //Method to populate customer bar chart data.
        private void PopulateCustomerChartData()
        {
            //Instantiate new DAO object for chart population.
            DAO newDAO = new DAO();

            //Clear chart to make ready for incoming data.
            barChart1.Series.Clear();
            barChart1.ChartAreas.Clear();
            barChart1.Titles.Clear();

            //Set chart properties for display.
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

            barChart1.BackColor = Color.Transparent;
            barChart1.ChartAreas[0].BackColor = Color.Transparent;

            barChart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            barChart1.ChartAreas[0].AxisY2.LabelStyle.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);

            //Create new series object, and set display properties.
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

            barChart1.Series.Add(series);

            //Create a new datatable, set the datatable equal to the returned datatable from the called function.
            DataTable dt = newDAO.LoadCustomerChartData();

            int i = 0;

            //Load the data from the datatable into the column chart using the series objects.
            foreach (DataRow row in dt.Rows)
            {
                string customerName = row["CUSTOMER_NAME"].ToString();

                int totalQuantity = Convert.ToInt32(row["TotalQuantity"]);

                int newIndex = barChart1.Series["Quantity"].Points.AddXY(i, totalQuantity);

                barChart1.Series["Quantity"].Points[newIndex].AxisLabel = customerName;

                i++;
            }

            //Set colors for data differentation on chart visual.
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
                 Docking = Docking.Top,
                 Alignment = StringAlignment.Center,
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
                  slice.BorderWidth = 2;
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
                //Docking = Docking.Top,
                //Alignment = StringAlignment.Center,
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
                slice.BorderWidth = 2;
            }
        }
        private void PopulateVolumeChartData()
        {
            DAO dao = new DAO();
            DataTable dt = dao.LoadQuarterHistory();

            columnChart1.Series.Clear();
            columnChart1.Titles.Clear();

            columnChart1.Titles.Add("Quarterly Scheduled Items (Actual + Forecast)");
            columnChart1.Titles[0].Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);

            Series actual = new Series("Actual Scheduled Items")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.MediumPurple
            };

            var qtyList = new List<float>();

            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                string label = $"Q{row["QuarterNumber"]} {row["Year"]}";
                float value = Convert.ToSingle(row["Total Scheduled Items"]);

                qtyList.Add(value);

                actual.Points.AddXY(i, value);
                actual.Points[i].AxisLabel = label;

                i++;
            }

            var forecast = ForecastSeries(qtyList);

            Series forecastSeries = new Series("Forecast Scheduled Items")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Orange
            };

            int lastYear = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Year"]);
            int lastQuarter = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["QuarterNumber"]);

            List<string> futureLabels = GenerateFutureQuarters(lastYear, lastQuarter, 4);

            for (int f = 0; f < 4; f++)
            {
                forecastSeries.Points.AddXY(i + f, forecast[f]);
                forecastSeries.Points[f].AxisLabel = futureLabels[f];
            }

            columnChart1.Series.Add(actual);
            columnChart1.Series.Add(forecastSeries);

            ApplyChartStyle(columnChart1);
        }
        private void PopulateOrdersChartData()
        {
            DAO dao = new DAO();
            DataTable dt = dao.LoadQuarterHistory();

            chartOrders.Series.Clear();
            chartOrders.Titles.Clear();

            chartOrders.Titles.Add("Quarterly Orders (Actual + Forecast)");
            chartOrders.Titles[0].Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);

            Series actual = new Series("Actual Orders")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SteelBlue
            };

            var orders = new List<float>();

            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                string label = $"Q{row["QuarterNumber"]} {row["Year"]}";
                float value = Convert.ToSingle(row["Total Orders"]);

                orders.Add(value);

                actual.Points.AddXY(i, value);
                actual.Points[i].AxisLabel = label;

                i++;
            }

            var forecast = ForecastSeries(orders);

            Series forecastSeries = new Series("Forecast Orders")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Orange
            };

            int lastYear = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Year"]);
            int lastQuarter = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["QuarterNumber"]);

            List<string> futureLabels = GenerateFutureQuarters(lastYear, lastQuarter, 4);

            for (int f = 0; f < 4; f++)
            {
                forecastSeries.Points.AddXY(i + f, forecast[f]);
                forecastSeries.Points[f].AxisLabel = futureLabels[f];
            }

            chartOrders.Series.Add(actual);
            chartOrders.Series.Add(forecastSeries);

            ApplyChartStyle(chartOrders);
        }

        private float[] ForecastSeries(List<float> data)
        {
            if (data == null || data.Count < 6)
                throw new Exception("Need at least 6 data points for forecasting.");

            var ml = new MLContext();

            // ----------------------------
            // INPUT FORMAT FOR ML.NET
            // ----------------------------
            var series = data.Select(x => new ModelInput
            {
                Value = x
            });

            var dataView = ml.Data.LoadFromEnumerable(series);

            // ----------------------------
            // SSA FORECAST PIPELINE
            // ----------------------------
            var pipeline = ml.Forecasting.ForecastBySsa(
                outputColumnName: nameof(ModelOutput.Forecasted),
                inputColumnName: nameof(ModelInput.Value),
                windowSize: 4,
                seriesLength: data.Count,
                trainSize: data.Count,
                horizon: 4,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: nameof(ModelOutput.Lower),
                confidenceUpperBoundColumn: nameof(ModelOutput.Upper)
            );

            var model = pipeline.Fit(dataView);

            // ----------------------------
            // TRANSFORM (NO ENGINE USED)
            // ----------------------------
            var transformed = model.Transform(dataView);

            var results = ml.Data
                .CreateEnumerable<ModelOutput>(transformed, reuseRowObject: false)
                .Last();

            return results.Forecasted;
        }


        private void PopulateDepartmentGridView()
        {
            DAO newDAO = new DAO();
            dataGridView1.DataSource = newDAO.LoadDepartmentGridViewData();
        }

        private void ApplyChartStyle(Chart chart)
        {
            var area = chart.ChartAreas[0];

            chart.BackColor = Color.Transparent;
            area.BackColor = Color.Transparent;

            // Axis styling
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9f);

            // Grid cleanup
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // Improve bar spacing
            foreach (var series in chart.Series)
            {
                series["PointWidth"] = "0.45";
                series.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            }

            // Clean legend
            chart.Legends.Clear();

            Legend legend = new Legend()
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = Color.Transparent
            };

            chart.Legends.Add(legend);
        }
        private List<string> GenerateFutureQuarters(int year, int quarter, int count)
        {
            List<string> labels = new List<string>();

            for (int i = 0; i < count; i++)
            {
                quarter++;

                if (quarter > 4)
                {
                    quarter = 1;
                    year++;
                }

                labels.Add($"Q{quarter} {year}");
            }

            return labels;
        }
    }
}
