namespace DevExpress.XtraPrinting.XamlExport
{
    using System;
    using System.Collections.Generic;

    public class ResourceMap
    {
        private Dictionary<VisualBrick, string> borderStylesDictionary = new Dictionary<VisualBrick, string>();
        private Dictionary<VisualBrick, string> borderDashStylesDictionary = new Dictionary<VisualBrick, string>();
        private Dictionary<TextBrick, string> textBlockStylesDictionary = new Dictionary<TextBrick, string>();
        private Dictionary<LineBrick, string> lineStylesDictionary = new Dictionary<LineBrick, string>();
        private Dictionary<BrickBase, string> imageResourcesDictionary = new Dictionary<BrickBase, string>();
        private bool shouldAddCheckBoxTemplates;

        public Dictionary<VisualBrick, string> BorderStylesDictionary =>
            this.borderStylesDictionary;

        public Dictionary<VisualBrick, string> BorderDashStylesDictionary =>
            this.borderDashStylesDictionary;

        public Dictionary<TextBrick, string> TextBlockStylesDictionary =>
            this.textBlockStylesDictionary;

        public Dictionary<LineBrick, string> LineStylesDictionary =>
            this.lineStylesDictionary;

        public Dictionary<BrickBase, string> ImageResourcesDictionary =>
            this.imageResourcesDictionary;

        public bool ShouldAddCheckBoxTemplates
        {
            get => 
                this.shouldAddCheckBoxTemplates;
            set => 
                this.shouldAddCheckBoxTemplates = value;
        }
    }
}

