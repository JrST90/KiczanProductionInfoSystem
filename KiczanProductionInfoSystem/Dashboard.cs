using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KiczanProductionInfoSystem
{
    public partial class Dashboard : Form
    {
        //Build connection string to connect to Microsoft SQL Server.
        private readonly string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=KICZAN_PRODUCTION_SYSTEM;Trusted_Connection=True;TrustServerCertificate=True;";
        public Dashboard()
        {
            InitializeComponent();
            this.Text = "Kiczan Dashboard";
            this.WindowState = FormWindowState.Normal;
            this.ClientSize = new System.Drawing.Size(1270, 585);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //Event handler to load dashboard chart on form initialization.
        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadCustomerChartData();
            LoadOperatorChartData();
        }

        //Method to run GET_CUSTOMER_QTY_LAST_6_MONTHS query from SQL server to populate chart with queried data.
        private void LoadCustomerChartData()
        {
            //Create new datatable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("GET_CUSTOMER_QTY_LAST_6_MONTHS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Titles.Clear();

                ChartArea chartArea = new ChartArea("barChartArea");
                chart1.ChartAreas.Add(chartArea);

                chartArea.AxisX.IsReversed = true;
                chartArea.AxisX.IsMarksNextToAxis = true;
                chartArea.AxisX.Interval = 1;
                chartArea.AxisX.IsLabelAutoFit = true;
                chartArea.AxisX.LabelStyle.Enabled = true;
                chartArea.AxisX.MajorGrid.Enabled = false;

                chartArea.AxisY2.Enabled = AxisEnabled.True;
                chartArea.AxisY2.MajorGrid.Enabled = false;
                chartArea.AxisY.Enabled = AxisEnabled.False;
                chartArea.AxisY.MajorGrid.Enabled = false;

                chart1.Legends.Clear();

                chart1.Titles.Add("Customers by Quantity in Last 6 Months");

                Series series = new Series("Quantity")
                {
                    ChartType = SeriesChartType.Bar,
                    XValueMember = "CUSTOMER_NAME",
                    YValueMembers = "TotalQuantity",
                    IsValueShownAsLabel = true,

                    XAxisType = AxisType.Primary,
                    YAxisType = AxisType.Secondary
                };


                chart1.Series.Add(series);
                chart1.DataSource = dataTable;
                chart1.DataBind();

                System.Drawing.Color[] dashboardColors = new System.Drawing.Color[]
                {
                    System.Drawing.Color.FromArgb(52, 116, 181),
                    System.Drawing.Color.FromArgb(46, 139, 87),
                    System.Drawing.Color.FromArgb(139, 92, 246),
                    System.Drawing.Color.FromArgb(234, 179, 8),
                    System.Drawing.Color.FromArgb(249, 115, 22),
                    System.Drawing.Color.FromArgb(220, 38, 38),
                    System.Drawing.Color.FromArgb(13, 148, 136)
                };

                for (int i = 0; i < chart1.Series["Quantity"].Points.Count; i++)
                {
                    var point = chart1.Series["Quantity"].Points[i];
                    System.Drawing.Color chosenColor = dashboardColors[i % dashboardColors.Length];
                    point.Color = chosenColor;
                    point.BorderColor = System.Drawing.Color.FromArgb(150, chosenColor);
                    point.BorderWidth = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load customer dashboard chart data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Method to run GET_OPERATOR_QTY_LAST_6_MONTHS query from SQL server to populate chart with queried data.
        private void LoadOperatorChartData()
        {
            //Create new datatable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("GET_OPERATOR_QTY_LAST_6_MONTHS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                pieChart1.Series.Clear();
                pieChart1.ChartAreas.Clear();
                pieChart1.Titles.Clear();

                ChartArea pieChartArea = new ChartArea("pieChartArea");
                pieChart1.ChartAreas.Add(pieChartArea);

                pieChart1.Titles.Add("Operators by Quantity in Last 6 Months");

                Series series = new Series("Quantity")
                {
                    ChartType = SeriesChartType.Pie,
                    XValueMember = "OPERATOR_NAME",
                    YValueMembers = "TotalQuantity",
                    IsValueShownAsLabel = true,
                };

                series.Label = "#PERCENT{P1}";
                series.LegendText = "#VALX";

                pieChartArea.Area3DStyle.Enable3D = true;
                pieChartArea.Area3DStyle.Inclination = 45;

                pieChart1.Series.Add(series);

                Legend legend = new Legend("Main Legend")
                {
                    Docking = Docking.Right,
                    BackColor = System.Drawing.Color.Transparent
                };
                pieChart1.Legends.Add(legend);

                pieChart1.DataSource = dataTable;
                pieChart1.DataBind();

                System.Drawing.Color[] dashboardColors = new System.Drawing.Color[]
                {
                    System.Drawing.Color.FromArgb(52, 116, 181),
                    System.Drawing.Color.FromArgb(46, 139, 87),
                    System.Drawing.Color.FromArgb(139, 92, 246),
                    System.Drawing.Color.FromArgb(234, 179, 8),
                    System.Drawing.Color.FromArgb(249, 115, 22),
                    System.Drawing.Color.FromArgb(220, 38, 38),
                    System.Drawing.Color.FromArgb(13, 148, 136)
                };

                for (int i = 0; i < pieChart1.Series["Quantity"].Points.Count; i++)
                {
                    var slice = pieChart1.Series["Quantity"].Points[i];
                    slice.Color = dashboardColors[i % dashboardColors.Length];
                    slice.BorderColor = System.Drawing.Color.White;
                    slice.BorderWidth = 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load operator dashboard chart data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
