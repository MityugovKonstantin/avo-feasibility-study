using System;
using System.Windows.Forms;

namespace avo_feasibility_study
{
    public partial class MainForm : Form
    {

        private const int _rowHeight = 51;

        public MainForm()
        {
            InitializeComponent();
            ButtonAddCompetitiveness.Click += addEntryButton_Click;
        }

        /// <summary>
        /// Method used to connect to dynamically created buttons (change)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeEntryButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Method used to connect to dynamically created buttons (delete)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteEntryButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Method used to connect to buttons (add)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEntryButton_Click(object sender, EventArgs e)
        {
            TableCompetitiveness.Height += _rowHeight;
            TableCompetitiveness.RowCount++; // Create new row in table
            TableCompetitiveness.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            Label index = new Label()
            {
                Name = "LabelIndex" + (TableCompetitiveness.RowCount - 1),
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 50,
                Text = "Index" // Value from DB
            };
            Button buttonChange = new Button()
            {
                Name = "ButtonChange" + (TableCompetitiveness.RowCount - 1),
                Dock = DockStyle.Fill,
                Height = 50,
                Text = "Изменить" // Value from DB
            };
            TableCompetitiveness.Controls.Add(index, 0, TableCompetitiveness.RowCount - 1);
            TableCompetitiveness.Controls.Add(buttonChange, 4, TableCompetitiveness.RowCount - 1);
        }
    }
}
