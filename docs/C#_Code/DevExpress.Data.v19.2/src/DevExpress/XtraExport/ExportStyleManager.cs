namespace DevExpress.XtraExport
{
    using System;
    using System.Drawing;
    using System.IO;

    public sealed class ExportStyleManager : ExportStyleManagerBase
    {
        public ExportStyleManager(string fileName, Stream stream) : base(fileName, stream)
        {
        }

        protected override ExportCacheCellStyle CreateDefaultStyle() => 
            new ExportCacheCellStyle { 
                TextColor = SystemColors.WindowText,
                TextAlignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                TextFont = new Font("Tahoma", 8f),
                BkColor = SystemColors.Window,
                FgColor = Color.Black,
                BrushStyle_ = BrushStyle.Clear,
                LeftBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1,
                    IsDefault = true
                },
                TopBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1,
                    IsDefault = true
                },
                RightBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1,
                    IsDefault = true
                },
                BottomBorder = { 
                    Color_ = SystemColors.ActiveBorder,
                    Width = 1,
                    IsDefault = true
                }
            };
    }
}

