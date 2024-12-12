namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ThemeDrawingColorCollection
    {
        private readonly IDocumentModel documentModel;
        private readonly Dictionary<ThemeColorIndex, DrawingColor> innerCollection;
        private readonly Dictionary<SchemeColorValues, ThemeColorIndex> schemeColorValuesToThemeColorIndexTranslationTable = CreateSchemeColorValuesToThemeColorIndexTranslationTable();
        private string name;

        public ThemeDrawingColorCollection(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.name = string.Empty;
            this.innerCollection = new Dictionary<ThemeColorIndex, DrawingColor>();
        }

        private bool CheckValidation() => 
            (this.name != null) && ((this.innerCollection.Count == 12) && (this.innerCollection.ContainsKey(ThemeColorIndex.Dark1) && (this.innerCollection.ContainsKey(ThemeColorIndex.Light1) && (this.innerCollection.ContainsKey(ThemeColorIndex.Dark2) && (this.innerCollection.ContainsKey(ThemeColorIndex.Light2) && (this.innerCollection.ContainsKey(ThemeColorIndex.Accent1) && (this.innerCollection.ContainsKey(ThemeColorIndex.Accent2) && (this.innerCollection.ContainsKey(ThemeColorIndex.Accent3) && (this.innerCollection.ContainsKey(ThemeColorIndex.Accent4) && (this.innerCollection.ContainsKey(ThemeColorIndex.Accent5) && (this.innerCollection.ContainsKey(ThemeColorIndex.Accent6) && (this.innerCollection.ContainsKey(ThemeColorIndex.Hyperlink) && this.innerCollection.ContainsKey(ThemeColorIndex.FollowedHyperlink)))))))))))));

        internal void Clear()
        {
            this.name = string.Empty;
            this.innerCollection.Clear();
        }

        internal void CopyFrom(ThemeDrawingColorCollection sourceObj)
        {
            this.Clear();
            this.name = sourceObj.name;
            foreach (KeyValuePair<ThemeColorIndex, DrawingColor> pair in sourceObj.innerCollection)
            {
                this.innerCollection.Add(pair.Key, this.GetColor(pair.Value, sourceObj.documentModel));
            }
        }

        private DrawingColor CreateDrawingColor(SystemColorValues value) => 
            DrawingColor.Create(this.documentModel, DrawingColorModelInfo.CreateSystem(value));

        private DrawingColor CreateDrawingColor(Color value) => 
            DrawingColor.Create(this.documentModel, DrawingColorModelInfo.CreateARGB(value));

        private static Dictionary<SchemeColorValues, ThemeColorIndex> CreateSchemeColorValuesToThemeColorIndexTranslationTable() => 
            new Dictionary<SchemeColorValues, ThemeColorIndex> { 
                { 
                    SchemeColorValues.Background1,
                    ThemeColorIndex.Light1
                },
                { 
                    SchemeColorValues.Background2,
                    ThemeColorIndex.Light2
                },
                { 
                    SchemeColorValues.Text1,
                    ThemeColorIndex.Dark1
                },
                { 
                    SchemeColorValues.Text2,
                    ThemeColorIndex.Dark2
                },
                { 
                    SchemeColorValues.Light1,
                    ThemeColorIndex.Light1
                },
                { 
                    SchemeColorValues.Light2,
                    ThemeColorIndex.Light2
                },
                { 
                    SchemeColorValues.Dark1,
                    ThemeColorIndex.Dark1
                },
                { 
                    SchemeColorValues.Dark2,
                    ThemeColorIndex.Dark2
                },
                { 
                    SchemeColorValues.Accent1,
                    ThemeColorIndex.Accent1
                },
                { 
                    SchemeColorValues.Accent2,
                    ThemeColorIndex.Accent2
                },
                { 
                    SchemeColorValues.Accent3,
                    ThemeColorIndex.Accent3
                },
                { 
                    SchemeColorValues.Accent4,
                    ThemeColorIndex.Accent4
                },
                { 
                    SchemeColorValues.Accent5,
                    ThemeColorIndex.Accent5
                },
                { 
                    SchemeColorValues.Accent6,
                    ThemeColorIndex.Accent6
                },
                { 
                    SchemeColorValues.Hyperlink,
                    ThemeColorIndex.Hyperlink
                },
                { 
                    SchemeColorValues.FollowedHyperlink,
                    ThemeColorIndex.FollowedHyperlink
                }
            };

        public Color GetColor(SchemeColorValues value) => 
            this.GetColor(this.schemeColorValuesToThemeColorIndexTranslationTable[value]);

        public Color GetColor(ThemeColorIndex key)
        {
            DrawingColor color = this.TryGetDrawingColor(key);
            if (color == null)
            {
                Exceptions.ThrowInternalException();
            }
            return color.FinalColor;
        }

        private DrawingColor GetColor(DrawingColor sourceColor, IDocumentModel sourceDocumentModel) => 
            !ReferenceEquals(sourceDocumentModel, this.documentModel) ? sourceColor.CloneTo(this.documentModel) : sourceColor;

        public bool IsEqualColors(ThemeDrawingColorCollection other)
        {
            Guard.ArgumentNotNull(other, "other");
            for (int i = 0; i < 12; i++)
            {
                ThemeColorIndex index = new ThemeColorIndex(i);
                DrawingColor color = this.TryGetDrawingColor(index);
                DrawingColor color2 = other.TryGetDrawingColor(index);
                if ((color == null) && (color2 != null))
                {
                    return false;
                }
                if ((color != null) && (color2 == null))
                {
                    return false;
                }
                if ((color != null) && ((color2 != null) && (color.FinalColor != color2.FinalColor)))
                {
                    return false;
                }
            }
            return true;
        }

        internal void SetColor(ThemeColorIndex index, SystemColorValues value)
        {
            this.SetDrawingColor(index, this.CreateDrawingColor(value));
        }

        internal void SetColor(ThemeColorIndex index, Color value)
        {
            this.SetDrawingColor(index, this.CreateDrawingColor(value));
        }

        public void SetDrawingColor(ThemeColorIndex index, DrawingColor value)
        {
            Guard.ArgumentNotNull(value, "DrawingColor");
            if (this.innerCollection.ContainsKey(index))
            {
                this.innerCollection[index] = value;
            }
            else
            {
                this.innerCollection.Add(index, value);
            }
        }

        private DrawingColor TryGetDrawingColor(ThemeColorIndex index) => 
            !this.innerCollection.ContainsKey(index) ? null : this.innerCollection[index];

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public DrawingColor Light1
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Light1);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Light1, value);
        }

        public DrawingColor Light2
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Light2);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Light2, value);
        }

        public DrawingColor Dark1
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Dark1);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Dark1, value);
        }

        public DrawingColor Dark2
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Dark2);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Dark2, value);
        }

        public DrawingColor Accent1
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Accent1);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Accent1, value);
        }

        public DrawingColor Accent2
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Accent2);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Accent2, value);
        }

        public DrawingColor Accent3
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Accent3);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Accent3, value);
        }

        public DrawingColor Accent4
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Accent4);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Accent4, value);
        }

        public DrawingColor Accent5
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Accent5);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Accent5, value);
        }

        public DrawingColor Accent6
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Accent6);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Accent6, value);
        }

        public DrawingColor Hyperlink
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.Hyperlink);
            set => 
                this.SetDrawingColor(ThemeColorIndex.Hyperlink, value);
        }

        public DrawingColor FollowedHyperlink
        {
            get => 
                this.TryGetDrawingColor(ThemeColorIndex.FollowedHyperlink);
            set => 
                this.SetDrawingColor(ThemeColorIndex.FollowedHyperlink, value);
        }

        public bool IsValidate =>
            this.CheckValidation();

        public Dictionary<SchemeColorValues, ThemeColorIndex> SchemeColorValuesToThemeColorIndexTranslationTable =>
            this.schemeColorValuesToThemeColorIndexTranslationTable;
    }
}

