namespace DevExpress.XtraSpellChecker
{
    using DevExpress.XtraSpellChecker.Parser;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IIgnoreList : IEnumerable<IIgnoreItem>, IEnumerable
    {
        void Add(string word);
        void Add(Position start, Position end, string word);
        void Clear();
        bool Contains(string word);
        bool Contains(Position start, Position end, string word);
        void Remove(string word);
        void Remove(Position start, Position end, string word);
    }
}

