namespace DevExpress.Xpf.Utils.Themes
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;

    public abstract class ThemeKeyExtensionInternalBase<T> : ThemeKeyExtensionGeneric, IDisposable
    {
        private bool isVisibleInBlendCore;

        protected ThemeKeyExtensionInternalBase()
        {
            this.isVisibleInBlendCore = true;
        }

        protected override bool Equals(ThemeKeyExtensionGeneric other)
        {
            if (!base.Equals(other))
            {
                return false;
            }
            ThemeKeyExtensionInternalBase<T> base2 = (ThemeKeyExtensionInternalBase<T>) other;
            return Equals(base.ResourceKeyCore, base2.ResourceKeyCore);
        }

        protected override int GenerateHashCode() => 
            HashCodeHelper.CalculateGeneric<Type, object>(typeof(T), base.ResourceKeyCore);

        private void SetResourceKey(T value)
        {
            if (value == null)
            {
                throw new NullReferenceException("ThemeKeyExtensionBase");
            }
            if (typeof(T).IsEnum && !Enum.IsDefined(typeof(T), value))
            {
                throw new ArgumentException("Resource key isn`t defined");
            }
            base.ResourceKeyCore = value;
            this.SetHashCode();
        }

        void IDisposable.Dispose()
        {
        }

        public override string ToString() => 
            base.GetType().Name + "_" + base.ResourceKeyCore;

        public T ResourceKey
        {
            get => 
                (T) base.ResourceKeyCore;
            set => 
                this.SetResourceKey(value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsVisibleInBlend
        {
            get => 
                this.isVisibleInBlendCore;
            set => 
                this.isVisibleInBlendCore = value;
        }
    }
}

