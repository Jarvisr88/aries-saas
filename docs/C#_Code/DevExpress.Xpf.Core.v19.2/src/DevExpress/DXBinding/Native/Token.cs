namespace DevExpress.DXBinding.Native
{
    using System;

    internal class Token
    {
        public int kind;
        public int pos;
        public int charPos;
        public int col;
        public int line;
        public string val;
        public Token next;
    }
}

