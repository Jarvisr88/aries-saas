namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ScaffoldColumnAttributeBuilderProvider : CustomAttributeBuilderProviderBase
    {
        internal override LambdaExpression GetConstructorExpressionCore() => 
            DataAnnotationsAttributeHelper.GetScaffoldColumnAttributeCreateExpression();

        internal override IEnumerable<object> GetConstructorParametersCore(Attribute attribute) => 
            DataAnnotationsAttributeHelper.GetScaffoldColumnAttributeConstructorParameters(attribute);

        protected override Type AttributeType =>
            DataAnnotationsAttributeHelper.GetScaffoldColumnAttributeType();
    }
}

