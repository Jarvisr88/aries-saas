namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal abstract class TTFTable
    {
        protected const string tableMissingError = "The required table doesn't exist in the font file";
        private TTFFile owner;
        private TTFTableDirectoryEntry entry;

        public TTFTable(TTFFile owner)
        {
            this.owner = owner;
        }

        public void Initialize(TTFTable pattern)
        {
            this.Initialize(pattern, null);
        }

        public void Initialize(TTFTable pattern, TTFInitializeParam param)
        {
            if ((pattern != null) && base.GetType().Equals(pattern.GetType()))
            {
                this.InitializeTable(pattern, param);
                if (this.Entry == null)
                {
                    this.Owner.TableDirectory.Register(this);
                }
            }
        }

        protected virtual void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
        }

        public void Read(TTFStream ttfStream)
        {
            if (this.Entry == null)
            {
                throw new TTFFileException("The required table doesn't exist in the font file");
            }
            ttfStream.Seek(this.Entry.Offset);
            this.ReadTable(ttfStream);
        }

        protected virtual void ReadTable(TTFStream ttfStream)
        {
        }

        public void Write(TTFStream ttfStream)
        {
            ttfStream.Pad4();
            if (this.Entry != null)
            {
                this.Entry.InitializeOffset(ttfStream.Position);
            }
            this.WriteTable(ttfStream);
        }

        protected virtual void WriteTable(TTFStream ttfStream)
        {
        }

        protected internal TTFTableDirectoryEntry Entry
        {
            get
            {
                this.entry ??= this.Owner.TableDirectory[this.Tag];
                return this.entry;
            }
        }

        protected internal abstract string Tag { get; }

        public TTFFile Owner =>
            this.owner;

        public virtual int Length =>
            0;
    }
}

