namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class DateTimeMaskBuilder<T, TParentBuilder> : MaskBuilderBase<T, System.DateTime, DateTimeMaskAttribute, DateTimeMaskBuilder<T, TParentBuilder>, TParentBuilder> where TParentBuilder: PropertyMetadataBuilderBase<T, System.DateTime, TParentBuilder>
    {
        public DateTimeMaskBuilder(TParentBuilder parent) : base(parent)
        {
        }

        public DateTimeMaskBuilder<T, TParentBuilder> MaskAutomaticallyAdvanceCaret()
        {
            Action<DateTimeMaskAttribute> action = <>c<T, TParentBuilder>.<>9__2_0;
            if (<>c<T, TParentBuilder>.<>9__2_0 == null)
            {
                Action<DateTimeMaskAttribute> local1 = <>c<T, TParentBuilder>.<>9__2_0;
                action = <>c<T, TParentBuilder>.<>9__2_0 = delegate (DateTimeMaskAttribute x) {
                    x.AutomaticallyAdvanceCaret = true;
                };
            }
            return this.ChangeAttribute(action);
        }

        public DateTimeMaskBuilder<T, TParentBuilder> MaskCulture(CultureInfo culture) => 
            base.ChangeAttribute(delegate (DateTimeMaskAttribute x) {
                x.CultureInfo = culture;
            });

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateTimeMaskBuilder<T, TParentBuilder>.<>c <>9;
            public static Action<DateTimeMaskAttribute> <>9__2_0;

            static <>c()
            {
                DateTimeMaskBuilder<T, TParentBuilder>.<>c.<>9 = new DateTimeMaskBuilder<T, TParentBuilder>.<>c();
            }

            internal void <MaskAutomaticallyAdvanceCaret>b__2_0(DateTimeMaskAttribute x)
            {
                x.AutomaticallyAdvanceCaret = true;
            }
        }
    }
}

