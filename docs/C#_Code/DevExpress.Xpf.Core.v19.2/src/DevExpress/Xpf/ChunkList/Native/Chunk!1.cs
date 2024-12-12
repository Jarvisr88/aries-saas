namespace DevExpress.Xpf.ChunkList.Native
{
    using DevExpress.Xpf.ChunkList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class Chunk<T>
    {
        private readonly DevExpress.Xpf.ChunkList.ChunkList<T> chunkList;

        public Chunk(DevExpress.Xpf.ChunkList.ChunkList<T> chunkList, int chunkSize)
        {
            this.Items = new List<T>(chunkSize);
            this.chunkList = chunkList;
        }

        public void AddItem(T item)
        {
            this.Items.Add(item);
            this.OnItemAdded(item);
        }

        private int GetIndexFast(T item)
        {
            List<T> items = this.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(T item) => 
            this.PrevCount + this.GetIndexFast(item);

        public void OnItemAdded(T item)
        {
            IChunkListObject obj2 = item as IChunkListObject;
            if (obj2 != null)
            {
                obj2.ChunkObject = this;
            }
            else if (this.chunkList.UseChunksCache)
            {
                this.chunkList.ChunksCache[item] = (Chunk<T>) this;
            }
            if (this.chunkList.SupportPropertyChanged)
            {
                INotifyPropertyChanging changing = item as INotifyPropertyChanging;
                if (changing != null)
                {
                    changing.PropertyChanging += new PropertyChangingEventHandler(this.propertyChanging_PropertyChanging);
                }
                INotifyPropertyChanged changed = item as INotifyPropertyChanged;
                if (changed != null)
                {
                    changed.PropertyChanged += new PropertyChangedEventHandler(this.propertyChanged_PropertyChanged);
                }
            }
        }

        public void OnItemRemoved(T item)
        {
            IChunkListObject obj2 = item as IChunkListObject;
            if (obj2 != null)
            {
                obj2.ChunkObject = null;
            }
            else if (this.chunkList.UseChunksCache)
            {
                this.chunkList.ChunksCache.Remove(item);
            }
            if (this.chunkList.SupportPropertyChanged)
            {
                INotifyPropertyChanging changing = item as INotifyPropertyChanging;
                if (changing != null)
                {
                    changing.PropertyChanging -= new PropertyChangingEventHandler(this.propertyChanging_PropertyChanging);
                }
                INotifyPropertyChanged changed = item as INotifyPropertyChanged;
                if (changed != null)
                {
                    changed.PropertyChanged -= new PropertyChangedEventHandler(this.propertyChanged_PropertyChanged);
                }
            }
        }

        private void propertyChanged_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.chunkList.OnItemPropertyChanged((Chunk<T>) this, (T) sender, e.PropertyName);
        }

        private void propertyChanging_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            this.chunkList.OnItemPropertyChanging((Chunk<T>) this, (T) sender, e.PropertyName);
        }

        public void RemoveItem(T item)
        {
            this.Items.Remove(item);
            this.OnItemRemoved(item);
        }

        public int PrevCount { get; set; }

        public List<T> Items { get; set; }
    }
}

