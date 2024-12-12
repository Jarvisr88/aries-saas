namespace DevExpress.Office
{
    using System;

    public abstract class UndoableClonableCollection<T> : UndoableCollection<T>, ICloneable<UndoableClonableCollection<T>>, ISupportsCopyFrom<UndoableClonableCollection<T>>
    {
        protected UndoableClonableCollection(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public UndoableClonableCollection<T> Clone()
        {
            UndoableClonableCollection<T> newCollection = this.GetNewCollection(base.DocumentModelPart);
            newCollection.CopyFrom((UndoableClonableCollection<T>) this);
            return newCollection;
        }

        public void CopyFrom(UndoableClonableCollection<T> value)
        {
            this.ClearCore();
            int count = value.Count;
            for (int i = 0; i < count; i++)
            {
                this.AddInternal(this.GetCloneItem(value[i], base.DocumentModelPart));
            }
        }

        public override bool Equals(object obj)
        {
            UndoableClonableCollection<T> clonables = obj as UndoableClonableCollection<T>;
            if (clonables == null)
            {
                return false;
            }
            if (base.Count != clonables.Count)
            {
                return false;
            }
            for (int i = 0; i < base.Count; i++)
            {
                T local = base[i];
                if (!local.Equals(clonables[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract T GetCloneItem(T item, IDocumentModelPart documentModelPart);
        public override int GetHashCode()
        {
            int num = base.GetType().GetHashCode() ^ base.Count;
            for (int i = 0; i < base.Count; i++)
            {
                T local = base[i];
                num ^= local.GetHashCode();
            }
            return num;
        }

        public abstract UndoableClonableCollection<T> GetNewCollection(IDocumentModelPart documentModelPart);
    }
}

