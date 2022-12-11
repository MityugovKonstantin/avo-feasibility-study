using avo_feasibility_study.Models;
using System;
using System.Windows.Forms;

namespace avo_feasibility_study.Forms.AddForms
{
    public partial class FormCompetitiveness : Form
    {

        public event EventHandler<Competitiveness> AddCompetitiveness;
        public event EventHandler<ChangeCompetitiveness> ChangeCompetitiveness;

        private int _row;

        public FormCompetitiveness()
        {
            InitializeComponent();

            ButtonPost.Text = "Добавить";
            this.Name = "Добавление записи";

            ButtonPost.Click += AddButton_Click;
        }

        public FormCompetitiveness(ChangeCompetitiveness competitiveness)
        {
            InitializeComponent();

            ButtonPost.Text =       "Изменить";
            this.Name =             "Измение записи";
            TextQualityScore.Text = competitiveness.QualityScore;
            TextCoef.Text =         competitiveness.Coef.ToString();
            TextProject.Text =      competitiveness.ProjectEvaluation.ToString();
            TextAnalog.Text =       competitiveness.AnalogEvaluation.ToString();
            _row =                  competitiveness.Row;

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

                ChangeCompetitiveness changeCompetitiveness = new ChangeCompetitiveness()
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

        private Competitiveness CollectParams()
        {
            var quolityScore = TextQualityScore.Text;
            if (String.IsNullOrEmpty(quolityScore))
                throw new ArgumentException("Неверно введено название показателя качества. Параметр пуст!");

            bool isCoefParseSuccess = float.TryParse(TextCoef.Text, out float coef);
            if (!isCoefParseSuccess)
                throw new ArgumentException("Коэффициент весомости был введён неверно!");
            if (coef < 0.001 || coef > 1)
                throw new ArgumentOutOfRangeException(
                    "Коэффициент весомости",
                    "Значение введено неверно!\n" +
                    "Значение должно быть больше нуля и меньше одного!"
                );

            bool isProjectEvaluationParseSuccess = int.TryParse(TextProject.Text, out int projectEvaluation);
            if (!isProjectEvaluationParseSuccess)
                throw new ArgumentException("Оценка проекта была введена неверно!");
            if (projectEvaluation <= 0 || projectEvaluation > 10)
                throw new ArgumentOutOfRangeException(
                    "Оценка проекта",
                    "Значение введено неверно!\n" +
                    "Значение должно быть больше нуля и меньше или равно десяти!"
                );

            bool isAnalogEvaluationParseSuccess = int.TryParse(TextAnalog.Text, out int analogEvaluation);
            if (!isAnalogEvaluationParseSuccess)
                throw new ArgumentException("Оценка аналога была введена неверно!");
            if (analogEvaluation <= 0 || analogEvaluation > 10)
                throw new ArgumentOutOfRangeException(
                    "Оценка аналога",
                    "Значение введено неверно!\n" +
                    "Значение должно быть больше нуля и меньше или равно десяти!"
                );

            Competitiveness competitiveness = new Competitiveness()
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
