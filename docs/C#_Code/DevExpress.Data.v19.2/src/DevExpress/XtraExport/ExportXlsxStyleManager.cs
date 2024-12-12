namespace DevExpress.XtraExport
{
    using System;
    using System.Drawing;
    using System.IO;

    public sealed class ExportXlsxStyleManager : ExportStyleManagerBase
    {
        public ExportXlsxStyleManager(string fileName, Stream stream) : base(fileName, stream)
        {
        }

        protected override ExportCacheCellStyle CreateDefaultStyle() => 
            new ExportCacheCellStyle { 
                TextColor = SystemColors.WindowText,
                TextAlignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                TextFont = new Font("Calibri", 11f),
                BkColor = Color.Transparent,
                FgColor = Color.Black,
                BrushStyle_ = BrushStyle.Clear,
                LeftBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1
                },
                TopBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1
                },
                RightBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1
                },
                BottomBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1
                }
            };
    }
}

