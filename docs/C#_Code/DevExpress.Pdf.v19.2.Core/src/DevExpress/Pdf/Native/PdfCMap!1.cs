namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfCMap<TResult>
    {
        private readonly PdfCMapTreeBranch<TResult> mappingTree;
        private readonly int maxCodeLength;

        protected PdfCMap(IDictionary<byte[], TResult> map)
        {
            this.mappingTree = new PdfCMapTreeBranch<TResult>(this.DefaultValue);
            foreach (KeyValuePair<byte[], TResult> pair in map)
            {
                byte[] key = pair.Key;
                this.maxCodeLength = Math.Max(this.maxCodeLength, key.Length);
                this.mappingTree.Fill(key, 0, pair.Value);
            }
        }

        protected virtual PdfCMapFindResult<TResult> Find(byte[] code, int position)
        {
            PdfCMapFindResult<TResult> result = this.mappingTree.Find(code, position);
            int num = code.Length - position;
            return (((result.CodeLength > 0) || (num >= this.maxCodeLength)) ? result : new PdfCMapFindResult<TResult>(this.DefaultValue, 0));
        }

        protected bool TryMapCode(byte[] code, out TResult result)
        {
            PdfCMapFindResult<TResult> result2 = this.Find(code, 0);
            if (result2.CodeLength > 0)
            {
                result = result2.Value;
                return true;
            }
            result = this.DefaultValue;
            return false;
        }

        internal bool IsEmpty =>
            this.maxCodeLength == 0;

        protected virtual TResult DefaultValue =>
            default(TResult);
    }
}

