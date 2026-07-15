using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiczanProductionInfoSystem
{
    internal class ModelInput
    {
        public float Value { get; set; }
    }

    internal class ModelOutput
    {
        public float[] Forecasted { get; set; }
        public float[] Lower { get; set; }
        public float[] Upper { get; set; }
    }
}