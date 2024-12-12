namespace DevExpress.Entity.Model.DescendantBuilding
{
    using System;
    using System.IO;
    using System.Linq.Expressions;

    public class DescendantInstanceActivator : IDisposable
    {
        private string path;
        private object defaultInstance;
        private Func<object> createNew;

        public DescendantInstanceActivator(string path, Func<object> func)
        {
            this.path = path;
            this.createNew = func;
            this.defaultInstance = this.createNew();
        }

        public DescendantInstanceActivator(string path, Expression expression)
        {
            this.path = path;
            this.createNew = Expression.Lambda<Func<object>>(Expression.Convert(expression, typeof(object)), new ParameterExpression[0]).Compile();
            this.defaultInstance = this.createNew();
        }

        public object Create() => 
            this.createNew();

        void IDisposable.Dispose()
        {
            if (!string.IsNullOrEmpty(this.path) && Directory.Exists(this.path))
            {
                try
                {
                    string[] files = Directory.GetFiles(this.path);
                    int index = 0;
                    while (true)
                    {
                        if (index >= files.Length)
                        {
                            Directory.Delete(this.path, true);
                            break;
                        }
                        string path = files[index];
                        File.Delete(path);
                        index++;
                    }
                }
                catch
                {
                }
            }
        }

        public object DefaultInstance =>
            this.defaultInstance;
    }
}

