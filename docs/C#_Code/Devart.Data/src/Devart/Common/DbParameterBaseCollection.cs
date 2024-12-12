namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data.Common;

    public abstract class DbParameterBaseCollection : DbParameterCollection
    {
        private ArrayList a;

        protected DbParameterBaseCollection()
        {
        }

        private void a(int A_0)
        {
            ArrayList innerList = this.InnerList;
            DbParameterBase base2 = (DbParameterBase) innerList[A_0];
            innerList.RemoveAt(A_0);
            base2.ResetParent();
        }

        private void a(int A_0, object A_1)
        {
            this.ValidateType(A_1);
            this.Validate(A_0, A_1);
            ArrayList innerList = this.InnerList;
            DbParameterBase base2 = (DbParameterBase) innerList[A_0];
            innerList[A_0] = (DbParameterBase) A_1;
            base2.ResetParent();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int Add(object value)
        {
            this.OnChange();
            this.ValidateType(value);
            if ((this.Parent != null) && this.Parent.ParameterCheck)
            {
                DbParameter parameter = (DbParameter) value;
                int index = this.IndexOf(parameter.ParameterName);
                if (index >= 0)
                {
                    this.SetParameterInParameterCheckMode(index, parameter);
                    return index;
                }
            }
            this.Validate(-1, value);
            this.InnerList.Add((DbParameterBase) value);
            return (this.Count - 1);
        }

        public override void AddRange(Array values)
        {
            Utils.CheckArgumentNull(values, "values");
            this.OnChange();
            foreach (object obj2 in values)
            {
                this.ValidateType(obj2);
            }
            foreach (DbParameter parameter in values)
            {
                this.Validate(-1, parameter);
                this.InnerList.Add((DbParameterBase) parameter);
            }
        }

        private void b(int A_0)
        {
            if ((A_0 < 0) || (A_0 >= this.Count))
            {
                throw new IndexOutOfRangeException(g.a("ParametersMappingIndex", A_0));
            }
        }

        protected int CheckName(string parameterName)
        {
            int index = this.IndexOf(parameterName);
            if (index < 0)
            {
                throw new ArgumentException(g.a("ParametersSourceIndex", parameterName));
            }
            return index;
        }

        public override void Clear()
        {
            this.OnChange();
            ArrayList a = this.a;
            if (a != null)
            {
                using (IEnumerator enumerator = a.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ((DbParameterBase) enumerator.Current).ResetParent();
                    }
                }
                a.Clear();
            }
        }

        public override bool Contains(object value) => 
            this.IndexOf(value) != -1;

        public override bool Contains(string value) => 
            this.IndexOf(value) != -1;

        public override void CopyTo(Array array, int index)
        {
            this.InnerList.CopyTo(array, index);
        }

        public override IEnumerator GetEnumerator() => 
            this.InnerList.GetEnumerator();

        protected override DbParameter GetParameter(int index)
        {
            this.b(index);
            return (DbParameter) this.InnerList[index];
        }

        protected override DbParameter GetParameter(string name) => 
            this.GetParameter(this.CheckName(name));

        public override int IndexOf(object value)
        {
            if (value != null)
            {
                this.ValidateType(value);
                ArrayList a = this.a;
                if (a != null)
                {
                    int count = a.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (value == a[i])
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        public override int IndexOf(string parameterName) => 
            IndexOf(this.a, parameterName);

        protected internal static int IndexOf(IEnumerable items, string parameterName)
        {
            int num2;
            if (items == null)
            {
                goto TR_0008;
            }
            else
            {
                IEnumerator enumerator;
                int num = 0;
                using (enumerator = items.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        DbParameter current = (DbParameter) enumerator.Current;
                        if (parameterName != current.ParameterName)
                        {
                            num++;
                            continue;
                        }
                        return num;
                    }
                }
                num = 0;
                using (enumerator = items.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            DbParameter current = (DbParameter) enumerator.Current;
                            if (string.Compare(parameterName, current.ParameterName, StringComparison.CurrentCultureIgnoreCase) != 0)
                            {
                                num++;
                                continue;
                            }
                            num2 = num;
                        }
                        else
                        {
                            goto TR_0008;
                        }
                        break;
                    }
                }
            }
            return num2;
        TR_0008:
            return -1;
        }

        public override void Insert(int index, object value)
        {
            this.OnChange();
            this.ValidateType(value);
            if ((this.Parent != null) && this.Parent.ParameterCheck)
            {
                DbParameter parameter = (DbParameter) value;
                int num = this.IndexOf(parameter.ParameterName);
                if (num >= 0)
                {
                    DbParameterBase base2 = (DbParameterBase) this.InnerList[num];
                    this.InnerList.Remove(base2);
                    this.InnerList.Insert(index, base2);
                    this.SetParameterInParameterCheckMode(index, parameter);
                    return;
                }
            }
            this.Validate(-1, value);
            this.InnerList.Insert(index, (DbParameterBase) value);
        }

        protected virtual void OnChange()
        {
        }

        public override void Remove(object value)
        {
            this.OnChange();
            this.ValidateType(value);
            int index = this.IndexOf(value);
            if (index != -1)
            {
                this.a(index);
            }
            else if (!ReferenceEquals(this, ((DbParameterBase) value).a(null, this)))
            {
                throw new InvalidOperationException(g.a("CollectionRemoveInvalidObject", this.ItemType, this));
            }
        }

        public override void RemoveAt(int index)
        {
            this.OnChange();
            this.b(index);
            this.a(index);
        }

        public override void RemoveAt(string parameterName)
        {
            this.OnChange();
            int num = this.CheckName(parameterName);
            this.a(num);
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            this.OnChange();
            this.b(index);
            this.a(index, value);
        }

        protected override void SetParameter(string name, DbParameter value)
        {
            this.SetParameter(this.CheckName(name), value);
        }

        protected virtual void SetParameterInParameterCheckMode(int index, DbParameter value)
        {
            this.SetParameter(index, value);
        }

        protected virtual void Validate(int index, object value)
        {
            Utils.CheckArgumentNull(value, "value");
            DbParameterBase base2 = (DbParameterBase) value;
            object obj2 = base2.a(this, null);
            if (obj2 != null)
            {
                if (obj2 != this)
                {
                    throw new ArgumentException(g.a("ParametersIsNotParent", base2.ParameterName));
                }
                if (this.IndexOf(value) != index)
                {
                    throw new ArgumentException(g.a("ParametersIsParent", base2.ParameterName));
                }
            }
            if (base2.ParameterName.Length == 0)
            {
                index = 1;
                while (true)
                {
                    string parameterName = this.ParameterNamePrefix + index.ToString();
                    index++;
                    if (this.IndexOf(parameterName) == -1)
                    {
                        base2.ParameterName = parameterName;
                        return;
                    }
                }
            }
        }

        protected virtual void ValidateType(object value)
        {
            Utils.CheckArgumentNull(value, "value");
            if (!this.ItemType.IsInstanceOfType(value))
            {
                throw new ArgumentException(g.a("InvalidParameterType"));
            }
        }

        public override int Count =>
            (this.a != null) ? this.a.Count : 0;

        private ArrayList InnerList
        {
            get
            {
                ArrayList a = this.a;
                if (a == null)
                {
                    a = new ArrayList();
                    this.a = a;
                }
                return a;
            }
        }

        public override bool IsFixedSize =>
            this.InnerList.IsFixedSize;

        public override bool IsReadOnly =>
            this.InnerList.IsReadOnly;

        public override bool IsSynchronized =>
            this.InnerList.IsSynchronized;

        protected abstract Type ItemType { get; }

        protected virtual string ParameterNamePrefix =>
            "Parameter";

        public override object SyncRoot =>
            this.InnerList.SyncRoot;

        protected abstract DbCommandBase Parent { get; }
    }
}

