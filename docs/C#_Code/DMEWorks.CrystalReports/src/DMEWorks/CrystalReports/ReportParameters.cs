namespace DMEWorks.CrystalReports
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class ReportParameters
    {
        private readonly Dictionary<string, object> _storage;
        private bool _readonly;

        public ReportParameters()
        {
            this._storage = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            this._readonly = false;
        }

        public ReportParameters(string query)
        {
            this._storage = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            if (!string.IsNullOrEmpty(query))
            {
                int num = 0;
                int length = query.Length;
                while (num < length)
                {
                    int startIndex = num;
                    int num4 = -1;
                    while (true)
                    {
                        if (num < length)
                        {
                            char ch = query[num];
                            if (ch != '&')
                            {
                                if ((ch == '=') && (num4 < 0))
                                {
                                    num4 = num;
                                }
                                num++;
                                continue;
                            }
                        }
                        string str = null;
                        string str2 = null;
                        if (0 > num4)
                        {
                            str2 = query.Substring(startIndex, num - startIndex);
                        }
                        else
                        {
                            str = query.Substring(startIndex, num4 - startIndex);
                            str2 = query.Substring(num4 + 1, (num - num4) - 1);
                        }
                        this[str] = str2;
                        if ((num == (length - 1)) && (query[num] == '&'))
                        {
                            this[""] = "";
                        }
                        num++;
                        break;
                    }
                }
            }
        }

        public ReportParameters(string key, object value)
        {
            this._storage = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            this._storage[key] = value;
            this._readonly = true;
        }

        public void Clear()
        {
            if (this._readonly)
            {
                throw new InvalidOperationException("Cannot change readonly object");
            }
            this._storage.Clear();
        }

        public bool ContainsKey(string key) => 
            this._storage.ContainsKey(key);

        public void SetReadonly()
        {
            this._readonly = true;
        }

        public bool TryGetValue(string key, out object value) => 
            this._storage.TryGetValue(key, out value);

        public object this[string key]
        {
            get
            {
                object obj2;
                return (!this._storage.TryGetValue(key, out obj2) ? null : obj2);
            }
            set
            {
                if (this._readonly)
                {
                    throw new InvalidOperationException("Cannot change readonly object");
                }
                this._storage[key] = value;
            }
        }

        public int Count =>
            this._storage.Count;
    }
}

