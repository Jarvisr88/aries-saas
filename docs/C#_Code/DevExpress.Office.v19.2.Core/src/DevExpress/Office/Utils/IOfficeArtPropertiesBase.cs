namespace DevExpress.Office.Utils
{
    using System;

    public interface IOfficeArtPropertiesBase
    {
        bool IsBehindDoc { get; set; }

        bool UseIsBehindDoc { get; set; }

        bool Filled { get; set; }

        bool UseFilled { get; set; }

        bool LayoutInCell { get; set; }

        bool UseLayoutInCell { get; set; }
    }
}

