namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class RequiredParameters : IEnumerable<Parameter>, IEnumerable
    {
        private Dictionary<string, Parameter> _HashTable = new Dictionary<string, Parameter>(StringComparer.InvariantCultureIgnoreCase);
        private DevExpress.Utils.OAuth.Url _Url;
        private IList<ValidationError> _Errors = new List<ValidationError>();

        public RequiredParameters(DevExpress.Utils.OAuth.Url url)
        {
            this._Url = url;
        }

        private static bool DoMatch(DevExpress.Utils.OAuth.Url url, string name, bool required, out Parameter found, out ValidationError error)
        {
            found = Parameter.Empty;
            error = null;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is null or empty.", "name");
            }
            IEnumerable<Parameter> queryParams = url.GetQueryParams(name);
            if (queryParams != null)
            {
                using (IEnumerator<Parameter> enumerator = queryParams.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        Parameter current = enumerator.Current;
                        if (found.IsEmpty)
                        {
                            found = current;
                            continue;
                        }
                        error = new ValidationError(400, "Duplicated OAuth Protocol Parameter: " + name);
                        found = Parameter.Empty;
                        return false;
                    }
                }
            }
            if (!found.IsEmpty)
            {
                error = null;
                return true;
            }
            if (required)
            {
                error = new ValidationError(400, "Missing required parameter: " + name);
            }
            return false;
        }

        public IEnumerator<Parameter> GetEnumerator() => 
            this._HashTable.Values.GetEnumerator();

        public void Match(string name, bool required, string defaultValue)
        {
            Parameter parameter;
            ValidationError error;
            if (DoMatch(this.Url, name, required, out parameter, out error))
            {
                if (!parameter.IsEmpty)
                {
                    this._HashTable[name] = new Parameter(parameter.Name, parameter.Value);
                }
            }
            else if (!required)
            {
                this._HashTable[name] = new Parameter(name, defaultValue);
            }
            if (error != null)
            {
                this._Errors.Add(error);
            }
        }

        public void Require(params string[] names)
        {
            if ((names != null) && (names.Length != 0))
            {
                foreach (string str in names)
                {
                    this.Match(str, true, null);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public DevExpress.Utils.OAuth.Url Url =>
            this._Url;

        public IEnumerable<ValidationError> Errors =>
            this._Errors;

        public Parameter this[string name]
        {
            get
            {
                Parameter empty;
                if (string.IsNullOrEmpty(name))
                {
                    return Parameter.Empty;
                }
                if (!this._HashTable.TryGetValue(name, out empty))
                {
                    empty = Parameter.Empty;
                }
                return empty;
            }
        }
    }
}

