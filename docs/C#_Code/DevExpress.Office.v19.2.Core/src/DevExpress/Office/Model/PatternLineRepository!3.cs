namespace DevExpress.Office.Model
{
    using DevExpress.Utils;
    using System;

    public abstract class PatternLineRepository<T, TItem, TCollection> where T: struct where TItem: PatternLine<T> where TCollection: List<TItem>, new()
    {
        private readonly TCollection collection;

        protected PatternLineRepository()
        {
            this.collection = Activator.CreateInstance<TCollection>();
            this.PopulateRepository();
        }

        public TItem GetPatternLineByType(T line)
        {
            int count = this.collection.Count;
            for (int i = 0; i < count; i++)
            {
                T id = this.collection[i].Id;
                if (id.Equals(line))
                {
                    return this.collection[i];
                }
            }
            return default(TItem);
        }

        internal TItem GetPatternLineByTypeInternal(T type)
        {
            TItem patternLineByType = this.GetPatternLineByType(type);
            return ((patternLineByType != null) ? patternLineByType : this.collection[0]);
        }

        protected internal abstract void PopulateRepository();
        public bool RegisterPatternLine(TItem line)
        {
            Guard.ArgumentNotNull(line, "line");
            if (this.GetPatternLineByType(line.Id) != null)
            {
                return false;
            }
            this.collection.Add(line);
            return true;
        }

        public bool UnregisterPatternLine(T type)
        {
            TItem patternLineByType = this.GetPatternLineByType(type);
            if (patternLineByType == null)
            {
                return false;
            }
            this.collection.Remove(patternLineByType);
            return true;
        }

        public bool UnregisterPatternLine(TItem line)
        {
            Guard.ArgumentNotNull(line, "line");
            return this.UnregisterPatternLine(line.Id);
        }

        public TCollection Items =>
            this.collection;
    }
}

