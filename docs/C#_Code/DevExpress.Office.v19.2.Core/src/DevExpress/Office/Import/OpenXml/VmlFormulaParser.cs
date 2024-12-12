namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;

    internal static class VmlFormulaParser
    {
        private static object syncFormulaTypeTable = new object();
        private static Dictionary<string, ModelShapeGuideFormulaType> formulaTypeTable;

        private static Dictionary<string, ModelShapeGuideFormulaType> CreateFormulaTypeTable() => 
            new Dictionary<string, ModelShapeGuideFormulaType> { 
                { 
                    "val",
                    ModelShapeGuideFormulaType.Value
                },
                { 
                    "sum",
                    ModelShapeGuideFormulaType.AddSubtract
                },
                { 
                    "prod",
                    ModelShapeGuideFormulaType.MultiplyDivide
                },
                { 
                    "mid",
                    ModelShapeGuideFormulaType.Mid
                },
                { 
                    "abs",
                    ModelShapeGuideFormulaType.Abs
                },
                { 
                    "min",
                    ModelShapeGuideFormulaType.Min
                },
                { 
                    "max",
                    ModelShapeGuideFormulaType.Max
                },
                { 
                    "if",
                    ModelShapeGuideFormulaType.IfElse
                },
                { 
                    "mod",
                    ModelShapeGuideFormulaType.Mod
                },
                { 
                    "atan2",
                    ModelShapeGuideFormulaType.ArcTan
                },
                { 
                    "sin",
                    ModelShapeGuideFormulaType.Sin
                },
                { 
                    "cos",
                    ModelShapeGuideFormulaType.Cos
                },
                { 
                    "cosatan2",
                    ModelShapeGuideFormulaType.CosArcTan
                },
                { 
                    "sinatan2",
                    ModelShapeGuideFormulaType.SinArcTan
                },
                { 
                    "sqrt",
                    ModelShapeGuideFormulaType.Sqrt
                },
                { 
                    "tan",
                    ModelShapeGuideFormulaType.Tan
                },
                { 
                    "sumangle",
                    ModelShapeGuideFormulaType.SumAngle
                },
                { 
                    "ellipse",
                    ModelShapeGuideFormulaType.Ellipse
                }
            };

        private static ModelShapeGuideFormulaType GetFormulaType(string value)
        {
            ModelShapeGuideFormulaType type;
            return (FormulaTypeTable.TryGetValue(value, out type) ? type : ModelShapeGuideFormulaType.Undefined);
        }

        public static ModelShapeGuideFormula Parse(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return ModelShapeGuideFormula.Empty;
            }
            char[] separator = new char[] { ' ' };
            string[] strArray = formula.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 0)
            {
                return ModelShapeGuideFormula.Empty;
            }
            string str = strArray[0];
            string str2 = (strArray.Length >= 2) ? UpdateArgument(strArray[1]) : string.Empty;
            string str3 = (strArray.Length >= 3) ? UpdateArgument(strArray[2]) : string.Empty;
            return new ModelShapeGuideFormula(GetFormulaType(str), AdjustableCoordinate.FromString(str2), AdjustableCoordinate.FromString(str3), AdjustableCoordinate.FromString((strArray.Length >= 4) ? UpdateArgument(strArray[3]) : string.Empty));
        }

        private static string UpdateArgument(string arg)
        {
            if (arg.StartsWith("#"))
            {
                arg = "adj" + arg.Substring(1);
                return arg;
            }
            if (arg.StartsWith("@"))
            {
                arg = "G" + arg.Substring(1);
            }
            return arg;
        }

        private static Dictionary<string, ModelShapeGuideFormulaType> FormulaTypeTable
        {
            get
            {
                if (formulaTypeTable == null)
                {
                    object syncFormulaTypeTable = VmlFormulaParser.syncFormulaTypeTable;
                    lock (syncFormulaTypeTable)
                    {
                        formulaTypeTable ??= CreateFormulaTypeTable();
                    }
                }
                return formulaTypeTable;
            }
        }
    }
}

