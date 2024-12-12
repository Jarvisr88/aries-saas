namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [BrickExporter(typeof(BrickBaseExporter))]
    public abstract class BrickBase : StorableObjectBase, IXtraSupportShouldSerialize, IXtraSupportCreateContentPropertyValue, IXtraSupportDeserializeCollectionItem, ICloneable
    {
        private static readonly BitVector32.Section ModifierSection = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(BrickModifier)));
        protected static readonly int bitCanPublish = BitVector32Helper.CreateMask(ModifierSection);
        protected static readonly BitVector32.Section XlsExportNativeFormatSection = BitVector32Helper.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(DefaultBoolean)), bitCanPublish);
        protected static readonly BitVector32.Section SeparabilitySection = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(Separability)), XlsExportNativeFormatSection);
        protected static readonly BitVector32.Section ImageAlignmentSection = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(ImageAlignment)), SeparabilitySection);
        protected static readonly int bitCanGrow = BitVector32Helper.CreateMask(ImageAlignmentSection);
        protected static readonly int bitCanShrink = BitVector32.CreateMask(bitCanGrow);
        protected static readonly int bitCanOverflow = BitVector32.CreateMask(bitCanShrink);
        protected static readonly int bitNoClip = BitVector32.CreateMask(bitCanOverflow);
        protected static readonly int bitMerged = BitVector32.CreateMask(bitNoClip);
        protected static readonly int bitUseTextAsDefaultHint = BitVector32.CreateMask(bitMerged);
        protected static readonly int bitIsVisible = BitVector32.CreateMask(bitUseTextAsDefaultHint);
        protected static readonly int bitCanMultiColumn = BitVector32.CreateMask(bitIsVisible);
        protected static readonly int bitCanAddToPage = BitVector32.CreateMask(bitCanMultiColumn);
        protected static readonly int bitRightToLeftLayout = BitVector32.CreateMask(bitCanAddToPage);
        protected static readonly int bitRepeatForVerticallySplitContent = BitVector32.CreateMask(bitRightToLeftLayout);
        protected static readonly int bitAutoWidth = BitVector32.CreateMask(bitRepeatForVerticallySplitContent);
        private RectangleF fRect;
        protected BitVector32 flags;

        protected BrickBase()
        {
            this.fRect = RectangleF.Empty;
            this.Modifier = BrickModifier.None;
            this.CanPublish = true;
        }

        internal BrickBase(BrickBase brickBase)
        {
            this.fRect = RectangleF.Empty;
            this.fRect = brickBase.Rect;
            this.CanPublish = brickBase.CanPublish;
        }

        internal BrickBase(RectangleF rect) : this()
        {
            this.fRect = rect;
        }

        protected internal virtual unsafe bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<BrickBase, RectangleF> callback)
        {
            IList innerBrickList = this.InnerBrickList;
            List<IDisposable> list2 = null;
            int item = 0;
            while (true)
            {
                while (true)
                {
                    if (item >= innerBrickList.Count)
                    {
                        if (list2 != null)
                        {
                            foreach (IDisposable disposable in list2)
                            {
                                disposable.Dispose();
                            }
                        }
                        return true;
                    }
                    try
                    {
                        indices.Add(item);
                        BrickBase base2 = (BrickBase) innerBrickList[item];
                        if (!base2.IsServiceBrick)
                        {
                            RectangleF rect = base2.Rect;
                            RectangleF* efPtr1 = &rect;
                            efPtr1.X += brickBounds.Location.X + this.InnerBrickListOffset.X;
                            RectangleF* efPtr2 = &rect;
                            efPtr2.Y += brickBounds.Location.Y + this.InnerBrickListOffset.Y;
                            RectangleF ef2 = RectangleF.Intersect(clipRect, brickBounds);
                            if (!rect.IntersectsWith(ef2))
                            {
                                break;
                            }
                            if (base2.AfterPrintOnPage(indices, rect, ef2, page, pageIndex, pageCount, callback) || innerBrickList.IsFixedSize)
                            {
                                callback(base2, rect);
                                break;
                            }
                        }
                        if (base2 is IDisposable)
                        {
                            list2 = new List<IDisposable> {
                                (IDisposable) base2
                            };
                        }
                        innerBrickList.RemoveAt(item);
                        item--;
                    }
                    finally
                    {
                        indices.RemoveAt(indices.Count - 1);
                    }
                    break;
                }
                item++;
            }
        }

        [IteratorStateMachine(typeof(<AllBricks>d__77))]
        internal IEnumerable<BrickBase> AllBricks()
        {
            BrickBase current;
            IEnumerator<BrickBase> <>7__wrap2;
            IEnumerator enumerator = this.InnerBrickList.GetEnumerator();
            if (enumerator.MoveNext())
            {
                current = (BrickBase) enumerator.Current;
                <>7__wrap2 = current.AllBricks().GetEnumerator();
            }
            else
            {
                enumerator = null;
            }
            if (<>7__wrap2.MoveNext())
            {
                BrickBase current = <>7__wrap2.Current;
                yield return current;
                yield break;
            }
            else
            {
                <>7__wrap2 = null;
                yield return current;
                yield break;
            }
        }

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }

        protected virtual object CreateCollectionItemCore(string propertyName, XtraItemEventArgs e) => 
            null;

        protected virtual object CreateContentPropertyValue(XtraItemEventArgs e) => 
            null;

        object IXtraSupportCreateContentPropertyValue.Create(XtraItemEventArgs e) => 
            this.CreateContentPropertyValue(e);

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) => 
            this.CreateCollectionItemCore(propertyName, e);

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            this.SetIndexCollectionItemCore(propertyName, e);
        }

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            this.ShouldSerializeCore(propertyName);

        protected internal virtual RectangleF GetViewRectangle() => 
            this.fRect;

        internal bool HasModifier(params BrickModifier[] modifiers) => 
            Array.IndexOf<BrickModifier>(modifiers, this.Modifier) >= 0;

        internal void SetBounds(RectangleF bounds, float dpi)
        {
            this.InitialRect = GraphicsUnitConverter.Convert(bounds, dpi, (float) 300f);
        }

        protected virtual void SetBoundsCore(float x, float y, float width, float height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.X) > BoundsSpecified.None)
            {
                this.fRect.X = x;
            }
            if ((specified & BoundsSpecified.Y) > BoundsSpecified.None)
            {
                this.fRect.Y = y;
            }
            if ((specified & BoundsSpecified.Width) > BoundsSpecified.None)
            {
                this.fRect.Width = width;
            }
            if ((specified & BoundsSpecified.Height) > BoundsSpecified.None)
            {
                this.fRect.Height = height;
            }
        }

        protected virtual void SetIndexCollectionItemCore(string propertyName, XtraSetItemIndexEventArgs e)
        {
        }

        internal void SetModifierRecursive(BrickModifier value)
        {
            this.Modifier = value;
            foreach (BrickBase base2 in this.AllBricks())
            {
                base2.Modifier = value;
            }
        }

        protected virtual bool ShouldSerializeCore(string propertyName)
        {
            if (propertyName != "Modifier")
            {
                return ((propertyName != "Printed") && ((propertyName == "StoredId") ? StoredIDSerializationManager.ShouldSerializeStoredID : true));
            }
            BrickModifier[] modifiers = new BrickModifier[] { BrickModifier.MarginalFooter, BrickModifier.MarginalHeader };
            return this.HasModifier(modifiers);
        }

        [XtraSerializableProperty, DefaultValue(-2147483648)]
        public int StoredId
        {
            get => 
                this.StoredID.Id;
            set => 
                this.StoredID = new StoredID(value);
        }

        public virtual bool RepeatForVerticallySplitContent
        {
            get => 
                this.flags[bitRepeatForVerticallySplitContent];
            set => 
                this.flags[bitRepeatForVerticallySplitContent] = value;
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool RightToLeftLayout
        {
            get => 
                false;
            set
            {
            }
        }

        internal virtual bool IsServiceBrick =>
            false;

        internal virtual IList InnerBrickList =>
            new BrickBase[0];

        internal virtual PointF InnerBrickListOffset =>
            PointF.Empty;

        [DefaultValue(false), XtraSerializableProperty]
        public virtual bool NoClip
        {
            get => 
                this.flags[bitNoClip];
            set => 
                this.flags[bitNoClip] = value;
        }

        [Obsolete("Use the CanPublish property instead."), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty]
        public bool Printed
        {
            get => 
                this.CanPublish;
            set => 
                this.CanPublish = value;
        }

        [DefaultValue(true), XtraSerializableProperty]
        public bool CanPublish
        {
            get => 
                this.flags[bitCanPublish];
            set => 
                this.flags[bitCanPublish] = value;
        }

        [Description("Specifies the page area for displaying a specific brick."), XtraSerializableProperty]
        public BrickModifier Modifier
        {
            get => 
                (BrickModifier) ((short) this.flags[ModifierSection]);
            set => 
                this.flags[ModifierSection] = (int) value;
        }

        [Description("Defines the current brick's location and size, in GraphicsUnit.Document measurement units."), XtraSerializableProperty]
        public virtual RectangleF Rect
        {
            get => 
                this.fRect;
            set => 
                this.InitialRect = value;
        }

        protected internal virtual RectangleF InitialRect
        {
            get => 
                this.fRect;
            set => 
                this.SetBoundsCore(value.X, value.Y, value.Width, value.Height, BoundsSpecified.All);
        }

        [Description("Defines the current brick's location, in GraphicsUnit.Document measurement units.")]
        public PointF Location
        {
            get => 
                this.Rect.Location;
            set => 
                this.SetBoundsCore(value.X, value.Y, 0f, 0f, BoundsSpecified.Location);
        }

        internal float X
        {
            get => 
                this.Rect.X;
            set => 
                this.SetBoundsCore(value, 0f, 0f, 0f, BoundsSpecified.X);
        }

        internal float Y
        {
            get => 
                this.Rect.Y;
            set => 
                this.SetBoundsCore(0f, value, 0f, 0f, BoundsSpecified.Y);
        }

        internal float Right =>
            this.Rect.Right;

        internal float Bottom =>
            this.Rect.Bottom;

        [Description("Defines the current brick's size, in GraphicsUnit.Document measurement units.")]
        public SizeF Size
        {
            get => 
                this.Rect.Size;
            set => 
                this.fRect.Size = value;
        }

        internal float Width
        {
            get => 
                this.Rect.Width;
            set => 
                this.SetBoundsCore(0f, 0f, value, 0f, BoundsSpecified.Width);
        }

        internal float Height
        {
            get => 
                this.Rect.Height;
            set => 
                this.SetBoundsCore(0f, 0f, 0f, value, BoundsSpecified.Height);
        }

    }
}

