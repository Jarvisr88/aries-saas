namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;

    public class ShapeEffectComparer : IComparer<IDrawingEffect>
    {
        public int Compare(IDrawingEffect x, IDrawingEffect y) => 
            this.GetOrder(x).CompareTo(this.GetOrder(y));

        private int GetOrder(IDrawingEffect effect)
        {
            Type key = effect.GetType();
            return (!ShapeEffectRenderingWalker.EffectPriority.ContainsKey(key) ? 0xff : ShapeEffectRenderingWalker.EffectPriority[key]);
        }
    }
}

