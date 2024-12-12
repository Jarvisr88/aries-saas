namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RangeValueConverter<T> : IComparable
    {
        public RangeValueConverter(object value, Func<object> getNullableValue, Func<object> getDefaultValue)
        {
            this.GetNullableValue = getNullableValue;
            this.GetDefaultValue = getDefaultValue;
            this.Assign(value);
        }

        private void Assign(object baseValue)
        {
            object obj3;
            object obj2 = baseValue ?? this.GetNullableValue();
            if (this.ConvertToT(obj2, out obj3))
            {
                this.Value = (T) obj3;
            }
            else
            {
                this.Value = this.ConvertToT(this.GetNullableValue(), out obj3) ? ((T) obj3) : this.GetDefaultValue();
            }
        }

        public int CompareTo(object obj) => 
            !(this.Value is IComparable) ? -1 : ((IComparable) this.Value).CompareTo(obj);

        private bool ConvertToT(object value, out object result)
        {
            bool flag;
            try
            {
                if (value == null)
                {
                    result = null;
                    flag = false;
                }
                else
                {
                    result = (T) Convert.ChangeType(value, typeof(T));
                    flag = true;
                }
            }
            catch
            {
                result = null;
                flag = false;
            }
            return flag;
        }

        public T Value { get; private set; }

        private Func<object> GetNullableValue { get; set; }

        private Func<object> GetDefaultValue { get; set; }
    }
}

