namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class VmlShapeImageData : ISupportsCopyFrom<VmlShapeImageData>
    {
        private readonly IDocumentModel documentModel;
        private string alternateRef;
        private bool? biLevel;
        private string blackLevel;
        private Color chromaKey;
        private string cropBottom;
        private string cropLeft;
        private string cropRight;
        private string cropTop;
        private bool? detectMouseClick;
        private Color embossColor;
        private string gain;
        private string gamma;
        private bool? grayScale;
        private string href;
        private string id;
        private float? movie;
        private float? oleId;
        private Color reColorTarget;
        private string src;
        private string title;
        private OfficeImage image;

        public VmlShapeImageData(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
        }

        public void CopyFrom(VmlShapeImageData source)
        {
            this.alternateRef = source.alternateRef;
            this.biLevel = source.biLevel;
            this.blackLevel = source.blackLevel;
            this.chromaKey = source.chromaKey;
            this.cropBottom = source.cropBottom;
            this.cropLeft = source.cropLeft;
            this.cropRight = source.cropRight;
            this.cropTop = source.cropTop;
            this.detectMouseClick = source.detectMouseClick;
            this.embossColor = source.embossColor;
            this.gain = source.gain;
            this.gamma = source.gamma;
            this.grayScale = source.grayScale;
            this.href = source.href;
            this.id = source.id;
            this.movie = source.movie;
            this.oleId = source.oleId;
            this.reColorTarget = source.reColorTarget;
            this.src = source.src;
            this.title = source.title;
            OfficeImage image = source.image;
            if (image == null)
            {
                this.image = null;
            }
            else
            {
                this.image = image.Clone(this.documentModel);
            }
        }

        public string AlternateRef
        {
            get => 
                this.alternateRef;
            set => 
                this.alternateRef = value;
        }

        public bool? BiLevel
        {
            get => 
                this.biLevel;
            set => 
                this.biLevel = value;
        }

        public string BlackLevel
        {
            get => 
                this.blackLevel;
            set => 
                this.blackLevel = value;
        }

        public Color ChromaKey
        {
            get => 
                this.chromaKey;
            set => 
                this.chromaKey = value;
        }

        public string CropBottom
        {
            get => 
                this.cropBottom;
            set => 
                this.cropBottom = value;
        }

        public string CropLeft
        {
            get => 
                this.cropLeft;
            set => 
                this.cropLeft = value;
        }

        public string CropRight
        {
            get => 
                this.cropRight;
            set => 
                this.cropRight = value;
        }

        public string CropTop
        {
            get => 
                this.cropTop;
            set => 
                this.cropTop = value;
        }

        public bool? DetectMouseClick
        {
            get => 
                this.detectMouseClick;
            set => 
                this.detectMouseClick = value;
        }

        public Color EmbossColor
        {
            get => 
                this.embossColor;
            set => 
                this.embossColor = value;
        }

        public string Gain
        {
            get => 
                this.gain;
            set => 
                this.gain = value;
        }

        public string Gamma
        {
            get => 
                this.gamma;
            set => 
                this.gamma = value;
        }

        public bool? Grayscale
        {
            get => 
                this.grayScale;
            set => 
                this.grayScale = value;
        }

        public string Href
        {
            get => 
                this.href;
            set => 
                this.href = value;
        }

        public string Id
        {
            get => 
                this.id;
            set => 
                this.id = value;
        }

        public float? Movie
        {
            get => 
                this.movie;
            set => 
                this.movie = value;
        }

        public float? OleId
        {
            get => 
                this.oleId;
            set => 
                this.oleId = value;
        }

        public Color RecolorTarget
        {
            get => 
                this.reColorTarget;
            set => 
                this.reColorTarget = value;
        }

        public string Src
        {
            get => 
                this.src;
            set => 
                this.src = value;
        }

        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        public OfficeImage Image
        {
            get => 
                this.image;
            set => 
                this.image = value;
        }
    }
}

