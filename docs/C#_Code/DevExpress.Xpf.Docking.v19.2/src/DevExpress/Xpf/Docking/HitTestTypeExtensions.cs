namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;

    internal static class HitTestTypeExtensions
    {
        public static SizingAction ToSizingAction(this HitTestType hitTestType)
        {
            SizingAction none = SizingAction.None;
            switch (hitTestType)
            {
                case HitTestType.SizeN:
                    none = SizingAction.North;
                    break;

                case HitTestType.SizeS:
                    none = SizingAction.South;
                    break;

                case HitTestType.SizeE:
                    none = SizingAction.East;
                    break;

                case HitTestType.SizeW:
                    none = SizingAction.West;
                    break;

                case HitTestType.SizeNE:
                    none = SizingAction.NorthEast;
                    break;

                case HitTestType.SizeNW:
                    none = SizingAction.NorthWest;
                    break;

                case HitTestType.SizeSE:
                    none = SizingAction.SouthEast;
                    break;

                case HitTestType.SizeSW:
                    none = SizingAction.SouthWest;
                    break;

                default:
                    break;
            }
            return none;
        }

        public static SizingAction ToSizingAction(object hitTestType)
        {
            HitTestType? nullable = hitTestType as HitTestType?;
            return ((nullable != null) ? nullable.GetValueOrDefault() : HitTestType.Undefined).ToSizingAction();
        }
    }
}

