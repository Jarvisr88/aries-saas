namespace DevExpress.Mvvm.UI.Interactivity
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class BehaviorCollection : AttachableCollection<Behavior>
    {
        private void CheckBehavior(Behavior item)
        {
            Type itemType = UniqueBehaviorTypeAttribute.GetDeclaredType(item.GetType());
            if (itemType != null)
            {
                Func<Behavior, string> evaluator = <>c.<>9__1_1;
                if (<>c.<>9__1_1 == null)
                {
                    Func<Behavior, string> local1 = <>c.<>9__1_1;
                    evaluator = <>c.<>9__1_1 = x => x.GetType().Name;
                }
                string str = this.FirstOrDefault<Behavior>(x => (!ReferenceEquals(x, item) && itemType.IsAssignableFrom(x.GetType()))).With<Behavior, string>(evaluator);
                if (!string.IsNullOrEmpty(str))
                {
                    throw new InvalidOperationException($"A behavior of the {str} base type already exists.");
                }
            }
        }

        internal override void ItemAdded(Behavior item)
        {
            this.CheckBehavior(item);
            base.ItemAdded(item);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BehaviorCollection.<>c <>9 = new BehaviorCollection.<>c();
            public static Func<Behavior, string> <>9__1_1;

            internal string <CheckBehavior>b__1_1(Behavior x) => 
                x.GetType().Name;
        }
    }
}

