namespace DevExpress.Xpo.DB.Helpers
{
    using System;

    [Serializable]
    public class TableAge
    {
        public string Name;
        public long Age;

        public TableAge()
        {
        }

        public TableAge(string name, long age)
        {
            this.Name = name;
            this.Age = age;
        }
    }
}

