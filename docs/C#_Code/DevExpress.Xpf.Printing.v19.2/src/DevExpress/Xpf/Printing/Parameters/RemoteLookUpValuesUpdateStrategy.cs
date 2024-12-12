namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class RemoteLookUpValuesUpdateStrategy : LookUpValuesUpdateStrategy
    {
        private readonly InstanceIdentity reportIdentity;
        private readonly Func<IReportServiceClient> getClient;

        public RemoteLookUpValuesUpdateStrategy(IEnumerable<ParameterModel> parameterModels, InstanceIdentity reportIdentity, Func<IReportServiceClient> getClient, IDialogService dialogService) : base(parameterModels, dialogService)
        {
            this.reportIdentity = reportIdentity;
            this.getClient = getClient;
        }

        private void client_GetLookUpValuesCompleted(object sender, ScalarOperationCompletedEventArgs<ParameterLookUpValues[]> e)
        {
            if ((e.Error != null) && (base.DialogService != null))
            {
                base.DialogService.ShowError(e.Error.Message, PrintingLocalizer.GetString(PrintingStringId.Error));
            }
            else
            {
                foreach (ParameterLookUpValues lookUps in e.Result)
                {
                    ParameterModel model = base.ParameterModels.FirstOrDefault<ParameterModel>(x => x.Path == lookUps.Path);
                    if (model != null)
                    {
                        model.UpdateLookUpValues(lookUps.LookUpValues);
                    }
                }
            }
            base.RaiseLookUpValuesUpdated();
        }

        public override void Update(ParameterModel changedModel)
        {
            IReportServiceClient client = this.getClient();
            Func<ParameterModel, ReportParameter> selector = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<ParameterModel, ReportParameter> local1 = <>c.<>9__3_0;
                selector = <>c.<>9__3_0 = model => model.GetReportParameterStub();
            }
            ReportParameter[] parameterValues = base.ParameterModels.Select<ParameterModel, ReportParameter>(selector).ToArray<ReportParameter>();
            Func<ParameterModel, bool> predicate = <>c.<>9__3_2;
            if (<>c.<>9__3_2 == null)
            {
                Func<ParameterModel, bool> local2 = <>c.<>9__3_2;
                predicate = <>c.<>9__3_2 = x => x.IsFilteredLookUpSettings;
            }
            Func<ParameterModel, string> func3 = <>c.<>9__3_3;
            if (<>c.<>9__3_3 == null)
            {
                Func<ParameterModel, string> local3 = <>c.<>9__3_3;
                func3 = <>c.<>9__3_3 = x => x.Path;
            }
            client.GetLookUpValuesCompleted += new EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>>(this.client_GetLookUpValuesCompleted);
            client.GetLookUpValuesAsync(this.reportIdentity, parameterValues, base.ParameterModels.Reverse<ParameterModel>().TakeWhile<ParameterModel>(x => !ReferenceEquals(x, changedModel)).Where<ParameterModel>(predicate).Select<ParameterModel, string>(func3).ToArray<string>(), null);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RemoteLookUpValuesUpdateStrategy.<>c <>9 = new RemoteLookUpValuesUpdateStrategy.<>c();
            public static Func<ParameterModel, ReportParameter> <>9__3_0;
            public static Func<ParameterModel, bool> <>9__3_2;
            public static Func<ParameterModel, string> <>9__3_3;

            internal ReportParameter <Update>b__3_0(ParameterModel model) => 
                model.GetReportParameterStub();

            internal bool <Update>b__3_2(ParameterModel x) => 
                x.IsFilteredLookUpSettings;

            internal string <Update>b__3_3(ParameterModel x) => 
                x.Path;
        }
    }
}

