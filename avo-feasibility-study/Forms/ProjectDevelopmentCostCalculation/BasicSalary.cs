using System;
using System.Drawing;
using System.Windows.Forms;

namespace avo_feasibility_study.Forms.ProjectDevelopmentCostCalculation
{
    public class BasicSalary
    {
        private Label _result;
        private Button _calculateButton;
        private TableLayoutPanel _table;
        private NumericUpDown _days;

        public BasicSalary(
            Label result,
            Button calculateButton,
            TableLayoutPanel table,
            NumericUpDown days
            )
        {
            _result = result;
            _calculateButton = calculateButton;
            _table = table;
            _days = days;

            _calculateButton.Click += _calculateButton_Click;
        }

        private void _calculateButton_Click(object sender, EventArgs e)
        {
            var supervisorOfficialSalary = _table.GetControlFromPosition(1, 1) as TextBox;
            var supervisorDevelopmentTime = _table.GetControlFromPosition(3, 1) as Label;
            var programmerOfficialSalary = _table.GetControlFromPosition(1, 2) as TextBox;
            var programmerDevelopmentTime = _table.GetControlFromPosition(3, 2) as Label;

            try
            {
                bool isParseSupervisorOfficialSalary = int.TryParse(supervisorOfficialSalary.Text, out int supervisorSalary);
                if (!isParseSupervisorOfficialSalary)
                    throw new ArgumentException("Введено неверное значение должностного оклада руководителя.");

                bool isParseProgrammerOfficialSalary = int.TryParse(programmerOfficialSalary.Text, out int programmerSalary);
                if (!isParseProgrammerOfficialSalary)
                    throw new ArgumentException("Введено неверное значение должностного оклада программиста.");

                int supervisorHours;
                if (!string.IsNullOrEmpty(supervisorDevelopmentTime.Text))
                    supervisorHours = int.Parse(supervisorDevelopmentTime.Text);
                else
                    throw new ArgumentException("Сначала рассчитайте затраты времени на разработку руководителя во вкладке " +
                        "\"Планирование комплекса работ по разработке проекта и оценка\".");

                int programmerHours;
                if (!string.IsNullOrEmpty(supervisorDevelopmentTime.Text))
                    programmerHours = int.Parse(programmerDevelopmentTime.Text);
                else
                    throw new ArgumentException("Сначала рассчитайте затраты времени на разработку программиста во вкладке " +
                        "\"Планирование комплекса работ по разработке проекта и оценка\".");

                var supervisorAverageDaily = _table.GetControlFromPosition(2, 1) as Label;
                var programmerAverageDaily = _table.GetControlFromPosition(2, 2) as Label;
                var supervisorOZP = _table.GetControlFromPosition(4, 1) as Label;
                var programmerOZP = _table.GetControlFromPosition(4, 2) as Label;

                var days = (int)_days.Value;

                supervisorAverageDaily.Text = Math.Round((float)supervisorSalary / days, 2).ToString();
                programmerAverageDaily.Text = Math.Round((float)programmerSalary / days, 2).ToString();
                supervisorOZP.Text = (supervisorHours * (float)supervisorSalary / days).ToString();
                programmerOZP.Text = (programmerHours * (float)programmerSalary / days).ToString();

                _result.Text =
                    ((supervisorHours * (float)supervisorSalary / days) +
                    (programmerHours * (float)programmerSalary / days)).ToString();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
