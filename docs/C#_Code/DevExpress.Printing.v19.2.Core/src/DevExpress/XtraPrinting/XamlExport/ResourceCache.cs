namespace DevExpress.XtraPrinting.XamlExport
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ResourceCache
    {
        private const string watermarkResourceName = "Watermark";
        private Collection<XamlBorderStyle> borderStyles = new Collection<XamlBorderStyle>();
        private Collection<XamlLineStyle> borderDashStyles = new Collection<XamlLineStyle>();
        private Collection<XamlTextBlockStyle> textBlockStyles = new Collection<XamlTextBlockStyle>();
        private Collection<XamlLineStyle> lineStyles = new Collection<XamlLineStyle>();
        private Collection<ImageResource> imageResources = new Collection<ImageResource>();

        private string GenerateBorderDashStyleName() => 
            $"BorderDashStyle{this.borderDashStyles.Count + 1}";

        private string GenerateBorderStyleName() => 
            $"BorderStyle{this.borderStyles.Count + 1}";

        private string GenerateImageName() => 
            $"Image{this.imageResources.Count + 1}";

        private string GenerateLineStyleName() => 
            $"LineStyle{this.lineStyles.Count + 1}";

        private string GenerateTextBlockStyleName() => 
            $"TextBlockStyle{this.textBlockStyles.Count + 1}";

        public string RegisterBorderDashStyle(XamlLineStyle style) => 
            RegisterResourceCore<XamlLineStyle>(style, this.borderDashStyles, new Func<string>(this.GenerateBorderDashStyleName));

        public string RegisterBorderStyle(XamlBorderStyle style) => 
            RegisterResourceCore<XamlBorderStyle>(style, this.borderStyles, new Func<string>(this.GenerateBorderStyleName));

        public string RegisterImageResource(ImageResource resource) => 
            RegisterResourceCore<ImageResource>(resource, this.imageResources, new Func<string>(this.GenerateImageName));

        public string RegisterLineStyle(XamlLineStyle style) => 
            RegisterResourceCore<XamlLineStyle>(style, this.lineStyles, new Func<string>(this.GenerateLineStyleName));

        private static string RegisterResourceCore<T>(T resource, Collection<T> resourceCollection, Func<string> generateStyleName) where T: XamlResourceBase
        {
            string name;
            if (resourceCollection.Contains(resource))
            {
                name = resourceCollection[resourceCollection.IndexOf(resource)].Name;
            }
            else
            {
                name = generateStyleName();
                resource.SetName(name);
                resourceCollection.Add(resource);
            }
            return name;
        }

        public string RegisterTextBlockStyle(XamlTextBlockStyle style) => 
            RegisterResourceCore<XamlTextBlockStyle>(style, this.textBlockStyles, new Func<string>(this.GenerateTextBlockStyleName));

        public string RegisterWatermarkImageResource(ImageResource resource)
        {
            this.imageResources.Add(resource);
            resource.SetName("Watermark");
            return "Watermark";
        }

        public IEnumerable<XamlBorderStyle> BorderStyles =>
            this.borderStyles;

        public IEnumerable<XamlLineStyle> BorderDashStyles =>
            this.borderDashStyles;

        public IEnumerable<XamlTextBlockStyle> TextBlockStyles =>
            this.textBlockStyles;

        public IEnumerable<XamlLineStyle> LineStyles =>
            this.lineStyles;

        public IEnumerable<ImageResource> ImageResources =>
            this.imageResources;
    }
}

