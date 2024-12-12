namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Controls;

    public interface IScrollSnapPointsInfo
    {
        event EventHandler HorizontalSnapPointsChanged;

        event EventHandler VerticalSnapPointsChanged;

        IEnumerable<float> GetIrregularSnapPoints(Orientation orientation, SnapPointsAlignment alignment);
        float GetRegularSnapPoints(Orientation orientation, SnapPointsAlignment alignment, out float offset);

        bool AreHorizontalSnapPointsRegular { get; }

        bool AreVerticalSnapPointsRegular { get; }
    }
}

