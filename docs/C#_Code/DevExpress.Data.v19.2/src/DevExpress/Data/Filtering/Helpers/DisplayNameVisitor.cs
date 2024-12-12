namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DisplayNameVisitor : IClientCriteriaVisitor, ICriteriaVisitor
    {
        private Stack<string> listPath;
        internal bool DryRun;
        internal IBoundProperty LastFoundProperty;

        public DisplayNameVisitor();
        private IBoundProperty CreateUnknownBoundProperty(List<string> path, IBoundProperty property, string columnName, out string patchedPath);
        private IBoundProperty GetColumnByMixedPath(IBoundProperty root, string columnName, bool toFields, out string patchedPath);
        private string GetCurrentPath();
        protected IBoundProperty GetCurrentProperty(string lastPropertyName);
        private List<List<IBoundProperty>> GetListPathColumns(IBoundProperty child);
        protected void SafeAccept(CriteriaOperator criteria);
        private IBoundProperty SearchColumn(IEnumerable<IBoundProperty> columns, Func<IBoundProperty, string> nameSelector, ref string columnName);
        public virtual void Visit(AggregateOperand theOperand);
        public virtual void Visit(BetweenOperator theOperator);
        public virtual void Visit(BinaryOperator theOperator);
        public virtual void Visit(FunctionOperator theOperator);
        public virtual void Visit(GroupOperator theOperator);
        public virtual void Visit(InOperator theOperator);
        public virtual void Visit(JoinOperand theOperand);
        public virtual void Visit(OperandProperty theOperand);
        public virtual void Visit(OperandValue theOperand);
        public virtual void Visit(UnaryOperator theOperator);

        public IEnumerable<IBoundProperty> Columns { get; set; }

        public bool FromEditor { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DisplayNameVisitor.<>c <>9;
            public static Func<IBoundProperty, string> <>9__18_0;
            public static Func<IBoundProperty, string> <>9__18_1;
            public static Func<IBoundProperty, string> <>9__18_2;

            static <>c();
            internal string <GetColumnByMixedPath>b__18_0(IBoundProperty p);
            internal string <GetColumnByMixedPath>b__18_1(IBoundProperty p);
            internal string <GetColumnByMixedPath>b__18_2(IBoundProperty p);
        }

        internal class UnknownBoundProperty : IBoundProperty
        {
            public string Name { get; set; }

            public string DisplayName { get; set; }

            public IBoundProperty Parent { get; set; }

            public System.Type Type { get; }

            public bool HasChildren { get; }

            public List<IBoundProperty> Children { get; }

            public bool IsAggregate { get; }

            public bool IsList { get; }
        }
    }
}

