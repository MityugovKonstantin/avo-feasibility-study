using avo_feasibility_study.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace avo_feasibility_study.Forms.Competitiveness
{
    public class DynamicCompetitivenessTable
    {
        private const int _rowHeight = 27;
        private TableLayoutPanel _table;
        private Label _checkLabel;
        private Button _evaluationButton;
        private Button _addButton;
        private RowStyle _rowStyle = new RowStyle()
        {
            Height = _rowHeight,
            SizeType = SizeType.Absolute
        };

        public DynamicCompetitivenessTable(
            TableLayoutPanel table,
            Label checkLabel,
            Button buttonEvaluation,
            Button addButton
            )
        {
            _checkLabel = checkLabel;
            _evaluationButton = buttonEvaluation;
            _addButton = addButton;
            _table = table;

            _addButton.Click += ButtonAddEntry_Click;

            var changeButton1 = table.GetControlFromPosition(4, 0) as Button;
            var changeButton2 = table.GetControlFromPosition(4, 1) as Button;
            var deleteButton1 = table.GetControlFromPosition(5, 0) as Button;
            var deleteButton2 = table.GetControlFromPosition(5, 1) as Button;

            changeButton1.Click += ButtonChange_Click;
            changeButton2.Click += ButtonChange_Click;
            deleteButton1.Click += ButtonDelete_Click;
            deleteButton2.Click += ButtonDelete_Click;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var table = currentButton.Parent as TableLayoutPanel;
            int currentRow = table.GetPositionFromControl(currentButton).Row;

            // Удаление элементов управления из строки
            var indexLabel = table.GetControlFromPosition(0, currentRow) as Label;
            var coefLabel = table.GetControlFromPosition(1, currentRow) as Label;
            var projectEvaluationLabel = table.GetControlFromPosition(2, currentRow) as Label;
            var analogEvaluationLabel = table.GetControlFromPosition(3, currentRow) as Label;
            var changeButton = table.GetControlFromPosition(4, currentRow) as Button;
            var deleteButton = table.GetControlFromPosition(5, currentRow) as Button;

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
                table.Controls.Add(nextIndexLabel, 0, i);
                table.Controls.Add(nextCoefLabel, 1, i);
                table.Controls.Add(nextProjectEvaluationLabel, 2, i);
                table.Controls.Add(nextAnalogEvaluationLabel, 3, i);
                table.Controls.Add(nextChangeButton, 4, i);
                table.Controls.Add(nextDeleteButton, 5, i);

                // Удаление их из своей ячейки
                table.Controls.Remove(indexLabel);
                table.Controls.Remove(coefLabel);
                table.Controls.Remove(projectEvaluationLabel);
                table.Controls.Remove(analogEvaluationLabel);
                table.Controls.Remove(changeButton);
                table.Controls.Remove(deleteButton);
            }

            table.RowCount--;
            table.Height -= _rowHeight + 2;

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

            ChangeCompetitivenessEntry competitiveness = new ChangeCompetitivenessEntry()
            {
                QualityScore = qualityScore,
                Coef = coef,
                ProjectEvaluation = projectEvaluation,
                AnalogEvaluation = analogEvaluation,
                Row = row
            };

            FormCompetitiveness form = new FormCompetitiveness(competitiveness);
            form.ChangeCompetitiveness += ChangeRow;
            form.ShowDialog();
        }

        private void ButtonAddEntry_Click(object sender, EventArgs e)
        {
            FormCompetitiveness addForm = new FormCompetitiveness();
            addForm.AddCompetitiveness += AddEntry;
            addForm.ShowDialog();
        }

        public void ChangeRow(object sender, ChangeCompetitivenessEntry competitiveness)
        {
            var row = competitiveness.Row;

            var indexLabel = _table.GetControlFromPosition(0, row) as Label;
            var coefLabel = _table.GetControlFromPosition(1, row) as Label;
            var projectEvaluationLabel = _table.GetControlFromPosition(2, row) as Label;
            var analogEvaluationLabel = _table.GetControlFromPosition(3, row) as Label;

            indexLabel.Text = competitiveness.QualityScore;
            coefLabel.Text = competitiveness.Coef.ToString();
            projectEvaluationLabel.Text = competitiveness.ProjectEvaluation.ToString();
            analogEvaluationLabel.Text = competitiveness.AnalogEvaluation.ToString();

            CheckCoef();
        }

        public void CheckCoef()
        {
            var rowCount = _table.RowCount;

            float sumCoefs = 0f;
            for (int i = 0; i < rowCount; i++)
            {
                var currentCoefLabel = _table.GetControlFromPosition(1, i) as Label;
                var currentCoef = float.Parse(currentCoefLabel.Text);
                sumCoefs += currentCoef;
            }

            if (Math.Abs(sumCoefs - 1) < 0.001)
            {
                _checkLabel.BackColor = Color.FromArgb(192, 255, 192);
                _checkLabel.ForeColor = Color.FromArgb(0, 64, 0);
                _checkLabel.Text = "Всё хорошо!";
                _evaluationButton.Enabled = true;
            }
            else
            {
                _checkLabel.BackColor = Color.FromArgb(255, 192, 192);
                _checkLabel.ForeColor = Color.Maroon;
                _checkLabel.Text = "Сумма коэфициентов весомости = " + sumCoefs + ", а должна быть = 1.";
                _evaluationButton.Enabled = false;
            }
        }

        public void AddEntry(object sender, CompetitivenessEntry competitiveness)
        {
            _table.Height += _rowHeight + 2;
            _table.RowCount++;

            Label index = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = competitiveness.QualityScore
            };
            Label coef = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = competitiveness.Coef.ToString()
            };
            Label project = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = competitiveness.ProjectEvaluation.ToString()
            };
            Label analog = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = competitiveness.AnalogEvaluation.ToString()
            };
            Button buttonChange = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Изменить"
            };
            buttonChange.Click += ButtonChange_Click;
            Button buttonDelete = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Удалить"
            };
            buttonDelete.Click += ButtonDelete_Click;

            var lastRowIndex = _table.RowCount - 1;
            _table.Controls.Add(index, 0, lastRowIndex);
            _table.Controls.Add(coef, 1, lastRowIndex);
            _table.Controls.Add(project, 2, lastRowIndex);
            _table.Controls.Add(analog, 3, lastRowIndex);
            _table.Controls.Add(buttonChange, 4, lastRowIndex);
            _table.Controls.Add(buttonDelete, 5, lastRowIndex);

            var rowStyles = _table.RowStyles;
            foreach (RowStyle rowStyle in rowStyles)
            {
                rowStyle.Height = _rowStyle.Height;
                rowStyle.SizeType = _rowStyle.SizeType;
            }

            CheckCoef();
        }
    }
}
