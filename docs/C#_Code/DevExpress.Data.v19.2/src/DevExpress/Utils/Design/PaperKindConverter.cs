namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;
    using System.Collections;

    public class PaperKindConverter : EnumTypeConverter
    {
        private static IComparer comparer = new PaperKindComparer();

        public PaperKindConverter(Type type) : base(type)
        {
        }

        protected override void InitializeInternal(Type enumType)
        {
            Initialize(enumType, typeof(ResFinder));
        }

        protected override IComparer Comparer =>
            comparer;

        protected class PaperKindComparer : IComparer
        {
            public int Compare(object x, object y) => 
                Comparer.Default.Compare(x.ToString(), y.ToString());
        }
    }
}

