using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace A2_Graphs_RAD
{
    public partial class A2_Graphs_Main : Form
    {
        public List<DataSet> dataCollection = new List<DataSet>();
        public bool lockedEditButton = false;
        
        public A2_Graphs_Main()
        {
            InitializeComponent();

            // Populates sample Title and axis labels
            chart.Titles[0].Text = textBox_GraphTitle.Text;
            chart.ChartAreas[0].AxisY.Title = textBox_YAxisName.Text;
            chart.ChartAreas[0].AxisX.Title = textBox_XAxisName.Text;
        }

        #region Add DataSet Button
        // Add custom data button, takes in name, value and a color, adds to obj list.
        private void BTN_Add_To_List_Click(object sender, EventArgs e)
        {
            // Check to see if proper input has been added.
            if (string.IsNullOrEmpty(textBox_DataName.Text) || string.IsNullOrEmpty(textBox_DataValue.Text))
            {
                MessageBox.Show("Please try again", "Invalid Data Passed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_DataName.Clear();
                textBox_DataValue.Clear();
                return;
            }

            // Checks to see if number value is parsable.
            foreach (char str in (textBox_DataValue.Text).ToString())
            {
                if (!char.IsDigit(str))
                {
                    MessageBox.Show("Not a valid number", "Invalid Data Passed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox_DataValue.Clear();
                    return;
                }
            }

            // Adding valid data to the object class list.
            dataCollection.Add(new DataSet(textBox_DataName.Text, Convert.ToDouble(textBox_DataValue.Text), buttonDataColour.BackColor));

            // Small last input validation label
            labelLastItemAdded.Text = $"Name: {textBox_DataName.Text}\n" +
                                      $" Value: {Convert.ToDouble(textBox_DataValue.Text)}\n" +
                                      $"  Color: {buttonDataColour.BackColor}";

            textBox_DataName.Clear();
            textBox_DataValue.Clear();
            buttonDataColour.BackColor = Color.OldLace;
        }
        #endregion

        #region Color DialogBox Popup
        private void buttonDataColour_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.Color = buttonDataColour.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                buttonDataColour.BackColor = MyDialog.Color;
        }
        #endregion

        #region Update Title and Axis Label Operations
        private void button_UpdateLabels_Click(object sender, EventArgs e)
        {
            if (!lockedEditButton)
            {
                chart.Titles[0].Text = textBox_GraphTitle.Text;
                chart.ChartAreas[0].AxisY.Title = textBox_YAxisName.Text;
                chart.ChartAreas[0].AxisX.Title = textBox_XAxisName.Text;

                // Disable input and lock field once set
                textBox_GraphTitle.Enabled = false;
                textBox_XAxisName.Enabled = false;
                textBox_YAxisName.Enabled = false;

                button_UpdateLabels.Text = "UNLOCK Fields";
                button_UpdateLabels.ForeColor = Color.DarkSlateGray;
                lockedEditButton = true;
            }
            else
            {
                // Enable fields for editing
                textBox_GraphTitle.Enabled = true;
                textBox_XAxisName.Enabled = true;
                textBox_YAxisName.Enabled = true;

                button_UpdateLabels.Text = "Update Fields";
                button_UpdateLabels.ForeColor = Color.LightSeaGreen;
                lockedEditButton = false;
            }
        }
        private void textBox_GraphTitle_Click(object sender, EventArgs e)
        {
            textBox_GraphTitle.Clear();
        }
        private void textBox_YAxisName_Click(object sender, EventArgs e)
        {
            textBox_YAxisName.Clear();
        }
        private void textBox_XAxisName_Click(object sender, EventArgs e)
        {
            textBox_XAxisName.Clear();
        }
        #endregion

        #region Graph Generation
        private void button_GenerateGraph_Click(object sender, EventArgs e)
        {
            GenerateNewGraph();
        }
        private void GenerateNewGraph()
        {
            if (button_GenerateGraph.Enabled)
            { 
                // Disables the generate button and changes color
                button_GenerateGraph.Enabled = false;
                button_GenerateGraph.BackColor = Color.LightSlateGray;
                // Clear any preexisting series data
                chart.Series.Clear();

                foreach (DataSet data in dataCollection)
                {
                    chart.Series.Add(data.Name);
                    chart.Series[0].Color = data.Color;
                    chart.Series[0].Palette = ChartColorPalette.BrightPastel;
                    chart.Series[0].Points.AddXY(data.Name, data.Value);
                }
            }
        }
        private Color GetColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
        #endregion

        #region Clear / Reset Data Operations
        private void buttonClearAllData_Click(object sender, EventArgs e)
        {
            button_GenerateGraph.Enabled = true;
            button_GenerateGraph.BackColor = Color.LightSeaGreen;
            button_GenerateGraph.ForeColor = Color.White;
            checkBoxDummy.Checked = false;
            checkBoxDummy.Enabled = true;

            chart.Series.Clear();
            dataCollection.Clear();
            labelLastItemAdded.Text = "All items cleared";
        }
        #endregion

        #region Help Icon Messege Info
        private void A2_Graphs_Main_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show(
                "Welcome to A2 Graphs\n\n" +
                "Step 1. To add your own data, simply enter a name and integer value and press [Add Data]\n" +
                "Step 2. Next, press the [Generate Graph] button to see your chart data\n\n" +
                "To see dummy example data, click the checkbox and run [Generate Graph]\n\n" +
                "Finally, you can add your own Title, X and Y Axis names to the chart. Once you edit and update the labels, the dialogs will lock, but you cannot unlock them to edit again!", "Jody's A2 Help Prompt");
        }
        #endregion

        #region Dummy Data for Example, Checkbox to init
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxDummy.Enabled = false;

            dataCollection.Add(new DataSet("Cows", 15, GetColor()));
            dataCollection.Add(new DataSet("Horses", 10, GetColor()));
            dataCollection.Add(new DataSet("Snakes", 5, GetColor()));
            dataCollection.Add(new DataSet("Donkeys", 20, GetColor()));
            dataCollection.Add(new DataSet("Cats", 4.5, GetColor()));

            labelLastItemAdded.Text = "Dummy data loaded";
        }
        #endregion
    }
}
