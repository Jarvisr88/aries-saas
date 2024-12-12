namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class SvgGradient : SvgElement
    {
        private int? hashCode;

        public SvgGradient()
        {
            this.GradientTransformation = new SvgTransformCollection();
            this.Stops = new List<SvgGradientStop>();
        }

        protected override void AddElementCore(SvgElement element)
        {
            base.AddElementCore(element);
            if (element is SvgGradientStop)
            {
                this.Stops.Add(element as SvgGradientStop);
            }
        }

        public override T DeepCopy<T>(Action<SvgElement, Hashtable> updateStyle = null) where T: SvgElement, new()
        {
            T local = base.DeepCopy<T>(updateStyle);
            SvgGradient gradient = local as SvgGradient;
            if (gradient != null)
            {
                foreach (SvgTransform transform in this.GradientTransformation)
                {
                    gradient.GradientTransformation.Add(transform.DeepCopy());
                }
            }
            return local;
        }

        public override bool Equals(object obj)
        {
            SvgGradient gradient = obj as SvgGradient;
            return ((gradient != null) ? EqualsCore(this, gradient) : false);
        }

        private static bool EqualsCore(SvgGradient gradient1, SvgGradient gradient2) => 
            ((gradient1 == null) || (gradient2 == null)) ? ((gradient1 == null) && ReferenceEquals(gradient2, null)) : (gradient1.GetHashCode() == gradient2.GetHashCode());

        public override int GetHashCode()
        {
            this.hashCode = new int?(this.GetHashCodeCore());
            foreach (SvgGradientStop stop in this.Stops)
            {
                int[] numArray1 = new int[4];
                numArray1[0] = this.hashCode.Value;
                numArray1[1] = stop.Offset.ToString().GetHashCode();
                numArray1[2] = (stop.StopColor != null) ? stop.StopColor.GetHashCode() : 0;
                int[] array = numArray1;
                array[3] = stop.Opacity.GetValueOrDefault(1.0).GetHashCode();
                this.hashCode = new int?(HashCodeHelper.CalcHashCode2(array));
            }
            this.hashCode = (this.hashCode.Value != 0x7fffffff) ? this.hashCode : new int?(base.GetHashCode());
            return this.hashCode.Value;
        }

        protected virtual int GetHashCodeCore() => 
            ((((int) this.SpreadMethod) ^ this.GradientUnits.GetHashCode()) ^ base.Transformations.GetHashCode()) ^ SvgTransformConverter.Instance.ConvertTo(this.GradientTransformation, typeof(string)).GetHashCode();

        public static bool operator ==(SvgGradient gradient1, SvgGradient gradient2) => 
            EqualsCore(gradient1, gradient2);

        public static bool operator !=(SvgGradient gradient1, SvgGradient gradient2) => 
            !EqualsCore(gradient1, gradient2);

        [SvgPropertyNameAlias("gradientunits")]
        public SvgCoordinateUnits GradientUnits
        {
            get => 
                this.GetValueCore<SvgCoordinateUnits>("GradientUnits", false);
            protected set => 
                this.SetValueCore<SvgCoordinateUnits>("GradientUnits", value);
        }

        [SvgPropertyNameAlias("spreadmethod")]
        public SvgGradientSpreadMethod SpreadMethod
        {
            get => 
                this.GetValueCore<SvgGradientSpreadMethod>("SpreadMethod", false);
            protected set => 
                this.SetValueCore<SvgGradientSpreadMethod>("SpreadMethod", value);
        }

        [SvgPropertyNameAlias("gradientTransform"), TypeConverter(typeof(SvgTransformConverter))]
        public SvgTransformCollection GradientTransformation { get; internal set; }

        public IList<SvgGradientStop> Stops { get; internal set; }
    }
}

