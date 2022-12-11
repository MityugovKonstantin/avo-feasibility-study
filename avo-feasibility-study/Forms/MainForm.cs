using avo_feasibility_study.Forms.AddForms;
using avo_feasibility_study.Interfaces;
using avo_feasibility_study.Models;
using avo_feasibility_study.BL.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using avo_feasibility_study.BL.Models.Results;

namespace avo_feasibility_study
{
    public partial class MainForm : Form, Iui
    {

        public event EventHandler<CompetitivenessParams> OnEvaluation;
        private const int _rowHeight = 52;

        public MainForm()
        {
            InitializeComponent();

            new ToolTip().SetToolTip(LabelCoef, "Сумма всех коэфициентов не должна превышать единицу");

            ButtonAddCompetitiveness.Click += ButtonAddEntry_Click;
            ButtonEvaluationCompetitiveness.Click += ButtonAssessmentCompetitiveness_Click;
            button2.Click += ButtonChange_Click;
            button4.Click += ButtonChange_Click;
            button3.Click += ButtonDelete_Click;
            button5.Click += ButtonDelete_Click;
        }

        private void ButtonAssessmentCompetitiveness_Click(object sender, EventArgs e)
        {
            var table = TableCompetitiveness;
            var rowCount = table.RowCount;

            float[] coefs = new float[rowCount];
            int[] projectEvaluation = new int[rowCount];
            int[] analogueEvaluation = new int[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                var currentCoefLabel = table.GetControlFromPosition(1, i) as Label;
                var currentCoef = float.Parse(currentCoefLabel.Text);
                var currentProjectEvaluationLabel = table.GetControlFromPosition(2, i) as Label;
                var currentProjectEvaluation = int.Parse(currentProjectEvaluationLabel.Text);
                var currentAnalogEvaluationLabel = table.GetControlFromPosition(3, i) as Label;
                var currentAnalogEvaluation = int.Parse(currentAnalogEvaluationLabel.Text);

                coefs[i] = currentCoef;
                projectEvaluation[i] = currentProjectEvaluation;
                analogueEvaluation[i] = currentAnalogEvaluation;
            }

            CompetitivenessParams competitivenessParams = new CompetitivenessParams()
            {
                ArraySize = rowCount,
                Coefs = coefs,
                ProjectEvaluations = projectEvaluation,
                AnalogueEvaluations = analogueEvaluation
            };

            OnEvaluation?.Invoke(sender, competitivenessParams);
        }

        private void ButtonAddEntry_Click(object sender, EventArgs e)
        {
            FormCompetitiveness addForm = new FormCompetitiveness();
            addForm.AddCompetitiveness += AddRow;
            addForm.ShowDialog();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var table = currentButton.Parent as TableLayoutPanel;
            int column = table.GetPositionFromControl(currentButton).Column;
            int currentRow = table.GetPositionFromControl(currentButton).Row;

            // Удаление элементов управления из строки
            var indexLabel =                table.GetControlFromPosition(0, currentRow) as Label;
            var coefLabel =                 table.GetControlFromPosition(1, currentRow) as Label;
            var projectEvaluationLabel =    table.GetControlFromPosition(2, currentRow) as Label;
            var analogEvaluationLabel =     table.GetControlFromPosition(3, currentRow) as Label;
            var changeButton =              table.GetControlFromPosition(4, currentRow) as Button;
            var deleteButton =              table.GetControlFromPosition(5, currentRow) as Button;

            table.Controls.Remove(indexLabel);
            table.Controls.Remove(coefLabel);
            table.Controls.Remove(projectEvaluationLabel);
            table.Controls.Remove(analogEvaluationLabel);
            table.Controls.Remove(changeButton);
            table.Controls.Remove(deleteButton);

            // Перемещение всех элементов управления ниже текущей записи на одну позицию выше
            var rowCount = table.RowCount;
            for (int i = currentRow; i < rowCount - 1; i++)
            {
                // Взятие элементов управления следующей строки
                var nextIndexLabel = table.GetControlFromPosition(0, i + 1) as Label;
                var nextCoefLabel = table.GetControlFromPosition(1, i + 1) as Label;
                var nextProjectEvaluationLabel = table.GetControlFromPosition(2, i + 1) as Label;
                var nextAnalogEvaluationLabel = table.GetControlFromPosition(3, i + 1) as Label;
                var nextChangeButton = table.GetControlFromPosition(4, i + 1) as Button;
                var nextDeleteButton = table.GetControlFromPosition(5, i + 1) as Button;

                // Перенос их на строку выше
                TableCompetitiveness.Controls.Add(nextIndexLabel, 0, i);
                TableCompetitiveness.Controls.Add(nextCoefLabel, 1, i);
                TableCompetitiveness.Controls.Add(nextProjectEvaluationLabel, 2, i);
                TableCompetitiveness.Controls.Add(nextAnalogEvaluationLabel, 3, i);
                TableCompetitiveness.Controls.Add(nextChangeButton, 4, i);
                TableCompetitiveness.Controls.Add(nextDeleteButton, 5, i);

                // Удаление их из своей ячейки
                table.Controls.Remove(indexLabel);
                table.Controls.Remove(coefLabel);
                table.Controls.Remove(projectEvaluationLabel);
                table.Controls.Remove(analogEvaluationLabel);
                table.Controls.Remove(changeButton);
                table.Controls.Remove(deleteButton);
            }

            TableCompetitiveness.RowCount--;
            TableCompetitiveness.Height -= _rowHeight;

            CheckCoef();
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

        private void AddRow(object sender, CompetitivenessEntry competitiveness)
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

            CheckCoef();
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

            CheckCoef();
        }

        private void CheckCoef()
        {
            var table = TableCompetitiveness;
            var rowCount = table.RowCount;

            float sumCoefs = 0f;
            for (int i = 0; i < rowCount; i++)
            {
                var currentCoefLabel = table.GetControlFromPosition(1, i) as Label;
                var currentCoef = float.Parse(currentCoefLabel.Text);
                sumCoefs += currentCoef;
            }

            if (Math.Abs(sumCoefs - 1) < 0.001)
            {
                LabelCoefCheck.BackColor = Color.FromArgb(192, 255, 192);
                LabelCoefCheck.ForeColor = Color.FromArgb(0, 64, 0);
                LabelCoefCheck.Text = "Всё хорошо!";
                ButtonEvaluationCompetitiveness.Enabled = true;
            }
            else if (sumCoefs > 1)
            {
                LabelCoefCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefCheck.ForeColor = Color.Maroon;
                LabelCoefCheck.Text = "Сумма коэфициентов больше одного!";
                ButtonEvaluationCompetitiveness.Enabled = false;
            }
            else if (sumCoefs < 1)
            {
                LabelCoefCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefCheck.ForeColor = Color.Maroon;
                LabelCoefCheck.Text = "Сумма коэфициентов меньше одного!";
                ButtonEvaluationCompetitiveness.Enabled = false;
            }
        }

        public void ShowResult(EvaluationResult result)
        {
            LabelResult.Text = result.ResultMessage +
                "\nПотому что КТС первого программного продукта по отношению ко второму = " + Math.Round(result.Teс, 2);
        }
    }
}