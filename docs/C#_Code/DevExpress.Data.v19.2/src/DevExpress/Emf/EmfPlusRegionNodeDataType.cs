namespace DevExpress.Emf
{
    using System;

    public enum EmfPlusRegionNodeDataType
    {
        RegionNodeDataTypeAnd = 1,
        RegionNodeDataTypeOr = 2,
        RegionNodeDataTypeXor = 3,
        RegionNodeDataTypeExclude = 4,
        RegionNodeDataTypeComplement = 5,
        RegionNodeDataTypeRect = 0x10000000,
        RegionNodeDataTypePath = 0x10000001,
        RegionNodeDataTypeEmpty = 0x10000002,
        RegionNodeDataTypeInfinite = 0x10000003
    }
}

