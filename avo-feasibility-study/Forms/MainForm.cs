using avo_feasibility_study.Interfaces;
using avo_feasibility_study.BL.Models;
using System;
using System.Windows.Forms;
using avo_feasibility_study.BL.Models.Results;
using avo_feasibility_study.Forms.Competitiveness;
using avo_feasibility_study.Forms.PlanerTable;
using avo_feasibility_study.Forms.ProjectDevelopmentCostCalculation;
using System.Drawing;
using Microsoft.SqlServer.Server;

namespace avo_feasibility_study
{
    public partial class MainForm : Form, Iui
    {

        public event EventHandler<CompetitivenessParams> OnEvaluation;

        public MainForm()
        {
            InitializeComponent();

            #region First form
            new ToolTip().SetToolTip(LabelCoef, "Сумма всех коэфициентов не должна превышать единицу");
            DynamicCompetitivenessTable competitivenessTable = new DynamicCompetitivenessTable(
                    TableCompetitiveness,
                    LabelCoefCheck,
                    ButtonEvaluationCompetitiveness,
                    ButtonAddCompetitiveness
                );
            ButtonEvaluationCompetitiveness.Click += ButtonEvaluationCompetitiveness_Click;
            #endregion

            #region Second form
            TableLayoutPanel[] tables = new TableLayoutPanel[] {
                TablePreparation,
                TableDesign,
                ProgrammingAndTestingTable,
                DocumentationTable
            };
            PlannerTableService tableService = new PlannerTableService(tables);
            tableService.AddEvents();

            var firstDate = TablePreparation.GetControlFromPosition(1, 0) as DateTimePicker;
            var secondDate = TableDesign.GetControlFromPosition(1, 1) as DateTimePicker;
            firstDate.ValueChanged += tableService.ConnectFirstDate_ValueChange;
            firstDate.Value = new DateTime(2013, 1, 21);
            ButtonHoursCalculate.Click += ButtonHoursCalculate_Click;
            #endregion

            BasicSalary tableBasicSalary = new BasicSalary(
                LabelBasicSalary,
                ButtonBasicSalaryCalculate,
                TableBasicSalary,
                NumericBasicSalaryDays
            );
            LabelProgrammerHours.TextChanged += LabelProgrammerHours_TextChanged;
            LabelSupervisorHours.TextChanged += LabelSupervisorHours_TextChanged;

            DynamicMaterialCostsTable materialCostsTable = new DynamicMaterialCostsTable(
                TableMaterial,
                ButtonAddMaterial,
                ButtonMaterialCalculate,
                LabelMaterialCostsResult
                );
            ButtonDevelopmentCost.Click += ButtonDevelopmentCost_Click;

            TextDistrictCoefficient.TextChanged += CoefsCheck;
            TextOverheadRatio.TextChanged += CoefsCheck;

        }

        // TODO
        private void CoefsCheck(object sender, EventArgs e)
        {
            float districtCoefficient;
            try
            {
                districtCoefficient = float.Parse(TextDistrictCoefficient.Text);
                if (districtCoefficient < 0 || districtCoefficient > 1)
                    throw new ArgumentOutOfRangeException();
                LabelCoefsCheck.BackColor = Color.FromArgb(192, 255, 192);
            }
            catch (ArgumentNullException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент не может быть равен нулю!";
            }
            catch (FormatException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент указан неверно!";
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент должен находиться в диапазоне [0, 1]!";
            }

            float overheadRatio;
            try
            {
                overheadRatio = float.Parse(TextOverheadRatio.Text);
                if (overheadRatio < 0 || overheadRatio > 1)
                    throw new ArgumentOutOfRangeException();
                LabelCoefsCheck.BackColor = Color.FromArgb(192, 255, 192);
            }
            catch (ArgumentNullException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент не может быть равен нулю!";
            }
            catch (FormatException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент указан неверно!";
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент должен находиться в диапазоне [0, 1]!";
            }
        }

