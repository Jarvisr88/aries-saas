namespace Devart.Common
{
    using Devart.Data;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Windows.Forms;

    internal class z : IDisposable
    {
        private Control a;
        private string[] b;
        private IListSource c;
        private object d;
        private object e;
        private CurrencyManager f;
        private o g;
        private ListChangedEventHandler h;
        private EventHandler i;
        private EventHandler j;

        public z(DbDataTable A_0, Control A_1, o A_2)
        {
            this.i = new EventHandler(this.b);
            this.j = new EventHandler(this.b);
            this.h = new ListChangedEventHandler(this.a);
            this.g = A_2;
            this.a = A_1;
            this.b((IListSource) A_0);
        }

        private CurrencyManager a()
        {
            ICurrencyManagerProvider c = this.c as ICurrencyManagerProvider;
            return ((c == null) ? (((this.c == null) || !(this.d is Control)) ? null : ((CurrencyManager) ((Control) this.d).BindingContext[this.c])) : c.CurrencyManager);
        }

        private void a(bool A_0)
        {
            this.g = null;
            this.d();
            if (this.c != null)
            {
                this.a(this.h);
            }
        }

        private static object a(IListSource A_0)
        {
            DbDataTable table = A_0 as DbDataTable;
            return table?.Owner;
        }

        public void a(string[] A_0)
        {
            this.b = A_0;
            this.c();
        }

        private void a(ListChangedEventHandler A_0)
        {
            if (this.c != null)
            {
                DbDataTable c = this.c as DbDataTable;
                if (c != null)
                {
                    ((IBindingList) ((IListSource) c).GetList()).ListChanged -= A_0;
                }
                else
                {
                    DataLink link = this.c as DataLink;
                    if (link != null)
                    {
                        link.ListChanged -= A_0;
                    }
                }
            }
        }

        private bool a(object A_0)
        {
            if ((A_0 != null) || (this.e != null))
            {
                if ((A_0 == null) || (this.e == null))
                {
                    return false;
                }
                if ((A_0 == DBNull.Value) && (this.e == DBNull.Value))
                {
                    return true;
                }
                object[] objArray = A_0 as object[];
                object[] e = this.e as object[];
                if ((objArray == null) || (e == null))
                {
                    return A_0.Equals(this.e);
                }
                if (objArray.Length != e.Length)
                {
                    return false;
                }
                for (int i = 0; i < objArray.Length; i++)
                {
                    if (objArray[i] != e[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void a(object A_0, ListChangedEventArgs A_1)
        {
            bool flag = false;
            switch (A_1.ListChangedType)
            {
                case ListChangedType.Reset:
                case ListChangedType.ItemDeleted:
                    flag = true;
                    break;

                case ListChangedType.ItemAdded:
                case ListChangedType.ItemChanged:
                    if (A_1.NewIndex == this.f.Position)
                    {
                        flag = true;
                    }
                    break;

                case ListChangedType.PropertyDescriptorChanged:
                    if (A_1.PropertyDescriptor != null)
                    {
                        for (int i = 0; i < this.b.Length; i++)
                        {
                            if (string.Compare(A_1.PropertyDescriptor.Name, this.b[i], true) == 0)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
            if (flag)
            {
                this.f();
            }
        }

        private void a(object A_0, EventArgs A_1)
        {
            this.e();
            this.f();
        }

        private bool b() => 
            !(this.c is ICurrencyManagerProvider) ? ((this.c != null) && ((this.d != null) && !((Control) this.d).IsDisposed)) : true;

        public void b(IListSource A_0)
        {
            if (!ReferenceEquals(this.c, A_0) || ((A_0 != null) && (a(A_0) != this.d)))
            {
                this.a(this.h);
                this.c = A_0;
                this.d = null;
                if (A_0 != null)
                {
                    this.d = a(A_0);
                }
                this.b(this.h);
                this.f();
            }
        }

        private void b(ListChangedEventHandler A_0)
        {
            if (this.c != null)
            {
                DbDataTable c = this.c as DbDataTable;
                if (c != null)
                {
                    ((IBindingList) ((IListSource) c).GetList()).ListChanged += A_0;
                }
                else
                {
                    DataLink link = this.c as DataLink;
                    if (link != null)
                    {
                        link.ListChanged += A_0;
                    }
                }
            }
        }

        public void b(object A_0)
        {
            if (!ReferenceEquals(this.a, A_0 as Control))
            {
                this.a = A_0 as Control;
                this.f();
            }
        }

        private void b(object A_0, EventArgs A_1)
        {
        }

        private object c()
        {
            object obj2 = null;
            if ((this.f != null) && ((this.f.Position >= 0) && ((this.b != null) && (this.b.Length != 0))))
            {
                DbDataRowView current = this.f.Current as DbDataRowView;
                if (current != null)
                {
                    DataRow row = current.Row;
                    if (this.b.Length > 1)
                    {
                        obj2 = new object[this.b.Length];
                    }
                    for (int i = 0; i < this.b.Length; i++)
                    {
                        if (this.b.Length > 1)
                        {
                            ((object[]) obj2)[i] = row[this.b[i]];
                        }
                        else
                        {
                            obj2 = row[this.b[i]];
                        }
                    }
                }
            }
            return obj2;
        }

        private void d()
        {
            if (this.f != null)
            {
                this.f.CurrentChanged -= new EventHandler(this.a);
                this.f.PositionChanged -= this.i;
                this.f.MetaDataChanged -= this.j;
            }
        }

        private bool e()
        {
            if ((this.a == null) || this.a.IsDisposed)
            {
                this.d();
                return false;
            }
            if (this.b())
            {
                return true;
            }
            this.d();
            return false;
        }

        public void f()
        {
            if ((this.c != null) && (a(this.c) != this.d))
            {
                this.b(this.c);
            }
            if ((this.a == null) || !this.b())
            {
                this.e = null;
                this.d();
            }
            else
            {
                this.d();
                this.f = this.a();
                if (this.f != null)
                {
                    this.f.CurrentChanged += new EventHandler(this.a);
                    this.f.PositionChanged += this.i;
                    this.f.MetaDataChanged += this.j;
                }
                object obj2 = null;
                if (this.g != null)
                {
                    obj2 = this.c();
                    if (!this.a(obj2))
                    {
                        this.e = obj2;
                        this.g(this.c, this.f.Position);
                    }
                }
            }
        }

        private void g()
        {
            this.a(true);
            GC.SuppressFinalize(this);
        }

        internal object h() => 
            this.d;

        protected override void i()
        {
            try
            {
                this.a(false);
            }
            finally
            {
                base.Finalize();
            }
        }

        public int j() => 
            (this.f == null) ? -1 : this.f.Position;

        public string[] k() => 
            this.b;
    }
}

