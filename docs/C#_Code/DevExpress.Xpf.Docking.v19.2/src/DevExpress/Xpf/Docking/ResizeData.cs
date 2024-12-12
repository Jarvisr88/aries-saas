namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class ResizeData
    {
        public DefinitionBase Definition1;
        public int Definition1Index;
        public DefinitionBase Definition2;
        public int Definition2Index;
        public System.Windows.Controls.Grid Grid;
        public double OriginalDefinition1ActualLength;
        public GridLength OriginalDefinition1Length;
        public double OriginalDefinition2ActualLength;
        public GridLength OriginalDefinition2Length;
        public GridResizeDirection ResizeDirection;
        public DevExpress.Xpf.Docking.SplitBehavior SplitBehavior;
        public int SplitterIndex;
        public double SplitterLength;
    }
}

