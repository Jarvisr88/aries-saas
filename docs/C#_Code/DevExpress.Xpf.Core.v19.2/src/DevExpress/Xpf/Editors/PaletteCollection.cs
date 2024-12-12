namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public class PaletteCollection : List<ColorPalette>
    {
        private readonly string name;

        public PaletteCollection(PaletteCollection collection) : this(collection.Name, collection.ToArray())
        {
        }

        public PaletteCollection(string name, params ColorPalette[] palettes) : base(palettes)
        {
            this.name = name;
        }

        public void Add(ColorPalette newPalette)
        {
            if ((newPalette != null) && ((newPalette.Name != null) && !ReferenceEquals(this.FindPaletteByName(newPalette.Name), NullPalette.Instance)))
            {
                throw new ArgumentException("Palette has been already added", newPalette.Name);
            }
            ColorPalette item = newPalette;
            if (newPalette == null)
            {
                ColorPalette local1 = newPalette;
                item = NullPalette.Instance;
            }
            this.Add(item);
        }

        private ColorPalette FindPaletteByName(string name)
        {
            ColorPalette palette2;
            using (List<ColorPalette>.Enumerator enumerator = base.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ColorPalette current = enumerator.Current;
                        if ((name == null) || (name != current.Name))
                        {
                            continue;
                        }
                        palette2 = current;
                    }
                    else
                    {
                        return NullPalette.Instance;
                    }
                    break;
                }
            }
            return palette2;
        }

        public ColorPalette this[string name] =>
            this.FindPaletteByName(name);

        [Description("Gets the collection's name.")]
        public string Name =>
            this.name;
    }
}

