namespace DevExpress.Data.Filtering
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(ParameterValue)), XmlInclude(typeof(QueryOperand)), XmlInclude(typeof(QuerySubQueryContainer)), XmlInclude(typeof(ContainsOperator)), XmlInclude(typeof(BetweenOperator)), XmlInclude(typeof(BinaryOperator)), XmlInclude(typeof(UnaryOperator)), XmlInclude(typeof(InOperator)), XmlInclude(typeof(GroupOperator)), XmlInclude(typeof(OperandValue)), XmlInclude(typeof(ConstantValue)), XmlInclude(typeof(OperandProperty)), XmlInclude(typeof(AggregateOperand)), XmlInclude(typeof(JoinOperand)), XmlInclude(typeof(FunctionOperator)), XmlInclude(typeof(NotOperator)), XmlInclude(typeof(NullOperator)), XmlInclude(typeof(QueryOperand))]
    public abstract class CriteriaOperator : ICloneable
    {
        private const string operatorTrueFalseObsoleteText = "Please replace == operator with ReferenceEquals, != with !ReferenceEquals (or use | and & operators instead of || and && in the simplified criteria syntax)";
        private static CustomFunctionCollection commonCustomFunctionCollection;
        private static readonly CustomAggregateCollection commonAggregateCollection;
        internal const string TagToString = "ToString";
        internal const string TagEnum = "Enum";

        public static event EventHandler<CustomFunctionEventArgs> QueryCustomFunctions;

        public static event EventHandler<UserValueProcessingEventArgs> UserValueParse;

        public static event EventHandler<UserValueProcessingEventArgs> UserValueToString;

        static CriteriaOperator();
        protected CriteriaOperator();
        public abstract void Accept(ICriteriaVisitor visitor);
        public abstract T Accept<T>(ICriteriaVisitor<T> visitor);
        public static CriteriaOperator And(IEnumerable<CriteriaOperator> operands);
        public static CriteriaOperator And(params CriteriaOperator[] operands);
        public static CriteriaOperator And(CriteriaOperator left, CriteriaOperator right);
        public static CriteriaOperator Clone(CriteriaOperator origin);
        public static OperandProperty Clone(OperandProperty origin);
        protected static ICollection<CriteriaOperator> Clone(ICollection origins);
        protected abstract CriteriaOperator CloneCommon();
        public static bool CriterionEquals(CriteriaOperator left, CriteriaOperator right);
        internal static CustomFunctionEventArgs DoQueryCustomFunctions(CustomFunctionEventArgs e);
        internal static UserValueProcessingEventArgs DoUserValueParse(string tag, string data);
        internal static UserValueProcessingEventArgs DoUserValueToString(object value);
        public override bool Equals(object obj);
        public static ICustomAggregate GetCustomAggregate(string aggregateName);
        public static CustomAggregateCollection GetCustomAggregates();
        public static ICustomFunctionOperator GetCustomFunction(string functionName);
        public static CustomFunctionCollection GetCustomFunctions();
        public override int GetHashCode();
        public UnaryOperator IsNotNull();
        public UnaryOperator IsNull();
        public string LegacyToString();
        public static string LegacyToString(CriteriaOperator criteria);
        public UnaryOperator Not();
        protected static CriteriaOperator ObjectToCriteriaSafe(object o);
        public static BinaryOperator operator +(CriteriaOperator left, CriteriaOperator right);
        public static CriteriaOperator operator &(CriteriaOperator left, CriteriaOperator right);
        public static CriteriaOperator operator |(CriteriaOperator left, CriteriaOperator right);
        public static BinaryOperator operator /(CriteriaOperator left, CriteriaOperator right);
        public static BinaryOperator operator ==(CriteriaOperator left, CriteriaOperator right);
        public static explicit operator CriteriaOperator(bool val);
        [Obsolete("Please replace == operator with ReferenceEquals, != with !ReferenceEquals (or use | and & operators instead of || and && in the simplified criteria syntax)", true)]
        public static bool operator false(CriteriaOperator operand);
        public static BinaryOperator operator >(CriteriaOperator left, CriteriaOperator right);
        public static BinaryOperator operator >=(CriteriaOperator left, CriteriaOperator right);
        public static implicit operator CriteriaOperator(byte val);
        public static implicit operator CriteriaOperator(char val);
        public static implicit operator CriteriaOperator(DateTime val);
        public static implicit operator CriteriaOperator(decimal val);
        public static implicit operator CriteriaOperator(double val);
        public static implicit operator CriteriaOperator(Guid val);
        public static implicit operator CriteriaOperator(short val);
        public static implicit operator CriteriaOperator(int val);
        public static implicit operator CriteriaOperator(long val);
        public static implicit operator CriteriaOperator(float val);
        public static implicit operator CriteriaOperator(string val);
        public static implicit operator CriteriaOperator(TimeSpan val);
        public static implicit operator CriteriaOperator(byte[] val);
        public static BinaryOperator operator !=(CriteriaOperator left, CriteriaOperator right);
        public static BinaryOperator operator <(CriteriaOperator left, CriteriaOperator right);
        public static BinaryOperator operator <=(CriteriaOperator left, CriteriaOperator right);
        public static UnaryOperator op_LogicalNot(CriteriaOperator operand);
        public static BinaryOperator operator %(CriteriaOperator left, CriteriaOperator right);
        public static BinaryOperator operator *(CriteriaOperator left, CriteriaOperator right);
        public static BinaryOperator operator -(CriteriaOperator left, CriteriaOperator right);
        [Obsolete("Please replace == operator with ReferenceEquals, != with !ReferenceEquals (or use | and & operators instead of || and && in the simplified criteria syntax)")]
        public static bool operator true(CriteriaOperator operand);
        public static UnaryOperator operator -(CriteriaOperator operand);
        public static UnaryOperator operator +(CriteriaOperator operand);
        private static bool operatorTrueFalseCore(CriteriaOperator operand);
        public static CriteriaOperator Or(IEnumerable<CriteriaOperator> operands);
        public static CriteriaOperator Or(params CriteriaOperator[] operands);
        public static CriteriaOperator Or(CriteriaOperator left, CriteriaOperator right);
        public static CriteriaOperator Parse(string stringCriteria, out OperandValue[] criteriaParametersList);
        public static CriteriaOperator Parse(string criteria, params object[] parameters);
        public static CriteriaOperator[] ParseList(string criteriaList, out OperandValue[] criteriaParametersList);
        public static CriteriaOperator[] ParseList(string criteriaList, params object[] parameters);
        public static void RegisterCustomAggregate(ICustomAggregate customAggregate);
        public static void RegisterCustomAggregates(IEnumerable<ICustomAggregate> customAggregates);
        public static void RegisterCustomFunction(ICustomFunctionOperator customFunction);
        public static void RegisterCustomFunctions(IEnumerable<ICustomFunctionOperator> customFunctions);
        object ICloneable.Clone();
        public static string ToBasicStyleString(CriteriaOperator criteria);
        public static string ToBasicStyleString(CriteriaOperator criteria, out OperandValue[] criteriaParametersList);
        public static string ToCStyleString(CriteriaOperator criteria);
        public static string ToCStyleString(CriteriaOperator criteria, out OperandValue[] criteriaParametersList);
        public override string ToString();
        public static string ToString(CriteriaOperator criteria);
        public static string ToString(CriteriaOperator criteria, out OperandValue[] criteriaParametersList);
        public static CriteriaOperator TryParse(string criteria, params object[] parameters);
        public static bool UnregisterCustomAggregate(ICustomAggregate customAggregate);
        public static bool UnregisterCustomAggregate(string aggregateName);
        public static bool UnregisterCustomFunction(ICustomFunctionOperator customFunction);
        public static bool UnregisterCustomFunction(string functionName);

        public static int CustomFunctionCount { get; }

        public static int CustomAggregateCount { get; }
    }
}

