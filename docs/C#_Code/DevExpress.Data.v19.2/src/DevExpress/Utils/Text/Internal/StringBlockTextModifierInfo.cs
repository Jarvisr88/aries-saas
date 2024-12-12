namespace DevExpress.Utils.Text.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public class StringBlockTextModifierInfo
    {
        public StringBlockTextModifier Modifier { get; set; }

        public int Height { get; set; }

        public int AscentHeight { get; set; }

        public int InternalLeading { get; set; }

        public int InnerHeight =>
            this.AscentHeight - this.InternalLeading;

        public float Size { get; set; }
    }
}

