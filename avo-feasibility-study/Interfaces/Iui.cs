using avo_feasibility_study.BL.Models;
using avo_feasibility_study.BL.Models.Results;
using System;

namespace avo_feasibility_study.Interfaces
{
    public interface Iui
    {
        void ShowResult(EvaluationResult result);
        event EventHandler<CompetitivenessParams> OnEvaluation;
    }
}
