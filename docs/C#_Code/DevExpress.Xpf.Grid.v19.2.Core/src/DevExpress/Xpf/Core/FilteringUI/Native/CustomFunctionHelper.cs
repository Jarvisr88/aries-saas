namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Filtering;
    using System;

    public static class CustomFunctionHelper
    {
        private const string LikeName = "Like";

        public static ICustomFunctionOperatorBrowsable GetCustomFunctionOperatorBrowsable(string customFunctionName) => 
            CriteriaOperator.GetCustomFunction(customFunctionName) as ICustomFunctionOperatorBrowsable;

        public static bool IsLikeCustomFunction(string customFunctionName) => 
            customFunctionName == "Like";
    }
}

