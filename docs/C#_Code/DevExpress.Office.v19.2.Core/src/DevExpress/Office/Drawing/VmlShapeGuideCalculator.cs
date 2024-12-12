namespace DevExpress.Office.Drawing
{
    using System;

    public class VmlShapeGuideCalculator : ShapeGuideCalculator
    {
        public VmlShapeGuideCalculator(ModelShapeCustomGeometry geometry, double widthEmu, double heightEmu, ModelShapeGuideList presetAdjustList) : base(geometry, widthEmu, heightEmu, presetAdjustList)
        {
        }

        protected override double ProcessAddDivide(double x, double y, double z) => 
            Math.Truncate(base.ProcessAddDivide(x, y, z));

        protected override double ProcessMultiplyDivide(double x, double y, double z) => 
            Math.Ceiling(base.ProcessMultiplyDivide(x, y, z));

        protected override double ProcessSqrt(double x) => 
            Math.Floor(base.ProcessSqrt(x));
    }
}

