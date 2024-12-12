namespace DevExpress.Pdf.Native
{
    using System;

    public class Pdf3dActivationParameters : PdfObject
    {
        private readonly Pdf3dActivationCircumstances activationCircumstances;
        private readonly Pdf3dActivationArtworkState activationArtworkState;
        private readonly Pdf3dDeactivationCircumstances deactivationCircumstances;
        private readonly Pdf3dDeactivationArtworkState deactivationArtworkState;
        private readonly bool showToolbar;
        private readonly bool showInterface;

        public Pdf3dActivationParameters(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.activationCircumstances = dictionary.GetEnumName<Pdf3dActivationCircumstances>("A");
            this.activationArtworkState = dictionary.GetEnumName<Pdf3dActivationArtworkState>("AIS");
            this.deactivationCircumstances = dictionary.GetEnumName<Pdf3dDeactivationCircumstances>("D");
            this.deactivationArtworkState = dictionary.GetEnumName<Pdf3dDeactivationArtworkState>("DIS");
            bool? boolean = dictionary.GetBoolean("TB");
            this.showToolbar = (boolean != null) ? boolean.GetValueOrDefault() : true;
            boolean = dictionary.GetBoolean("NP");
            this.showInterface = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddEnumName<Pdf3dActivationCircumstances>("A", this.activationCircumstances);
            dictionary.AddEnumName<Pdf3dActivationArtworkState>("AIS", this.activationArtworkState);
            dictionary.AddEnumName<Pdf3dDeactivationCircumstances>("D", this.deactivationCircumstances);
            dictionary.AddEnumName<Pdf3dDeactivationArtworkState>("DIS", this.deactivationArtworkState);
            dictionary.Add("TB", this.showToolbar, true);
            dictionary.Add("NP", this.showInterface, false);
            return dictionary;
        }

        public Pdf3dActivationCircumstances ActivationCircumstances =>
            this.activationCircumstances;

        public Pdf3dActivationArtworkState ActivationArtworkState =>
            this.activationArtworkState;

        public Pdf3dDeactivationCircumstances DeactivationCircumstances =>
            this.deactivationCircumstances;

        public Pdf3dDeactivationArtworkState DeactivationArtworkState =>
            this.deactivationArtworkState;

        public bool ShowToolbar =>
            this.showToolbar;

        public bool ShowInterface =>
            this.showInterface;
    }
}

