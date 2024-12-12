namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ModelItemCollectionWrapper
    {
        public const string SimpleBindingColumnPrefix = "$simpleBinding_";
        private readonly IModelItemCollection source;
        private Dictionary<string, IModelItem> _modelItems;

        public ModelItemCollectionWrapper(IModelItemCollection source)
        {
            this.source = source;
        }

        public virtual void Add(IModelItem element)
        {
            this.source.Add(element);
            this.AddToHashTable(element);
        }

        private void AddToHashTable(IModelItem column)
        {
            string key = GetKey(column);
            if ((key != null) && !this.ModelItems.ContainsKey(key))
            {
                this.ModelItems.Add(key, column);
            }
        }

        private void CreateAndFillHashTable()
        {
            List<IModelItem> list = this.source.ToList<IModelItem>();
            this._modelItems = new Dictionary<string, IModelItem>(list.Count);
            foreach (IModelItem item in list)
            {
                this.AddToHashTable(item);
            }
        }

        public List<IModelItem> FindColumns()
        {
            Func<IModelItem, bool> predicate = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<IModelItem, bool> local1 = <>c.<>9__13_0;
                predicate = <>c.<>9__13_0 = columnModel => true;
            }
            return this.FindColumns(predicate);
        }

        public List<IModelItem> FindColumns(Func<IModelItem, bool> predicate)
        {
            List<IModelItem> list = new List<IModelItem>();
            foreach (KeyValuePair<string, IModelItem> pair in this.ModelItems)
            {
                if (predicate(pair.Value))
                {
                    list.Add(pair.Value);
                }
            }
            return list;
        }

        public IModelItem FirstOrDefault(string fieldName)
        {
            IModelItem item = null;
            this.ModelItems.TryGetValue(fieldName, out item);
            return item;
        }

        internal static string GetKey(IModelItem columnModel)
        {
            ColumnBase currentValue = columnModel.GetCurrentValue() as ColumnBase;
            if ((currentValue != null) && (currentValue.IsSimpleBindingEnabled && (currentValue.SimpleBindingProcessor.DataControllerDescriptor != null)))
            {
                return ("$simpleBinding_" + currentValue.SimpleBindingProcessor.DataControllerDescriptor.Name);
            }
            Func<IModelProperty, object> evaluator = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<IModelProperty, object> local1 = <>c.<>9__8_0;
                evaluator = <>c.<>9__8_0 = pr => pr.ComputedValue;
            }
            Func<object, string> func2 = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                Func<object, string> local2 = <>c.<>9__8_1;
                func2 = <>c.<>9__8_1 = c => c.ToString();
            }
            return columnModel.Properties["FieldName"].With<IModelProperty, object>(evaluator).With<object, string>(func2);
        }

        public virtual void Remove(IModelItem element)
        {
            this.RemoveFromHashTable(element);
            this.source.Remove(element);
        }

        private void RemoveFromHashTable(IModelItem column)
        {
            string key = GetKey(column);
            if ((key != null) && this.ModelItems.ContainsKey(key))
            {
                this.ModelItems.Remove(key);
            }
        }

        private Dictionary<string, IModelItem> ModelItems
        {
            get
            {
                if (this._modelItems == null)
                {
                    this.CreateAndFillHashTable();
                }
                return this._modelItems;
            }
        }

        public int Count =>
            this.ModelItems.Count;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ModelItemCollectionWrapper.<>c <>9 = new ModelItemCollectionWrapper.<>c();
            public static Func<IModelProperty, object> <>9__8_0;
            public static Func<object, string> <>9__8_1;
            public static Func<IModelItem, bool> <>9__13_0;

            internal bool <FindColumns>b__13_0(IModelItem columnModel) => 
                true;

            internal object <GetKey>b__8_0(IModelProperty pr) => 
                pr.ComputedValue;

            internal string <GetKey>b__8_1(object c) => 
                c.ToString();
        }
    }
}

