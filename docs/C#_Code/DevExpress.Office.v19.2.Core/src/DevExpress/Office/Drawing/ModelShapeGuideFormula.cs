namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ModelShapeGuideFormula : ISupportsCopyFrom<ModelShapeGuideFormula>, ICloneable<ModelShapeGuideFormula>
    {
        private static object syncFormulaTypeTable = new object();
        private static Dictionary<string, ModelShapeGuideFormulaType> formulaTypeTable;
        private static object syncFormulaDescriptionTable = new object();
        private static Dictionary<ModelShapeGuideFormulaType, string> formulaDescriptionTable;
        public static ModelShapeGuideFormula Empty = new ModelShapeGuideFormula();

        public ModelShapeGuideFormula()
        {
            this.Type = ModelShapeGuideFormulaType.Undefined;
        }

        public ModelShapeGuideFormula(ModelShapeGuideFormulaType type, AdjustableCoordinate arg1, AdjustableCoordinate arg2, AdjustableCoordinate arg3)
        {
            this.Type = type;
            this.Argument1 = arg1;
            this.Argument2 = arg2;
            this.Argument3 = arg3;
        }

        public ModelShapeGuideFormula Clone()
        {
            ModelShapeGuideFormula formula = new ModelShapeGuideFormula();
            formula.CopyFrom(this);
            return formula;
        }

        public void CopyFrom(ModelShapeGuideFormula source)
        {
            Guard.ArgumentNotNull(source, "ModelShapeGuideFormula");
            this.Type = source.Type;
            this.Argument1 = source.Argument1;
            this.Argument2 = source.Argument2;
            this.Argument3 = source.Argument3;
        }

        private static Dictionary<ModelShapeGuideFormulaType, string> CreateFormulaDescriptionTable() => 
            new Dictionary<ModelShapeGuideFormulaType, string> { 
                { 
                    ModelShapeGuideFormulaType.MultiplyDivide,
                    "*/"
                },
                { 
                    ModelShapeGuideFormulaType.AddSubtract,
                    "+-"
                },
                { 
                    ModelShapeGuideFormulaType.AddDivide,
                    "+/"
                },
                { 
                    ModelShapeGuideFormulaType.IfElse,
                    "?:"
                },
                { 
                    ModelShapeGuideFormulaType.Abs,
                    "abs"
                },
                { 
                    ModelShapeGuideFormulaType.ArcTan,
                    "at2"
                },
                { 
                    ModelShapeGuideFormulaType.CosArcTan,
                    "cat2"
                },
                { 
                    ModelShapeGuideFormulaType.Cos,
                    "cos"
                },
                { 
                    ModelShapeGuideFormulaType.Max,
                    "max"
                },
                { 
                    ModelShapeGuideFormulaType.Min,
                    "min"
                },
                { 
                    ModelShapeGuideFormulaType.Mod,
                    "mod"
                },
                { 
                    ModelShapeGuideFormulaType.Pin,
                    "pin"
                },
                { 
                    ModelShapeGuideFormulaType.SinArcTan,
                    "sat2"
                },
                { 
                    ModelShapeGuideFormulaType.Sin,
                    "sin"
                },
                { 
                    ModelShapeGuideFormulaType.Sqrt,
                    "sqrt"
                },
                { 
                    ModelShapeGuideFormulaType.Tan,
                    "tan"
                },
                { 
                    ModelShapeGuideFormulaType.Value,
                    "val"
                }
            };

        private static Dictionary<string, ModelShapeGuideFormulaType> CreateFormulaTypeTable() => 
            DictionaryUtils.CreateBackTranslationTable<ModelShapeGuideFormulaType>(CreateFormulaDescriptionTable());

        public override bool Equals(object obj)
        {
            ModelShapeGuideFormula formula = obj as ModelShapeGuideFormula;
            return ((formula != null) ? ((this.Type == formula.Type) && (ReferenceEquals(this.Argument1, formula.Argument1) && (ReferenceEquals(this.Argument2, formula.Argument2) && ReferenceEquals(this.Argument3, formula.Argument3)))) : false);
        }

        public static string FormulaTypeToString(ModelShapeGuideFormulaType type)
        {
            string str;
            return (FormulaDescriptionTable.TryGetValue(type, out str) ? str : string.Empty);
        }

        public static ModelShapeGuideFormula FromString(string formula)
        {
            ModelShapeGuideFormula formula2 = new ModelShapeGuideFormula();
            if (!string.IsNullOrEmpty(formula))
            {
                string[] strArray = formula.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 0)
                {
                    return formula2;
                }
                string str = strArray[0];
                string str2 = (strArray.Length >= 2) ? strArray[1] : string.Empty;
                string str3 = (strArray.Length >= 3) ? strArray[2] : string.Empty;
                formula2.Type = GetFormulaTypeFromString(str);
                formula2.Argument1 = AdjustableCoordinate.FromString(str2);
                formula2.Argument2 = AdjustableCoordinate.FromString(str3);
                formula2.Argument3 = AdjustableCoordinate.FromString((strArray.Length >= 4) ? strArray[3] : string.Empty);
            }
            return formula2;
        }

        public static ModelShapeGuideFormulaType GetFormulaTypeFromString(string value)
        {
            ModelShapeGuideFormulaType type;
            return (FormulaTypeTable.TryGetValue(value, out type) ? type : ModelShapeGuideFormulaType.Undefined);
        }

        public override int GetHashCode()
        {
            int hashCode = this.Type.GetHashCode();
            if (this.Argument1 != null)
            {
                hashCode ^= this.Argument1.GetHashCode();
            }
            if (this.Argument2 != null)
            {
                hashCode ^= this.Argument2.GetHashCode();
            }
            if (this.Argument3 != null)
            {
                hashCode ^= this.Argument3.GetHashCode();
            }
            return hashCode;
        }

        public override string ToString()
        {
            if (this.Type == ModelShapeGuideFormulaType.Undefined)
            {
                return string.Empty;
            }
            object[] values = new object[] { FormulaTypeToString(this.Type), this.Argument1, this.Argument2, this.Argument3 };
            return string.Join(" ", values).TrimEnd(new char[0]);
        }

        private static Dictionary<string, ModelShapeGuideFormulaType> FormulaTypeTable
        {
            get
            {
                if (formulaTypeTable == null)
                {
                    object syncFormulaTypeTable = ModelShapeGuideFormula.syncFormulaTypeTable;
                    lock (syncFormulaTypeTable)
                    {
                        formulaTypeTable ??= CreateFormulaTypeTable();
                    }
                }
                return formulaTypeTable;
            }
        }

        private static Dictionary<ModelShapeGuideFormulaType, string> FormulaDescriptionTable
        {
            get
            {
                if (formulaDescriptionTable == null)
                {
                    object syncFormulaDescriptionTable = ModelShapeGuideFormula.syncFormulaDescriptionTable;
                    lock (syncFormulaDescriptionTable)
                    {
                        formulaDescriptionTable ??= CreateFormulaDescriptionTable();
                    }
                }
                return formulaDescriptionTable;
            }
        }

        public ModelShapeGuideFormulaType Type { get; private set; }

        public AdjustableCoordinate Argument1 { get; private set; }

        public AdjustableCoordinate Argument2 { get; private set; }

        public AdjustableCoordinate Argument3 { get; private set; }
    }
}

