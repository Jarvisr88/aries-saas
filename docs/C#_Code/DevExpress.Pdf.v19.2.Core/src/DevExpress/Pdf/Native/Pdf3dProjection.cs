namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class Pdf3dProjection : PdfObject
    {
        private readonly Pdf3dProjectionType type;
        private readonly Pdf3dClippingStyle clippingStyle;
        private readonly double? farClippingDistance;
        private readonly double? nearClippingDistance;
        private readonly double? fov;
        private readonly object perspectiveScaling;
        private readonly double? orthographicScaling;
        private readonly Pdf3dOrthographicBinding orthographicBinding;

        public Pdf3dProjection(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.type = dictionary.GetEnumName<Pdf3dProjectionType>("Subtype");
            this.clippingStyle = dictionary.GetEnumName<Pdf3dClippingStyle>("CS");
            this.farClippingDistance = dictionary.GetNumber("F");
            this.nearClippingDistance = dictionary.GetNumber("N");
            this.fov = dictionary.GetNumber("FOV");
            if (!dictionary.TryGetValue("PS", out this.perspectiveScaling))
            {
                this.perspectiveScaling = Pdf3dPerspectiveScaling.Width;
            }
            else
            {
                this.perspectiveScaling = dictionary.Objects.TryResolve(this.perspectiveScaling, null);
                PdfName perspectiveScaling = this.perspectiveScaling as PdfName;
                if (perspectiveScaling != null)
                {
                    this.perspectiveScaling = PdfEnumToStringConverter.Parse<Pdf3dPerspectiveScaling>(perspectiveScaling.Name, true);
                }
            }
            double? number = dictionary.GetNumber("OS");
            this.orthographicScaling = new double?((number != null) ? number.GetValueOrDefault() : 1.0);
            this.orthographicBinding = dictionary.GetEnumName<Pdf3dOrthographicBinding>("OB");
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddEnumName<Pdf3dProjectionType>("Subtype", this.type);
            dictionary.AddEnumName<Pdf3dClippingStyle>("CS", this.clippingStyle);
            dictionary.AddIfPresent("F", this.farClippingDistance);
            dictionary.AddIfPresent("N", this.nearClippingDistance);
            dictionary.AddIfPresent("FOV", this.fov);
            if (this.perspectiveScaling is Pdf3dPerspectiveScaling)
            {
                dictionary.AddEnumName<Pdf3dPerspectiveScaling>("PS", (Pdf3dPerspectiveScaling) this.perspectiveScaling);
            }
            else
            {
                dictionary.AddIfPresent("PS", this.perspectiveScaling);
            }
            dictionary.Add("OS", this.orthographicScaling, 1.0);
            dictionary.AddEnumName<Pdf3dOrthographicBinding>("OB", this.orthographicBinding);
            return dictionary;
        }

        public Pdf3dProjectionType Type =>
            this.type;

        public Pdf3dClippingStyle ClippingStyle =>
            this.clippingStyle;

        public double? FarClippingDistance =>
            this.farClippingDistance;

        public double? NearClippingDistance =>
            this.nearClippingDistance;

        public double? Fov =>
            this.fov;

        public object PerspectiveScaling =>
            this.perspectiveScaling;

        public double? OrthographicScaling =>
            this.orthographicScaling;

        public Pdf3dOrthographicBinding OrthographicBinding =>
            this.orthographicBinding;
    }
}

