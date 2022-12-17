using avo_feasibility_study.Interfaces;
using avo_feasibility_study.BL.Interfaces;
using avo_feasibility_study.BL.Models;

namespace avo_feasibility_study
{
    internal class Presenter
    {
        private readonly Iui _view;
        private readonly Ibl _model;

        public Presenter(Iui view, Ibl model)
        {
            _view = view;
            _model = model;

            _view.OnEvaluation += Evaluation;
        }

        private void Evaluation(object sender, CompetitivenessParams parameters)
        {
            var result = _model.Evaluation(parameters);
            _view.ShowFirstResult(result);
        }
    }
}
