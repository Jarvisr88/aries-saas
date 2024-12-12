namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Windows.Media;

    public interface IVisualChildrenContainer
    {
        void AddChild(Visual visual);
        void Clear();
        IEnumerator GetEnumerator();
        Visual GetVisualChild(int index);
        void RemoveChild(Visual visual);

        int VisualChildrenCount { get; }
    }
}

