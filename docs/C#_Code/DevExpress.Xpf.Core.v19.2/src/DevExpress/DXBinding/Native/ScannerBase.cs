namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections;
    using System.IO;

    internal abstract class ScannerBase
    {
        protected const char EOL = '\n';
        protected const int eofSym = 0;
        public DevExpress.DXBinding.Native.Buffer buffer;
        protected Token t;
        protected int ch;
        protected int pos;
        protected int charPos;
        protected int col;
        protected int line;
        protected int oldEols;
        protected static readonly Hashtable start = new Hashtable(0x80);
        protected Token tokens;
        protected Token pt;
        protected char[] tval;
        protected int tlen;

        public ScannerBase(Stream s)
        {
            this.tval = new char[0x80];
            this.buffer = new DevExpress.DXBinding.Native.Buffer(s, true);
            this.Init();
        }

        public ScannerBase(string fileName)
        {
            this.tval = new char[0x80];
            try
            {
                Stream s = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                this.buffer = new DevExpress.DXBinding.Native.Buffer(s, false);
                this.Init();
            }
            catch (IOException)
            {
                throw new FatalError("Cannot open file " + fileName);
            }
        }

        protected void AddCh()
        {
            if (this.tlen >= this.tval.Length)
            {
                char[] destinationArray = new char[2 * this.tval.Length];
                Array.Copy(this.tval, 0, destinationArray, 0, this.tval.Length);
                this.tval = destinationArray;
            }
            if (this.ch != 0x10000)
            {
                this.Casing2();
                this.NextCh();
            }
        }

        protected abstract void Casing1();
        protected abstract void Casing2();
        protected abstract void CheckLiteral();
        protected abstract int GetMaxT();
        protected void Init()
        {
            this.pos = -1;
            this.line = 1;
            this.col = 0;
            this.charPos = -1;
            this.oldEols = 0;
            this.NextCh();
            if (this.ch == 0xef)
            {
                this.NextCh();
                int ch = this.ch;
                this.NextCh();
                int num2 = this.ch;
                if ((ch != 0xbb) || (num2 != 0xbf))
                {
                    throw new FatalError(string.Format("illegal byte order mark: EF {0,2:X} {1,2:X}", ch, num2));
                }
                this.buffer = new UTF8Buffer(this.buffer);
                this.col = 0;
                this.charPos = -1;
                this.NextCh();
            }
            this.pt = this.tokens = new Token();
        }

        protected void NextCh()
        {
            if (this.oldEols > 0)
            {
                this.ch = 10;
                this.oldEols--;
            }
            else
            {
                this.pos = this.buffer.Pos;
                this.ch = this.buffer.Read();
                this.col++;
                this.charPos++;
                if ((this.ch == 13) && (this.buffer.Peek() != 10))
                {
                    this.ch = 10;
                }
                if (this.ch == 10)
                {
                    this.line++;
                    this.col = 0;
                }
            }
            this.Casing1();
        }

        protected abstract Token NextToken();
        public Token Peek()
        {
            while (true)
            {
                this.pt.next ??= this.NextToken();
                this.pt = this.pt.next;
                if (this.pt.kind <= this.GetMaxT())
                {
                    return this.pt;
                }
            }
        }

        public Token PeekWithPragmas()
        {
            this.pt.next ??= this.NextToken();
            this.pt = this.pt.next;
            return this.pt;
        }

        public void ResetPeek()
        {
            this.pt = this.tokens;
        }

        public Token Scan()
        {
            if (this.tokens.next == null)
            {
                return this.NextToken();
            }
            this.pt = this.tokens = this.tokens.next;
            return this.tokens;
        }

        protected void SetScannerBehindT()
        {
            this.buffer.Pos = this.t.pos;
            this.NextCh();
            this.line = this.t.line;
            this.col = this.t.col;
            this.charPos = this.t.charPos;
            for (int i = 0; i < this.tlen; i++)
            {
                this.NextCh();
            }
        }
    }
}