        private void ButtonDevelopmentCost_Click(object sender, EventArgs e)
        {
            var basicSalary = TableDevelopmentСosts.GetControlFromPosition(1, 1) as Label;
            var additionSalary = TableDevelopmentСosts.GetControlFromPosition(1, 2) as Label;
            var socialNeeds = TableDevelopmentСosts.GetControlFromPosition(1, 3) as Label;
            var materialCost = TableDevelopmentСosts.GetControlFromPosition(1, 4) as Label;
            var machineCost = TableDevelopmentСosts.GetControlFromPosition(1, 5) as Label;
            var organizationOverhead = TableDevelopmentСosts.GetControlFromPosition(1, 6) as Label;

            if (!string.IsNullOrEmpty(LabelBasicSalary.Text))
                basicSalary.Text = LabelBasicSalary.Text;
            else
                throw new Exception("Необходимо расчитать основную заработную плату работников!");

            if (!string.IsNullOrEmpty(LabelMaterialCostsResult.Text))
                materialCost.Text = LabelMaterialCostsResult.Text;
            else
                throw new Exception("Необходимо расчитать затраты на материалы!");

            float programmerTime;
            if (!string.IsNullOrEmpty(LabelProgrammerOZP.Text))
                programmerTime = float.Parse(LabelProgrammerOZP.Text);
            else
                throw new Exception("Необходимо расчитать время работы программиста!");

            bool isMachineCostParse = float.TryParse(TextMachineTimeCost.Text, out var machineCostValue);
            if (!isMachineCostParse)
                throw new ArgumentException("Неверно задана стоимость одного машинного часа!");
            if (machineCostValue <= 1)
                throw new ArgumentException("Cтоимость одного машинного часа болжна быть больше или равна 1!");

            bool isMultFactorParse = float.TryParse(TextMultiprogrammingFactor.Text, out var multFactor);
            if (!isMultFactorParse)
                throw new ArgumentException("Неверно задан коэффициент мультипрограммности!");
            if (multFactor < 0 || multFactor > 1)
                throw new ArgumentException("Коэффициент мультипрограммности должен лежать в диапазоне: [0, 1]");

            machineCost.Text = Math.Round(programmerTime * 4 * machineCostValue * multFactor, 2).ToString();


        }

        private void LabelProgrammerHours_TextChanged(object sender, EventArgs e)
        {
            var programmerHours = sender as Label;
            LabelProgrammerDevelopmentTime.Text = programmerHours.Text;
        }

        private void LabelSupervisorHours_TextChanged(object sender, EventArgs e)
        {
            var sopervisorHours = sender as Label;
            LabelSupervisorDevelopmentTime.Text = sopervisorHours.Text;
        }

        private void ButtonHoursCalculate_Click(object sender, EventArgs e)
        {
            int supervisorHours = 0;
            int programmerHours = 0;

            TableLayoutPanel[] tables = new TableLayoutPanel[]
            {
                TablePreparation,
                TableDesign,
                ProgrammingAndTestingTable,
                DocumentationTable
            };

            for (int row = 0; row < TablePreparation.RowCount; row += 2)
            {
                supervisorHours += (int)(TablePreparation.GetControlFromPosition(0, row) as NumericUpDown).Value;
                programmerHours += (int)(TablePreparation.GetControlFromPosition(0, row + 1) as NumericUpDown).Value;
            }
            for (int row = 0; row < TableDesign.RowCount; row += 2)
            {
                supervisorHours += (int)(TableDesign.GetControlFromPosition(0, row) as NumericUpDown).Value;
                programmerHours += (int)(TableDesign.GetControlFromPosition(0, row + 1) as NumericUpDown).Value;
            }
            for (int row = 0; row < ProgrammingAndTestingTable.RowCount; row += 2)
            {
                supervisorHours += (int)(ProgrammingAndTestingTable.GetControlFromPosition(0, row) as NumericUpDown).Value;
                programmerHours += (int)(ProgrammingAndTestingTable.GetControlFromPosition(0, row + 1) as NumericUpDown).Value;
            }
            for (int row = 0; row < DocumentationTable.RowCount; row += 2)
            {
                supervisorHours += (int)(DocumentationTable.GetControlFromPosition(0, row) as NumericUpDown).Value;
                programmerHours += (int)(DocumentationTable.GetControlFromPosition(0, row + 1) as NumericUpDown).Value;
            }

            ShowSecondResult(supervisorHours, programmerHours);
        }

        private void ButtonEvaluationCompetitiveness_Click(object sender, EventArgs e)
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

        public void ShowFirstResult(EvaluationResult result)
        {
            LabelResult.Text = result.ResultMessage +
                "\nКТС первого программного продукта по отношению ко второму = " + Math.Round(result.Teс, 2) + ".";
        }

        public void ShowSecondResult(int sh, int ph)
        {
            LabelSupervisorHours.Text = sh.ToString();
            LabelProgrammerHours.Text = ph.ToString();
        }
    }
}