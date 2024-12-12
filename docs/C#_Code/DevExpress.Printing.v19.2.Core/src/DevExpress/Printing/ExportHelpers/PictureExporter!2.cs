namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class PictureExporter<TCol, TRow> : ExportHelperBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public PictureExporter(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        public override void Execute()
        {
        }

        private void ExportImage(Image exportedImage, int column, int row, int width, int height, FuncType<TCol, TRow> setAction)
        {
            if (exportedImage != null)
            {
                IXlPicture picture = base.ExportInfo.Exporter.BeginPicture();
                picture.Image = exportedImage;
                if (setAction != null)
                {
                    setAction(column, row, width, height, picture);
                }
                base.ExportInfo.Exporter.EndPicture();
            }
        }

        private static void FitToCell(int column, int row, int width, int height, IXlPicture picture)
        {
            picture.FitToCell(new XlCellPosition(column, row), width, height, true);
        }

        private static void SetOneCellAnchor(int column, int row, int width, int height, IXlPicture picture)
        {
            picture.SetOneCellAnchor(new XlAnchorPoint(column, row), width, height);
        }

        public void SetPicture(Image picture, int column, int row, int width, int height, XlAnchorType type)
        {
            FuncType<TCol, TRow> setAction = null;
            switch (type)
            {
                case XlAnchorType.TwoCell:
                    setAction = new FuncType<TCol, TRow>(PictureExporter<TCol, TRow>.SetTwoCellAnchor);
                    break;

                case XlAnchorType.OneCell:
                    setAction = new FuncType<TCol, TRow>(PictureExporter<TCol, TRow>.SetOneCellAnchor);
                    break;

                case XlAnchorType.Absolute:
                    setAction = new FuncType<TCol, TRow>(PictureExporter<TCol, TRow>.FitToCell);
                    break;

                default:
                    break;
            }
            this.ExportImage(picture, column, row, width, height, setAction);
        }

        private static void SetTwoCellAnchor(int column, int row, int width, int height, IXlPicture picture)
        {
            picture.SetTwoCellAnchor(new XlAnchorPoint(column, row), new XlAnchorPoint(width, height), XlAnchorType.TwoCell);
        }

        private delegate void FuncType(int column, int row, int width, int height, IXlPicture picture);
    }
}

