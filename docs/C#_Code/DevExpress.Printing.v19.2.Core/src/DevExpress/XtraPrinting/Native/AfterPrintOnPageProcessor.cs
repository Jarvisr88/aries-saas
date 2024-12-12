namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class AfterPrintOnPageProcessor : IAfterPrintOnPageProcessor
    {
        private readonly PrintingDocument document;

        public AfterPrintOnPageProcessor(PrintingDocument document)
        {
            this.document = document;
            this.MaxBrickRight = 0f;
        }

        public void Process(Page page)
        {
            page.Initialize();
            List<int> indices = new List<int>(10);
            RectangleF rect = page.GetRect(PointF.Empty);
            page.AfterPrintOnPage(indices, rect, rect, page, page.Index, this.document.Pages.Count, delegate (BrickBase brick, RectangleF brickBounds) {
                VisualBrick brick2 = brick as VisualBrick;
                if (brick2 != null)
                {
                    EditingField field;
                    BrickModifier[] modifiers = new BrickModifier[] { BrickModifier.MarginalHeader, BrickModifier.MarginalFooter };
                    if (!brick.HasModifier(modifiers))
                    {
                        this.MaxBrickRight = Math.Max(this.MaxBrickRight, brick2.GetRightOnPage());
                    }
                    if (this.document.NavigationInfo != null)
                    {
                        this.document.NavigationInfo.UpdateTargets(page, indices.ToArray(), brick2, brickBounds);
                    }
                    if (this.document.PrintingSystem.EditingFields.TryGetEditingField(brick2, out field) && (field.PageIndex < 0))
                    {
                        field.UpdatePageInfo(page, indices.ToArray());
                    }
                }
            });
        }

        public float MaxBrickRight { get; private set; }
    }
}

