namespace DevExpress.Data.Filtering
{
    using System;

    public class CriteriaCompilerAuxSettings
    {
        internal static readonly CriteriaCompilerAuxSettings DefaultInstance;
        public bool CaseSensitive;
        public CustomFunctionCollection AdditionalFunctions;
        public CustomAggregateCollection AdditionalAggregates;
        public bool? CheckedMath;
        public static bool DefaultCheckedMath;
        public bool? Is3ValuedLogic;
        public static bool DefaultIs3ValuedLogic;

        static CriteriaCompilerAuxSettings();
        public CriteriaCompilerAuxSettings();
        public CriteriaCompilerAuxSettings(bool caseSensitive);
        public CriteriaCompilerAuxSettings(bool caseSensitive, CustomFunctionCollection additionalFunctions);
        public CriteriaCompilerAuxSettings(bool caseSensitive, CustomFunctionCollection additionalFunctions, CustomAggregateCollection additionalAggregates);
        internal bool GetActiveCheckedMath();
        internal bool GetActiveIs3ValuedLogic();
    }
}

