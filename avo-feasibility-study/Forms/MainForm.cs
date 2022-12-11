using avo_feasibility_study.Forms.AddForms;
using avo_feasibility_study.Models;
using System;
using System.Windows.Forms;

namespace avo_feasibility_study
{
    public partial class MainForm : Form
    {

        private const int _rowHeight = 52;

        public MainForm()
        {
            InitializeComponent();
            ButtonAddCompetitiveness.Click += ButtonAddEntry_Click;

            button2.Click += ButtonChange_Click;
            button4.Click += ButtonChange_Click;

            button3.Click += ButtonDelete_Click;
            button5.Click += ButtonDelete_Click;
        }

        private void ButtonAddEntry_Click(object sender, EventArgs e)
        {
            FormCompetitiveness addForm = new FormCompetitiveness();
            addForm.AddCompetitiveness += AddRow;
            addForm.ShowDialog();
        }

        private void AddRow(object sender, Competitiveness competitiveness)
        {
            TableCompetitiveness.Height += _rowHeight;          // Increase table height
            TableCompetitiveness.RowCount++;                    // Create new row in table
            var lastRowIndex = TableCompetitiveness.RowCount - 1;
            Label index = new Label()
            {
                Name = "LabelIndex" + lastRowIndex,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 45,
                Text = competitiveness.QualityScore
            };
            Label coef = new Label()
            {
                Name = "LabelCoef" + lastRowIndex,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 45,
                Text = competitiveness.Coef.ToString()
            };
            Label project = new Label()
            {
                Name = "LabelProject" + lastRowIndex,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 45,
                Text = competitiveness.ProjectEvaluation.ToString()
            };
            Label analog = new Label()
            {
                Name = "LabelAnalog" + lastRowIndex,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 45,
                Text = competitiveness.AnalogEvaluation.ToString()
            };
            Button buttonChange = new Button()
            {
                Name = "ButtonChange" + lastRowIndex,
                Dock = DockStyle.Fill,
                Height = 45,
                Text = "Изменить"
            };
            buttonChange.Click += ButtonChange_Click;
            Button buttonDelete = new Button()
            {
                Name = "ButtonDelete" + lastRowIndex,
                Dock = DockStyle.Fill,
                Height = 45,
                Text = "Удалить"
            };
            buttonDelete.Click += ButtonDelete_Click;
            // Add elements in row
            TableCompetitiveness.Controls.Add(index, 0, lastRowIndex);
            TableCompetitiveness.Controls.Add(coef, 1, lastRowIndex);
            TableCompetitiveness.Controls.Add(project, 2, lastRowIndex);
            TableCompetitiveness.Controls.Add(analog, 3, lastRowIndex);
            TableCompetitiveness.Controls.Add(buttonChange, 4, lastRowIndex);
            TableCompetitiveness.Controls.Add(buttonDelete, 5, lastRowIndex);

            // Change row style
            var rowStyles = TableCompetitiveness.RowStyles;
            foreach (RowStyle rowStyle in rowStyles)
            {
                rowStyle.Height = 50;
                rowStyle.SizeType = SizeType.Absolute;
            }
        }

        private void ChangeRow(object sender, ChangeCompetitiveness competitiveness)
        {
            var row = competitiveness.Row;

            var indexLabel = TableCompetitiveness.GetControlFromPosition(0, row) as Label;
            var coefLabel = TableCompetitiveness.GetControlFromPosition(1, row) as Label;
            var projectEvaluationLabel = TableCompetitiveness.GetControlFromPosition(2, row) as Label;
            var analogEvaluationLabel = TableCompetitiveness.GetControlFromPosition(3, row) as Label;

            indexLabel.Text = competitiveness.QualityScore;
            coefLabel.Text = competitiveness.Coef.ToString();
            projectEvaluationLabel.Text = competitiveness.ProjectEvaluation.ToString();
            analogEvaluationLabel.Text = competitiveness.AnalogEvaluation.ToString();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var table = currentButton.Parent as TableLayoutPanel;
            int column = table.GetPositionFromControl(currentButton).Column;
            int row = table.GetPositionFromControl(currentButton).Row;
            MessageBox.Show("Row: " + row + " Column: " + column);
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var table = currentButton.Parent as TableLayoutPanel;
            int row = table.GetPositionFromControl(currentButton).Row;

            var indexLabel = table.GetControlFromPosition(0, row) as Label;
            var coefLabel = table.GetControlFromPosition(1, row) as Label;
            var projectEvaluationLabel = table.GetControlFromPosition(2, row) as Label;
            var analogEvaluationLabel = table.GetControlFromPosition(3, row) as Label;

            var qualityScore = indexLabel.Text;
            var coef = float.Parse(coefLabel.Text);
            var projectEvaluation = int.Parse(projectEvaluationLabel.Text);
            var analogEvaluation = int.Parse(analogEvaluationLabel.Text);

            ChangeCompetitiveness competitiveness = new ChangeCompetitiveness()
            {
                QualityScore = qualityScore,
                Coef = coef,
                ProjectEvaluation = projectEvaluation,
                AnalogEvaluation = analogEvaluation,
                Row = row
            };

            FormCompetitiveness addForm = new FormCompetitiveness(competitiveness);
            addForm.ChangeCompetitiveness += ChangeRow;
            addForm.ShowDialog();
        }
    }
}
