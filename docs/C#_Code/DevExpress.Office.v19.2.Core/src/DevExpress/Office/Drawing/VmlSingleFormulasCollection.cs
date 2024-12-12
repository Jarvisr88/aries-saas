namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class VmlSingleFormulasCollection : SimpleCollection<VmlSingleFormula>, ISupportsCopyFrom<VmlSingleFormulasCollection>, ICloneable<VmlSingleFormulasCollection>
    {
        public VmlSingleFormulasCollection Clone()
        {
            VmlSingleFormulasCollection formulass = new VmlSingleFormulasCollection();
            formulass.CopyFrom(this);
            return formulass;
        }

        public void CopyFrom(VmlSingleFormulasCollection source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.Clear();
            source.ForEach(f => this.Add(f.Clone()));
        }
    }
}

