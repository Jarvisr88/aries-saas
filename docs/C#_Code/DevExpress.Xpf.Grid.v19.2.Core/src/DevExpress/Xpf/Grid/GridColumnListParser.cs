namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class GridColumnListParser
    {
        private List<ColumnBase> columnsSource;
        private List<string> columnsNames;
        private List<string> result;

        private GridColumnListParser(List<ColumnBase> columnsSource, string searchColumns)
        {
            this.columnsSource = columnsSource;
            this.columnsNames = SearchControlHelper.ParseColumnsString(searchColumns);
        }

        private ObservableCollection<string> GetSearchColumns()
        {
            if ((this.columnsNames.Count == 1) && (this.columnsNames[0] == "*"))
            {
                return null;
            }
            this.result = new List<string>();
            this.UpdateAllowColumnInSeachPanelIfNeed(false, false);
            if (this.columnsNames.Count != 0)
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.columnsNames.Count)
                    {
                        this.UpdateAllowColumnInSeachPanelIfNeed(false, true);
                        break;
                    }
                    this.columnsNames[num] = this.columnsNames[num].ToLower();
                    num++;
                }
            }
            if (this.columnsNames.Count != 0)
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 >= this.columnsNames.Count)
                    {
                        this.UpdateAllowColumnInSeachPanelIfNeed(true, true);
                        break;
                    }
                    this.columnsNames[num2] = this.columnsNames[num2].Replace(" ", string.Empty);
                    num2++;
                }
            }
            return new ObservableCollection<string>(this.result);
        }

        public static ObservableCollection<string> GetSearchColumns(List<ColumnBase> columnsSource, string searchColumns) => 
            new GridColumnListParser(columnsSource, searchColumns).GetSearchColumns();

        public static bool IsColumnsListsEquals(IList oldColumns, IList newColumns)
        {
            bool flag;
            using (IEnumerator enumerator = oldColumns.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    ColumnBase current = (ColumnBase) enumerator.Current;
                    if (!newColumns.Contains(current))
                    {
                        return false;
                    }
                }
            }
            using (IEnumerator enumerator2 = newColumns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator2.MoveNext())
                    {
                        ColumnBase current = (ColumnBase) enumerator2.Current;
                        if (oldColumns.Contains(current))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        private void UpdateAllowColumnInSeachPanelIfNeed(bool removeSpaces, bool isToLower)
        {
            List<string> collection = new List<string>();
            List<ColumnBase> list2 = new List<ColumnBase>();
            foreach (ColumnBase base2 in this.columnsSource)
            {
                string item = removeSpaces ? base2.FieldName.ToString().Replace(" ", string.Empty) : base2.FieldName.ToString();
                item = isToLower ? item.ToLower() : item;
                if (this.columnsNames.Contains(item))
                {
                    collection.Add(base2.FieldName);
                    this.columnsNames.Remove(item);
                    list2.Add(base2);
                }
            }
            foreach (ColumnBase base3 in list2)
            {
                if (this.columnsSource.Contains(base3))
                {
                    this.columnsSource.Remove(base3);
                }
            }
            this.result.AddRange(collection);
        }
    }
}

