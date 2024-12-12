namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class ModelObjectUndoableCollection<T> : UndoableCollection<T> where T: IDocumentModelObject
    {
        public ModelObjectUndoableCollection(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public override int AddCore(T item)
        {
            item.DocumentModelPart = base.DocumentModelPart;
            return base.AddCore(item);
        }

        public override void AddRangeCore(IEnumerable<T> collection)
        {
            foreach (T local in collection)
            {
                local.DocumentModelPart = base.DocumentModelPart;
            }
            base.AddRangeCore(collection);
        }

        public override void ClearCore()
        {
            Action<T> action = <>c<T>.<>9__5_0;
            if (<>c<T>.<>9__5_0 == null)
            {
                Action<T> local1 = <>c<T>.<>9__5_0;
                action = <>c<T>.<>9__5_0 = i => i.DocumentModelPart = null;
            }
            this.ForEach(action);
            base.ClearCore();
        }

        protected internal override void InsertCore(int index, T item)
        {
            item.DocumentModelPart = base.DocumentModelPart;
            base.InsertCore(index, item);
        }

        public override void RemoveAtCore(int index)
        {
            base[index].DocumentModelPart = null;
            base.RemoveAtCore(index);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ModelObjectUndoableCollection<T>.<>c <>9;
            public static Action<T> <>9__5_0;

            static <>c()
            {
                ModelObjectUndoableCollection<T>.<>c.<>9 = new ModelObjectUndoableCollection<T>.<>c();
            }

            internal void <ClearCore>b__5_0(T i)
            {
                i.DocumentModelPart = null;
            }
        }
    }
}

