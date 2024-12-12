namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.Native;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class RemoteLookUpValuesProvider : ILookUpValuesProvider
    {
        private readonly Func<IReportServiceClient> getActualClient;
        private readonly ClientParameterContainer parametersContainer;
        private readonly InstanceIdentity reportIdentity;

        public RemoteLookUpValuesProvider(ClientParameterContainer parametersContainer, InstanceIdentity reportIdentity, Func<IReportServiceClient> getActualClient)
        {
            this.parametersContainer = parametersContainer;
            this.getActualClient = getActualClient;
            this.reportIdentity = reportIdentity;
        }

        private object GetActualEditorValue(ReportParameter parameterStub, IParameterEditorValueProvider editorValueProvider)
        {
            ClientParameter parameter = this.parametersContainer.FirstOrDefault<IClientParameter>(x => (((ClientParameter) x).Path == parameterStub.Path)) as ClientParameter;
            return ((parameter != null) ? editorValueProvider.GetValue(parameter.OriginalParameter) : parameterStub.Value);
        }

        public Task<IEnumerable<ParameterLookUpValuesContainer>> GetLookUpValues(Parameter changedParameter, IParameterEditorValueProvider editorValueProvider)
        {
            List<ParameterLookUpValuesContainer> list = new List<ParameterLookUpValuesContainer>();
            TaskCompletionSource<IEnumerable<ParameterLookUpValuesContainer>> asyncState = new TaskCompletionSource<IEnumerable<ParameterLookUpValuesContainer>>();
            Func<ClientParameter, bool> predicate = <>c.<>9__4_1;
            if (<>c.<>9__4_1 == null)
            {
                Func<ClientParameter, bool> local1 = <>c.<>9__4_1;
                predicate = <>c.<>9__4_1 = x => x.IsFilteredLookUpSettings;
            }
            if (!this.parametersContainer.ClientParameters.Reverse<ClientParameter>().TakeWhile<ClientParameter>(x => !ReferenceEquals(x.OriginalParameter, changedParameter)).Any<ClientParameter>(predicate))
            {
                asyncState.SetResult(new ParameterLookUpValuesContainer[0]);
                return asyncState.Task;
            }
            IReportServiceClient client = this.getActualClient();
            if (client == null)
            {
                asyncState.SetException(new InvalidOperationException("IReportServiceClient instance does not initialized."));
                return asyncState.Task;
            }
            ReportParameter[] en = this.parametersContainer.ToParameterStubs();
            try
            {
                en.ForEach<ReportParameter>(delegate (ReportParameter x) {
                    x.Value = this.GetActualEditorValue(x, editorValueProvider);
                });
            }
            catch (Exception exception1)
            {
                asyncState.SetException(exception1);
                return asyncState.Task;
            }
            ClientParameterContainer parametersContainer = this.parametersContainer;
            Func<ClientParameter, bool> func2 = <>c.<>9__4_4;
            if (<>c.<>9__4_4 == null)
            {
                Func<ClientParameter, bool> local2 = <>c.<>9__4_4;
                func2 = <>c.<>9__4_4 = x => x.IsFilteredLookUpSettings;
            }
            Func<ClientParameter, string> selector = <>c.<>9__4_5;
            if (<>c.<>9__4_5 == null)
            {
                Func<ClientParameter, string> local3 = <>c.<>9__4_5;
                selector = <>c.<>9__4_5 = x => x.Path;
            }
            client.GetLookUpValuesCompleted += new EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>>(this.OnGetLookUpValuesCompleted);
            client.GetLookUpValuesAsync(this.reportIdentity, en, this.parametersContainer.Cast<ClientParameter>().Reverse<ClientParameter>().TakeWhile<ClientParameter>(x => !ReferenceEquals(x.OriginalParameter, changedParameter)).Where<ClientParameter>(func2).Select<ClientParameter, string>(selector).ToArray<string>(), asyncState);
            return asyncState.Task;
        }

        private void OnGetLookUpValuesCompleted(object sender, ScalarOperationCompletedEventArgs<ParameterLookUpValues[]> e)
        {
            (sender as IReportServiceClient).GetLookUpValuesCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>>(this.OnGetLookUpValuesCompleted);
            TaskCompletionSource<IEnumerable<ParameterLookUpValuesContainer>> userState = e.UserState as TaskCompletionSource<IEnumerable<ParameterLookUpValuesContainer>>;
            if (!userState.Task.IsCompleted && !userState.Task.IsFaulted)
            {
                if (e.Error != null)
                {
                    userState.SetException(e.Error);
                }
                else
                {
                    userState.SetResult(from x in e.Result select new ParameterLookUpValuesContainer(((ClientParameter) this.parametersContainer[x.Path]).OriginalParameter, x.LookUpValues));
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RemoteLookUpValuesProvider.<>c <>9 = new RemoteLookUpValuesProvider.<>c();
            public static Func<ClientParameter, bool> <>9__4_1;
            public static Func<ClientParameter, bool> <>9__4_4;
            public static Func<ClientParameter, string> <>9__4_5;

            internal bool <GetLookUpValues>b__4_1(ClientParameter x) => 
                x.IsFilteredLookUpSettings;

            internal bool <GetLookUpValues>b__4_4(ClientParameter x) => 
                x.IsFilteredLookUpSettings;

            internal string <GetLookUpValues>b__4_5(ClientParameter x) => 
                x.Path;
        }
    }
}

