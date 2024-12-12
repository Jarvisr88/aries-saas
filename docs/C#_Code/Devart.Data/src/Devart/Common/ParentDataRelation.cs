namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    [TypeConverter(typeof(q))]
    public class ParentDataRelation
    {
        private IListSource a;
        private DbDataTable b;
        private string[] c;
        private string[] d;

        internal ParentDataRelation()
        {
        }

        public ParentDataRelation(IListSource parentTable, string[] parentColumnNames, string[] childColumnNames)
        {
            this.a = parentTable;
            this.c = parentColumnNames;
            this.d = childColumnNames;
        }

        internal void a()
        {
            if ((this.ChildColumnNames == null) || (this.ChildColumnNames.Length == 0))
            {
                throw new ArgumentException("The ChildColumnNames property is not set in the ParentDataRelation object.");
            }
            if ((this.ParentColumnNames == null) || (this.ParentColumnNames.Length != this.ChildColumnNames.Length))
            {
                throw new ArgumentException("The ParentColumnNames property is not set in the ParentDataRelation object.");
            }
            if (this.ParentTable == null)
            {
                throw new ArgumentException("The ParentTable property is not set in the ParentDataRelation object.");
            }
            this.b.z.b((IListSource) this.ParentTable);
        }

        internal static bool a(ParentDataRelation A_0) => 
            (A_0 != null) && (A_0.ParentTable != null);

        private static bool a(string[] A_0, string A_1)
        {
            for (int i = 0; i < A_0.Length; i++)
            {
                if (string.Compare(A_0[i], A_1, true, CultureInfo.InvariantCulture) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        internal void ResetChildColumnNames()
        {
            this.ChildColumnNames = null;
        }

        internal void ResetParentColumnNames()
        {
            this.ParentColumnNames = null;
        }

        internal void ResetParentTable()
        {
            this.ParentTable = null;
        }

        [RefreshProperties(RefreshProperties.Repaint), DefaultValue((string) null), Editor("Devart.Common.Design.ParentDataRelationParentTableEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public DbDataTable ParentTable
        {
            get => 
                (DbDataTable) this.a;
            set
            {
                if (!ReferenceEquals(this.a, value))
                {
                    if ((this.b != null) && (this.a is ISupportInitializeNotification))
                    {
                        ((ISupportInitializeNotification) this.a).Initialized -= this.b.o;
                    }
                    this.a = value;
                    if ((this.b != null) && (this.a is ISupportInitializeNotification))
                    {
                        ((ISupportInitializeNotification) this.a).Initialized += this.b.o;
                    }
                    if (this.b != null)
                    {
                        this.b.z.b((IListSource) this.ParentTable);
                    }
                }
            }
        }

        internal DbDataTable ChildTableInternal
        {
            set
            {
                if (!ReferenceEquals(this.b, value))
                {
                    if (value == null)
                    {
                        this.b.z.b((IListSource) null);
                    }
                    if ((this.b != null) && (this.a is ISupportInitializeNotification))
                    {
                        ((ISupportInitializeNotification) this.a).Initialized -= this.b.o;
                    }
                    this.b = value;
                    if ((this.b != null) && (this.a is ISupportInitializeNotification))
                    {
                        ((ISupportInitializeNotification) this.a).Initialized += this.b.o;
                    }
                    if (this.b != null)
                    {
                        this.b.z.b((IListSource) this.ParentTable);
                        this.b.z.a(this.ParentColumnNames);
                    }
                }
            }
        }

        internal DbDataTable ChildTable =>
            this.b;

        [RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(f)), Editor("Devart.Common.Design.ParentDataRelationParentColumnsNamesEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string[] ParentColumnNames
        {
            get => 
                this.c;
            set
            {
                this.c = value;
                if (this.b != null)
                {
                    this.b.z.a(value);
                }
            }
        }

        [TypeConverter(typeof(f)), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Common.Design.ParentDataRelationChildColumnsNamesEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string[] ChildColumnNames
        {
            get => 
                this.d;
            set => 
                this.d = value;
        }
    }
}

