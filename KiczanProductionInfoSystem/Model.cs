using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiczanProductionInfoSystem
{ }
public class ModelInput
{
    public float Value { get; set; }
}

public class ModelOutput
{
    public float[] Forecasted { get; set; }
    public float[] Lower { get; set; }
    public float[] Upper { get; set; }
}