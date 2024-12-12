namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    [ListBindable(false)]
    public class DbLoaderColumnCollection : CollectionBase, IList
    {
        private int a()
        {
            int num2 = 1;
            while (true)
            {
                bool flag = false;
                int num = 0;
                while (true)
                {
                    if (num < base.List.Count)
                    {
                        if (((DbLoaderColumn) base.List[num]).Name != ("Column" + num2.ToString()))
                        {
                            num++;
                            continue;
                        }
                        flag = true;
                        num2++;
                    }
                    if (flag)
                    {
                        break;
                    }
                    return num2;
                }
            }
        }

        private int a(string A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException("name");
            }
            int num = -1;
            int num2 = 0;
            while (true)
            {
                if (num2 < base.List.Count)
                {
                    if (!Utils.Compare(A_0, this[num2].Name, false))
                    {
                        num2++;
                        continue;
                    }
                    num = num2;
                }
                if (num > -1)
                {
                    return num;
                }
                for (int i = 0; i < base.List.Count; i++)
                {
                    if (Utils.Compare(A_0, this[i].Name, true))
                    {
                        if (num != -1)
                        {
                            num = -2;
                            break;
                        }
                        num = i;
                    }
                }
                break;
            }
            return num;
        }

        public int Add(DbLoaderColumn value) => 
            base.List.Add(value);

        public bool Contains(DbLoaderColumn value) => 
            base.List.Contains(value);

        public bool Contains(string name)
        {
            for (int i = 0; i < base.List.Count; i++)
            {
                if (string.Compare(((DbLoaderColumn) base.List[i]).Name, name, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(DbLoaderColumn[] array, int index)
        {
            base.List.CopyTo(array, index);
        }

        public int IndexOf(DbLoaderColumn value) => 
            base.List.IndexOf(value);

        public int IndexOf(string name)
        {
            int num1 = this.a(name);
            if (num1 == -2)
            {
                throw new ArgumentException(string.Format(g.a("DbLoader_TwoColunmsExist"), name));
            }
            return num1;
        }

        public void Insert(int index, DbLoaderColumn value)
        {
            base.List.Insert(index, value);
        }

        protected override void OnInsert(int index, object value)
        {
            DbLoaderColumn column = (DbLoaderColumn) value;
            if (Utils.IsEmpty(column.Name))
            {
                column.Name = "Column" + this.a();
            }
        }

        public void Remove(DbLoaderColumn value)
        {
            base.List.Remove(value);
        }

        public DbLoaderColumn this[int index]
        {
            get => 
                (DbLoaderColumn) base.List[index];
            set => 
                base.List[index] = value;
        }

        public DbLoaderColumn this[string name]
        {
            get
            {
                int index = this.IndexOf(name);
                return ((index >= 0) ? ((DbLoaderColumn) base.List[index]) : null);
            }
            set
            {
                int index = this.IndexOf(name);
                if (index >= 0)
                {
                    base.List[index] = value;
                }
            }
        }
    }
}

