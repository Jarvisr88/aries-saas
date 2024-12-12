namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Model;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class SystemColorDestination : DrawingColorPropertiesDestinationBase
    {
        public static Dictionary<SystemColorValues, string> systemColorTable = CreateSystemColorTable();

        public SystemColorDestination(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        private static Dictionary<SystemColorValues, string> CreateSystemColorTable() => 
            new Dictionary<SystemColorValues, string> { 
                { 
                    SystemColorValues.Sc3dDkShadow,
                    "3dDkShadow"
                },
                { 
                    SystemColorValues.Sc3dLight,
                    "3dLight"
                },
                { 
                    SystemColorValues.ScActiveBorder,
                    "activeBorder"
                },
                { 
                    SystemColorValues.ScActiveCaption,
                    "activeCaption"
                },
                { 
                    SystemColorValues.ScAppWorkspace,
                    "appWorkspace"
                },
                { 
                    SystemColorValues.ScBackground,
                    "background"
                },
                { 
                    SystemColorValues.ScBtnFace,
                    "btnFace"
                },
                { 
                    SystemColorValues.ScBtnHighlight,
                    "btnHighlight"
                },
                { 
                    SystemColorValues.ScBtnShadow,
                    "btnShadow"
                },
                { 
                    SystemColorValues.ScBtnText,
                    "btnText"
                },
                { 
                    SystemColorValues.ScCaptionText,
                    "captionText"
                },
                { 
                    SystemColorValues.ScGradientActiveCaption,
                    "gradientActiveCaption"
                },
                { 
                    SystemColorValues.ScGradientInactiveCaption,
                    "gradientInactiveCaption"
                },
                { 
                    SystemColorValues.ScGrayText,
                    "grayText"
                },
                { 
                    SystemColorValues.ScHighlight,
                    "highlight"
                },
                { 
                    SystemColorValues.ScHighlightText,
                    "highlightText"
                },
                { 
                    SystemColorValues.ScHotLight,
                    "hotLight"
                },
                { 
                    SystemColorValues.ScInactiveBorder,
                    "inactiveBorder"
                },
                { 
                    SystemColorValues.ScInactiveCaption,
                    "inactiveCaption"
                },
                { 
                    SystemColorValues.ScInactiveCaptionText,
                    "inactiveCaptionText"
                },
                { 
                    SystemColorValues.ScInfoBk,
                    "infoBk"
                },
                { 
                    SystemColorValues.ScInfoText,
                    "infoText"
                },
                { 
                    SystemColorValues.ScMenu,
                    "menu"
                },
                { 
                    SystemColorValues.ScMenuBar,
                    "menuBar"
                },
                { 
                    SystemColorValues.ScMenuHighlight,
                    "menuHighlight"
                },
                { 
                    SystemColorValues.ScMenuText,
                    "menuText"
                },
                { 
                    SystemColorValues.ScScrollBar,
                    "scrollBar"
                },
                { 
                    SystemColorValues.ScWindow,
                    "window"
                },
                { 
                    SystemColorValues.ScWindowFrame,
                    "windowFrame"
                },
                { 
                    SystemColorValues.ScWindowText,
                    "windowText"
                }
            };

        protected override void SetColorPropertyValue(XmlReader reader)
        {
            SystemColorValues values = this.Importer.GetWpEnumValue<SystemColorValues>(reader, "val", systemColorTable, SystemColorValues.Empty);
            if (values == SystemColorValues.Empty)
            {
                this.Importer.ThrowInvalidFile();
            }
            base.ColorModelInfo.SystemColor = values;
        }
    }
}

