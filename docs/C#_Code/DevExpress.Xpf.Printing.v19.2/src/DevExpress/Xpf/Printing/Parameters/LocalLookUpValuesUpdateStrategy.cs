namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Data.Browsing;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class LocalLookUpValuesUpdateStrategy : LookUpValuesUpdateStrategy
    {
        private readonly DataContext dataContext;

        public LocalLookUpValuesUpdateStrategy(IEnumerable<ParameterModel> parameterModels, DataContext dataContext, IDialogService dialogService) : base(parameterModels, dialogService)
        {
            this.dataContext = dataContext;
        }

        public override void Update(ParameterModel changedModel)
        {
            <>c__DisplayClass2_0 class_;
            Task.Factory.StartNew(delegate {
                IEnumerable<Parameter> dependentParameters = CascadingParametersService.GetDependentParameters(changedModel.Parameter);
                (from model in this.ParameterModels
                    where dependentParameters.Contains<Parameter>(model.Parameter)
                    select model).ForEach<ParameterModel>(model => model.UpdateLookUpValues(LookUpHelper.GetLookUpValues(model.Parameter.LookUpSettings, class_.dataContext, base.ParameterValuesProvider)));
            }).ContinueWith(delegate (Task task) {
                if ((task.Exception != null) && (base.DialogService != null))
                {
                    base.DialogService.ShowError(task.Exception.Message, PrintingLocalizer.GetString(PrintingStringId.Error));
                }
                base.RaiseLookUpValuesUpdated();
            });
        }
    }
}

