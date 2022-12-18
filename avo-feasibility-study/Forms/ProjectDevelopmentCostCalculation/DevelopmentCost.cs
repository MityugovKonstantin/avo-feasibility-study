using System.Windows.Forms;

namespace avo_feasibility_study.Forms.ProjectDevelopmentCostCalculation
{
    public class DevelopmentCost
    {
        private TableLayoutPanel _table;
        private Button _calculateButton;

        public DevelopmentCost(TableLayoutPanel table, Button calculateButton)
        {
            _table = table;
            _calculateButton = calculateButton;

        }
    }
}
