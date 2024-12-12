namespace DMEWorks.Forms.Maps
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Address
    {
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zip;
        public string Address1 =>
            this._address1 ?? "";
        public string Address2 =>
            this._address1 ?? "";
        public string City =>
            this._city ?? "";
        public string State =>
            this._state ?? "";
        public string Zip =>
            this._zip ?? "";
        private static string Normalize(string value)
        {
            if (value == null)
            {
                return null;
            }
            char[] trimChars = new char[] { ' ' };
            value = value.Trim(trimChars);
            return ((0 < value.Length) ? value : null);
        }

        public Address(string address1, string address2, string city, string state, string zip)
        {
            this._address1 = Normalize(address1);
            this._address2 = Normalize(address2);
            this._city = Normalize(city);
            this._state = Normalize(state);
            this._zip = Normalize(zip);
        }
    }
}

