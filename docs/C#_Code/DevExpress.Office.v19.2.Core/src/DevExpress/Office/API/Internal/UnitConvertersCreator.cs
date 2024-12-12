namespace DevExpress.Office.API.Internal
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;

    public class UnitConvertersCreator
    {
        public Dictionary<DocumentUnit, UnitConverter> CreateUnitConverters(DocumentModelUnitConverter modelUnitConverter) => 
            new Dictionary<DocumentUnit, UnitConverter> { 
                { 
                    DocumentUnit.Document,
                    new DocumentsToModelUnitsConverter(modelUnitConverter)
                },
                { 
                    DocumentUnit.Inch,
                    new InchesToModelUnitsConverter(modelUnitConverter)
                },
                { 
                    DocumentUnit.Millimeter,
                    new MillimetersToModelUnitsConverter(modelUnitConverter)
                },
                { 
                    DocumentUnit.Centimeter,
                    new CentimetersToModelUnitsConverter(modelUnitConverter)
                },
                { 
                    DocumentUnit.Point,
                    new PointsToModelUnitsConverter(modelUnitConverter)
                }
            };
    }
}

