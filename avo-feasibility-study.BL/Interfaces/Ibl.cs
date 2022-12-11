using avo_feasibility_study.BL.Models;
using avo_feasibility_study.BL.Models.Results;

namespace avo_feasibility_study.BL.Interfaces
{
    public interface Ibl
    {
        EvaluationResult Evaluation(CompetitivenessParams parameters);
    }
}
