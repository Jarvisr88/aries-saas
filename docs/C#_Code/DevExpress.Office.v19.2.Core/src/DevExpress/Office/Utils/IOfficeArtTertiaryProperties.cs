namespace DevExpress.Office.Utils
{
    using System;

    public interface IOfficeArtTertiaryProperties : IOfficeArtPropertiesBase
    {
        bool UseRelativeWidth { get; set; }

        bool UseRelativeHeight { get; set; }

        bool UsePosH { get; set; }

        bool UsePosV { get; set; }

        bool IsCanvas { get; set; }

        int PctHoriz { get; set; }

        int PctVert { get; set; }

        int PctHorizPos { get; set; }

        int PctVertPos { get; set; }

        bool HorizRule { get; set; }

        bool UseHorizRule { get; set; }

        int AlignHR { get; set; }

        int PctHR { get; set; }

        int DxHeightHR { get; set; }

        int DxWidthHR { get; set; }

        bool StandardHR { get; set; }

        bool UseStandardHR { get; set; }

        bool NoShadeHR { get; set; }

        bool UseNoShadeHR { get; set; }

        bool PseudoInline { get; set; }

        DrawingGroupShapePosH.Msoph PosH { get; set; }

        DrawingGroupShapePosV.Msopv PosV { get; set; }

        DrawingGroupShapePosRelH.Msoprh PosRelH { get; set; }

        DrawingGroupShapePosRelV.Msoprv PosRelV { get; set; }

        DrawingGroupShape2SizeRelH.RelativeFrom SizeRelH { get; set; }

        DrawingGroupShape2SizeRelV.RelativeFrom SizeRelV { get; set; }
    }
}

