namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    public class LegacyMaskInfo : IEnumerable
    {
        private readonly List<LegacyMaskPrimitive> container;

        public LegacyMaskInfo();
        private string BuildExtruderRegExp(char blank, bool saveLiteral);
        public string GetDisplayText(string[] elements, char blank);
        public string GetEditText(string[] elements, char blank, bool saveLiteral);
        public string[] GetElementsEmpty();
        public string[] GetElementsFromEditText(string editText, char blank, bool saveLiteral);
        public int GetFirstEditableIndex();
        public bool GetIsEditable();
        public int GetLastEditableIndex();
        public int GetNextEditableElement(int current);
        public int GetPosition(string[] elements, int element, int insideElement);
        public int GetPrevEditableElement(int current);
        public static LegacyMaskInfo GetRegularMaskInfo(string mask, CultureInfo maskCulture);
        public static LegacyMaskInfo GetSimpleMaskInfo(string mask, CultureInfo maskCulture);
        public void PatchQuantifier(int min, int max);
        private static void PatchZeroLengthMaskInfo(LegacyMaskInfo info, char caseConversion);
        IEnumerator IEnumerable.GetEnumerator();

        public int Count { get; }

        public LegacyMaskPrimitive this[int primitiveIndex] { get; }
    }
}

