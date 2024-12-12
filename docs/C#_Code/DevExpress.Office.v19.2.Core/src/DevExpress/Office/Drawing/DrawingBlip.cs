namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public class DrawingBlip : ICloneable<DrawingBlip>, ISupportsCopyFrom<DrawingBlip>, IDrawingBullet, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey EmbeddedPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey LinkPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey CompressionStatePropertyKey = new PropertyKey(2);
        public static readonly PropertyKey ImagePropertyKey = new PropertyKey(3);
        public static readonly PropertyKey EffectsPropertyKey = new PropertyKey(4);
        private readonly InvalidateProxy innerParent;
        private readonly PropertyChangedNotifier notifier;
        private readonly DrawingEffectCollection effects;
        private bool embedded = true;
        private string link = string.Empty;
        private DevExpress.Office.Drawing.CompressionState compressionState;
        private OfficeImage image;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        public DrawingBlip(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.innerParent = new InvalidateProxy();
            this.notifier = new PropertyChangedNotifier(this);
            this.effects = new DrawingEffectCollection(documentModel);
            this.effects.Modified += new EventHandler(this.OnEffectsModified);
            this.compressionState = DevExpress.Office.Drawing.CompressionState.None;
        }

        private void ApplyHistoryItem(HistoryItem item)
        {
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        public DrawingBlip Clone()
        {
            DrawingBlip blip = new DrawingBlip(this.DocumentModel);
            blip.CopyFrom(this);
            return blip;
        }

        public IDrawingBullet CloneTo(IDocumentModel documentModel)
        {
            DrawingBlip blip = new DrawingBlip(documentModel);
            blip.CopyFrom(this);
            return blip;
        }

        public void CopyFrom(DrawingBlip value)
        {
            this.link = value.Link;
            this.embedded = value.Embedded;
            this.compressionState = value.compressionState;
            this.effects.CopyFrom(value.effects);
            if (value.Image == null)
            {
                this.Image = null;
            }
            else
            {
                try
                {
                    this.Image = value.Image.Clone(this.DocumentModel);
                }
                catch
                {
                    this.Image = null;
                }
            }
        }

        public override bool Equals(object obj)
        {
            DrawingBlip blip = obj as DrawingBlip;
            return ((blip != null) ? (this.effects.Equals(blip.effects) && ((this.compressionState == blip.compressionState) && ((this.embedded == blip.embedded) && ((StringExtensions.CompareInvariantCultureIgnoreCase(this.link, blip.link) == 0) && Equals(this.image, blip.image))))) : false);
        }

        public override int GetHashCode() => 
            ((((base.GetType().GetHashCode() ^ this.effects.GetHashCode()) ^ this.compressionState.GetHashCode()) ^ this.embedded.GetHashCode()) ^ this.link.GetHashCode()) ^ this.image.GetHashCode();

        private void OnEffectsModified(object sender, EventArgs e)
        {
            this.notifier.OnPropertyChanged(EffectsPropertyKey);
        }

        public void SetCompressionStateCore(DevExpress.Office.Drawing.CompressionState value)
        {
            this.compressionState = value;
            this.notifier.OnPropertyChanged(CompressionStatePropertyKey);
        }

        public void SetEmbedded()
        {
            this.SetExternalCore(true, string.Empty);
        }

        public void SetEmbeddedCore(bool value)
        {
            this.embedded = value;
            this.notifier.OnPropertyChanged(EmbeddedPropertyKey);
        }

        public void SetExternal(string link)
        {
            this.SetExternalCore(false, link);
        }

        private void SetExternalCore(bool embedded, string link)
        {
            this.DocumentModel.History.BeginTransaction();
            this.Embedded = embedded;
            this.Link = link;
            this.DocumentModel.History.EndTransaction();
        }

        public void SetImageCore(OfficeImage value)
        {
            this.image = value;
            this.notifier.OnPropertyChanged(ImagePropertyKey);
            this.innerParent.Invalidate();
        }

        public void SetLinkCore(string value)
        {
            this.link = value;
            this.notifier.OnPropertyChanged(LinkPropertyKey);
        }

        public void Visit(IDrawingBulletVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IDocumentModel DocumentModel =>
            this.effects.DocumentModel;

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        public DrawingEffectCollection Effects =>
            this.effects;

        public bool IsEmpty =>
            this.Embedded && ((this.Effects.Count == 0) && ((this.CompressionState == DevExpress.Office.Drawing.CompressionState.None) && string.IsNullOrEmpty(this.Link)));

        public bool Embedded
        {
            get => 
                this.embedded;
            protected set
            {
                if (this.embedded != value)
                {
                    this.ApplyHistoryItem(new DrawingBlipEmbeddedChangeHistoryItem(this, this.embedded, value));
                }
            }
        }

        public string Link
        {
            get => 
                this.link;
            protected set
            {
                if (this.link != value)
                {
                    this.ApplyHistoryItem(new DrawingBlipLinkChangeHistoryItem(this, this.link, value));
                }
            }
        }

        public DevExpress.Office.Drawing.CompressionState CompressionState
        {
            get => 
                this.compressionState;
            set
            {
                if (this.CompressionState != value)
                {
                    this.ApplyHistoryItem(new DrawingBlipCompressionStateHistoryItem(this, this.compressionState, value));
                }
            }
        }

        public OfficeImage Image
        {
            get => 
                this.image;
            set
            {
                if (!Equals(this.image, value))
                {
                    this.ApplyHistoryItem(new DrawingBlipImageChangeHistoryItem(this, this.image, value));
                }
            }
        }

        public DrawingBulletType Type =>
            DrawingBulletType.Common;
    }
}

