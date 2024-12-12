namespace Devart.Data.MySql
{
    using System;
    using System.Collections;

    public class MySqlLoaderOptions
    {
        private MySqlLoaderPriority a;
        private MySqlLoaderConflictOption b;
        private string c;
        private string d;
        private string e;
        private char f;
        private char g;
        private bool h;
        private string i;
        private int j;
        private string k;
        private string l;
        private ArrayList m;
        internal const string n = "\t";
        internal const string o = "\n";
        internal const char p = '\\';

        public MySqlLoaderOptions();

        public MySqlLoaderPriority Priority { get; set; }

        public MySqlLoaderConflictOption ConflictOption { get; set; }

        public string CharSet { get; set; }

        public string FieldTerminator { get; set; }

        public string LineTerminator { get; set; }

        public char EscapeCharacter { get; set; }

        public char FieldQuotationCharacter { get; set; }

        public bool FieldQuotationOptional { get; set; }

        public string LinePrefix { get; set; }

        public int NumberOfLinesToSkip { get; set; }

        public string RowsIdentified { get; set; }

        public string Columns { get; set; }

        public ArrayList Expressions { get; }
    }
}

