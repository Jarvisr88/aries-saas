namespace DevExpress.XtraPrintingLinks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native.PageBuilder;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;

    [DefaultProperty("Links")]
    public class CompositeLinkBase : LinkBase
    {
        private int breakSpace;
        private InnerLinkCollection links;
        private List<IDisposable> garbageContainer;

        public CompositeLinkBase()
        {
            this.garbageContainer = new List<IDisposable>();
        }

        public CompositeLinkBase(DevExpress.XtraPrinting.PrintingSystemBase ps) : base(ps)
        {
            this.garbageContainer = new List<IDisposable>();
            ((InnerLinkCollection) this.Links).PrintingSystem = ps;
        }

        public CompositeLinkBase(IContainer container) : base(container)
        {
            this.garbageContainer = new List<IDisposable>();
        }

        protected void AddSubreport(LinkBase link, PointF offset)
        {
            link.PrintingSystemBase = base.ps;
            BrickModifier skipArea = link.SkipArea;
            link.SkipArea = base.SkipArea;
            link.AddSubreport(offset);
            link.SkipArea = skipArea;
        }

        private void ClearGarbage()
        {
            foreach (IDisposable disposable in this.garbageContainer)
            {
                disposable.Dispose();
            }
            this.garbageContainer.Clear();
        }

        protected override void CreateDetail(BrickGraphics gr)
        {
            if (this.Links.Count == 0)
            {
                throw new Exception("The Links property value must not be empty");
            }
            for (int i = 0; i < this.Links.Count; i++)
            {
                this.Links[i].PrintingSystemBase = base.ps;
                this.AddSubreport(this.Links[i], (i == 0) ? PointF.Empty : new PointF(0f, (float) this.breakSpace));
            }
        }

        public void CreatePageForEachLink()
        {
            this.PrintingSystemBase.Begin();
            this.PrintingSystemBase.End(this);
            this.ClearGarbage();
            foreach (LinkBase base2 in this.Links)
            {
                DevExpress.XtraPrinting.PrintingSystemBase item = new DevExpress.XtraPrinting.PrintingSystemBase();
                this.garbageContainer.Add(item);
                item.SetDocument(new SinglePageLinkDocument(item));
                base2.Owner = null;
                base2.CreateDocument(item);
                base2.Owner = this;
                if (item.CancelPending)
                {
                    this.PrintingSystemBase.Cancel();
                    break;
                }
                if (item.PageCount > 0)
                {
                    this.PrintingSystemBase.Pages.Add(item.Pages[0]);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ClearGarbage();
            }
            base.Dispose(disposing);
        }

        [Category("Printing"), Description("Specifies the indent between the printed content of individual links."), DefaultValue(0)]
        public int BreakSpace
        {
            get => 
                this.breakSpace;
            set => 
                this.breakSpace = value;
        }

        [Category("Printing"), Description("Gets a collection of links of a CompositeLinkBase object."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor("DevExpress.XtraPrintingLinks.Design.LinkSelectionEditor,DevExpress.XtraPrinting.v19.2.Design, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public LinkCollection Links
        {
            get
            {
                this.links ??= new InnerLinkCollection(this);
                return this.links;
            }
        }

        [Description("Gets or sets the Printing System used to create and print a document for this link."), DefaultValue((string) null)]
        public override DevExpress.XtraPrinting.PrintingSystemBase PrintingSystemBase
        {
            get => 
                base.ps;
            set
            {
                base.ps = value;
                ((InnerLinkCollection) this.Links).PrintingSystem = base.ps;
            }
        }

        protected override BrickModifier InternalSkipArea =>
            BrickModifier.None;

        private class InnerLinkCollection : LinkCollection
        {
            private CompositeLinkBase owner;

            internal InnerLinkCollection(CompositeLinkBase owner)
            {
                this.owner = owner;
            }

            protected override void OnInsertComplete(int index, object item)
            {
                base.OnInsertComplete(index, item);
                ((LinkBase) item).Owner = this.owner;
            }

            internal PrintingSystemBase PrintingSystem
            {
                get => 
                    base.ps;
                set
                {
                    base.ps = value;
                    foreach (LinkBase base2 in this)
                    {
                        base2.PrintingSystemBase = base.ps;
                    }
                }
            }
        }
    }
}

