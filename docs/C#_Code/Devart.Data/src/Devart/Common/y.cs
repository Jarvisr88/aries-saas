namespace Devart.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Resources;

    [AttributeUsage(AttributeTargets.Event | AttributeTargets.Property | AttributeTargets.Class)]
    internal class y : DescriptionAttribute
    {
        private static bool a;
        private static List<ResourceManager> b;
        private bool c;

        public y(string A_0) : base(A_0)
        {
            if (!a)
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                b = new List<ResourceManager>();
                bool flag = false;
                string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
                int index = 0;
                while (true)
                {
                    if (index >= manifestResourceNames.Length)
                    {
                        a = true;
                        break;
                    }
                    string str = manifestResourceNames[index];
                    if (str.StartsWith("Devart.", StringComparison.CurrentCultureIgnoreCase) && str.EndsWith(".Strings.resources", StringComparison.CurrentCultureIgnoreCase))
                    {
                        b.Add(new ResourceManager(str.Substring(0, str.Length - 10), executingAssembly));
                    }
                    if (!flag && (str.StartsWith("Devart.", StringComparison.CurrentCultureIgnoreCase) && str.EndsWith(".Common.resources", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        b.Add(new ResourceManager(str.Substring(0, str.Length - 10), executingAssembly));
                        flag = true;
                    }
                    index++;
                }
            }
        }

        public override string System.ComponentModel.DescriptionAttribute.Description
        {
            get
            {
                if (!this.c)
                {
                    this.c = true;
                    string str = null;
                    using (List<ResourceManager>.Enumerator enumerator = b.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            str = enumerator.Current.GetString(base.Description);
                            if (str != null)
                            {
                                break;
                            }
                        }
                    }
                    if (str != null)
                    {
                        base.DescriptionValue = str;
                    }
                }
                return base.Description;
            }
        }
    }
}

