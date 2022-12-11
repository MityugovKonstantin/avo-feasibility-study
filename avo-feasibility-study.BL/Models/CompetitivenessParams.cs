using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avo_feasibility_study.BL.Models
{
    public class CompetitivenessParams
    {
        public int ArraySize { get; set; }
        public float[] Coefs { get; set; }
        public int[] ProjectEvaluations { get; set; }
        public int[] AnalogueEvaluations { get; set; }
    }
}
