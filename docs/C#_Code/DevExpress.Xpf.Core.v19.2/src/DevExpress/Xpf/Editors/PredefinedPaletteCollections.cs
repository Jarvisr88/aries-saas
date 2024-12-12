namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public static class PredefinedPaletteCollections
    {
        private static readonly string themeColorsName = EditorLocalizer.GetString(EditorStringId.ColorEdit_ThemeColorsCaption);
        private static readonly string gradientColorsName = null;
        private static readonly string standardColorsName = EditorLocalizer.GetString(EditorStringId.ColorEdit_StandardColorsCaption);
        private static PaletteCollection apex;
        private static PaletteCollection aspect;
        private static PaletteCollection civic;
        private static PaletteCollection concourse;
        private static PaletteCollection equality;
        private static PaletteCollection flow;
        private static PaletteCollection foundry;
        private static PaletteCollection grayscale;
        private static PaletteCollection median;
        private static PaletteCollection metro;
        private static PaletteCollection module;
        private static PaletteCollection office;
        private static PaletteCollection opulent;
        private static PaletteCollection oriel;
        private static PaletteCollection origin;
        private static PaletteCollection paper;
        private static PaletteCollection solstice;
        private static PaletteCollection trek;
        private static PaletteCollection urban;
        private static PaletteCollection verve;
        private static ReadOnlyCollection<PaletteCollection> collections;

        static PredefinedPaletteCollections()
        {
            ColorPalette[] palettes = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Apex), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Apex), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            apex = new PaletteCollection("Apex", palettes);
            ColorPalette[] paletteArray2 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Aspect), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Aspect), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            aspect = new PaletteCollection("Aspect", paletteArray2);
            ColorPalette[] paletteArray3 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Civic), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Civic), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            civic = new PaletteCollection("Civic", paletteArray3);
            ColorPalette[] paletteArray4 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Concourse), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Concourse), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            concourse = new PaletteCollection("Concourse", paletteArray4);
            ColorPalette[] paletteArray5 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Equality), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Equality), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            equality = new PaletteCollection("Equality", paletteArray5);
            ColorPalette[] paletteArray6 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Flow), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Flow), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            flow = new PaletteCollection("Flow", paletteArray6);
            ColorPalette[] paletteArray7 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Foundry), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Foundry), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            foundry = new PaletteCollection("Foundry", paletteArray7);
            ColorPalette[] paletteArray8 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Grayscale), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Grayscale), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            grayscale = new PaletteCollection("Grayscale", paletteArray8);
            ColorPalette[] paletteArray9 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Median), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Median), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            median = new PaletteCollection("Median", paletteArray9);
            ColorPalette[] paletteArray10 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Metro), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Metro), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            metro = new PaletteCollection("Metro", paletteArray10);
            ColorPalette[] paletteArray11 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Module), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Module), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            module = new PaletteCollection("Module", paletteArray11);
            ColorPalette[] paletteArray12 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Office), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Office), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            office = new PaletteCollection("Office", paletteArray12);
            ColorPalette[] paletteArray13 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Opulent), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Opulent), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            opulent = new PaletteCollection("Opulent", paletteArray13);
            ColorPalette[] paletteArray14 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Oriel), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Oriel), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            oriel = new PaletteCollection("Oriel", paletteArray14);
            ColorPalette[] paletteArray15 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Origin), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Origin), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            origin = new PaletteCollection("Origin", paletteArray15);
            ColorPalette[] paletteArray16 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Paper), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Paper), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            paper = new PaletteCollection("Paper", paletteArray16);
            ColorPalette[] paletteArray17 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Solstice), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Solstice), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            solstice = new PaletteCollection("Solstice", paletteArray17);
            ColorPalette[] paletteArray18 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Trek), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Trek), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            trek = new PaletteCollection("Trek", paletteArray18);
            ColorPalette[] paletteArray19 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Urban), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Urban), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            urban = new PaletteCollection("Urban", paletteArray19);
            ColorPalette[] paletteArray20 = new ColorPalette[] { new CustomPalette(themeColorsName, PredefinedColorCollections.Verve), ColorPalette.CreateGradientPalette(gradientColorsName, PredefinedColorCollections.Verve), new CustomPalette(standardColorsName, PredefinedColorCollections.Standard) };
            verve = new PaletteCollection("Verve", paletteArray20);
            collections = CreatePaletteCollections();
        }

        private static ReadOnlyCollection<PaletteCollection> CreatePaletteCollections()
        {
            PaletteCollection[] collection = new PaletteCollection[20];
            collection[0] = Apex;
            collection[1] = Aspect;
            collection[2] = Civic;
            collection[3] = Concourse;
            collection[4] = Equality;
            collection[5] = Flow;
            collection[6] = Foundry;
            collection[7] = Grayscale;
            collection[8] = Median;
            collection[9] = Metro;
            collection[10] = Module;
            collection[11] = Office;
            collection[12] = Opulent;
            collection[13] = Oriel;
            collection[14] = Origin;
            collection[15] = Paper;
            collection[0x10] = Solstice;
            collection[0x11] = Trek;
            collection[0x12] = Urban;
            collection[0x13] = Verve;
            return new List<PaletteCollection>(collection).AsReadOnly();
        }

        [Description("Gets the Apex palette collection.")]
        public static PaletteCollection Apex =>
            new PaletteCollection(apex);

        [Description("Gets the Aspect palette collection.")]
        public static PaletteCollection Aspect =>
            new PaletteCollection(aspect);

        [Description("Gets the Civic palette collection.")]
        public static PaletteCollection Civic =>
            new PaletteCollection(civic);

        [Description("Gets the Concourse palette collection.")]
        public static PaletteCollection Concourse =>
            new PaletteCollection(concourse);

        [Description("Gets the Equality palette collection.")]
        public static PaletteCollection Equality =>
            new PaletteCollection(equality);

        [Description("Gets the Flow palette collection.")]
        public static PaletteCollection Flow =>
            new PaletteCollection(flow);

        [Description("Gets the Foundry palette collection.")]
        public static PaletteCollection Foundry =>
            new PaletteCollection(foundry);

        [Description("Gets the Grayscale palette collection.")]
        public static PaletteCollection Grayscale =>
            new PaletteCollection(grayscale);

        [Description("Gets the Median palette collection.")]
        public static PaletteCollection Median =>
            new PaletteCollection(median);

        [Description("Gets the Metro palette collection.")]
        public static PaletteCollection Metro =>
            new PaletteCollection(metro);

        [Description("Gets the Module palette collection.")]
        public static PaletteCollection Module =>
            new PaletteCollection(module);

        [Description("Gets the Office palette collection.")]
        public static PaletteCollection Office =>
            new PaletteCollection(office);

        [Description("Gets the Opulent palette collection.")]
        public static PaletteCollection Opulent =>
            new PaletteCollection(opulent);

        [Description("Gets the Oriel palette collection.")]
        public static PaletteCollection Oriel =>
            new PaletteCollection(oriel);

        [Description("Gets the Origin palette collection.")]
        public static PaletteCollection Origin =>
            new PaletteCollection(origin);

        [Description("Gets the Paper palette collection.")]
        public static PaletteCollection Paper =>
            new PaletteCollection(paper);

        [Description("Gets the Solstice palette collection.")]
        public static PaletteCollection Solstice =>
            new PaletteCollection(solstice);

        [Description("Gets the Trek palette collection.")]
        public static PaletteCollection Trek =>
            new PaletteCollection(trek);

        [Description("Gets the Urban palette collection.")]
        public static PaletteCollection Urban =>
            new PaletteCollection(urban);

        [Description("Gets the Verve palette collection.")]
        public static PaletteCollection Verve =>
            new PaletteCollection(verve);

        [Description("Gets the collection of the predefined palettes collections.")]
        public static ReadOnlyCollection<PaletteCollection> Collections =>
            collections;
    }
}

