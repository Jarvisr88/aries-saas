namespace DevExpress.Office.Utils
{
    using System;

    internal static class OfficeArtSolverContainerFileBlockFactory
    {
        public static OfficeArtSolverContainerFileBlock CreateInstance(int typeCode) => 
            (typeCode == 0xf012) ? ((OfficeArtSolverContainerFileBlock) new OfficeArtFConnectionRule()) : ((typeCode == 0xf014) ? ((OfficeArtSolverContainerFileBlock) new OfficeArtFArcRule()) : ((typeCode == 0xf017) ? ((OfficeArtSolverContainerFileBlock) new OfficeArtFCalloutRule()) : null));
    }
}

