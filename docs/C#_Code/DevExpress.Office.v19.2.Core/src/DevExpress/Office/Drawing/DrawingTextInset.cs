namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingTextInset : ISupportsCopyFrom<DrawingTextInset>, ITextInset, ITextInsetOptions, ISupportsBinaryReadWrite
    {
        private const int length = 4;
        private const int LeftIndex = 0;
        private const int RightIndex = 1;
        private const int TopIndex = 2;
        private const int BottomIndex = 3;
        public const int DefaultTopBottom = 0x48;
        public const int DefaultLeftRight = 0x90;
        private readonly IDocumentModel documentModel;
        private readonly float[] inset;
        private readonly bool[] hasValues;

        public DrawingTextInset(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.inset = new float[] { 144f, 144f, 72f, 72f };
            this.hasValues = new bool[4];
        }

        private void ApplyHistoryItem(HistoryItem item)
        {
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        public void CopyFrom(DrawingTextInset value)
        {
            this.SetInset(0, value.Left, value.Options.HasLeft);
            this.SetInset(1, value.Right, value.Options.HasRight);
            this.SetInset(2, value.Top, value.Options.HasTop);
            this.SetInset(3, value.Bottom, value.Options.HasBottom);
        }

        public override bool Equals(object obj)
        {
            DrawingTextInset inset = obj as DrawingTextInset;
            return ((inset != null) ? ((this.inset[0] == inset.Left) && ((this.inset[1] == inset.Right) && ((this.inset[2] == inset.Top) && ((this.inset[3] == inset.Bottom) && ((this.hasValues[0] == inset.Options.HasLeft) && ((this.hasValues[1] == inset.Options.HasRight) && ((this.hasValues[2] == inset.Options.HasTop) && (this.hasValues[3] == inset.Options.HasBottom)))))))) : false);
        }

        public override int GetHashCode() => 
            ((((((this.inset[0].GetHashCode() ^ this.inset[1].GetHashCode()) ^ this.inset[2].GetHashCode()) ^ this.inset[3].GetHashCode()) ^ this.hasValues[0].GetHashCode()) ^ this.hasValues[1].GetHashCode()) ^ this.hasValues[2].GetHashCode()) ^ this.hasValues[3].GetHashCode();

        public void Read(BinaryReader reader)
        {
            for (int i = 0; i < 4; i++)
            {
                this.inset[i] = reader.ReadSingle();
            }
            for (int j = 0; j < 4; j++)
            {
                this.hasValues[j] = reader.ReadBoolean();
            }
        }

        public void ResetToStyle()
        {
            this.SetInset(0, 144f, false);
            this.SetInset(1, 144f, false);
            this.SetInset(2, 72f, false);
            this.SetInset(3, 72f, false);
        }

        private void SetInset(int index, float value)
        {
            if ((this.inset[index] != value) || !this.hasValues[index])
            {
                if (this.inset[index] == value)
                {
                    this.ApplyHistoryItem(new DrawingTextInsetHasValuesChangeHistoryItem(this, index, this.hasValues[index], true));
                }
                else
                {
                    DrawingValueChecker.CheckCoordinate32F(this.inset[index], "InsetCoordinate");
                    this.DocumentModel.History.BeginTransaction();
                    this.ApplyHistoryItem(new DrawingTextInsetHasValuesChangeHistoryItem(this, index, this.hasValues[index], true));
                    this.ApplyHistoryItem(new DrawingTextInsetChangeHistoryItem(this, index, this.inset[index], value));
                    this.DocumentModel.History.EndTransaction();
                }
            }
        }

        private void SetInset(int index, float value, bool hasValue)
        {
            if (this.hasValues[index] != hasValue)
            {
                this.ApplyHistoryItem(new DrawingTextInsetHasValuesChangeHistoryItem(this, index, this.hasValues[index], hasValue));
            }
            if (this.inset[index] != value)
            {
                this.ApplyHistoryItem(new DrawingTextInsetChangeHistoryItem(this, index, this.inset[index], value));
            }
        }

        public void SetInsetCore(int index, float value)
        {
            this.inset[index] = value;
        }

        public void SetInsetHasValuesCore(int index, bool value)
        {
            this.hasValues[index] = value;
        }

        public void Write(BinaryWriter writer)
        {
            for (int i = 0; i < 4; i++)
            {
                writer.Write(this.inset[i]);
            }
            for (int j = 0; j < 4; j++)
            {
                writer.Write(this.hasValues[j]);
            }
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        public float Left
        {
            get => 
                this.inset[0];
            set => 
                this.SetInset(0, value);
        }

        public float Right
        {
            get => 
                this.inset[1];
            set => 
                this.SetInset(1, value);
        }

        public float Top
        {
            get => 
                this.inset[2];
            set => 
                this.SetInset(2, value);
        }

        public float Bottom
        {
            get => 
                this.inset[3];
            set => 
                this.SetInset(3, value);
        }

        public ITextInsetOptions Options =>
            this;

        bool ITextInsetOptions.HasLeft =>
            this.hasValues[0];

        bool ITextInsetOptions.HasRight =>
            this.hasValues[1];

        bool ITextInsetOptions.HasTop =>
            this.hasValues[2];

        bool ITextInsetOptions.HasBottom =>
            this.hasValues[3];

        public bool IsDefault =>
            !this.hasValues[0] && (!this.hasValues[1] && (!this.hasValues[2] && !this.hasValues[3]));
    }
}

