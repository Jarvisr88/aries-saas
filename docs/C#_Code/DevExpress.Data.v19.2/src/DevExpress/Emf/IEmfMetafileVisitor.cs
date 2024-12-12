namespace DevExpress.Emf
{
    using System;

    public interface IEmfMetafileVisitor
    {
        void Visit(EmfMetafileHeaderRecord headerRecord);
        void Visit(EmfPlusClearRecord clearRecord);
        void Visit(EmfPlusContinuedObjectRecord record);
        void Visit(EmfPlusDrawArcRecord record);
        void Visit(EmfPlusDrawBeziersRecord record);
        void Visit(EmfPlusDrawDriverStringRecord record);
        void Visit(EmfPlusDrawEllipseRecord record);
        void Visit(EmfPlusDrawImagePointsRecord record);
        void Visit(EmfPlusDrawImageRecord record);
        void Visit(EmfPlusDrawLinesRecord record);
        void Visit(EmfPlusDrawPathRecord record);
        void Visit(EmfPlusDrawPieRecord record);
        void Visit(EmfPlusDrawRectsRecord record);
        void Visit(EmfPlusDrawStringRecord record);
        void Visit(EmfPlusFillEllipseRecord record);
        void Visit(EmfPlusFillPathRecord record);
        void Visit(EmfPlusFillPieRecord record);
        void Visit(EmfPlusFillPolygonRecord record);
        void Visit(EmfPlusFillRectsRecord record);
        void Visit(EmfPlusHeaderRecord headerRecord);
        void Visit(EmfPlusModifyWorldTransform record);
        void Visit(EmfPlusObjectRecord record);
        void Visit(EmfPlusResetClipRecord record);
        void Visit(EmfPlusResetWorldTransformRecord record);
        void Visit(EmfPlusRestoreRecord record);
        void Visit(EmfPlusRotateWorldTransformRecord record);
        void Visit(EmfPlusSaveRecord record);
        void Visit(EmfPlusSetClipPathRecord record);
        void Visit(EmfPlusSetClipRectRecord record);
        void Visit(EmfPlusSetClipRegionRecord record);
        void Visit(EmfPlusSetPageTransformRecord record);
        void Visit(EmfPlusSetWorldTransformRecord record);
    }
}

