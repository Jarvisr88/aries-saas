namespace DevExpress.XtraSpellChecker.Parser
{
    using System;
    using System.Reflection;

    public interface ISpellCheckTextController
    {
        bool CanDoNextStep(Position position);
        bool DeleteWord(ref Position start, ref Position finish);
        Position GetNextPosition(Position pos);
        string GetPreviousWord(Position pos);
        Position GetPrevPosition(Position pos);
        Position GetSentenceEndPosition(Position pos);
        Position GetSentenceStartPosition(Position pos);
        Position GetTextLength(string text);
        string GetWord(Position start, Position finish);
        Position GetWordStartPosition(Position pos);
        bool HasLetters(Position start, Position finish);
        bool ReplaceWord(Position start, Position finish, string word);

        string Text { get; set; }

        char this[Position position] { get; }
    }
}

