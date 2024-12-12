namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class RowValidationError : BaseValidationError
    {
        protected readonly int fRowHandle;

        public RowValidationError(object errorContent, Exception exception, ErrorType errorType, int rowHandle, bool isCellError) : this(errorContent, errorContent.YieldToArray<object>(), exception, errorType, rowHandle, isCellError)
        {
        }

        internal RowValidationError(object errorContent, object[] errors, Exception exception, ErrorType errorType, int rowHandle, bool isCellError) : base(errorContent, exception, errorType)
        {
            this.fRowHandle = rowHandle;
            this.<IsCellError>k__BackingField = isCellError;
            this.<Errors>k__BackingField = errors;
        }

        internal bool IsCellError { get; }

        public IEnumerable<object> Errors { get; }
    }
}

