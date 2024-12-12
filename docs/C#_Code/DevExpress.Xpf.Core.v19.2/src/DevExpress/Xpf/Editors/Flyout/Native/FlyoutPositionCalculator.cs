namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Flyout;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Forms;

    public class FlyoutPositionCalculator
    {
        private Lazy<CalculatorResult> result;
        private static Point NaN = new Point(double.NaN, double.NaN);

        public FlyoutPositionCalculator()
        {
            this.InitializeInternal();
        }

        private Point ApplyAlignment(Point location, System.Windows.HorizontalAlignment horizontalAlignment, System.Windows.VerticalAlignment verticalAlignment, Rect targetBounds, Size elementSize, FlyoutPlacement placement) => 
            ((placement == FlyoutPlacement.Top) || (placement == FlyoutPlacement.Bottom)) ? new Point(this.GetX(targetBounds, elementSize, horizontalAlignment), location.Y) : (((placement == FlyoutPlacement.Left) || (placement == FlyoutPlacement.Right)) ? new Point(location.X, this.GetY(targetBounds, elementSize, verticalAlignment)) : location);

        public Point CalcAlignedLocationInternal(FlyoutPlacement placement, Rect targetBounds) => 
            this.ApplyAlignment(this.CalcLocationInternal(placement, targetBounds), this.HorizontalAlignment, this.VerticalAlignment, targetBounds, this.GetPopupSize(placement), placement);

        protected virtual Point CalcBottomPlacementPosition(Rect targetBounds) => 
            new Point(targetBounds.Left, targetBounds.Bottom);

        protected virtual Point CalcLeftPlacementPosition(Rect targetBounds) => 
            new Point(targetBounds.Left - this.GetPopupSize(FlyoutPlacement.Left).Width, targetBounds.Top);

        public virtual void CalcLocation()
        {
            this.ContentSize = this.CalcSize();
            CheckPlacementResult result = this.CalcResultPlacement(this.Placement);
            this.Result.Placement = result.Placement;
            this.Result.Size = result.Size;
            this.Result.Location = result.CorrectedLocation;
            if (!this.IndicatorSize.IsEmpty)
            {
                Point location = this.ApplyAlignment(this.Result.Location, this.IndicatorHorizontalAlignment, this.IndicatorVerticalAlignment, this.ResctictByResultBounds(this.GetCorrectedTargetBounds(this.Result.Placement, this.IndicatorTargetBounds), this.Result.Placement), this.GetCorrectedIndicatorSize(this.Result.Placement), this.Result.Placement);
                this.Result.IndicatorOffset = this.CorrectPostionByScreenRect(new Rect(location, this.GetCorrectedIndicatorSize(this.Result.Placement))) - this.Result.Location;
            }
            this.Result.State = CalculationState.Finished;
        }

        public Point CalcLocationInternal(FlyoutPlacement placement, Rect targetBounds)
        {
            switch (placement)
            {
                case FlyoutPlacement.Left:
                    return this.CalcLeftPlacementPosition(targetBounds);

                case FlyoutPlacement.Top:
                    return this.CalcTopPlacementPosition(targetBounds);

                case FlyoutPlacement.Right:
                    return this.CalcRightPlacementPosition(targetBounds);

                case FlyoutPlacement.Bottom:
                    return this.CalcBottomPlacementPosition(targetBounds);
            }
            throw new Exception();
        }

        protected virtual CheckPlacementResult CalcResultPlacement(FlyoutPlacement defaultPlacement)
        {
            List<CheckPlacementResult> source = new List<CheckPlacementResult>();
            CheckPlacementResult defaultCheck = this.CheckPlacement(defaultPlacement, null);
            foreach (FlyoutPlacement placement in Enum.GetValues(typeof(FlyoutPlacement)))
            {
                if (placement != defaultPlacement)
                {
                    source.Add(this.CheckPlacement(placement, defaultCheck));
                    continue;
                }
                source.Insert(0, defaultCheck);
            }
            Func<CheckPlacementResult, bool> predicate = <>c.<>9__75_0;
            if (<>c.<>9__75_0 == null)
            {
                Func<CheckPlacementResult, bool> local1 = <>c.<>9__75_0;
                predicate = <>c.<>9__75_0 = x => x.IsMatch;
            }
            IEnumerable<CheckPlacementResult> enumerable = source.Where<CheckPlacementResult>(predicate);
            CheckPlacementResult result2 = source[0];
            if (enumerable.Any<CheckPlacementResult>())
            {
                Func<CheckPlacementResult, double> selector = <>c.<>9__75_1;
                if (<>c.<>9__75_1 == null)
                {
                    Func<CheckPlacementResult, double> local2 = <>c.<>9__75_1;
                    selector = <>c.<>9__75_1 = x => x.Distance;
                }
                double minDistance = enumerable.Min<CheckPlacementResult>(selector);
                result2 = enumerable.First<CheckPlacementResult>(c => c.Distance == minDistance);
            }
            return result2;
        }

        protected virtual Point CalcRightPlacementPosition(Rect targetBounds) => 
            new Point(targetBounds.Right, targetBounds.Top);

        public virtual Size CalcSize()
        {
            Size popupDesiredSize;
            double height;
            double width;
            if (this.VerticalAlignment == System.Windows.VerticalAlignment.Stretch)
            {
                height = this.TargetBounds.Height;
            }
            else
            {
                popupDesiredSize = this.PopupDesiredSize;
                height = popupDesiredSize.Height;
            }
            double d = height;
            if (this.HorizontalAlignment == System.Windows.HorizontalAlignment.Stretch)
            {
                width = this.TargetBounds.Width;
            }
            else
            {
                popupDesiredSize = this.PopupDesiredSize;
                width = popupDesiredSize.Width;
            }
            double num2 = width;
            if (!double.IsInfinity(d) && !double.IsInfinity(num2))
            {
                return new Size(num2, d);
            }
            return new Size();
        }

        protected virtual Point CalcTopPlacementPosition(Rect targetBounds) => 
            new Point(targetBounds.Left, targetBounds.Top - this.GetPopupSize(FlyoutPlacement.Top).Height);

        private CheckPlacementResult CheckPlacement(FlyoutPlacement placement, CheckPlacementResult defaultCheck = null)
        {
            CheckPlacementResult result1 = new CheckPlacementResult();
            result1.Placement = placement;
            CheckPlacementResult result = result1;
            result.TargetBounds = this.GetCorrectedTargetBounds(placement, this.TargetBounds);
            result.BaseLocation = this.CalcAlignedLocationInternal(placement, result.TargetBounds);
            result.Size = this.GetPopupSize(placement);
            result.BaseRect = new Rect(this.ApplyAlignment(result.BaseLocation, this.HorizontalAlignment, this.VerticalAlignment, result.TargetBounds, result.Size, result.Placement), result.Size);
            result.CorrectedLocation = this.CorrectPostionByRect(result.BaseRect, this.NormalizedScreenRect);
            result.Distance = Math.Sqrt((((defaultCheck == null) ? result.BaseLocation : defaultCheck.BaseLocation) - result.CorrectedLocation).LengthSquared);
            Rect rect = Rect.Intersect(new Rect(result.CorrectedLocation, result.Size), result.TargetBounds);
            result.IsMatch = (!Equals(result.CorrectedLocation, NaN) && ((rect.Width <= 1.0) || (rect.Height <= 1.0))) && result.TargetBounds.IntersectsWith(this.NormalizedScreenRect);
            return result;
        }

        protected virtual Point CorrectPostionByRect(Rect bounds, Rect restrictRect)
        {
            if (this.AllowOutOfScreen)
            {
                return bounds.Location;
            }
            Point point = new Point(bounds.Left, bounds.Top);
            if (bounds.Right >= restrictRect.Right)
            {
                point.X = restrictRect.Right - bounds.Width;
            }
            if (bounds.Bottom > restrictRect.Bottom)
            {
                point.Y = restrictRect.Bottom - bounds.Height;
            }
            point.X = Math.Max(restrictRect.Left, point.X);
            point.Y = Math.Max(restrictRect.Top, point.Y);
            return point;
        }

        protected virtual Point CorrectPostionByScreenRect(Rect bounds) => 
            this.CorrectPostionByRect(bounds, this.NormalizedScreenRect);

        protected virtual CalculatorResult CreateResult()
        {
            Point point = new Point();
            CalculatorResult result1 = new CalculatorResult();
            result1.Location = point;
            result1.Size = Size.Empty;
            result1.State = CalculationState.Calculating;
            return result1;
        }

        protected Size GetCorrectedIndicatorSize(FlyoutPlacement placement) => 
            (this.IsPlacementHorizontal(placement) == this.IsPlacementHorizontal(this.ActualIndicatorDirection)) ? this.IndicatorSize : new Size(this.IndicatorSize.Height, this.IndicatorSize.Width);

        private Rect GetCorrectedTargetBounds(FlyoutPlacement placement, Rect targetBounds)
        {
            Size correctedIndicatorSize = this.GetCorrectedIndicatorSize(placement);
            return (!this.IsPlacementHorizontal(placement) ? ((targetBounds.Height >= correctedIndicatorSize.Height) ? targetBounds : this.GetIncrementedY(targetBounds, correctedIndicatorSize, this.IndicatorVerticalAlignment)) : ((targetBounds.Width >= correctedIndicatorSize.Width) ? targetBounds : this.GetIncrementedX(targetBounds, correctedIndicatorSize, this.IndicatorHorizontalAlignment)));
        }

        protected Rect GetIncrementedX(Rect targetBounds, Size elementSize, System.Windows.HorizontalAlignment horizontalAlignment) => 
            (horizontalAlignment != System.Windows.HorizontalAlignment.Left) ? ((horizontalAlignment != System.Windows.HorizontalAlignment.Right) ? (((horizontalAlignment == System.Windows.HorizontalAlignment.Center) || (horizontalAlignment == System.Windows.HorizontalAlignment.Stretch)) ? new Rect(targetBounds.Left + ((targetBounds.Width - elementSize.Width) / 2.0), targetBounds.Y, elementSize.Width, targetBounds.Height) : targetBounds) : new Rect(targetBounds.Right - (elementSize.Width / 2.0), targetBounds.Y, elementSize.Width, targetBounds.Height)) : new Rect(targetBounds.Left - (elementSize.Width / 2.0), targetBounds.Y, elementSize.Width, targetBounds.Height);

        protected Rect GetIncrementedY(Rect targetBounds, Size elementSize, System.Windows.VerticalAlignment verticalAlignment)
        {
            if (verticalAlignment == System.Windows.VerticalAlignment.Top)
            {
                return new Rect(targetBounds.X, targetBounds.Top - (elementSize.Height / 2.0), targetBounds.Width, elementSize.Height);
            }
            if (verticalAlignment == System.Windows.VerticalAlignment.Bottom)
            {
                return new Rect(targetBounds.X, targetBounds.Bottom - (elementSize.Height / 2.0), targetBounds.Width, elementSize.Height);
            }
            if ((verticalAlignment != System.Windows.VerticalAlignment.Center) && (verticalAlignment != System.Windows.VerticalAlignment.Stretch))
            {
                return targetBounds;
            }
            return new Rect(targetBounds.X, targetBounds.Top + ((targetBounds.Height - elementSize.Height) / 2.0), targetBounds.Width, elementSize.Height);
        }

        protected Size GetPopupSize(FlyoutPlacement placement)
        {
            if (this.State == CalculationState.Uninitialized)
            {
                throw new Exception();
            }
            bool flag = this.IsPlacementHorizontal(placement);
            Size correctedIndicatorSize = this.GetCorrectedIndicatorSize(placement);
            return new Size(Math.Max(correctedIndicatorSize.Width, this.ContentSize.Width + (flag ? 0.0 : correctedIndicatorSize.Width)), Math.Max(correctedIndicatorSize.Height, this.ContentSize.Height + (flag ? correctedIndicatorSize.Height : 0.0)));
        }

        public virtual Rect GetScreenRect(Point point, bool hideInTaskBar) => 
            s => (hideInTaskBar ? s.WorkingArea.FromWinForms() : s.Bounds.FromWinForms())(Screen.FromPoint(point.ToWinFormsPoint()));

        protected double GetX(Rect targetBounds, Size elementSize, System.Windows.HorizontalAlignment horizontalAlignment) => 
            (horizontalAlignment != System.Windows.HorizontalAlignment.Left) ? ((horizontalAlignment != System.Windows.HorizontalAlignment.Right) ? (((horizontalAlignment == System.Windows.HorizontalAlignment.Center) || (horizontalAlignment == System.Windows.HorizontalAlignment.Stretch)) ? (targetBounds.Left + ((targetBounds.Width - elementSize.Width) / 2.0)) : targetBounds.Left) : (targetBounds.Right - elementSize.Width)) : targetBounds.Left;

        protected double GetY(Rect targetBounds, Size elementSize, System.Windows.VerticalAlignment verticalAlignment) => 
            (verticalAlignment != System.Windows.VerticalAlignment.Top) ? ((verticalAlignment != System.Windows.VerticalAlignment.Bottom) ? (((verticalAlignment == System.Windows.VerticalAlignment.Center) || (verticalAlignment == System.Windows.VerticalAlignment.Stretch)) ? (targetBounds.Top + ((targetBounds.Height - elementSize.Height) / 2.0)) : targetBounds.Top) : (targetBounds.Bottom - elementSize.Height)) : targetBounds.Top;

        public void Initialize(System.Windows.VerticalAlignment verticalAlignment, System.Windows.HorizontalAlignment horizontalAlignment, bool allowOutOfScreen)
        {
            this.InitializeInternal();
            this.AllowOutOfScreen = allowOutOfScreen;
            this.VerticalAlignment = verticalAlignment;
            this.HorizontalAlignment = horizontalAlignment;
        }

        protected void InitializeInternal()
        {
            this.result = new Lazy<CalculatorResult>(() => this.CreateResult());
        }

        private bool IsPlacementHorizontal(FlyoutPlacement placement) => 
            (placement == FlyoutPlacement.Top) || (placement == FlyoutPlacement.Bottom);

        private bool IsPlacementHorizontal(IndicatorDirection indicatorDirection) => 
            (indicatorDirection == IndicatorDirection.Bottom) || (indicatorDirection == IndicatorDirection.Top);

        protected virtual Rect ResctictByResultBounds(Rect targetBounds, FlyoutPlacement placement)
        {
            double x = this.IsPlacementHorizontal(placement) ? RestrictByRange(targetBounds.Left, this.Result.Bounds.Left, this.Result.Bounds.Right) : targetBounds.Left;
            double num2 = this.IsPlacementHorizontal(placement) ? RestrictByRange(targetBounds.Right, this.Result.Bounds.Left, this.Result.Bounds.Right) : targetBounds.Right;
            double y = this.IsPlacementHorizontal(placement) ? targetBounds.Top : RestrictByRange(targetBounds.Top, this.Result.Bounds.Top, this.Result.Bounds.Bottom);
            return new Rect(new Point(x, y), new Point(num2, this.IsPlacementHorizontal(placement) ? targetBounds.Left : RestrictByRange(targetBounds.Bottom, this.Result.Bounds.Top, this.Result.Bounds.Bottom)));
        }

        public static double RestrictByRange(double value, double bound1, double bound2)
        {
            double num = Math.Min(bound1, bound2);
            double num2 = Math.Max(bound1, bound2);
            return ((value <= num) ? num : ((num2 <= value) ? num2 : value));
        }

        public bool HideInTaskBar { get; set; }

        public IndicatorDirection ActualIndicatorDirection { get; set; }

        public Size PopupDesiredSize { get; set; }

        public Size ContentSize { get; set; }

        public CalculatorResult Result =>
            this.result.Value;

        public Size IndicatorSize { get; set; }

        public Rect OriginScreenRect { get; set; }

        public Rect NormalizedScreenRect { get; set; }

        public Rect ScreenRect { get; set; }

        protected bool AllowOutOfScreen { get; set; }

        public Rect TargetBounds { get; internal set; }

        protected System.Windows.VerticalAlignment VerticalAlignment { get; set; }

        protected System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }

        public System.Windows.VerticalAlignment IndicatorVerticalAlignment { get; set; }

        public System.Windows.HorizontalAlignment IndicatorHorizontalAlignment { get; set; }

        public FlyoutPlacement Placement { get; set; }

        public Rect IndicatorTargetBounds { get; set; }

        protected CalculationState State
        {
            get
            {
                Func<CalculatorResult, CalculationState> evaluator = <>c.<>9__70_0;
                if (<>c.<>9__70_0 == null)
                {
                    Func<CalculatorResult, CalculationState> local1 = <>c.<>9__70_0;
                    evaluator = <>c.<>9__70_0 = x => x.State;
                }
                return this.Result.Return<CalculatorResult, CalculationState>(evaluator, (<>c.<>9__70_1 ??= () => CalculationState.Uninitialized));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FlyoutPositionCalculator.<>c <>9 = new FlyoutPositionCalculator.<>c();
            public static Func<CalculatorResult, CalculationState> <>9__70_0;
            public static Func<CalculationState> <>9__70_1;
            public static Func<CheckPlacementResult, bool> <>9__75_0;
            public static Func<CheckPlacementResult, double> <>9__75_1;

            internal bool <CalcResultPlacement>b__75_0(CheckPlacementResult x) => 
                x.IsMatch;

            internal double <CalcResultPlacement>b__75_1(CheckPlacementResult x) => 
                x.Distance;

            internal CalculationState <get_State>b__70_0(CalculatorResult x) => 
                x.State;

            internal CalculationState <get_State>b__70_1() => 
                CalculationState.Uninitialized;
        }
    }
}

