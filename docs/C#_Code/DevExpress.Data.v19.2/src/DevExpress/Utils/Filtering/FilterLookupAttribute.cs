namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public sealed class FilterLookupAttribute : BaseFilterLookupAttribute
    {
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly FilterLookupAttribute Implicit = new FilterLookupAttribute();
        internal bool? useBlanks;

        public FilterLookupAttribute() : this(null, 0, 0)
        {
        }

        public FilterLookupAttribute(int top, int maxCount = 0) : this(null, top, maxCount)
        {
        }

        public FilterLookupAttribute(string topOrTopMember, string maxCountOrMaxCountMember)
        {
            int num;
            int num2;
            if (!int.TryParse(topOrTopMember, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
            {
                this.TopMember = topOrTopMember;
            }
            else
            {
                this.Top = new int?(num);
            }
            if (!int.TryParse(maxCountOrMaxCountMember, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
            {
                this.MaxCountMember = maxCountOrMaxCountMember;
            }
            else
            {
                this.MaxCount = new int?(num2);
            }
        }

        public FilterLookupAttribute(object dataSourceOrDataSourceMember, int top = 0, int maxCount = 0)
        {
            int? nullable;
            int? nullable1;
            int? nullable2;
            if (dataSourceOrDataSourceMember is string)
            {
                this.DataSourceMember = (string) dataSourceOrDataSourceMember;
            }
            else
            {
                this.DataSource = dataSourceOrDataSourceMember;
            }
            if (top != 0)
            {
                nullable1 = (maxCount > 0) ? new int?(Math.Min(top, maxCount)) : new int?(top);
            }
            else
            {
                nullable = null;
                nullable1 = nullable;
            }
            this.Top = nullable1;
            if (maxCount != 0)
            {
                nullable2 = new int?(Math.Max(top, maxCount));
            }
            else
            {
                nullable = null;
                nullable2 = nullable;
            }
            this.MaxCount = nullable2;
        }

        public string GetBlanksName() => 
            base.GetLocalizableValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttribute)), (MethodInfo) methodof(FilterLookupAttribute.get_BlanksName)), new ParameterExpression[0]));

        protected override IEnumerable<Expression<Func<string>>> GetLocalizableProperties() => 
            new Expression<Func<string>>[] { Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_SelectAllName)), new ParameterExpression[0]), Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttribute)), (MethodInfo) methodof(BaseFilterLookupAttribute.get_NullName)), new ParameterExpression[0]), Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttribute)), (MethodInfo) methodof(FilterLookupAttribute.get_BlanksName)), new ParameterExpression[0]) };

        protected internal override string[] GetMembers() => 
            new string[] { this.DataSourceMember, this.TopMember, this.MaxCountMember };

        public bool IsImplicit =>
            ReferenceEquals(this, Implicit);

        public LookupUIEditorType EditorType { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool UseFlags
        {
            get => 
                base.SelectionMode != ValueSelectionMode.Single;
            set
            {
                if ((base.SelectionMode == ValueSelectionMode.Default) && !value)
                {
                    base.SelectionMode = ValueSelectionMode.Single;
                }
            }
        }

        [Browsable(false)]
        public object DataSource { get; private set; }

        public int? Top { get; private set; }

        public int? MaxCount { get; private set; }

        public string DataSourceMember { get; set; }

        public string ValueMember { get; set; }

        public string DisplayMember { get; set; }

        public string TopMember { get; set; }

        public string MaxCountMember { get; set; }

        public bool UseBlanks
        {
            get => 
                this.useBlanks.GetValueOrDefault();
            set => 
                this.useBlanks = new bool?(value);
        }

        public string BlanksName
        {
            get => 
                base.GetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttribute)), (MethodInfo) methodof(FilterLookupAttribute.get_BlanksName)), new ParameterExpression[0]));
            set => 
                base.SetLocalizablePropertyValue(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterLookupAttribute)), (MethodInfo) methodof(FilterLookupAttribute.get_BlanksName)), new ParameterExpression[0]), value);
        }
    }
}

