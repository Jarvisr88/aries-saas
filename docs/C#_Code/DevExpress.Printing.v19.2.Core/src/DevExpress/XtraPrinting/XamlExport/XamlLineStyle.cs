namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class XamlLineStyle : XamlResourceBase
    {
        private Color stroke;
        private float strokeThickness;
        private float[] strokeDashArray;

        public XamlLineStyle(Color stroke, float strokeThickness, float[] strokeDashArray)
        {
            this.stroke = stroke;
            this.strokeThickness = strokeThickness;
            this.strokeDashArray = strokeDashArray;
        }

        public override bool Equals(object obj)
        {
            XamlLineStyle style = obj as XamlLineStyle;
            return ((style != null) ? ((style.Stroke == this.stroke) && ((style.StrokeThickness == this.strokeThickness) && FloatArrayEquals(style.StrokeDashArray, this.strokeDashArray))) : false);
        }

        private static bool FloatArrayEquals(float[] array1, float[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            Converter<float, int> converter = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Converter<float, int> local1 = <>c.<>9__11_0;
                converter = <>c.<>9__11_0 = value => (int) value;
            }
            return HashCodeHelper.CalculateGeneric<int, int, int>(this.Stroke.GetHashCode(), this.StrokeThickness.GetHashCode(), HashCodeHelper.CalculateGeneric<int[]>(ArrayHelper.ConvertAll<float, int>(this.StrokeDashArray, converter)));
        }

        public Color Stroke =>
            this.stroke;

        public float StrokeThickness =>
            this.strokeThickness;

        public float[] StrokeDashArray =>
            this.strokeDashArray;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly XamlLineStyle.<>c <>9 = new XamlLineStyle.<>c();
            public static Converter<float, int> <>9__11_0;

            internal int <GetHashCode>b__11_0(float value) => 
                (int) value;
        }
    }
}

