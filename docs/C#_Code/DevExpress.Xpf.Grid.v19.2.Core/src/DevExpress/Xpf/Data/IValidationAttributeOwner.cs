namespace DevExpress.Xpf.Data
{
    using System;

    public interface IValidationAttributeOwner
    {
        bool CalculateValidationAttribute(string columnName, int controllerRow);
    }
}

