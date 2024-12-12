namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Model;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class SchemeColorDestination : DrawingColorPropertiesDestinationBase
    {
        public static Dictionary<SchemeColorValues, string> schemeColorTable = CreateSchemeColorTable();

        public SchemeColorDestination(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        private static Dictionary<SchemeColorValues, string> CreateSchemeColorTable() => 
            new Dictionary<SchemeColorValues, string> { 
                { 
                    SchemeColorValues.Accent1,
                    "accent1"
                },
                { 
                    SchemeColorValues.Accent2,
                    "accent2"
                },
                { 
                    SchemeColorValues.Accent3,
                    "accent3"
                },
                { 
                    SchemeColorValues.Accent4,
                    "accent4"
                },
                { 
                    SchemeColorValues.Accent5,
                    "accent5"
                },
                { 
                    SchemeColorValues.Accent6,
                    "accent6"
                },
                { 
                    SchemeColorValues.Background1,
                    "bg1"
                },
                { 
                    SchemeColorValues.Background2,
                    "bg2"
                },
                { 
                    SchemeColorValues.Dark1,
                    "dk1"
                },
                { 
                    SchemeColorValues.Dark2,
                    "dk2"
                },
                { 
                    SchemeColorValues.FollowedHyperlink,
                    "folHlink"
                },
                { 
                    SchemeColorValues.Hyperlink,
                    "hlink"
                },
                { 
                    SchemeColorValues.Light1,
                    "lt1"
                },
                { 
                    SchemeColorValues.Light2,
                    "lt2"
                },
                { 
                    SchemeColorValues.Style,
                    "phClr"
                },
                { 
                    SchemeColorValues.Text1,
                    "tx1"
                },
                { 
                    SchemeColorValues.Text2,
                    "tx2"
                }
            };

        protected override void SetColorPropertyValue(XmlReader reader)
        {
            SchemeColorValues values = this.Importer.GetWpEnumValue<SchemeColorValues>(reader, "val", schemeColorTable, SchemeColorValues.Empty);
            if (values == SchemeColorValues.Empty)
            {
                this.Importer.ThrowInvalidFile();
            }
            base.ColorModelInfo.SchemeColor = values;
        }
    }
}

