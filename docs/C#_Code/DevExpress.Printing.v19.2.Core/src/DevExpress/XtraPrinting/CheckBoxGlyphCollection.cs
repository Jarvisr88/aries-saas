namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    public class CheckBoxGlyphCollection : Collection<CheckBoxGlyph>
    {
        public CheckBoxGlyphCollection()
        {
        }

        public CheckBoxGlyphCollection(CheckBoxGlyphCollection collection) : base(collection)
        {
        }

        public void Add(CheckState checkState, ImageSource imageSource)
        {
            base.Add(new CheckBoxGlyph(checkState, imageSource));
        }

        protected override void InsertItem(int index, CheckBoxGlyph item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            CheckBoxGlyph glyph = base.Items.FirstOrDefault<CheckBoxGlyph>(x => x.CheckState == item.CheckState);
            base.InsertItem(index, item);
            if (glyph != null)
            {
                base.Items.Remove(glyph);
            }
        }

        protected override void SetItem(int index, CheckBoxGlyph item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            CheckBoxGlyph glyph = base.Items.FirstOrDefault<CheckBoxGlyph>(x => (x.CheckState == item.CheckState) && (this.Items.IndexOf(x) != index));
            base.SetItem(index, item);
            if (glyph != null)
            {
                base.Items.Remove(glyph);
            }
        }

        public ImageSource this[CheckState key]
        {
            get
            {
                CheckBoxGlyph local1 = base.Items.FirstOrDefault<CheckBoxGlyph>(x => x.CheckState == key);
                if (local1 != null)
                {
                    return local1.ImageSource;
                }
                CheckBoxGlyph local2 = local1;
                return null;
            }
            set
            {
                CheckBoxGlyph glyph = base.Items.FirstOrDefault<CheckBoxGlyph>(x => x.CheckState == key);
                if (glyph != null)
                {
                    glyph.ImageSource = value;
                }
                else
                {
                    base.Items.Add(new CheckBoxGlyph(key, value));
                }
            }
        }
    }
}

