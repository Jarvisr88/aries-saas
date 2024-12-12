namespace DevExpress.DocumentServices.ServiceModel.Native
{
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ClientParameterContainer : IParameterContainer, IEnumerable<IClientParameter>, IEnumerable
    {
        private readonly ReadOnlyCollection<ClientParameter> parameters;

        internal event EventHandler ParameterValueChanged;

        public ClientParameterContainer() : this(new ClientParameter[0])
        {
        }

        public ClientParameterContainer(ReportParameterContainer container) : this(ToClientParameters(container))
        {
        }

        internal ClientParameterContainer(IList<ClientParameter> parameters)
        {
            this.parameters = new ReadOnlyCollection<ClientParameter>(parameters);
            ArrayHelper.ForEach<ClientParameter>(parameters, delegate (ClientParameter p) {
                p.ValueChanged += (o, e) => this.RaiseParameterValueChanged();
            });
        }

        public IEnumerator<IClientParameter> GetEnumerator() => 
            (IEnumerator<IClientParameter>) this.parameters.GetEnumerator();

        private void RaiseParameterValueChanged()
        {
            if (this.ParameterValueChanged != null)
            {
                this.ParameterValueChanged(this, EventArgs.Empty);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        private static IList<ClientParameter> ToClientParameters(ReportParameterContainer container)
        {
            List<ClientParameter> list = new List<ClientParameter>();
            foreach (ReportParameter parameter in container.Parameters)
            {
                ValueSourceSettings settings = null;
                if (parameter.LookUpValues != null)
                {
                    StaticListLookUpSettings settings2 = new StaticListLookUpSettings();
                    foreach (LookUpValue value2 in parameter.LookUpValues)
                    {
                        settings2.LookUpValues.Add(value2);
                    }
                    settings = settings2;
                }
                if ((settings == null) && (parameter.Value is IRange))
                {
                    IRange range = (IRange) parameter.Value;
                    RangeParametersSettings settings1 = new RangeParametersSettings();
                    settings1.StartParameter.Value = range.Start;
                    settings1.EndParameter.Value = range.End;
                    settings = settings1;
                }
                Type type = ParameterValueTypeHelper.GetValueType(parameter.Value, parameter.MultiValue, parameter.TypeName);
                Parameter parameter1 = new Parameter();
                parameter1.Description = parameter.Description;
                parameter1.Name = parameter.Name;
                parameter1.Visible = parameter.Visible;
                parameter1.Type = type;
                parameter1.MultiValue = parameter.MultiValue;
                parameter1.AllowNull = parameter.AllowNull;
                parameter1.ValueSourceSettings = settings;
                parameter1.Value = parameter.Value;
                ClientParameter item = new ClientParameter(parameter1, parameter.Path);
                item.IsFilteredLookUpSettings = parameter.IsFilteredLookUpSettings;
                list.Add(item);
            }
            return list;
        }

        internal IEnumerable<Parameter> OriginalParameters
        {
            get
            {
                Func<ClientParameter, Parameter> selector = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<ClientParameter, Parameter> local1 = <>c.<>9__5_0;
                    selector = <>c.<>9__5_0 = x => x.OriginalParameter;
                }
                return this.parameters.Select<ClientParameter, Parameter>(selector);
            }
        }

        internal IEnumerable<ClientParameter> ClientParameters =>
            this.parameters;

        public int Count =>
            this.parameters.Count;

        public IClientParameter this[string path] =>
            this.parameters.FirstOrDefault<ClientParameter>(cp => cp.Path == path);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ClientParameterContainer.<>c <>9 = new ClientParameterContainer.<>c();
            public static Func<ClientParameter, Parameter> <>9__5_0;

            internal Parameter <get_OriginalParameters>b__5_0(ClientParameter x) => 
                x.OriginalParameter;
        }
    }
}

