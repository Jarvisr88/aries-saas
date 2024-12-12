namespace Devart.Data
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Windows.Forms;

    [DesignerSerializer("Devart.Common.Design.DataLinkSerializer, Devart.Data.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), y("DataLink_Description"), ToolboxItem(typeof(DataLinkToolboxItem)), DesignTimeVisible(true), DefaultEvent("CurrentChanged"), Designer("Devart.Common.Design.DataLinkDesigner, Devart.Data.Design")]
    public class DataLink : BindingSource, IListSource
    {
        private object a;
        private object b;
        private DbDataTable c;
        private DataLink d;
        private string e;
        private object f;
        private Devart.Common.r g;
        private DbDataTableView h;
        private CurrencyManager i;
        private bool j;
        private CurrencyManager k;
        private EventHandler l;
        private EventHandler m;
        private EventHandler n;
        private EventHandler o;
        private ListChangedEventHandler p;
        private EventHandler q;
        private bool r;
        private bool s;
        private bool t;
        private bool u;
        private static readonly object v = new object();
        private static readonly object w = new object();
        private static readonly object x = new object();
        private static readonly object y = new object();
        private bool shouldSerializeDataSource;
        private bool z;
        private DbDataTableView aa;
        private string ab;

        internal event EventHandler LinkChanged
        {
            add
            {
                this.q += A_0;
            }
            remove
            {
                this.q -= A_0;
            }
        }

        public DataLink()
        {
            this.l = new EventHandler(this.e);
            this.m = new EventHandler(this.d);
            this.n = new EventHandler(this.c);
            this.o = new EventHandler(this.b);
            this.p = new ListChangedEventHandler(this.a);
            this.shouldSerializeDataSource = false;
        }

        private bool a(DbDataTableView A_0) => 
            (A_0 != null) && ((A_0.u != null) && (A_0.u.ListChangedType == ListChangedType.ItemMoved));

        private void a(object A_0, ListChangedEventArgs A_1)
        {
            if (!this.s)
            {
                this.s = true;
                this.z = true;
                try
                {
                    if (A_1.ListChangedType == ListChangedType.Reset)
                    {
                        this.d();
                    }
                }
                finally
                {
                    this.s = false;
                    this.z = false;
                }
            }
        }

        private void a(object A_0, EventArgs A_1)
        {
            this.d();
        }

        public override object AddNew()
        {
            if (this.h != null)
            {
                DbDataTable dataTable = this.h.DataTable;
                if ((dataTable != null) && !dataTable.FetchComplete)
                {
                    dataTable.b(true);
                }
            }
            return base.AddNew();
        }

        private void b(object A_0, EventArgs A_1)
        {
            if (!this.r && this.f())
            {
                this.r = true;
                try
                {
                }
                finally
                {
                    this.r = false;
                }
            }
        }

        private void c()
        {
            if (this.c != null)
            {
                this.c.a(new EventHandler(this.f));
            }
            this.c = null;
            this.b = null;
            this.h = null;
            ITypedList list = DbDataTable.a(this.a, this.e, out this.b);
            Devart.Common.r r = list as Devart.Common.r;
            if (r != null)
            {
                list = r.h();
            }
            this.h = list as DbDataTableView;
            if (this.h != null)
            {
                this.c = this.h.f;
                if (this.c != null)
                {
                    this.c.b(new EventHandler(this.f));
                }
            }
        }

        private void c(object A_0, EventArgs A_1)
        {
            if (this.t && (!this.r && this.f()))
            {
                this.r = true;
                try
                {
                    if (((this.k != null) && ((this.i != null) && ((this.i.Position != this.k.Position) && (this.k.Position < this.i.Count)))) && !this.a(this.DataTableView.h()))
                    {
                        this.i.Position = this.k.Position;
                    }
                }
                finally
                {
                    this.r = false;
                }
            }
        }

        private void d()
        {
            this.c();
            this.e();
            this.i = null;
            if (this.a != null)
            {
                if (this.b is Control)
                {
                    BindingContext bindingContext = ((Control) this.b).BindingContext;
                    if (this.h != null)
                    {
                        this.i = (CurrencyManager) bindingContext[this.a, this.DataMember];
                        this.j = false;
                    }
                }
                if ((this.i == null) && (this.h != null))
                {
                    this.i = this.h.CurrencyManager;
                    this.j = true;
                }
                if (this.i != null)
                {
                    this.i.CurrentChanged += this.l;
                    this.i.ListChanged += this.p;
                    this.i.MetaDataChanged += this.m;
                }
            }
            this.k = null;
            if (this.a != null)
            {
                this.k = this.CurrencyManager;
                if (this.k != null)
                {
                    this.k.MetaDataChanged += this.o;
                }
            }
            bool r = this.r;
            bool s = this.s;
            this.r = true;
            this.s = true;
            try
            {
                if (this.h != null)
                {
                    this.DataTableView.c(this.h);
                }
                else
                {
                    this.DataTableView.c(this.NullDataTableView);
                }
            }
            finally
            {
                this.r = r;
                this.s = s;
            }
            if ((this.k != null) && (this.i != null))
            {
                this.i.PositionChanged += this.l;
                this.k.PositionChanged += this.n;
                if (this.t && ((this.k.Position != this.i.Position) && (this.i.Position < this.k.Count)))
                {
                    this.k.Position = this.i.Position;
                }
            }
        }

        private void d(object A_0, EventArgs A_1)
        {
            if (!this.r && this.f())
            {
                this.r = true;
                try
                {
                    this.d();
                }
                finally
                {
                    this.r = false;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.e();
            if (this.c != null)
            {
                this.c.a(new EventHandler(this.f));
            }
            base.Dispose(disposing);
        }

        private void e()
        {
            if (this.i != null)
            {
                this.i.PositionChanged -= this.l;
                this.i.CurrentChanged -= this.l;
                this.i.ListChanged -= this.p;
                this.i.MetaDataChanged -= this.m;
            }
            if (this.k != null)
            {
                this.k.PositionChanged -= this.n;
                this.k.MetaDataChanged -= this.o;
            }
        }

        private void e(object A_0, EventArgs A_1)
        {
            if (this.t && (!this.r && this.f()))
            {
                this.r = true;
                try
                {
                    if (((this.k != null) && ((this.i != null) && ((this.i.Position != this.k.Position) && (this.i.Position < this.k.Count)))) && !this.a(this.h))
                    {
                        this.k.Position = this.i.Position;
                    }
                }
                finally
                {
                    this.r = false;
                }
            }
        }

        private bool f()
        {
            if ((this.Owner == null) || ((Control) this.Owner).IsDisposed)
            {
                this.e();
                return false;
            }
            if ((this.a != null) && (this.j || ((this.b != null) && !((Control) this.b).IsDisposed)))
            {
                return true;
            }
            this.e();
            return false;
        }

        internal void f(object A_0, EventArgs A_1)
        {
            this.d();
        }

        ~DataLink()
        {
            this.Dispose(false);
        }

        public int Find(string propertyName, object key)
        {
            if (this.g == null)
            {
                return base.Find(propertyName, key);
            }
            PropertyDescriptor prop = ((ITypedList) this.g).GetItemProperties(null)?.Find(propertyName, true);
            if (prop == null)
            {
                throw new ArgumentException($"DataMember property '{propertyName}' cannot be found on the DataSource.");
            }
            return this.Find(prop, key);
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (!this.z)
            {
                this.z = true;
                try
                {
                    if (e.ListChangedType == ListChangedType.Reset)
                    {
                        this.f(this, e);
                    }
                    base.OnListChanged(e);
                }
                finally
                {
                    this.z = false;
                }
            }
        }

        private bool ShouldSerializeDataSource() => 
            this.shouldSerializeDataSource || (this.DataSource != null);

        IList IListSource.GetList() => 
            this.DataTableView;

        [Browsable(false)]
        public IList List =>
            this.DataTableView.h();

        [DefaultValue(""), Editor("Devart.Common.Design.DataLinkDataMemberEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), MergableProperty(false), y("DataLink_DataMember")]
        public string DataMember
        {
            get => 
                (this.e != null) ? this.e : string.Empty;
            set
            {
                if (this.e != value)
                {
                    this.e = value;
                    this.d();
                    this.OnDataMemberChanged(EventArgs.Empty);
                }
            }
        }

        [y("DataLink_Synchronized"), DefaultValue(false)]
        public bool Synchronized
        {
            get => 
                this.t;
            set
            {
                if (this.t != value)
                {
                    this.t = value;
                    this.d();
                }
            }
        }

        [y("DataLink_DataSource"), Editor("Devart.Common.Design.DataLinkLinkEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), RefreshProperties(RefreshProperties.Repaint), MergableProperty(false), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design")]
        public object DataSource
        {
            get => 
                this.a;
            set
            {
                if (this.a != value)
                {
                    if (this.c != null)
                    {
                        this.c.a(new EventHandler(this.f));
                    }
                    if (this.d != null)
                    {
                        this.d.a(new EventHandler(this.a));
                    }
                    this.a = value;
                    this.b = null;
                    this.c = this.a as DbDataTable;
                    DbDataSet a = null;
                    this.h = null;
                    if ((this.c != null) && (this.DataMember == ""))
                    {
                        this.h = ((IListSource) this.c).GetList() as DbDataTableView;
                        this.b = this.c.Owner;
                        this.c.b(new EventHandler(this.f));
                    }
                    else
                    {
                        a = this.a as DbDataSet;
                        if (a != null)
                        {
                            this.b = a.Owner;
                        }
                        else
                        {
                            this.d = this.a as DataLink;
                            if (this.d != null)
                            {
                                this.b = this.d.Owner;
                                this.d.b(new EventHandler(this.a));
                            }
                        }
                    }
                    if ((value != null) && ((a == null) && ((this.c == null) && ((this.d == null) && (this.b == null)))))
                    {
                        this.a = null;
                        throw new ArgumentException(string.Format("Only values of {0}, {1}, {2} types are allowed in DataSource property", typeof(DbDataSet).Name, typeof(DbDataTable).Name));
                    }
                    base.DataSource = null;
                    base.DataSource = this.DataTableView;
                    this.d();
                    if (this.q != null)
                    {
                        this.q(this, new EventArgs());
                    }
                }
            }
        }

        bool IListSource.ContainsListCollection =>
            false;

        private DbDataTableView NullDataTableView
        {
            get
            {
                this.aa ??= new DbDataTableView(null, null);
                return this.aa;
            }
        }

        private Devart.Common.r DataTableView
        {
            get
            {
                this.g ??= ((this.h == null) ? new Devart.Common.r(this.NullDataTableView) : new Devart.Common.r(this.h));
                return this.g;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Position
        {
            get => 
                base.Position;
            set => 
                base.Position = value;
        }

        [DefaultValue(false), y("DataLink_DataMember")]
        public bool SeparateEditing
        {
            get => 
                this.u;
            set
            {
                if (this.u != value)
                {
                    this.u = value;
                    this.DataTableView.a(this.u);
                    if (!this.u)
                    {
                        this.d();
                    }
                }
            }
        }

        [DefaultValue(""), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
        {
            get => 
                (this.Site != null) ? this.Site.Name : ((this.ab == null) ? string.Empty : this.ab);
            set
            {
                if (this.Site == null)
                {
                    this.ab = (value == null) ? string.Empty : value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object Owner
        {
            get => 
                this.f;
            set
            {
                this.f = value;
                if (this.f != value)
                {
                    this.f(this, null);
                }
            }
        }

        public DbDataRowView CurrentRow =>
            base.Current as DbDataRowView;
    }
}

