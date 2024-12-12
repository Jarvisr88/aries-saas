namespace DevExpress.Xpf.Editors.Validation
{
    using System;

    public interface ISupportDXValidation
    {
        bool DoValidate();

        DevExpress.Xpf.Editors.Validation.InvalidValueBehavior InvalidValueBehavior { get; }

        bool HasValidationError { get; }

        BaseValidationError ValidationError { get; }
    }
}

