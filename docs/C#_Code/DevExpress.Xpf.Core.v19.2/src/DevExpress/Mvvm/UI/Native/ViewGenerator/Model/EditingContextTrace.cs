namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class EditingContextTrace
    {
        public readonly Action<ModelItemCollectionBase> ClearModelItemCollection;
        public readonly Action<ModelItemCollectionBase, object> AddModelItem;
        public readonly Action<ModelPropertyBase, object> SetModelPropertyValue;
        public readonly Action<ModelPropertyBase> ClearModelPropertyValue;
        public readonly Action<string> CreateEditingScope;
        public readonly Action<string> UpdateEditingScope;
        public readonly Action<string> RevertEditingScope;
        public readonly Action<string> CompleteEditingScope;
        public readonly Action<string> DisposeEditingScope;

        public EditingContextTrace(Action<ModelItemCollectionBase> clearModelItemCollection = null, Action<ModelItemCollectionBase, object> addModelItem = null, Action<ModelPropertyBase, object> setModelPropertyValue = null, Action<ModelPropertyBase> clearModelPropertyValue = null, Action<string> createEditingScope = null, Action<string> updateEditingScope = null, Action<string> revertEditingScope = null, Action<string> completeEditingScope = null, Action<string> disposeEditingScope = null)
        {
            Action<ModelItemCollectionBase> action1 = clearModelItemCollection;
            if (clearModelItemCollection == null)
            {
                Action<ModelItemCollectionBase> local1 = clearModelItemCollection;
                action1 = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Action<ModelItemCollectionBase> local2 = <>c.<>9__9_0;
                    action1 = <>c.<>9__9_0 = delegate (ModelItemCollectionBase _) {
                    };
                }
            }
            this.ClearModelItemCollection = action1;
            Action<ModelItemCollectionBase, object> action2 = addModelItem;
            if (addModelItem == null)
            {
                Action<ModelItemCollectionBase, object> local3 = addModelItem;
                action2 = <>c.<>9__9_1;
                if (<>c.<>9__9_1 == null)
                {
                    Action<ModelItemCollectionBase, object> local4 = <>c.<>9__9_1;
                    action2 = <>c.<>9__9_1 = delegate (ModelItemCollectionBase _, object __) {
                    };
                }
            }
            this.AddModelItem = action2;
            Action<ModelPropertyBase, object> action3 = setModelPropertyValue;
            if (setModelPropertyValue == null)
            {
                Action<ModelPropertyBase, object> local5 = setModelPropertyValue;
                action3 = <>c.<>9__9_2;
                if (<>c.<>9__9_2 == null)
                {
                    Action<ModelPropertyBase, object> local6 = <>c.<>9__9_2;
                    action3 = <>c.<>9__9_2 = delegate (ModelPropertyBase _, object __) {
                    };
                }
            }
            this.SetModelPropertyValue = action3;
            Action<ModelPropertyBase> action4 = clearModelPropertyValue;
            if (clearModelPropertyValue == null)
            {
                Action<ModelPropertyBase> local7 = clearModelPropertyValue;
                action4 = <>c.<>9__9_3;
                if (<>c.<>9__9_3 == null)
                {
                    Action<ModelPropertyBase> local8 = <>c.<>9__9_3;
                    action4 = <>c.<>9__9_3 = delegate (ModelPropertyBase _) {
                    };
                }
            }
            this.ClearModelPropertyValue = action4;
            Action<string> action5 = createEditingScope;
            if (createEditingScope == null)
            {
                Action<string> local9 = createEditingScope;
                action5 = <>c.<>9__9_4;
                if (<>c.<>9__9_4 == null)
                {
                    Action<string> local10 = <>c.<>9__9_4;
                    action5 = <>c.<>9__9_4 = delegate (string _) {
                    };
                }
            }
            this.CreateEditingScope = action5;
            Action<string> action6 = updateEditingScope;
            if (updateEditingScope == null)
            {
                Action<string> local11 = updateEditingScope;
                action6 = <>c.<>9__9_5;
                if (<>c.<>9__9_5 == null)
                {
                    Action<string> local12 = <>c.<>9__9_5;
                    action6 = <>c.<>9__9_5 = delegate (string _) {
                    };
                }
            }
            this.UpdateEditingScope = action6;
            Action<string> action7 = revertEditingScope;
            if (revertEditingScope == null)
            {
                Action<string> local13 = revertEditingScope;
                action7 = <>c.<>9__9_6;
                if (<>c.<>9__9_6 == null)
                {
                    Action<string> local14 = <>c.<>9__9_6;
                    action7 = <>c.<>9__9_6 = delegate (string _) {
                    };
                }
            }
            this.RevertEditingScope = action7;
            Action<string> action8 = completeEditingScope;
            if (completeEditingScope == null)
            {
                Action<string> local15 = completeEditingScope;
                action8 = <>c.<>9__9_7;
                if (<>c.<>9__9_7 == null)
                {
                    Action<string> local16 = <>c.<>9__9_7;
                    action8 = <>c.<>9__9_7 = delegate (string _) {
                    };
                }
            }
            this.CompleteEditingScope = action8;
            Action<string> action9 = disposeEditingScope;
            if (disposeEditingScope == null)
            {
                Action<string> local17 = disposeEditingScope;
                action9 = <>c.<>9__9_8;
                if (<>c.<>9__9_8 == null)
                {
                    Action<string> local18 = <>c.<>9__9_8;
                    action9 = <>c.<>9__9_8 = delegate (string _) {
                    };
                }
            }
            this.DisposeEditingScope = action9;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditingContextTrace.<>c <>9 = new EditingContextTrace.<>c();
            public static Action<ModelItemCollectionBase> <>9__9_0;
            public static Action<ModelItemCollectionBase, object> <>9__9_1;
            public static Action<ModelPropertyBase, object> <>9__9_2;
            public static Action<ModelPropertyBase> <>9__9_3;
            public static Action<string> <>9__9_4;
            public static Action<string> <>9__9_5;
            public static Action<string> <>9__9_6;
            public static Action<string> <>9__9_7;
            public static Action<string> <>9__9_8;

            internal void <.ctor>b__9_0(ModelItemCollectionBase _)
            {
            }

            internal void <.ctor>b__9_1(ModelItemCollectionBase _, object __)
            {
            }

            internal void <.ctor>b__9_2(ModelPropertyBase _, object __)
            {
            }

            internal void <.ctor>b__9_3(ModelPropertyBase _)
            {
            }

            internal void <.ctor>b__9_4(string _)
            {
            }

            internal void <.ctor>b__9_5(string _)
            {
            }

            internal void <.ctor>b__9_6(string _)
            {
            }

            internal void <.ctor>b__9_7(string _)
            {
            }

            internal void <.ctor>b__9_8(string _)
            {
            }
        }
    }
}

