namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class DisplayAttributeBuilderProvider : CustomAttributeBuilderProviderBase
    {
        internal override LambdaExpression GetConstructorExpressionCore() => 
            DataAnnotationsAttributeHelper.GetDisplayAttributeCreateExpression();

        internal override IEnumerable<Tuple<PropertyInfo, object>> GetPropertyValuePairsCore(Attribute attribute) => 
            DataAnnotationsAttributeHelper.GetDisplayAttributePropertyValuePairs(attribute);

        protected override Type AttributeType =>
            DataAnnotationsAttributeHelper.GetDisplayAttributeType();
    }
}

