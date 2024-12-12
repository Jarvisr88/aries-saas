namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [ListBindable(BindableSupport.No), TypeConverter(typeof(CollectionTypeConverter))]
    public class ParameterCollection : Collection<Parameter>, IEnumerable<IParameter>, IEnumerable
    {
        public void AddRange(Parameter[] parameters)
        {
            foreach (Parameter parameter in parameters)
            {
                base.Add(parameter);
            }
        }

        protected internal virtual bool Deserialize(string value, string typeName, out object result)
        {
            result = DBNull.Value;
            return false;
        }

        internal Parameter GetByName(string parameterName) => 
            this.AllParameters.FirstOrDefault<Parameter>(parameter => parameter.Name == parameterName);

        protected override void InsertItem(int index, Parameter item)
        {
            if (!base.Contains(item))
            {
                if (item is IRangeBoundaryParameter)
                {
                    throw new InvalidOperationException("Cannot add the parameter because it is a range's inner parameter. Use the RangeParametersSettings class instead.");
                }
                base.InsertItem(index, item);
                item.Owner = this;
            }
        }

        protected internal virtual string Serialize(object value) => 
            string.Empty;

        IEnumerator<IParameter> IEnumerable<IParameter>.GetEnumerator() => 
            base.Items.Cast<IParameter>().GetEnumerator();

        public Parameter this[string parameterName] =>
            this.GetByName(parameterName);

        internal virtual bool IsLoading =>
            false;

        internal IEnumerable<Parameter> AllParameters =>
            base.Items.GetAllParameters<Parameter>();

        internal IEnumerable<Parameter> ActualParameters =>
            base.Items.GetActualParameters<Parameter>();
    }
}

