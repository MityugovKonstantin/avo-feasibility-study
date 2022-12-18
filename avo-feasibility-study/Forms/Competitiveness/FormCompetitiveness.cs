using avo_feasibility_study.Models;
using System;
using System.Windows.Forms;

namespace avo_feasibility_study.Forms.Competitiveness
{
    public partial class FormCompetitiveness : Form
    {

        public event EventHandler<CompetitivenessEntry> AddCompetitiveness;
        public event EventHandler<ChangeCompetitivenessEntry> ChangeCompetitiveness;

        private int _row;

        public FormCompetitiveness()
        {
            InitializeComponent();

            ButtonPost.Text = "Добавить";
            this.Text = "Добавление записи";

            ButtonPost.Click += AddButton_Click;
        }

        public FormCompetitiveness(ChangeCompetitivenessEntry competitiveness)
        {
            InitializeComponent();

            ButtonPost.Text = "Изменить";
            this.Text = "Изменение записи";
            TextQualityScore.Text = competitiveness.QualityScore;
            TextCoef.Text = competitiveness.Coef.ToString();
            TextProject.Text = competitiveness.ProjectEvaluation.ToString();
            TextAnalog.Text = competitiveness.AnalogEvaluation.ToString();
            _row = competitiveness.Row;

            ButtonPost.Click += ChangeButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                var competitiveness = CollectParams();

                AddCompetitiveness?.Invoke(sender, competitiveness);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var competitiveness = CollectParams();

                ChangeCompetitivenessEntry changeCompetitiveness = new ChangeCompetitivenessEntry()
                {
                    QualityScore = competitiveness.QualityScore,
                    Coef = competitiveness.Coef,
                    ProjectEvaluation = competitiveness.ProjectEvaluation,
                    AnalogEvaluation = competitiveness.AnalogEvaluation,
                    Row = _row
                };

                ChangeCompetitiveness?.Invoke(sender, changeCompetitiveness);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private CompetitivenessEntry CollectParams()
        {
            var quolityScore = TextQualityScore.Text;
            if (String.IsNullOrEmpty(quolityScore))
                throw new ArgumentException("Неверно введено название показателя качества. Параметр пуст!");

            bool isCoefParseSuccess = float.TryParse(TextCoef.Text, out float coef);
            if (!isCoefParseSuccess)
                throw new ArgumentException("Коэффициент весомости был введён неверно!");
            if (coef < 0 || coef > 1)
                throw new ArgumentOutOfRangeException(
                    "Коэффициент весомости",
                    "Значение введено неверно!\n" +
                    "Значение должно находится в диапазоне от 0 до 1!"
                );

            bool isProjectEvaluationParseSuccess = int.TryParse(TextProject.Text, out int projectEvaluation);
            if (!isProjectEvaluationParseSuccess)
                throw new ArgumentException("Оценка проекта была введена неверно!");
            if (projectEvaluation <= 0 || projectEvaluation > 10)
                throw new ArgumentOutOfRangeException(
                    "Оценка проекта",
                    "Значение введено неверно!\n" +
                    "Значение должно находится в диапазоне от 1 до 10!"
                );

            bool isAnalogEvaluationParseSuccess = int.TryParse(TextAnalog.Text, out int analogEvaluation);
            if (!isAnalogEvaluationParseSuccess)
                throw new ArgumentException("Оценка аналога была введена неверно!");
            if (analogEvaluation <= 0 || analogEvaluation > 10)
                throw new ArgumentOutOfRangeException(
                    "Оценка аналога",
                    "Значение введено неверно!\n" +
                    "Значение должно находится в диапазоне от 1 до 10!"
                );

            CompetitivenessEntry competitiveness = new CompetitivenessEntry()
            {
                QualityScore = TextQualityScore.Text,
                Coef = float.Parse(TextCoef.Text),
                ProjectEvaluation = int.Parse(TextProject.Text),
                AnalogEvaluation = int.Parse(TextAnalog.Text)
            };

            return competitiveness;
        }
    }
}
