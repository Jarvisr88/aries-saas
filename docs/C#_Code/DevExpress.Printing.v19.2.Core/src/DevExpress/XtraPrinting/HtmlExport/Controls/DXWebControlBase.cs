namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class DXWebControlBase : IDisposable
    {
        private string cachedUniqueID;
        private DXWebControlState controlState;
        private string id;
        private DXWebControlBase namingContainer;
        private OccasionalFields occasionalFields;
        private DXWebControlBase parent;
        private DXStateBag viewState;
        internal DXSimpleBitVector32 flags;

        protected internal virtual void AddedControl(DXWebControlBase control, int index)
        {
            if (control.parent != null)
            {
                control.parent.Controls.Remove(control);
            }
            control.parent = this;
            control.flags.Clear(0x20000);
            DXWebControlBase namingContainer = this.flags[0x80] ? this : this.namingContainer;
            if (namingContainer != null)
            {
                control.UpdateNamingContainer(namingContainer);
                if ((control.id == null) && !control.flags[0x40])
                {
                    control.GenerateAutomaticID();
                }
                else if ((control.id != null) || ((control.occasionalFields != null) && (control.occasionalFields.Controls != null)))
                {
                    namingContainer.DirtyNameTable();
                }
            }
            if (this.controlState >= DXWebControlState.ChildrenInitialized)
            {
                control.InitRecursive(namingContainer);
                if (this.controlState >= DXWebControlState.ViewStateLoaded)
                {
                    object obj2 = null;
                    if ((this.occasionalFields != null) && (this.occasionalFields.ControlsViewState != null))
                    {
                        obj2 = this.occasionalFields.ControlsViewState[index];
                        this.occasionalFields.ControlsViewState.Remove(index);
                    }
                    if (this.controlState >= DXWebControlState.Loaded)
                    {
                        control.LoadRecursive();
                        if (this.controlState >= DXWebControlState.PreRendered)
                        {
                            control.PreRenderRecursiveInternal();
                        }
                    }
                }
            }
        }

        protected virtual void AddParsedSubObject(object obj)
        {
            DXWebControlBase child = obj as DXWebControlBase;
            if (child != null)
            {
                this.Controls.Add(child);
            }
        }

        protected void BuildProfileTree(bool calcViewState)
        {
            if ((this.occasionalFields != null) && (this.occasionalFields.Controls != null))
            {
                foreach (DXWebControlBase base2 in this.occasionalFields.Controls)
                {
                    base2.BuildProfileTree(calcViewState);
                }
            }
        }

        private void ClearCachedUniqueIDRecursive()
        {
            this.cachedUniqueID = null;
            if (this.occasionalFields != null)
            {
                this.occasionalFields.UniqueIDPrefix = null;
                if (this.occasionalFields.Controls != null)
                {
                    int count = this.occasionalFields.Controls.Count;
                    for (int i = 0; i < count; i++)
                    {
                        this.occasionalFields.Controls[i].ClearCachedUniqueIDRecursive();
                    }
                }
            }
        }

        protected void ClearChildViewState()
        {
            if (this.occasionalFields != null)
            {
                this.occasionalFields.ControlsViewState = null;
            }
        }

        internal void ClearNamingContainer()
        {
            this.EnsureOccasionalFields();
            this.occasionalFields.NamedControlsID = 0;
            this.DirtyNameTable();
        }

        protected internal virtual void CreateChildControls()
        {
        }

        protected virtual DXWebControlCollection CreateControlCollection() => 
            new DXWebControlCollection(this);

        internal void DirtyNameTable()
        {
            if (this.occasionalFields != null)
            {
                this.occasionalFields.NamedControls = null;
            }
        }

        public virtual void Dispose()
        {
            if (this.occasionalFields != null)
            {
                this.occasionalFields.Dispose();
            }
        }

        protected virtual void EnsureChildControls()
        {
            if (!this.ChildControlsCreated && !this.flags[0x100])
            {
                this.flags.Set(0x100);
                try
                {
                    this.ChildControlsCreated = true;
                }
                finally
                {
                    this.flags.Clear(0x100);
                }
            }
        }

        protected void EnsureID()
        {
            if (this.namingContainer != null)
            {
                if (this.id == null)
                {
                    this.GenerateAutomaticID();
                }
                this.flags.Set(0x800);
            }
        }

        private void EnsureNamedControlsTable()
        {
            this.occasionalFields.NamedControls = new Dictionary<string, DXWebControlBase>(this.occasionalFields.NamedControlsID);
            this.FillNamedControlsTable(this, this.occasionalFields.Controls);
        }

        private void EnsureOccasionalFields()
        {
            this.occasionalFields ??= new OccasionalFields();
        }

        private void FillNamedControlsTable(DXWebControlBase namingContainer, DXWebControlCollection controls)
        {
            foreach (DXWebControlBase base2 in controls)
            {
                if (base2.id != null)
                {
                    try
                    {
                        namingContainer.EnsureOccasionalFields();
                        namingContainer.occasionalFields.NamedControls.Add(base2.id, base2);
                    }
                    catch
                    {
                        throw new Exception("Duplicate id used");
                    }
                }
                if (base2.HasControls() && !base2.flags[0x80])
                {
                    this.FillNamedControlsTable(namingContainer, base2.Controls);
                }
            }
        }

        public virtual DXWebControlBase FindControl(string id) => 
            this.FindControl(id, 0);

        protected virtual DXWebControlBase FindControl(string id, int pathOffset)
        {
            string str;
            this.EnsureChildControls();
            if (!this.flags[0x80])
            {
                DXWebControlBase namingContainer = this.NamingContainer;
                return namingContainer?.FindControl(id, pathOffset);
            }
            if (this.HasControls() && (this.occasionalFields.NamedControls == null))
            {
                this.EnsureNamedControlsTable();
            }
            if ((this.occasionalFields == null) || (this.occasionalFields.NamedControls == null))
            {
                return null;
            }
            char[] anyOf = new char[] { '$', ':' };
            int num = id.IndexOfAny(anyOf, pathOffset);
            if (num == -1)
            {
                str = id.Substring(pathOffset);
                return (this.occasionalFields.NamedControls[str] as DXWebControlBase);
            }
            str = id.Substring(pathOffset, num - pathOffset);
            DXWebControlBase base2 = this.occasionalFields.NamedControls[str] as DXWebControlBase;
            return base2?.FindControl(id, num + 1);
        }

        private void GenerateAutomaticID()
        {
            this.flags.Set(0x200000);
            this.namingContainer.EnsureOccasionalFields();
            int namedControlsID = this.namingContainer.occasionalFields.NamedControlsID;
            this.namingContainer.occasionalFields.NamedControlsID = namedControlsID + 1;
            this.id = "_ctl" + namedControlsID.ToString(NumberFormatInfo.InvariantInfo);
            this.namingContainer.DirtyNameTable();
        }

        internal virtual string GetUniqueIDPrefix()
        {
            this.EnsureOccasionalFields();
            if (this.occasionalFields.UniqueIDPrefix == null)
            {
                string uniqueID = this.UniqueID;
                this.occasionalFields.UniqueIDPrefix = string.IsNullOrEmpty(uniqueID) ? string.Empty : (uniqueID + this.IdSeparator.ToString());
            }
            return this.occasionalFields.UniqueIDPrefix;
        }

        public virtual bool HasControls() => 
            (this.occasionalFields != null) && ((this.occasionalFields.Controls != null) && (this.occasionalFields.Controls.Count > 0));

        private bool HasRenderDelegate() => 
            (this.RareFields != null) && (this.RareFields.RenderMethod != null);

        internal bool HasRenderingData() => 
            this.HasControls() || this.HasRenderDelegate();

        internal virtual void InitRecursive(DXWebControlBase namingContainer)
        {
            if ((this.occasionalFields != null) && (this.occasionalFields.Controls != null))
            {
                if (this.flags[0x80])
                {
                    namingContainer = this;
                }
                string errorMsg = this.occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this.occasionalFields.Controls.Count;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= count)
                    {
                        this.occasionalFields.Controls.SetCollectionReadOnly(errorMsg);
                        break;
                    }
                    DXWebControlBase base2 = this.occasionalFields.Controls[num2];
                    base2.UpdateNamingContainer(namingContainer);
                    if ((base2.id == null) && ((namingContainer != null) && !base2.flags[0x40]))
                    {
                        base2.GenerateAutomaticID();
                    }
                    base2.InitRecursive(namingContainer);
                    num2++;
                }
            }
            if (this.controlState < DXWebControlState.Initialized)
            {
                this.controlState = DXWebControlState.ChildrenInitialized;
                this.OnInit(EventArgs.Empty);
                this.controlState = DXWebControlState.Initialized;
            }
            this.TrackViewState();
        }

        internal bool IsDescendentOf(DXWebControlBase ancestor)
        {
            DXWebControlBase objA = this;
            while (!ReferenceEquals(objA, ancestor) && (objA.Parent != null))
            {
                objA = objA.Parent;
            }
            return ReferenceEquals(objA, ancestor);
        }

        protected bool IsLiteralContent() => 
            (this.occasionalFields != null) && ((this.occasionalFields.Controls != null) && ((this.occasionalFields.Controls.Count == 1) && (this.occasionalFields.Controls[0] is DXHtmlLiteralControl)));

        protected internal virtual void LoadControlState(object savedState)
        {
        }

        internal virtual void LoadRecursive()
        {
            if (this.controlState < DXWebControlState.Loaded)
            {
                this.OnLoad(EventArgs.Empty);
            }
            if ((this.occasionalFields != null) && (this.occasionalFields.Controls != null))
            {
                string errorMsg = this.occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this.occasionalFields.Controls.Count;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= count)
                    {
                        this.occasionalFields.Controls.SetCollectionReadOnly(errorMsg);
                        break;
                    }
                    this.occasionalFields.Controls[num2].LoadRecursive();
                    num2++;
                }
            }
            if (this.controlState < DXWebControlState.Loaded)
            {
                this.controlState = DXWebControlState.Loaded;
            }
        }

        protected virtual bool OnBubbleEvent(object source, EventArgs args) => 
            false;

        protected virtual void OnDataBinding(EventArgs e)
        {
        }

        protected internal virtual void OnInit(EventArgs e)
        {
        }

        protected internal virtual void OnLoad(EventArgs e)
        {
        }

        protected internal virtual void OnPreRender(EventArgs e)
        {
        }

        protected internal virtual void OnUnload(EventArgs e)
        {
        }

        internal virtual void PreRenderRecursiveInternal()
        {
            if (!this.Visible)
            {
                this.flags.Set(0x10);
            }
            else
            {
                this.flags.Clear(0x10);
                this.EnsureChildControls();
                this.OnPreRender(EventArgs.Empty);
                if ((this.occasionalFields != null) && (this.occasionalFields.Controls != null))
                {
                    string errorMsg = this.occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
                    int count = this.occasionalFields.Controls.Count;
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 >= count)
                        {
                            this.occasionalFields.Controls.SetCollectionReadOnly(errorMsg);
                            break;
                        }
                        this.occasionalFields.Controls[num2].PreRenderRecursiveInternal();
                        num2++;
                    }
                }
            }
            this.controlState = DXWebControlState.PreRendered;
        }

        internal void PreventAutoID()
        {
            if (!this.flags[0x80])
            {
                this.flags.Set(0x40);
            }
        }

        protected void RaiseBubbleEvent(object source, EventArgs args)
        {
            for (DXWebControlBase base2 = this.Parent; base2 != null; base2 = base2.Parent)
            {
                if (base2.OnBubbleEvent(source, args))
                {
                    return;
                }
            }
        }

        public virtual void RemovedControl(DXWebControlBase control)
        {
            if ((this.namingContainer != null) && (control.id != null))
            {
                this.namingContainer.DirtyNameTable();
            }
            control.UnloadRecursive(false);
            control.parent = null;
            control.namingContainer = null;
            control.flags.Clear(0x800);
            control.ClearCachedUniqueIDRecursive();
        }

        protected internal virtual void Render(DXHtmlTextWriter writer)
        {
            this.RenderChildren(writer);
        }

        protected internal virtual void RenderChildren(DXHtmlTextWriter writer)
        {
            ICollection children = (this.occasionalFields == null) ? null : this.occasionalFields.Controls;
            this.RenderChildrenInternal(writer, children);
        }

        internal void RenderChildrenInternal(DXHtmlTextWriter writer, ICollection children)
        {
            if ((this.RareFields != null) && (this.RareFields.RenderMethod != null))
            {
                writer.BeginRender();
                this.RareFields.RenderMethod(writer, this);
                writer.EndRender();
            }
            else if (children != null)
            {
                foreach (DXWebControlBase base2 in children)
                {
                    base2.RenderControl(writer);
                }
            }
        }

        public virtual void RenderControl(DXHtmlTextWriter writer)
        {
            if (!this.flags[0x10] && !this.flags[0x200])
            {
                this.RenderControlInternal(writer);
            }
        }

        private void RenderControlInternal(DXHtmlTextWriter writer)
        {
            this.Render(writer);
        }

        public string ResolveClientUrl(string relativeUrl) => 
            relativeUrl;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void SetRenderMethodDelegate(RenderMethod renderMethod)
        {
            this.RareFieldsEnsured.RenderMethod = renderMethod;
            this.Controls.SetCollectionReadOnly("Collection_readonly_Codeblocks");
        }

        protected virtual void TrackViewState()
        {
            if (this.viewState != null)
            {
                this.viewState.TrackViewState();
            }
            this.flags.Set(2);
        }

        internal virtual void UnloadRecursive(bool dispose)
        {
            if (this.flags[0x200000])
            {
                this.id = null;
                this.flags.Clear(0x200000);
            }
            if ((this.occasionalFields != null) && (this.occasionalFields.Controls != null))
            {
                string errorMsg = this.occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this.occasionalFields.Controls.Count;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= count)
                    {
                        this.occasionalFields.Controls.SetCollectionReadOnly(errorMsg);
                        break;
                    }
                    this.occasionalFields.Controls[num2].UnloadRecursive(dispose);
                    num2++;
                }
            }
            this.OnUnload(EventArgs.Empty);
            if (dispose)
            {
                this.Dispose();
            }
        }

        private void UpdateNamingContainer(DXWebControlBase namingContainer)
        {
            if ((this.namingContainer != null) && !ReferenceEquals(this.namingContainer, namingContainer))
            {
                this.ClearCachedUniqueIDRecursive();
            }
            this.namingContainer = namingContainer;
        }

        protected bool ChildControlsCreated
        {
            get => 
                this.flags[8];
            set
            {
                if (!value && this.flags[8])
                {
                    this.Controls.Clear();
                }
                if (value)
                {
                    this.flags.Set(8);
                }
                else
                {
                    this.flags.Clear(8);
                }
            }
        }

        public virtual string ClientID
        {
            get
            {
                this.EnsureID();
                string uniqueID = this.UniqueID;
                return (((uniqueID == null) || (uniqueID.IndexOf(this.IdSeparator) < 0)) ? uniqueID : uniqueID.Replace(this.IdSeparator, '_'));
            }
        }

        public virtual DXWebControlCollection Controls
        {
            get
            {
                if ((this.occasionalFields == null) || (this.occasionalFields.Controls == null))
                {
                    this.EnsureOccasionalFields();
                    this.occasionalFields.Controls = this.CreateControlCollection();
                }
                return this.occasionalFields.Controls;
            }
        }

        public virtual bool EnableTheming
        {
            get => 
                (this.flags[0x2000] || (this.Parent == null)) ? !this.flags[0x1000] : this.Parent.EnableTheming;
            set
            {
                if (this.controlState >= DXWebControlState.FrameworkInitialized)
                {
                    throw new InvalidOperationException("PropertySetBeforePreInitOrAddToControls");
                }
                if (!value)
                {
                    this.flags.Set(0x1000);
                }
                else
                {
                    this.flags.Clear(0x1000);
                }
                this.flags.Set(0x2000);
            }
        }

        protected bool HasChildViewState =>
            (this.occasionalFields != null) && ((this.occasionalFields.ControlsViewState != null) && (this.occasionalFields.ControlsViewState.Count > 0));

        public virtual string ID
        {
            get => 
                (this.flags[1] || this.flags[0x800]) ? this.id : null;
            set
            {
                if ((value != null) && (value.Length == 0))
                {
                    value = null;
                }
                string id = this.id;
                this.id = value;
                this.ClearCachedUniqueIDRecursive();
                this.flags.Set(1);
                this.flags.Clear(0x200000);
                if ((this.namingContainer != null) && (id != null))
                {
                    this.namingContainer.DirtyNameTable();
                }
            }
        }

        protected char IdSeparator =>
            ':';

        protected internal bool IsChildControlStateCleared =>
            this.flags[0x40000];

        protected bool IsTrackingViewState =>
            this.flags[2];

        public virtual DXWebControlBase NamingContainer
        {
            get
            {
                if ((this.namingContainer == null) && (this.Parent != null))
                {
                    this.namingContainer = !this.Parent.flags[0x80] ? this.Parent.NamingContainer : this.Parent;
                }
                return this.namingContainer;
            }
        }

        public virtual DXWebControlBase Parent =>
            this.parent;

        private ControlRareFields RareFields =>
            (this.occasionalFields == null) ? null : this.occasionalFields.RareFields;

        private ControlRareFields RareFieldsEnsured
        {
            get
            {
                this.EnsureOccasionalFields();
                ControlRareFields rareFields = this.occasionalFields.RareFields;
                if (rareFields == null)
                {
                    rareFields = new ControlRareFields();
                    this.occasionalFields.RareFields = rareFields;
                }
                return rareFields;
            }
        }

        public virtual string SkinID
        {
            get => 
                ((this.occasionalFields == null) || (this.occasionalFields.SkinId == null)) ? string.Empty : this.occasionalFields.SkinId;
            set
            {
                if (this.flags[0x4000])
                {
                    throw new InvalidOperationException("PropertySetBeforeStyleSheetApplied");
                }
                if (this.controlState >= DXWebControlState.FrameworkInitialized)
                {
                    throw new InvalidOperationException("PropertySetBeforePreInitOrAddToControls");
                }
                this.EnsureOccasionalFields();
                this.occasionalFields.SkinId = value;
            }
        }

        public virtual string UniqueID
        {
            get
            {
                if (this.cachedUniqueID == null)
                {
                    DXWebControlBase namingContainer = this.NamingContainer;
                    if (namingContainer == null)
                    {
                        return this.id;
                    }
                    if (this.id == null)
                    {
                        this.GenerateAutomaticID();
                    }
                    string uniqueIDPrefix = namingContainer.GetUniqueIDPrefix();
                    if (uniqueIDPrefix.Length == 0)
                    {
                        return this.id;
                    }
                    this.cachedUniqueID = uniqueIDPrefix + this.id;
                }
                return this.cachedUniqueID;
            }
        }

        protected virtual DXStateBag ViewState
        {
            get
            {
                if (this.viewState == null)
                {
                    this.viewState = new DXStateBag();
                    if (this.IsTrackingViewState)
                    {
                        this.viewState.TrackViewState();
                    }
                }
                return this.viewState;
            }
        }

        public virtual bool Visible
        {
            get => 
                !this.flags[0x10] ? ((this.parent == null) || this.parent.Visible) : false;
            set
            {
                if (this.flags[2] && (!this.flags[0x10] != value))
                {
                    this.flags.Set(0x20);
                }
                if (!value)
                {
                    this.flags.Set(0x10);
                }
                else
                {
                    this.flags.Clear(0x10);
                }
            }
        }

        private sealed class ControlRareFields : IDisposable
        {
            public IDictionary ControlDesignerAccessorUserData;
            public IDictionary DesignModeState;
            public DevExpress.XtraPrinting.HtmlExport.Controls.RenderMethod RenderMethod;

            internal ControlRareFields()
            {
            }

            public void Dispose()
            {
                this.ControlDesignerAccessorUserData = null;
                this.DesignModeState = null;
            }
        }

        private sealed class OccasionalFields : IDisposable
        {
            public DXWebControlCollection Controls;
            public IDictionary ControlsViewState;
            public IDictionary NamedControls;
            public int NamedControlsID;
            public DXWebControlBase.ControlRareFields RareFields;
            public string SkinId;
            public string UniqueIDPrefix;

            internal OccasionalFields()
            {
            }

            public void Dispose()
            {
                if (this.RareFields != null)
                {
                    this.RareFields.Dispose();
                }
                this.ControlsViewState = null;
            }
        }
    }
}

