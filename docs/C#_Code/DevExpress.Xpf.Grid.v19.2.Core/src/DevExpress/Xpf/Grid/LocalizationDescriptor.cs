namespace DevExpress.Xpf.Grid
{
    using System;

    public class LocalizationDescriptor
    {
        private GridRuntimeStringCollection localizationStrings;

        public LocalizationDescriptor(GridRuntimeStringCollection localizationStrings)
        {
            this.localizationStrings = localizationStrings;
        }

        public string GetValue(string name)
        {
            GridControlRuntimeStringId id;
            if ((this.localizationStrings != null) && Enum.TryParse<GridControlRuntimeStringId>(name, out id))
            {
                RuntimeStringIdInfo item = new RuntimeStringIdInfo(id, "");
                int index = this.localizationStrings.IndexOf(item);
                if (index > -1)
                {
                    return this.localizationStrings[index].Value;
                }
            }
            return GridControlLocalizer.GetString((GridControlStringId) Enum.Parse(typeof(GridControlStringId), name, false));
        }

        public void SetValue(string name, string value)
        {
            if (this.localizationStrings != null)
            {
                RuntimeStringIdInfo item = new RuntimeStringIdInfo((GridControlRuntimeStringId) Enum.Parse(typeof(GridControlRuntimeStringId), name, false), value);
                int index = this.localizationStrings.IndexOf(item);
                if (index > -1)
                {
                    this.localizationStrings[index] = item;
                }
                else
                {
                    this.localizationStrings.Add(item);
                }
            }
        }
    }
}

