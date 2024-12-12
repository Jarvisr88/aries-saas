namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Runtime.CompilerServices;

    public class VmlSingleFormula : ICloneable<VmlSingleFormula>
    {
        public VmlSingleFormula()
        {
        }

        public VmlSingleFormula(string equation)
        {
            this.Equation = equation;
        }

        public VmlSingleFormula Clone() => 
            new VmlSingleFormula(this.Equation);

        public string Equation { get; set; }
    }
}

