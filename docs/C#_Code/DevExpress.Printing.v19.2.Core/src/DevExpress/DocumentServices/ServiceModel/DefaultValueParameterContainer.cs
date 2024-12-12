namespace DevExpress.DocumentServices.ServiceModel
{
    using DevExpress.DocumentServices.ServiceModel.Native;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DefaultValueParameterContainer : IParameterContainer, IEnumerable<IClientParameter>, IEnumerable
    {
        private readonly List<DefaultValueParameter> parameters = new List<DefaultValueParameter>(0);

        public void CopyFrom(ClientParameterContainer container)
        {
            foreach (ClientParameter parameter in container)
            {
                DefaultValueParameter item = new DefaultValueParameter(parameter.Path);
                item.CopyFrom(parameter);
                this.parameters.Add(item);
            }
        }

        public bool CopyTo(ClientParameterContainer reportParameters, out Exception error)
        {
            error = null;
            List<string> source = new List<string>(0);
            foreach (DefaultValueParameter parameter in this.parameters)
            {
                IClientParameter parameter2 = reportParameters[parameter.Path];
                if (parameter2 == null)
                {
                    source.Add(parameter.Path);
                    continue;
                }
                parameter.CopyTo(parameter2);
            }
            if (source.Count > 0)
            {
                Func<string, string> selector = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<string, string> local1 = <>c.<>9__2_0;
                    selector = <>c.<>9__2_0 = path => $"'{path}'";
                }
                IEnumerable<string> values = source.Select<string, string>(selector);
                error = new Exception(string.Format(PreviewLocalizer.GetString(PreviewStringId.Msg_NoParameters), string.Join(", ", values)));
            }
            return (error == null);
        }

        public IEnumerator<IClientParameter> GetEnumerator() => 
            (IEnumerator<IClientParameter>) this.parameters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public int Count =>
            this.parameters.Count;

        public IClientParameter this[string path]
        {
            get
            {
                DefaultValueParameter item = this.parameters.FirstOrDefault<DefaultValueParameter>(p => p.Path == path);
                if (item == null)
                {
                    item = new DefaultValueParameter(path);
                    this.parameters.Add(item);
                }
                return item;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultValueParameterContainer.<>c <>9 = new DefaultValueParameterContainer.<>c();
            public static Func<string, string> <>9__2_0;

            internal string <CopyTo>b__2_0(string path) => 
                $"'{path}'";
        }
    }
}

