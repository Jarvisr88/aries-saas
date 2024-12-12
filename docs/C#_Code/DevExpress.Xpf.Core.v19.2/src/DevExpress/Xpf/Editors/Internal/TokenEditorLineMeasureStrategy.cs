namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TokenEditorLineMeasureStrategy : TokenEditorMeasureStrategyBase
    {
        public TokenEditorLineMeasureStrategy(TokenEditorPanel panel) : base(panel)
        {
        }

        public override List<UIElement> Arrange(Size finalSize)
        {
            List<UIElement> list = new List<UIElement>();
            int index = 0;
            double x = 0.0;
            base.OwnerPanel.CalcRelativeOffsetAndIndex(ref x, ref index);
            while (true)
            {
                if (this.CanArrange(index, x))
                {
                    UIElement item = null;
                    double width = 0.0;
                    item = base.OwnerPanel.GetContainer(this.ConvertToEditableIndex(index));
                    if (item != null)
                    {
                        width = !base.IsNewToken(index) ? base.GetTokenMaxWidth(item.DesiredSize.Width) : (base.CanArrangeNewToken ? ((base.TokensCount > 0) ? item.DesiredSize.Width : finalSize.Width) : 0.0);
                        Rect finalRect = new Rect(x, base.CalcTopPoint(finalSize.Height, item.DesiredSize.Height), width, item.DesiredSize.Height);
                        item.Arrange(finalRect);
                        list.Add(item);
                        x += width;
                        index++;
                        continue;
                    }
                }
                return list;
            }
        }

        protected override bool CanArrange(int index, double x) => 
            index <= base.MaxVisibleIndex;

        public override int ConvertToEditableIndex(int visualIndex) => 
            !base.IsNewToken(visualIndex) ? ((base.TokensCount > 0) ? (visualIndex - 1) : -1) : base.TokensCount;

        public override int ConvertToVisibleIndex(int editableIndex) => 
            (editableIndex != base.TokensCount) ? ((base.TokensCount > 0) ? (editableIndex + 1) : -1) : this.NewTokenVisibleIndex;

        public override Size Measure(Size constraint)
        {
            double wholeWidth = 0.0;
            double wholeHeight = 0.0;
            int num3 = 1;
            int index = Math.Max(0, base.OwnerPanel.StartMeasureIndex - num3);
            double num5 = 0.0;
            while ((index <= base.TokensCount) && (num3 > 0))
            {
                Size desiredSize = new Size();
                if (!base.ShouldMeasureNewToken(index))
                {
                    desiredSize = this.MeasureToken(constraint, ref wholeWidth, ref wholeHeight, index);
                }
                else
                {
                    this.MeasureNewToken(constraint, ref wholeWidth, ref wholeHeight);
                    desiredSize = base.NewToken.DesiredSize;
                }
                if (index == base.OwnerPanel.StartMeasureIndex)
                {
                    num5 -= desiredSize.Width;
                }
                if (index > base.OwnerPanel.StartMeasureIndex)
                {
                    num5 += desiredSize.Width;
                }
                if (num5 > constraint.Width)
                {
                    num3--;
                }
                index++;
            }
            return new Size(base.CalcResultSize(constraint.Width, wholeWidth), base.CalcResultSize(constraint.Height, wholeHeight));
        }

        public override int MeasureFromEnd(Size constraint, out double offset)
        {
            int maxVisibleIndex = base.MaxVisibleIndex;
            double wholeWidth = 0.0;
            double wholeHeight = 0.0;
            while ((maxVisibleIndex > -1) && (wholeWidth < constraint.Width))
            {
                if (base.ShouldMeasureNewToken(maxVisibleIndex))
                {
                    this.MeasureNewToken(constraint, ref wholeWidth, ref wholeHeight);
                }
                else
                {
                    this.MeasureToken(constraint, ref wholeWidth, ref wholeHeight, maxVisibleIndex);
                }
                maxVisibleIndex--;
            }
            offset = Math.Abs((double) (wholeWidth - constraint.Width));
            return (maxVisibleIndex + 1);
        }

        private void MeasureNewToken(Size constraint, ref double wholeWidth, ref double wholeHeight)
        {
            base.NewToken.Measure(constraint);
            base.OwnerPanel.AddNewTokenContainerToMeasured();
            if (base.ShowNewToken)
            {
                wholeWidth += base.NewToken.DesiredSize.Width;
            }
            wholeHeight = Math.Max(wholeHeight, base.NewToken.DesiredSize.Height);
        }

        private Size MeasureToken(Size constraint, ref double wholeWidth, ref double wholeHeight, int index)
        {
            UIElement element = base.OwnerPanel.PrepareContainer(this.ConvertToEditableIndex(index));
            if (element == null)
            {
                return new Size();
            }
            constraint = new Size(base.GetTokenMaxWidth(constraint.Width), constraint.Height);
            element.Measure(constraint);
            wholeHeight = Math.Max(wholeHeight, element.DesiredSize.Height);
            Size size = new Size(base.GetTokenMaxWidth(element.DesiredSize.Width), element.DesiredSize.Height);
            wholeWidth += element.DesiredSize.Width;
            return element.DesiredSize;
        }

        public override int LinesCount =>
            1;

        protected override int NewTokenVisibleIndex =>
            0;

        public override System.Windows.Controls.Orientation Orientation =>
            System.Windows.Controls.Orientation.Horizontal;
    }
}

