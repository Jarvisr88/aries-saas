namespace DevExpress.Office.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public interface IOfficeScrollbar
    {
        event ScrollEventHandler Scroll;

        void BeginUpdate();
        void EndUpdate();

        int Value { get; set; }

        int Minimum { get; set; }

        int Maximum { get; set; }

        int LargeChange { get; set; }

        int SmallChange { get; set; }

        bool Enabled { get; set; }
    }
}

