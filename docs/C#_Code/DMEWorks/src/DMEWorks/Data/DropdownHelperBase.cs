namespace DMEWorks.Data
{
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public abstract class DropdownHelperBase
    {
        public readonly EventHandler EventClickNew;
        public readonly EventHandler EventClickEdit;
        public readonly InitDialogEventHandler EventInitDialog;
        private readonly Dictionary<string, DataTable> HashData;

        protected DropdownHelperBase()
        {
            this.EventClickNew = new EventHandler(this.ClickNew);
            this.EventClickEdit = new EventHandler(this.ClickEdit);
            this.EventInitDialog = new InitDialogEventHandler(this.InitDialog);
            this.HashData = new Dictionary<string, DataTable>(StringComparer.OrdinalIgnoreCase);
        }

        public virtual void AssignDatasource(Combobox ComboBox, DataTable table)
        {
            Functions.AssignDatasource(ComboBox, table, "Name", "ID");
        }

        public virtual void AssignDatasource(ExtendedDropdown ComboBox, DataTable table)
        {
            ComboBox.DataSource = table;
            ComboBox.TextMember = "Code";
        }

        public virtual void AssignDatasource(FindDialog dialog, DataTable table)
        {
            Functions.AssignDatasource(dialog, table, new string[0]);
        }

        public virtual void AssignDatasource(ComboBox ComboBox, DataTable table)
        {
            Functions.AssignDatasource(ComboBox, table, "Name", "ID");
        }

        public void ClearCachedData()
        {
            this.HashData.Clear();
        }

        public abstract void ClickEdit(object source, EventArgs args);
        public abstract void ClickNew(object source, EventArgs args);
        protected static FormParameters CreateHash_Edit(object source, string KeyField = "ID")
        {
            FormParameters parameters;
            Combobox combobox = source as Combobox;
            if ((combobox != null) && !combobox.IsDisposed)
            {
                FormParameters parameters1 = new FormParameters();
                parameters1[KeyField] = combobox.SelectedValue;
                parameters = parameters1;
            }
            else
            {
                ExtendedDropdown dropdown = source as ExtendedDropdown;
                if ((dropdown == null) || dropdown.IsDisposed)
                {
                    parameters = null;
                }
                else
                {
                    FormParameters parameters2 = new FormParameters();
                    parameters2[KeyField] = dropdown.Text;
                    parameters = parameters2;
                }
            }
            return parameters;
        }

        protected static FormParameters CreateHash_New(object source)
        {
            FormParameters parameters;
            Combobox combobox = source as Combobox;
            if ((combobox != null) && !combobox.IsDisposed)
            {
                FormParameters parameters1 = new FormParameters();
                parameters1["EntityCreatedListener"] = new Updater(combobox);
                parameters = parameters1;
            }
            else
            {
                ExtendedDropdown dropdown = source as ExtendedDropdown;
                if ((dropdown == null) || dropdown.IsDisposed)
                {
                    parameters = null;
                }
                else
                {
                    FormParameters parameters2 = new FormParameters();
                    parameters2["EntityCreatedListener"] = new Updater(dropdown);
                    parameters = parameters2;
                }
            }
            return parameters;
        }

        private DataTable GetCachedData(DMEWorks.Data.IFilter filter)
        {
            DataTable table = null;
            if (filter == null)
            {
                if (!this.HashData.TryGetValue(string.Empty, out table))
                {
                    table = this.GetTable();
                    this.HashData[string.Empty] = table;
                }
            }
            else if (!this.HashData.TryGetValue(filter.GetKey(), out table))
            {
                DataTable cachedData = this.GetCachedData(null);
                table = filter.Process(cachedData);
                this.HashData[filter.GetKey()] = table;
            }
            return table;
        }

        public abstract DataTable GetTable();
        public void InitDialog(FindDialog dialog, DMEWorks.Data.IFilter filter)
        {
            dialog.InitDialog -= this.EventInitDialog;
            this.AssignDatasource(dialog, this.GetCachedData(filter));
            dialog.InitDialog += this.EventInitDialog;
        }

        public abstract void InitDialog(object source, InitDialogEventArgs args);
        public void InitDropdown(Combobox dropdown, DMEWorks.Data.IFilter filter)
        {
            dropdown.ClickNew -= this.EventClickNew;
            dropdown.ClickEdit -= this.EventClickEdit;
            dropdown.InitDialog -= this.EventInitDialog;
            dropdown.DrawItem -= this.EventDrawItem;
            this.AssignDatasource(dropdown, this.GetCachedData(filter));
            dropdown.ClickNew += this.EventClickNew;
            dropdown.ClickEdit += this.EventClickEdit;
            dropdown.InitDialog += this.EventInitDialog;
            dropdown.DrawItem += this.EventDrawItem;
        }

        public void InitDropdown(ExtendedDropdown dropdown, DMEWorks.Data.IFilter filter)
        {
            dropdown.ClickNew -= this.EventClickNew;
            dropdown.ClickEdit -= this.EventClickEdit;
            dropdown.InitDialog -= this.EventInitDialog;
            this.AssignDatasource(dropdown, this.GetCachedData(filter));
            dropdown.ClickNew += this.EventClickNew;
            dropdown.ClickEdit += this.EventClickEdit;
            dropdown.InitDialog += this.EventInitDialog;
        }

        public void InitDropdown(ComboBox dropdown, DMEWorks.Data.IFilter filter)
        {
            this.AssignDatasource(dropdown, this.GetCachedData(filter));
        }

        public void PreloadData()
        {
            this.GetCachedData(null);
        }

        public virtual ComboboxDrawItemEventHandler EventDrawItem =>
            null;

        private class Updater : IEntityCreatedEventListener
        {
            private WeakReference Combobox;

            public event EventHandler DMEWorks.Core.IEntityCreatedEventListener.Unhook
            {
                [CompilerGenerated] add
                {
                    EventHandler unhookEvent = this.UnhookEvent;
                    while (true)
                    {
                        EventHandler comparand = unhookEvent;
                        EventHandler handler3 = comparand + obj;
                        unhookEvent = Interlocked.CompareExchange<EventHandler>(ref this.UnhookEvent, handler3, comparand);
                        if (ReferenceEquals(unhookEvent, comparand))
                        {
                            return;
                        }
                    }
                }
                [CompilerGenerated] remove
                {
                    EventHandler unhookEvent = this.UnhookEvent;
                    while (true)
                    {
                        EventHandler comparand = unhookEvent;
                        EventHandler handler3 = comparand - obj;
                        unhookEvent = Interlocked.CompareExchange<EventHandler>(ref this.UnhookEvent, handler3, comparand);
                        if (ReferenceEquals(unhookEvent, comparand))
                        {
                            return;
                        }
                    }
                }
            }

            public event EventHandler Unhook;

            public Updater(DMEWorks.Forms.Combobox Combobox)
            {
                this.Combobox = new WeakReference(Combobox, false);
            }

            public Updater(ExtendedDropdown Combobox)
            {
                this.Combobox = new WeakReference(Combobox, false);
            }

            public void Handle(object sender, EntityCreatedEventArgs args)
            {
                if (this.Combobox != null)
                {
                    Control target = this.Combobox.Target as Control;
                    this.Combobox = null;
                    EventHandler unhookEvent = this.UnhookEvent;
                    if (unhookEvent != null)
                    {
                        unhookEvent(this, EventArgs.Empty);
                    }
                    if ((target != null) && !target.IsDisposed)
                    {
                        try
                        {
                            DMEWorks.Forms.Combobox comboBox = target as DMEWorks.Forms.Combobox;
                            if (comboBox != null)
                            {
                                Functions.SetComboBoxValue(comboBox, args.ID);
                            }
                            ExtendedDropdown dropdown = target as ExtendedDropdown;
                            if (dropdown != null)
                            {
                                dropdown.Text = args.ID as string;
                            }
                        }
                        catch (Exception exception1)
                        {
                            Exception ex = exception1;
                            ProjectData.SetProjectError(ex);
                            TraceHelper.TraceException(ex);
                            ProjectData.ClearProjectError();
                        }
                    }
                }
            }
        }
    }
}

