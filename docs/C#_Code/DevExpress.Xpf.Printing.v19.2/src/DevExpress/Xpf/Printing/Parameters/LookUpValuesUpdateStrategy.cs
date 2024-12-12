namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal abstract class LookUpValuesUpdateStrategy
    {
        private readonly IDialogService dialogService;
        private readonly IEnumerable<ParameterModel> parameterModels;
        private readonly ParameterValueProvider valueProvider;

        public event EventHandler LookUpValuesUpdated;

        public LookUpValuesUpdateStrategy(IEnumerable<ParameterModel> parameterModels, IDialogService dialogService)
        {
            this.parameterModels = parameterModels;
            this.valueProvider = new ParameterValueProvider(parameterModels);
            this.dialogService = dialogService;
        }

        protected void RaiseLookUpValuesUpdated()
        {
            if (this.LookUpValuesUpdated != null)
            {
                this.LookUpValuesUpdated(this, EventArgs.Empty);
            }
        }

        public abstract void Update(ParameterModel changedModel);

        protected IDialogService DialogService =>
            this.dialogService;

        protected IEnumerable<ParameterModel> ParameterModels =>
            this.parameterModels;

        protected ParameterValueProvider ParameterValuesProvider =>
            this.valueProvider;
    }
}

