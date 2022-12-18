using avo_feasibility_study.Interfaces;
using avo_feasibility_study.BL.Models;
using System;
using System.Windows.Forms;
using avo_feasibility_study.BL.Models.Results;
using avo_feasibility_study.Forms.Competitiveness;
using avo_feasibility_study.Forms.PlanerTable;
using avo_feasibility_study.Forms.ProjectDevelopmentCostCalculation;
using System.Drawing;

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

            #region Development cost calculation
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

            ButtonProjectImplementationCostsCalculate.Click += ButtonProjectImplementationCostsCalculate_Click;
            ButtonImplementingAnalogueCostCalculate.Click += ButtonImplementingAnalogueCostCalculate_Click;
            #endregion

            ButtonOurOperatingCostsCalculate.Click += ButtonOurOperatingCostsCalculate_Click;
            ButtonAnalogueOperatingCostsCalculate.Click += ButtonAnalogueOperatingCostsCalculate_Click;
            ButtonAnnualOperatingCostsCalculate.Click += ButtonAnnualOperatingCostsCalculate_Click;
        }

        private void ButtonAnnualOperatingCostsCalculate_Click(object sender, EventArgs e)
        {
            var projectCostLabel = TableAnnualOperatingCosts.GetControlFromPosition(1, 1) as Label;
            var analogueCostLabel = TableAnnualOperatingCosts.GetControlFromPosition(2, 1) as Label;
            var projectAmortizationLabel = TableAnnualOperatingCosts.GetControlFromPosition(1, 2) as Label;
            var analogueAmortizationLabel = TableAnnualOperatingCosts.GetControlFromPosition(2, 2) as Label;
            var projectEnergyLabel = TableAnnualOperatingCosts.GetControlFromPosition(1, 3) as Label;
            var analogueEnergyLabel = TableAnnualOperatingCosts.GetControlFromPosition(2, 3) as Label;
            var projectRepairLabel = TableAnnualOperatingCosts.GetControlFromPosition(1, 4) as Label;
            var analogueRepairLabel = TableAnnualOperatingCosts.GetControlFromPosition(2, 4) as Label;
            var projectMaterialLabel = TableAnnualOperatingCosts.GetControlFromPosition(1, 5) as Label;
            var analogueMaterialLabel = TableAnnualOperatingCosts.GetControlFromPosition(2, 5) as Label;
            var projectOverheadsLabel = TableAnnualOperatingCosts.GetControlFromPosition(1, 6) as Label;
            var analogueOverheadsLabel = TableAnnualOperatingCosts.GetControlFromPosition(2, 6) as Label;

            var workerTimeTextBoxA = TableAnalogueOperatingCosts.GetControlFromPosition(3, 1) as TextBox;
            var programmerTimeTextBoxA = TableAnalogueOperatingCosts.GetControlFromPosition(3, 2) as TextBox;
            var workerTimeTextBoxP = TableOurOperatingCosts.GetControlFromPosition(3, 1) as TextBox;
            var programmerTimeTextBoxP = TableOurOperatingCosts.GetControlFromPosition(3, 2) as TextBox;

            try
            {
                if (string.IsNullOrEmpty(LabelOurOperatingCostsResult.Text))
                    throw new Exception("Необходимо посчитать эксплуатационные затраты для нашего проекта!");
                else
                    projectCostLabel.Text = LabelOurOperatingCostsResult.Text;

                if (string.IsNullOrEmpty(LabelAnalogueOperatingCostsResult.Text))
                    throw new Exception("Необходимо посчитать эксплуатационные затраты для проекта аналога!");
                else
                    analogueCostLabel.Text = LabelAnalogueOperatingCostsResult.Text;

                var isEquipmentBookParse = float.TryParse(TextEquipmentBookValue2.Text, out var equipmentBookValue);
                if (!isEquipmentBookParse)
                    throw new ArgumentException("Балансовая стоимость оборудования указана неверно!");
                if (equipmentBookValue < 0)
                    throw new ArgumentException("Балансовая стоимость оборудования не может быть меньше 0!");

                var isAnnualRateParse = float.TryParse(TextAnnualDepreciationRate.Text, out var annualRate);
                if (!isAnnualRateParse)
                    throw new ArgumentException("Норма годовых амортизационных отчислений указана неверно!");
                if (annualRate < 0 || annualRate > 1)
                    throw new ArgumentException("Норма годовых амортизационных отчислений должна лежать в диапазоне [0, 1]!");

                var equipmentCount = (int)NumericEquipmentPiecesNumber.Value;

                var isAverageStandardParse = float.TryParse(TextAverageDailyLoadStandard.Text, out var averageStandart);
                if (!isAverageStandardParse)
                    throw new ArgumentException("Норматив среднесуточной нагрузки указан неверно");
                if (averageStandart < 0)
                    throw new ArgumentException("Норматив среднесуточной нагрузки не может быть меньше 0!");

                var isWorkerTimeParseA = float.TryParse(workerTimeTextBoxA.Text, out var workerTimeValueA);
                if (!isWorkerTimeParseA)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта указано неверно в проекте аналоге!");
                if (workerTimeValueA < 0)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта в проекте аналоге не может быть меньше 0.");

                var isProgrammerTimeParseA = float.TryParse(programmerTimeTextBoxA.Text, out var programmerTimeValueA);
                if (!isProgrammerTimeParseA)
                    throw new ArgumentException("Время программиста указано неверно в проекте аналоге!");
                if (programmerTimeValueA < 0)
                    throw new ArgumentException("Время программиста в проекте аналоге не может быть меньше 0.");

                var isWorkerTimeParseP = float.TryParse(workerTimeTextBoxP.Text, out var workerTimeValueP);
                if (!isWorkerTimeParseP)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта указано неверно в нашем проекте!");
                if (workerTimeValueP < 0)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта в нашем проекте не может быть меньше 0.");

                var isProgrammerTimeParseP = float.TryParse(programmerTimeTextBoxP.Text, out var programmerTimeValueP);
                if (!isProgrammerTimeParseP)
                    throw new ArgumentException("Время программиста указано неверно в нашем проекте!");
                if (programmerTimeValueP < 0)
                    throw new ArgumentException("Время программиста в нашем проекте не может быть меньше 0.");

                analogueAmortizationLabel.Text = Math.Round
                    (equipmentBookValue * annualRate * equipmentCount *
                    (workerTimeValueA + programmerTimeValueA) * averageStandart / (247 * averageStandart), 2).ToString();
                projectAmortizationLabel.Text = Math.Round
                    (equipmentBookValue * annualRate * equipmentCount *
                    (workerTimeValueP + programmerTimeValueP) * averageStandart / (247 * averageStandart), 2).ToString();

                var isElectricityTariffParse = float.TryParse(TextElectricityTariff.Text, out var electricityTariffValue);
                if (!isElectricityTariffParse)
                    throw new ArgumentException("Тариф на электроэнергию указан неверно!");
                if (electricityTariffValue < 0)
                    throw new ArgumentException("Тариф на электроэнергию не может быть меньше 0!");

                var isEquipmentPowerParse = float.TryParse(TextEquipmentPower.Text, out var equipmentPowerValue);
                if (!isEquipmentPowerParse)
                    throw new ArgumentException("Мощность оборудования указана неверно!");
                if (equipmentPowerValue < 0)
                    throw new ArgumentException("Мощность оборудования не может быть меньше 0!");

                projectEnergyLabel.Text = Math.Round(
                    electricityTariffValue * equipmentPowerValue * (workerTimeValueP + programmerTimeValueP) * averageStandart,
                    2
                    ).ToString();
                analogueEnergyLabel.Text = Math.Round(
                    electricityTariffValue * equipmentPowerValue * (workerTimeValueA + programmerTimeValueA) * averageStandart,
                    2
                    ).ToString();

                var isRepairCostStandardParse = float.TryParse(TextRepairCostStandard.Text, out var repairCostStandardValue);
                if (!isRepairCostStandardParse)
                    throw new ArgumentException("Норматив затрат на ремонт указан неверно!");
                if (repairCostStandardValue < 0)
                    throw new ArgumentException("Норматив затрат на ремонт не может быть меньше 0!");

                projectRepairLabel.Text = Math.Round(
                    repairCostStandardValue * equipmentBookValue * (workerTimeValueP + programmerTimeValueP) * averageStandart
                    / (247 * averageStandart), 2
                    ).ToString();
                analogueRepairLabel.Text = Math.Round(
                    repairCostStandardValue * equipmentBookValue * (workerTimeValueA + programmerTimeValueA) * averageStandart
                    / (247 * averageStandart), 2
                    ).ToString();

                var isMaterialCostStandardParse =
                    float.TryParse(TextMaterialCostStandard.Text, out var materialCostStandardValue);
                if (!isMaterialCostStandardParse)
                    throw new ArgumentException("Норматив затрат на материалы указан неверно!");
                if (materialCostStandardValue < 0)
                    throw new ArgumentException("норматив затрат на материалы не может быть меньше 0!");

                projectMaterialLabel.Text = Math.Round(equipmentBookValue * materialCostStandardValue, 2).ToString();
                analogueMaterialLabel.Text = Math.Round(equipmentBookValue * materialCostStandardValue, 2).ToString();

                projectOverheadsLabel.Text =
                    Math.Round((
                    float.Parse(projectCostLabel.Text) +
                    float.Parse(projectAmortizationLabel.Text) +
                    float.Parse(projectEnergyLabel.Text) +
                    float.Parse(projectRepairLabel.Text) +
                    float.Parse(projectMaterialLabel.Text)) * 0.2, 2).ToString();
                analogueOverheadsLabel.Text =
                    Math.Round((
                    float.Parse(analogueCostLabel.Text) +
                    float.Parse(analogueAmortizationLabel.Text) +
                    float.Parse(analogueEnergyLabel.Text) +
                    float.Parse(analogueRepairLabel.Text) +
                    float.Parse(analogueMaterialLabel.Text)) * 0.2, 2).ToString();

                float projectResult = 0f;
                for (int row = 1; row < TableAnnualOperatingCosts.RowCount; row++)
                    projectResult +=
                        float.Parse((TableAnnualOperatingCosts.GetControlFromPosition(1, row) as Label).Text);

                float analogueResult = 0f;
                for (int row = 1; row < TableAnnualOperatingCosts.RowCount; row++)
                    analogueResult +=
                        float.Parse((TableAnnualOperatingCosts.GetControlFromPosition(2, row) as Label).Text);

                LabelProjectAnnualOperatingCostsResult.Text = projectResult.ToString();
                LabelAnalogueAnnualOperatingCostsResult.Text = analogueResult.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonAnalogueOperatingCostsCalculate_Click(object sender, EventArgs e)
        {
            var workerSalaryTextBox =       TableAnalogueOperatingCosts.GetControlFromPosition(1, 1) as TextBox;
            var programmerSalaryTextBox =   TableAnalogueOperatingCosts.GetControlFromPosition(1, 2) as TextBox;
            var workerAverageLabel =        TableAnalogueOperatingCosts.GetControlFromPosition(2, 1) as Label;
            var programmerAverageLabel =    TableAnalogueOperatingCosts.GetControlFromPosition(2, 2) as Label;
            var workerTimeTextBox =         TableAnalogueOperatingCosts.GetControlFromPosition(3, 1) as TextBox;
            var programmerTimeTextBox =     TableAnalogueOperatingCosts.GetControlFromPosition(3, 2) as TextBox;
            var workerFundLabel =           TableAnalogueOperatingCosts.GetControlFromPosition(4, 1) as Label;
            var programmerFundLabel =       TableAnalogueOperatingCosts.GetControlFromPosition(4, 2) as Label;

            try
            {
                var days = (int)NumericWorkDays.Value;

                var isDistrictCoefParse = float.TryParse(TextDistrictCoefficient2.Text, out var districtCoef);
                if (!isDistrictCoefParse)
                    throw new ArgumentException("Районный коэффициент указан неверно!");
                if (districtCoef < 0 || districtCoef > 1)
                    throw new ArgumentException("Районный коэффициент должен лежать в диапазоне [0, 1]!");

                var isSocialCoefParse = float.TryParse(TextSocialCoef2.Text, out var socialCoef);
                if (!isSocialCoefParse)
                    throw new ArgumentException("Коэффициент отчислений на соц. нужды указан неверно!");
                if (socialCoef < 0 || socialCoef > 1)
                    throw new ArgumentException("Коэффициент отчислений на соц. нужды должен лежать в диапазоне [0, 1]!");

                var isWorkerSalaryParse = float.TryParse(workerSalaryTextBox.Text, out var workerSalaryValue);
                if (!isWorkerSalaryParse)
                    throw new ArgumentException("Должностной оклад сотрудника отдела-эксплуатанта указан неверно!");
                if (workerSalaryValue < 0)
                    throw new ArgumentException("Должностной оклад сотрудника отдела-эксплуатанта должен не может быть меньше 0.");

                var isProgrammerSalaryParse = float.TryParse(programmerSalaryTextBox.Text, out var programmerSalaryValue);
                if (!isProgrammerSalaryParse)
                    throw new ArgumentException("Должностной оклад программиста указан неверно!");
                if (programmerSalaryValue < 0)
                    throw new ArgumentException("Должностной оклад программиста должен не может быть меньше 0.");

                var workerAverage = workerSalaryValue / days;
                var programmerAverage = programmerSalaryValue / days;
                workerAverageLabel.Text = workerAverage.ToString();
                programmerAverageLabel.Text = programmerAverage.ToString();

                var isWorkerTimeParse = float.TryParse(workerTimeTextBox.Text, out var workerTimeValue);
                if (!isWorkerTimeParse)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта указано неверно!");
                if (workerTimeValue < 0)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта не может быть меньше 0.");

                var isProgrammerTimeParse = float.TryParse(programmerTimeTextBox.Text, out var programmerTimeValue);
                if (!isProgrammerTimeParse)
                    throw new ArgumentException("Время программиста указано неверно!");
                if (programmerTimeValue < 0)
                    throw new ArgumentException("Время программиста не может быть меньше 0.");

                var workerFund = workerAverage * workerTimeValue;
                var programmerFund = programmerAverage * programmerTimeValue;
                workerFundLabel.Text = workerFund.ToString();
                programmerFundLabel.Text = programmerFund.ToString();

                LabelAnalogueOperatingCostsResult.Text =
                    ((workerFund + programmerFund) * (1 + districtCoef) * (1 + socialCoef)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проект-аналог. " + ex.Message);
            }
        }

        private void ButtonOurOperatingCostsCalculate_Click(object sender, EventArgs e)
        {
            var workerSalaryTextBox = TableAnalogueOperatingCosts.GetControlFromPosition(1, 1) as TextBox;
            var programmerSalaryTextBox = TableOurOperatingCosts.GetControlFromPosition(1, 2) as TextBox;
            var workerAverageLabel = TableOurOperatingCosts.GetControlFromPosition(2, 1) as Label;
            var programmerAverageLabel = TableOurOperatingCosts.GetControlFromPosition(2, 2) as Label;
            var workerTimeTextBox = TableOurOperatingCosts.GetControlFromPosition(3, 1) as TextBox;
            var programmerTimeTextBox = TableOurOperatingCosts.GetControlFromPosition(3, 2) as TextBox;
            var workerFundLabel = TableOurOperatingCosts.GetControlFromPosition(4, 1) as Label;
            var programmerFundLabel = TableOurOperatingCosts.GetControlFromPosition(4, 2) as Label;

            try
            {
                var days = (int)NumericWorkDays.Value;

                var isDistrictCoefParse = float.TryParse(TextDistrictCoefficient2.Text, out var districtCoef);
                if (!isDistrictCoefParse)
                    throw new ArgumentException("Районный коэффициент указан неверно!");
                if (districtCoef < 0 || districtCoef > 1)
                    throw new ArgumentException("Районный коэффициент должен лежать в диапазоне [0, 1]!");

                var isSocialCoefParse = float.TryParse(TextSocialCoef2.Text, out var socialCoef);
                if (!isSocialCoefParse)
                    throw new ArgumentException("Коэффициент отчислений на соц. нужды указан неверно!");
                if (socialCoef < 0 || socialCoef > 1)
                    throw new ArgumentException("Коэффициент отчислений на соц. нужды должен лежать в диапазоне [0, 1]!");

                var isWorkerSalaryParse = float.TryParse(workerSalaryTextBox.Text, out var workerSalaryValue);
                if (!isWorkerSalaryParse)
                    throw new ArgumentException("Должностной оклад сотрудника отдела-эксплуатанта указан неверно!");
                if (workerSalaryValue < 0)
                    throw new ArgumentException("Должностной оклад сотрудника отдела-эксплуатанта должен не может быть меньше 0.");

                var isProgrammerSalaryParse = float.TryParse(programmerSalaryTextBox.Text, out var programmerSalaryValue);
                if (!isProgrammerSalaryParse)
                    throw new ArgumentException("Должностной оклад программиста указан неверно!");
                if (programmerSalaryValue < 0)
                    throw new ArgumentException("Должностной оклад программиста должен не может быть меньше 0.");

                var workerAverage = workerSalaryValue / days;
                var programmerAverage = programmerSalaryValue / days;
                workerAverageLabel.Text = workerAverage.ToString();
                programmerAverageLabel.Text = programmerAverage.ToString();

                var isWorkerTimeParse = float.TryParse(workerTimeTextBox.Text, out var workerTimeValue);
                if (!isWorkerTimeParse)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта указано неверно!");
                if (workerTimeValue < 0)
                    throw new ArgumentException("Время сотрудника отдела-эксплуатанта не может быть меньше 0.");

                var isProgrammerTimeParse = float.TryParse(programmerTimeTextBox.Text, out var programmerTimeValue);
                if (!isProgrammerTimeParse)
                    throw new ArgumentException("Время программиста указано неверно!");
                if (programmerTimeValue < 0)
                    throw new ArgumentException("Время программиста не может быть меньше 0.");

                var workerFund = workerAverage * workerTimeValue;
                var programmerFund = programmerAverage * programmerTimeValue;
                workerFundLabel.Text = workerFund.ToString();
                programmerFundLabel.Text = programmerFund.ToString();

                LabelOurOperatingCostsResult.Text =
                    ((workerFund + programmerFund) * (1 + districtCoef) * (1 + socialCoef)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Наш проект. " + ex.Message);
            }
        }

        private void ButtonImplementingAnalogueCostCalculate_Click(object sender, EventArgs e)
        {
            var buyTextBox = TableImplementingAnalogueCost.GetControlFromPosition(0, 1) as TextBox;
            var downloadTextBox = TableImplementingAnalogueCost.GetControlFromPosition(1, 1) as TextBox;
            var envirovmentTextBox = TableImplementingAnalogueCost.GetControlFromPosition(2, 1) as TextBox;
            var studyTextBox = TableImplementingAnalogueCost.GetControlFromPosition(3, 1) as TextBox;

            try
            {
                var isBuyParse = float.TryParse(buyTextBox.Text, out var buyValue);
                if (!isBuyParse)
                    throw new ArgumentException("Затраты на приобретение программного продукта введены неверно!");
                if (buyValue < 0)
                    throw new ArgumentException("Затраты на приобретение программного продукта не могут быть меньше 0!");

                var isDownloadParse = float.TryParse(downloadTextBox.Text, out var downloadValue);
                if (!isDownloadParse)
                    throw new ArgumentException("Затраты по оплате услуг и установку и сопровождение продукта введены неверно!");
                if (downloadValue < 0)
                    throw new ArgumentException("Затраты по оплате услуг и установку и сопровождение продукта не могут быть меньше 0!");

                var isEnvirovmentParse = float.TryParse(envirovmentTextBox.Text, out var envirovmentValue);
                if (!isEnvirovmentParse)
                    throw new ArgumentException("Затраты на основное и вспомогательное оборудование введены неверно!");
                if (envirovmentValue < 0)
                    throw new ArgumentException("Затраты на основное и вспомогательное оборудование не могут быть меньше 0!");

                var isStudyParse = float.TryParse(studyTextBox.Text, out var studyValue);
                if (!isStudyParse)
                    throw new ArgumentException("Затраты на подготовку пользователя введены неверно!");
                if (studyValue < 0)
                    throw new ArgumentException("Затраты на подготовку пользователя не могут быть меньше 0!");

                LabelImplementingAnalogueCostResult.Text =
                    (buyValue + downloadValue + envirovmentValue + studyValue).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonProjectImplementationCostsCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var isbalanceCostParse = float.TryParse(TextModelBookValue.Text, out var balanceCost);
                if (!isbalanceCostParse)
                    throw new ArgumentException("Балансовая стоимость оборудования введена неверно!");
                if (balanceCost < 0)
                    throw new ArgumentException("Балансовая стоимость оборудования не может быть меньше 0!");

                var count = (int)NumericEquipmentPieces.Value;

                var isCompetitivenessInfoParse = float.TryParse(TextCompetitivenessInfo.Text, out var competitivenessInfo);
                if (!isCompetitivenessInfoParse)
                    throw new ArgumentException("Трудоемкость однократной обработки информации введена неверно!");
                if (competitivenessInfo < 0)
                    throw new ArgumentException("Трудоемкость однократной обработки информации не может быть меньше 0!");

                var isTestTaskParse = float.TryParse(TextTestTask.Text, out var testTask);
                if (!isTestTaskParse)
                    throw new ArgumentException("Частота решения тестовой задачи введена неверно!");
                if (testTask < 0)
                    throw new ArgumentException("Частота решения тестовой задачи не может быть меньше 0!");

                var isWorkDayHoursParse = float.TryParse(TextWorkDayHours.Text, out var workDayHours);
                if (!isWorkDayHoursParse)
                    throw new ArgumentException("Продолжительность рабочего дня введена неверно!");
                if (workDayHours < 0)
                    throw new ArgumentException("Продолжительность рабочего дня не может быть меньше 0!");

                var isWorkDayInYearParse = float.TryParse(textBox2.Text, out var workDayInYear);
                if (!isWorkDayInYearParse)
                    throw new ArgumentException("Количество рабочих дней в году введено неверно!");
                if (workDayInYear < 0)
                    throw new ArgumentException("Количество рабочих дней в году не может быть меньше 0!");

                LabelProjectImplementationCostsResult.Text =
                    ((balanceCost * count * competitivenessInfo * testTask) / (workDayInYear * workDayHours)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CoefsCheck(object sender, EventArgs e)
        {
            float districtCoefficient = -1;
            try
            {
                districtCoefficient = float.Parse(TextDistrictCoefficient.Text);
                if (districtCoefficient < 0 || districtCoefficient > 1)
                    throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentNullException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент не может быть равен нулю!";
                return;
            }
            catch (FormatException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент указан неверно!";
                return;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Районый коэффициент должен находиться в диапазоне [0, 1]!";
                return;
            }

            float overheadRatio = -1;
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
                LabelCoefsCheck.Text = "Коэффициент учёта накладных расходов не может быть равен нулю!";
                return;
            }
            catch (FormatException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Коэффициент учёта накладных расходов указан неверно!";
                return;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text = "Коэффициент учёта накладных расходов должен находиться в диапазоне [0, 1]!";
                return;
            }

            var coefsSum = overheadRatio + districtCoefficient;

            if ((coefsSum- 1) < 0.001 || coefsSum > 1)
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(255, 192, 192);
                LabelCoefsCheck.Text =
                    "Сумма коэффициента учёта накладных расходов и районного коэффициента равны = "
                    + (districtCoefficient + overheadRatio) + ", а должно быть = 1.";
            }
            else
            {
                LabelCoefsCheck.BackColor = Color.FromArgb(192, 255, 192);
                LabelCoefsCheck.Text = "Всё хорошо!";
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
            var result = TableDevelopmentСosts.GetControlFromPosition(1, 7) as Label;

            try
            {
                if (!string.IsNullOrEmpty(LabelBasicSalary.Text))
                    basicSalary.Text = LabelBasicSalary.Text;
                else
                    throw new Exception("Необходимо расчитать основную заработную плату работников!");

                if (!string.IsNullOrEmpty(LabelMaterialCostsResult.Text))
                    materialCost.Text = LabelMaterialCostsResult.Text;
                else
                    throw new Exception("Необходимо расчитать затраты на материалы!");

                float programmerTime;
                if (!string.IsNullOrEmpty(LabelProgrammerDevelopmentTime.Text))
                    programmerTime = float.Parse(LabelProgrammerDevelopmentTime.Text);
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

                if (LabelCoefsCheck.Text == "Всё хорошо!")
                {
                    additionSalary.Text =
                        Math.Round(
                            float.Parse(TextDistrictCoefficient.Text) * float.Parse(basicSalary.Text),
                            2
                        ).ToString();
                    organizationOverhead.Text =
                        Math.Round(
                            float.Parse(TextOverheadRatio.Text) * float.Parse(basicSalary.Text),
                            2
                        ).ToString();
                }

                bool isSocialCoefParse = float.TryParse(TextSocialCoef.Text, out var socialCoefValue);
                if (!isSocialCoefParse)
                    throw new ArgumentException("Неверно задан коэффициент учёта отчислений на социальные нужды.");
                if (socialCoefValue < 0 || socialCoefValue > 1)
                    throw new ArgumentOutOfRangeException(
                        "Коэффициент учёта отчислений на социальные нужды",
                        "Коэффициент должен лежать в диапазоне [0, 1]!");
                socialNeeds.Text =
                    Math.Round((float.Parse(basicSalary.Text) + float.Parse(additionSalary.Text)) * socialCoefValue, 2).ToString();

                var resultSum = 0f;
                for (int row = 1; row < TableDevelopmentСosts.RowCount - 1; row++)
                {
                    resultSum += float.Parse((TableDevelopmentСosts.GetControlFromPosition(1, row) as Label).Text);
                }

                result.Text = resultSum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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