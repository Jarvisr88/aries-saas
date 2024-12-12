namespace DevExpress.Utils.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public class ParseFilterCriteriaEventArgs : EventArgs
    {
        private readonly string path;
        private readonly IValueViewModel value;
        private CriteriaOperator criteria;
        internal bool HasResult;
        private static readonly object[] NullValue = new object[1];

        protected ParseFilterCriteriaEventArgs(string path, IValueViewModel value)
        {
            this.path = path;
            this.value = value;
        }

        [DebuggerStepThrough]
        internal DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs Initialize(CriteriaOperator criteria)
        {
            this.criteria = criteria;
            return this;
        }

        [DebuggerStepThrough]
        public bool SetBlanks() => 
            this.SetResult(NullValue, false);

        [DebuggerStepThrough]
        public bool SetEmpty() => 
            this.SetResult(NullValue, false);

        [DebuggerStepThrough]
        public bool SetNotBlanks() => 
            this.SetResult(NullValue, true);

        [DebuggerStepThrough]
        public bool SetNotEmpty() => 
            this.SetResult(NullValue, true);

        [DebuggerStepThrough]
        public bool SetNotNull() => 
            this.SetResult(NullValue, true);

        [DebuggerStepThrough]
        public bool SetNull() => 
            this.SetResult(NullValue, false);

        [DebuggerStepThrough]
        public bool SetRange(object from, object to)
        {
            int num = ValueComparer.Default.Compare(from, to);
            if (num == 0)
            {
                object[] objArray1 = new object[] { from };
                return this.SetResult(objArray1, false);
            }
            if (num > 0)
            {
                object[] objArray2 = new object[] { to, from };
                return this.SetResult(objArray2, false);
            }
            object[] values = new object[] { from, to };
            return this.SetResult(values, false);
        }

        [DebuggerStepThrough]
        public bool SetRange<T>(T from, T to) => 
            this.SetRange<T>(from, to);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetResult(object[] values, bool useInversion = false)
        {
            this.HasResult = (this.value as ValueViewModel).Get<ValueViewModel, bool>(x => x.TryInitializeFromParse(values, useInversion), false);
            return this.HasResult;
        }

        [DebuggerStepThrough]
        public bool SetValue<T>(T value)
        {
            object[] values = new object[] { value };
            return this.SetResult(values, false);
        }

        [DebuggerStepThrough]
        public bool SetValue(object value, bool useInversion = false)
        {
            object[] values = new object[] { value };
            return this.SetResult(values, useInversion);
        }

        [DebuggerStepThrough]
        public bool SetValues<T>(IReadOnlyCollection<T> values)
        {
            object[] objArray = new object[values.Count + 1];
            int num = 0;
            foreach (object obj2 in values)
            {
                objArray[num++] = obj2;
            }
            return this.SetResult(objArray, false);
        }

        [DebuggerStepThrough]
        public bool SetValues(object[] values) => 
            this.SetResult(values, false);

        [DebuggerStepThrough]
        public bool SetValuesOrBlanks<T>(IReadOnlyCollection<T> values)
        {
            object[] objArray = new object[] { NullValue[0] };
            int num = 1;
            foreach (object obj2 in values)
            {
                objArray[num++] = obj2;
            }
            return this.SetResult(objArray, false);
        }

        [DebuggerStepThrough]
        public bool SetValuesOrBlanks(object[] values)
        {
            object[] array = new object[] { NullValue[0] };
            values.CopyTo(array, 1);
            return this.SetResult(array, false);
        }

        public string Path =>
            this.path;

        public IValueViewModel Value =>
            this.value;

        public CriteriaOperator FilterCriteria =>
            this.criteria;
    }
}

