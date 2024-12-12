namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BrickLayoutInfoEnumerator : IEnumerator
    {
        private IEnumerator<KeyValuePair<Brick, RectangleDF>> en;
        private BrickLayoutInfo currentInfo;

        public BrickLayoutInfoEnumerator(IEnumerator<KeyValuePair<Brick, RectangleDF>> en);
        bool IEnumerator.MoveNext();
        void IEnumerator.Reset();

        object IEnumerator.Current { get; }
    }
}

