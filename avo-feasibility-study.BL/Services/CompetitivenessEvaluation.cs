using avo_feasibility_study.BL.Interfaces;
using avo_feasibility_study.BL.Models;
using avo_feasibility_study.BL.Models.Results;

namespace avo_feasibility_study.BL.Services
{
    public class CompetitivenessEvaluation : Ibl
    {
        public EvaluationResult Evaluation(CompetitivenessParams parameters)
        {
            float jProject = 0;
            float jAnalog = 0;

            var arraySize =             parameters.ArraySize;
            var coef =                  parameters.Coefs;
            var projectEvaluations =    parameters.ProjectEvaluations;
            var analogEvaluations =     parameters.AnalogueEvaluations;

            for (int i = 0; i < arraySize; i++)
            {
                jProject += coef[i] * projectEvaluations[i];
                jAnalog += coef[i] * analogEvaluations[i];
            }

            var tec = (float) jProject / jAnalog;
            string resultMessage;
            if (tec > 1)
                resultMessage = "Разработка проекта с технической точки зрения оправдана!";
            else
                resultMessage = "Разработка проекта с технической точки зрения не оправдана!";

            var evaluationResult = new EvaluationResult()
            {
                Teс = tec,
                ResultMessage = resultMessage
            };

            return evaluationResult;
        }
    }
}
