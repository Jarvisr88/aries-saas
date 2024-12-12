namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public static class PdfBidiBrackets
    {
        private static readonly IDictionary<char, char> bidiBrackets;

        static PdfBidiBrackets()
        {
            Dictionary<char, char> dictionary1 = new Dictionary<char, char>();
            dictionary1.Add('(', ')');
            dictionary1.Add(')', '(');
            dictionary1.Add('[', ']');
            dictionary1.Add(']', '[');
            dictionary1.Add('{', '}');
            dictionary1.Add('}', '{');
            dictionary1.Add('༺', '༻');
            dictionary1.Add('༻', '༺');
            dictionary1.Add('༼', '༽');
            dictionary1.Add('༽', '༼');
            dictionary1.Add('᚛', '᚜');
            dictionary1.Add('᚜', '᚛');
            dictionary1.Add('⁅', '⁆');
            dictionary1.Add('⁆', '⁅');
            dictionary1.Add('⁽', '⁾');
            dictionary1.Add('⁾', '⁽');
            dictionary1.Add('₍', '₎');
            dictionary1.Add('₎', '₍');
            dictionary1.Add('⌈', '⌉');
            dictionary1.Add('⌉', '⌈');
            dictionary1.Add('⌊', '⌋');
            dictionary1.Add('⌋', '⌊');
            dictionary1.Add('〈', '〉');
            dictionary1.Add('〉', '〈');
            dictionary1.Add('❨', '❩');
            dictionary1.Add('❩', '❨');
            dictionary1.Add('❪', '❫');
            dictionary1.Add('❫', '❪');
            dictionary1.Add('❬', '❭');
            dictionary1.Add('❭', '❬');
            dictionary1.Add('❮', '❯');
            dictionary1.Add('❯', '❮');
            dictionary1.Add('❰', '❱');
            dictionary1.Add('❱', '❰');
            dictionary1.Add('❲', '❳');
            dictionary1.Add('❳', '❲');
            dictionary1.Add('❴', '❵');
            dictionary1.Add('❵', '❴');
            dictionary1.Add('⟅', '⟆');
            dictionary1.Add('⟆', '⟅');
            dictionary1.Add('⟦', '⟧');
            dictionary1.Add('⟧', '⟦');
            dictionary1.Add('⟨', '⟩');
            dictionary1.Add('⟩', '⟨');
            dictionary1.Add('⟪', '⟫');
            dictionary1.Add('⟫', '⟪');
            dictionary1.Add('⟬', '⟭');
            dictionary1.Add('⟭', '⟬');
            dictionary1.Add('⟮', '⟯');
            dictionary1.Add('⟯', '⟮');
            dictionary1.Add('⦃', '⦄');
            dictionary1.Add('⦄', '⦃');
            dictionary1.Add('⦅', '⦆');
            dictionary1.Add('⦆', '⦅');
            dictionary1.Add('⦇', '⦈');
            dictionary1.Add('⦈', '⦇');
            dictionary1.Add('⦉', '⦊');
            dictionary1.Add('⦊', '⦉');
            dictionary1.Add('⦋', '⦌');
            dictionary1.Add('⦌', '⦋');
            dictionary1.Add('⦍', '⦐');
            dictionary1.Add('⦎', '⦏');
            dictionary1.Add('⦏', '⦎');
            dictionary1.Add('⦐', '⦍');
            dictionary1.Add('⦑', '⦒');
            dictionary1.Add('⦒', '⦑');
            dictionary1.Add('⦓', '⦔');
            dictionary1.Add('⦔', '⦓');
            dictionary1.Add('⦕', '⦖');
            dictionary1.Add('⦖', '⦕');
            dictionary1.Add('⦗', '⦘');
            dictionary1.Add('⦘', '⦗');
            dictionary1.Add('⧘', '⧙');
            dictionary1.Add('⧙', '⧘');
            dictionary1.Add('⧚', '⧛');
            dictionary1.Add('⧛', '⧚');
            dictionary1.Add('⧼', '⧽');
            dictionary1.Add('⧽', '⧼');
            dictionary1.Add('⸢', '⸣');
            dictionary1.Add('⸣', '⸢');
            dictionary1.Add('⸤', '⸥');
            dictionary1.Add('⸥', '⸤');
            dictionary1.Add('⸦', '⸧');
            dictionary1.Add('⸧', '⸦');
            dictionary1.Add('⸨', '⸩');
            dictionary1.Add('⸩', '⸨');
            dictionary1.Add('〈', '〉');
            dictionary1.Add('〉', '〈');
            dictionary1.Add('《', '》');
            dictionary1.Add('》', '《');
            dictionary1.Add('「', '」');
            dictionary1.Add('」', '「');
            dictionary1.Add('『', '』');
            dictionary1.Add('』', '『');
            dictionary1.Add('【', '】');
            dictionary1.Add('】', '【');
            dictionary1.Add('〔', '〕');
            dictionary1.Add('〕', '〔');
            dictionary1.Add('〖', '〗');
            dictionary1.Add('〗', '〖');
            dictionary1.Add('〘', '〙');
            dictionary1.Add('〙', '〘');
            dictionary1.Add('〚', '〛');
            dictionary1.Add('〛', '〚');
            dictionary1.Add(0xfe59, 0xfe5a);
            dictionary1.Add(0xfe5a, 0xfe59);
            dictionary1.Add(0xfe5b, 0xfe5c);
            dictionary1.Add(0xfe5c, 0xfe5b);
            dictionary1.Add(0xfe5d, 0xfe5e);
            dictionary1.Add(0xfe5e, 0xfe5d);
            dictionary1.Add(0xff08, 0xff09);
            dictionary1.Add(0xff09, 0xff08);
            dictionary1.Add(0xff3b, 0xff3d);
            dictionary1.Add(0xff3d, 0xff3b);
            dictionary1.Add(0xff5b, 0xff5d);
            dictionary1.Add(0xff5d, 0xff5b);
            dictionary1.Add(0xff5f, 0xff60);
            dictionary1.Add(0xff60, 0xff5f);
            dictionary1.Add(0xff62, 0xff63);
            dictionary1.Add(0xff63, 0xff62);
            bidiBrackets = dictionary1;
        }

        public static char TryGetMirroredBracket(char ch)
        {
            char ch2;
            return (!bidiBrackets.TryGetValue(ch, out ch2) ? ch : ch2);
        }
    }
}

