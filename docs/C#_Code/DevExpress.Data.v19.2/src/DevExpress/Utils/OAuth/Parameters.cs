namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text;

    public class Parameters : IList<Parameter>, ICollection<Parameter>, IEnumerable<Parameter>, IEnumerable
    {
        private IList<Parameter> _items;

        public Parameters()
        {
            this._items = new List<Parameter>();
        }

        private Parameters(IList<Parameter> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            this._items = items;
        }

        public void Add(Parameter item)
        {
            this._items.Add(item);
        }

        public void Clear()
        {
            this._items.Clear();
        }

        private static int CompareParams(Parameter x, Parameter y) => 
            x.CompareTo(y);

        public bool Contains(Parameter item) => 
            this._items.Contains(item);

        public void CopyTo(Parameter[] array, int index)
        {
            this._items.CopyTo(array, index);
        }

        public static Parameters FromUri(Uri uri, string authorizationHeader)
        {
            Parameters list = new Parameters();
            ParseQueryString(uri, list);
            ParseAuthorizationHeader(authorizationHeader, list);
            return list;
        }

        public IEnumerator<Parameter> GetEnumerator() => 
            this._items.GetEnumerator();

        public int IndexOf(Parameter item) => 
            this._items.IndexOf(item);

        public void Insert(int index, Parameter item)
        {
            this._items.Insert(index, item);
        }

        public static Parameters Parse(byte[] bytes) => 
            Parse(bytes, true);

        public static Parameters Parse(WebResponse response) => 
            Parse(response, true);

        public static Parameters Parse(Stream stream, bool unescape)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return Parse(reader, unescape);
            }
        }

        public static Parameters Parse(StreamReader reader, bool unescape)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            return ParseTokens(reader.ReadToEnd(), unescape);
        }

        public static Parameters Parse(byte[] bytes, bool unescape)
        {
            using (Stream stream = new MemoryStream(bytes))
            {
                return Parse(stream, unescape);
            }
        }

        public static Parameters Parse(WebResponse response, bool unescape)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            using (Stream stream = response.GetResponseStream())
            {
                return Parse(stream, unescape);
            }
        }

        private static void ParseAuthorizationHeader(string authorizationHeader, Parameters list)
        {
            if ((authorizationHeader != null) && (authorizationHeader.Length > 0))
            {
                if (!authorizationHeader.StartsWith("OAuth ", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new ArgumentException("The specified authorization header must start with 'OAuth'", "authorizationHeader");
                }
                authorizationHeader = authorizationHeader.Substring("OAuth ".Length);
                int num = 0;
                int index = num;
                while (num < authorizationHeader.Length)
                {
                    if (authorizationHeader[num] != ',')
                    {
                        num++;
                        continue;
                    }
                    list.Add(Parameter.Parse(authorizationHeader, index, num - index, true, true));
                    index = ++num;
                }
                if (index <= authorizationHeader.Length)
                {
                    list.Add(Parameter.Parse(authorizationHeader, index, authorizationHeader.Length - index, true, true));
                }
            }
        }

        private static void ParseQueryString(Uri uri, Parameters list)
        {
            if ((uri != null) && uri.IsAbsoluteUri)
            {
                string query = uri.Query;
                if (!string.IsNullOrEmpty(query))
                {
                    int num = 0;
                    while (char.IsWhiteSpace(query[num]) || (query[num] == '?'))
                    {
                        num++;
                    }
                    int index = num;
                    while (num < query.Length)
                    {
                        if (query[num] != '&')
                        {
                            num++;
                            continue;
                        }
                        list.Add(Parameter.Parse(query, index, num - index, true, false));
                        index = ++num;
                    }
                    if (index <= query.Length)
                    {
                        list.Add(Parameter.Parse(query, index, query.Length - index, true, false));
                    }
                }
            }
        }

        public static Parameters ParseTokens(string tokens) => 
            ParseTokens(tokens, true);

        public static Parameters ParseTokens(string tokens, bool unescape)
        {
            Parameters parameters = new Parameters();
            if ((tokens != null) && (tokens.Length > 0))
            {
                int num = 0;
                int index = 0;
                while (num < tokens.Length)
                {
                    if (tokens[num] != '&')
                    {
                        num++;
                        continue;
                    }
                    parameters.Add(Parameter.Parse(tokens, index, num - index, unescape, false));
                    index = ++num;
                }
                if (index <= tokens.Length)
                {
                    parameters.Add(Parameter.Parse(tokens, index, tokens.Length - index, unescape, false));
                }
            }
            return parameters;
        }

        public bool Remove(Parameter item) => 
            this._items.Remove(item);

        public void RemoveAt(int index)
        {
            this._items.RemoveAt(index);
        }

        public static IList<Parameter> Sort(params IEnumerable<Parameter>[] args)
        {
            List<Parameter> list = new List<Parameter>();
            if ((args != null) && (args.Length != 0))
            {
                IEnumerable<Parameter>[] enumerableArray = args;
                int index = 0;
                while (true)
                {
                    if (index >= enumerableArray.Length)
                    {
                        list.Sort(new Comparison<Parameter>(Parameters.CompareParams));
                        break;
                    }
                    IEnumerable<Parameter> enumerable = enumerableArray[index];
                    if (enumerable != null)
                    {
                        foreach (Parameter parameter in enumerable)
                        {
                            if (!parameter.IsEmpty)
                            {
                                list.Add(parameter);
                            }
                        }
                    }
                    index++;
                }
            }
            return list;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this._items.GetEnumerator();

        public int Count =>
            this._items.Count;

        public bool IsReadOnly =>
            false;

        public Parameter this[string name]
        {
            get
            {
                Parameter parameter2;
                using (IEnumerator<Parameter> enumerator = this._items.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            Parameter current = enumerator.Current;
                            if (!string.Equals(current.Name, name, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            parameter2 = current;
                        }
                        else
                        {
                            return new Parameter();
                        }
                        break;
                    }
                }
                return parameter2;
            }
        }

        public Parameter this[int index]
        {
            get => 
                this._items[index];
            set => 
                this._items[index] = value;
        }
    }
}

