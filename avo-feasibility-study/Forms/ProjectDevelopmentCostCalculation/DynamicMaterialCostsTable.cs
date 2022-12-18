using avo_feasibility_study.Forms.Competitiveness;
using avo_feasibility_study.Models;
using System.Drawing;
using System;
using System.Windows.Forms;
using System.Net.Http.Headers;

namespace avo_feasibility_study.Forms.ProjectDevelopmentCostCalculation
{
    public class DynamicMaterialCostsTable
    {
        private const int _rowHeight = 27;
        private TableLayoutPanel _table;
        private Button _addButton;
        private Button _calculateButton;
        private Label _resultLabel;
        private RowStyle _rowStyle = new RowStyle()
        {
            Height = _rowHeight,
            SizeType = SizeType.Absolute
        };

        public DynamicMaterialCostsTable(TableLayoutPanel table, Button addButton, Button calculateButton, Label resultLabel)
        {
            _table = table;
            _addButton = addButton;
            _calculateButton = calculateButton;
            _resultLabel = resultLabel;

            _addButton.Click += _addButton_Click;
            _calculateButton.Click += _calculateButton_Click;

            var changeButton = table.GetControlFromPosition(5, 0) as Button;
            var deleteButton = table.GetControlFromPosition(6, 0) as Button;

            changeButton.Click += ButtonChange_Click;
            deleteButton.Click += ButtonDelete_Click;
        }

        private void _calculateButton_Click(object sender, EventArgs e)
        {
            float sum = 0f;
            for (int row = 0; row < _table.RowCount; row++)
            {
                sum += float.Parse((_table.GetControlFromPosition(4, row) as Label).Text);
            }
            _resultLabel.Text = sum.ToString();
        }

        private void _addButton_Click(object sender, EventArgs e)
        {
            FormMaterialEntry addForm = new FormMaterialEntry();
            addForm.AddMaterial += AddEntry;
            addForm.ShowDialog();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var table = currentButton.Parent as TableLayoutPanel;
            int currentRow = table.GetPositionFromControl(currentButton).Row;

            // Удаление элементов управления из строки
            var name = table.GetControlFromPosition(0, currentRow) as Label;
            var unit = table.GetControlFromPosition(1, currentRow) as Label;
            var count = table.GetControlFromPosition(2, currentRow) as Label;
            var cost = table.GetControlFromPosition(3, currentRow) as Label;
            var sum = table.GetControlFromPosition(4, currentRow) as Label;
            var changeButton = table.GetControlFromPosition(5, currentRow) as Button;
            var deleteButton = table.GetControlFromPosition(6, currentRow) as Button;

            table.Controls.Remove(name);
            table.Controls.Remove(unit);
            table.Controls.Remove(count);
            table.Controls.Remove(cost);
            table.Controls.Remove(sum);
            table.Controls.Remove(changeButton);
            table.Controls.Remove(deleteButton);

            // Перемещение всех элементов управления ниже текущей записи на одну позицию выше
            var rowCount = table.RowCount;
            for (int i = currentRow; i < rowCount - 1; i++)
            {
                // Взятие элементов управления следующей строки
                var nextName = table.GetControlFromPosition(0, i + 1) as Label;
                var nextUnit = table.GetControlFromPosition(1, i + 1) as Label;
                var nextCount = table.GetControlFromPosition(2, i + 1) as Label;
                var nextCost = table.GetControlFromPosition(3, i + 1) as Label;
                var nextSum = table.GetControlFromPosition(4, i + 1) as Label;
                var nextChangeButton = table.GetControlFromPosition(5, i + 1) as Button;
                var nextDeleteButton = table.GetControlFromPosition(6, i + 1) as Button;

                // Перенос их на строку выше
                table.Controls.Add(nextName, 0, i);
                table.Controls.Add(nextUnit, 1, i);
                table.Controls.Add(nextCount, 2, i);
                table.Controls.Add(nextCost, 3, i);
                table.Controls.Add(nextSum, 4, i);
                table.Controls.Add(nextChangeButton, 5, i);
                table.Controls.Add(nextDeleteButton, 6, i);

                // Удаление их из своей ячейки
                table.Controls.Remove(name);
                table.Controls.Remove(unit);
                table.Controls.Remove(count);
                table.Controls.Remove(cost);
                table.Controls.Remove(sum);
                table.Controls.Remove(changeButton);
                table.Controls.Remove(deleteButton);
            }

            table.RowCount--;
            table.Height -= _rowHeight + 2;
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var table = currentButton.Parent as TableLayoutPanel;
            int row = table.GetPositionFromControl(currentButton).Row;

            var name = table.GetControlFromPosition(0, row) as Label;
            var unit = table.GetControlFromPosition(1, row) as Label;
            var count = table.GetControlFromPosition(2, row) as Label;
            var cost = table.GetControlFromPosition(3, row) as Label;
            var sum = table.GetControlFromPosition(4, row) as Label;

            ChangeMaterialEntry entry = new ChangeMaterialEntry()
            {
                MaterialName = name.Text,
                Unit = unit.Text,
                Count = float.Parse(count.Text),
                Cost = float.Parse(cost.Text),
                Sum = float.Parse(sum.Text),
                Row = row
            };

            FormMaterialEntry form = new FormMaterialEntry(entry);
            form.ChangeMaterial += ChangeRow;
            form.ShowDialog();
        }

        public void ChangeRow(object sender, ChangeMaterialEntry entry)
        {
            var row = entry.Row;

            var name = _table.GetControlFromPosition(0, row) as Label;
            var unit = _table.GetControlFromPosition(1, row) as Label;
            var count = _table.GetControlFromPosition(2, row) as Label;
            var cost = _table.GetControlFromPosition(3, row) as Label;
            var sum = _table.GetControlFromPosition(4, row) as Label;

            name.Text = entry.MaterialName;
            unit.Text = entry.Unit;
            count.Text = entry.Count.ToString();
            cost.Text = entry.Cost.ToString();
            sum.Text = entry.Sum.ToString();
        }

        public void AddEntry(object sender, MaterialEntry entry)
        {
            _table.Height += _rowHeight + 2;
            _table.RowCount++;

            Label name = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = entry.MaterialName
            };
            Label unit = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = entry.Unit
            };
            Label count = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = entry.Count.ToString()
            };
            Label cost = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = entry.Cost.ToString()
            };
            Label sum = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = entry.Sum.ToString()
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
            _table.Controls.Add(name, 0, lastRowIndex);
            _table.Controls.Add(unit, 1, lastRowIndex);
            _table.Controls.Add(count, 2, lastRowIndex);
            _table.Controls.Add(cost, 3, lastRowIndex);
            _table.Controls.Add(sum, 4, lastRowIndex);
            _table.Controls.Add(buttonChange, 5, lastRowIndex);
            _table.Controls.Add(buttonDelete, 6, lastRowIndex);

            var rowStyles = _table.RowStyles;
            foreach (RowStyle rowStyle in rowStyles)
            {
                rowStyle.Height = _rowStyle.Height;
                rowStyle.SizeType = _rowStyle.SizeType;
            }
        }
    }
}
