namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;

    public class ShapeGuideCalculator : IShapeGuideCalculator
    {
        private static readonly List<ModelShapeGuide> builtinGuides = PopulateBuiltInGuides();
        private readonly Dictionary<string, double> guides;
        private readonly ModelShapeCustomGeometry geometry;

        public ShapeGuideCalculator(ModelShapeCustomGeometry geometry, double widthEmu, double heightEmu, ModelShapeGuideList presetAdjustList) : this(geometry, widthEmu, heightEmu, presetAdjustList, false)
        {
        }

        public ShapeGuideCalculator(ModelShapeCustomGeometry geometry, double widthEmu, double heightEmu, ModelShapeGuideList presetAdjustList, bool ignoreCase)
        {
            this.guides = ignoreCase ? new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase) : new Dictionary<string, double>();
            this.geometry = geometry;
            this.PopulateConstantGuides();
            this.Guides.Add("h", heightEmu);
            this.Guides.Add("w", widthEmu);
            this.Guides.Add("b", heightEmu);
            this.Guides.Add("r", widthEmu);
            this.CalculateGuides(presetAdjustList);
        }

        internal void CalculateGuide(ModelShapeGuide guide)
        {
            double num = 0.0;
            try
            {
                ModelShapeGuideFormula formula = guide.Formula;
                double x = (formula.Argument1 != null) ? formula.Argument1.Evaluate(this) : 0.0;
                double y = (formula.Argument2 != null) ? formula.Argument2.Evaluate(this) : 0.0;
                double z = (formula.Argument3 != null) ? formula.Argument3.Evaluate(this) : 0.0;
                switch (formula.Type)
                {
                    case ModelShapeGuideFormulaType.MultiplyDivide:
                        num = this.ProcessMultiplyDivide(x, y, z);
                        break;

                    case ModelShapeGuideFormulaType.AddSubtract:
                        num = (x + y) - z;
                        break;

                    case ModelShapeGuideFormulaType.AddDivide:
                        num = this.ProcessAddDivide(x, y, z);
                        break;

                    case ModelShapeGuideFormulaType.IfElse:
                        num = (x > 0.0) ? y : z;
                        break;

                    case ModelShapeGuideFormulaType.Abs:
                        num = Math.Abs(x);
                        break;

                    case ModelShapeGuideFormulaType.ArcTan:
                        num = RadianToEMUDegree((x == 0.0) ? Math.Atan(y) : Math.Atan2(y, x));
                        break;

                    case ModelShapeGuideFormulaType.CosArcTan:
                        num = x * Math.Cos((y == 0.0) ? Math.Atan(z) : Math.Atan2(z, y));
                        break;

                    case ModelShapeGuideFormulaType.Cos:
                        num = x * Math.Cos(EMUDegreeToRadian(y));
                        break;

                    case ModelShapeGuideFormulaType.Max:
                        num = Math.Max(x, y);
                        break;

                    case ModelShapeGuideFormulaType.Min:
                        num = Math.Min(x, y);
                        break;

                    case ModelShapeGuideFormulaType.Mod:
                        num = Math.Sqrt(((x * x) + (y * y)) + (z * z));
                        break;

                    case ModelShapeGuideFormulaType.Pin:
                        num = (y < x) ? x : ((y > z) ? z : y);
                        break;

                    case ModelShapeGuideFormulaType.SinArcTan:
                        num = x * Math.Sin((y == 0.0) ? Math.Atan(z) : Math.Atan2(z, y));
                        break;

                    case ModelShapeGuideFormulaType.Sin:
                        num = x * Math.Sin(EMUDegreeToRadian(y));
                        break;

                    case ModelShapeGuideFormulaType.Sqrt:
                        num = this.ProcessSqrt(x);
                        break;

                    case ModelShapeGuideFormulaType.Tan:
                        num = x * Math.Tan(EMUDegreeToRadian(y));
                        break;

                    case ModelShapeGuideFormulaType.Value:
                        num = x;
                        break;

                    case ModelShapeGuideFormulaType.Mid:
                        num = (x + y) / 2.0;
                        break;

                    case ModelShapeGuideFormulaType.SumAngle:
                        num = (x + (y * 65536.0)) - (z * 65536.0);
                        break;

                    case ModelShapeGuideFormulaType.Ellipse:
                        num = z * Math.Sqrt(1.0 - Math.Pow((y == 0.0) ? x : (x / y), 2.0));
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                num = 0.0;
            }
            if (this.Guides.ContainsKey(guide.Name))
            {
                this.Guides.Remove(guide.Name);
            }
            this.Guides.Add(guide.Name, num);
        }

        private void CalculateGuides(ModelShapeGuideList presetAdjustList)
        {
            foreach (ModelShapeGuide guide in builtinGuides)
            {
                this.CalculateGuide(guide);
            }
            foreach (ModelShapeGuide guide2 in this.Geometry.AdjustValues)
            {
                this.CalculateGuide(guide2);
            }
            if (presetAdjustList != null)
            {
                foreach (ModelShapeGuide guide3 in presetAdjustList)
                {
                    this.CalculateGuide(guide3);
                }
            }
            foreach (ModelShapeGuide guide4 in this.Geometry.Guides)
            {
                this.CalculateGuide(guide4);
            }
        }

        public static double EMUDegreeToRadian(double degree) => 
            ((degree / 60000.0) / 180.0) * 3.1415926535897931;

        public double GetGuideValue(string guideName)
        {
            double num;
            return (!string.IsNullOrEmpty(guideName) ? (!double.TryParse(guideName, out num) ? (!this.Guides.TryGetValue(guideName, out num) ? 0.0 : num) : num) : 0.0);
        }

        private static List<ModelShapeGuide> PopulateBuiltInGuides() => 
            new List<ModelShapeGuide> { 
                new ModelShapeGuide("hc", "*/ w 1 2"),
                new ModelShapeGuide("hd2", "*/ h 1 2"),
                new ModelShapeGuide("hd3", "*/ h 1 3"),
                new ModelShapeGuide("hd4", "*/ h 1 4"),
                new ModelShapeGuide("hd5", "*/ h 1 5"),
                new ModelShapeGuide("hd6", "*/ h 1 6"),
                new ModelShapeGuide("hd8", "*/ h 1 8"),
                new ModelShapeGuide("hd10", "*/ h 1 10"),
                new ModelShapeGuide("ls", "max w h"),
                new ModelShapeGuide("ss", "min w h"),
                new ModelShapeGuide("ssd2", "*/ ss 1 2"),
                new ModelShapeGuide("ssd4", "*/ ss 1 4"),
                new ModelShapeGuide("ssd6", "*/ ss 1 6"),
                new ModelShapeGuide("ssd8", "*/ ss 1 8"),
                new ModelShapeGuide("ssd16", "*/ ss 1 16"),
                new ModelShapeGuide("ssd32", "*/ ss 1 32"),
                new ModelShapeGuide("vc", "*/ h 1 2"),
                new ModelShapeGuide("wd2", "*/ w 1 2"),
                new ModelShapeGuide("wd3", "*/ w 1 3"),
                new ModelShapeGuide("wd4", "*/ w 1 4"),
                new ModelShapeGuide("wd5", "*/ w 1 5"),
                new ModelShapeGuide("wd6", "*/ w 1 6"),
                new ModelShapeGuide("wd8", "*/ w 1 8"),
                new ModelShapeGuide("wd10", "*/ w 1 10"),
                new ModelShapeGuide("wd12", "*/ w 1 12"),
                new ModelShapeGuide("wd32", "*/ w 1 32")
            };

        private void PopulateConstantGuides()
        {
            this.Guides.Add("3cd4", 16200000.0);
            this.Guides.Add("3cd8", 8100000.0);
            this.Guides.Add("5cd8", 13500000.0);
            this.Guides.Add("7cd8", 18900000.0);
            this.Guides.Add("cd2", 10800000.0);
            this.Guides.Add("cd3", 7200000.0);
            this.Guides.Add("cd4", 5400000.0);
            this.Guides.Add("cd8", 2700000.0);
            this.Guides.Add("l", 0.0);
            this.Guides.Add("t", 0.0);
        }

        protected virtual double ProcessAddDivide(double x, double y, double z) => 
            (z == 0.0) ? (x + y) : ((x + y) / z);

        protected virtual double ProcessMultiplyDivide(double x, double y, double z) => 
            (z == 0.0) ? (x * y) : ((x * y) / z);

        protected virtual double ProcessSqrt(double x) => 
            Math.Sqrt(Math.Abs(x));

        public static double RadianToEMUDegree(double radian) => 
            ((radian / 3.1415926535897931) * 180.0) * 60000.0;

        private Dictionary<string, double> Guides =>
            this.guides;

        private ModelShapeCustomGeometry Geometry =>
            this.geometry;
    }
}

