namespace DevExpress.Utils.Commands
{
    using System;

    public interface ICommandUIState
    {
        bool Enabled { get; set; }

        bool Visible { get; set; }

        bool Checked { get; set; }

        object EditValue { get; set; }
    }
}

