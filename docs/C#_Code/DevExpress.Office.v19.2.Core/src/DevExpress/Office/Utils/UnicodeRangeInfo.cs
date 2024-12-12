namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class UnicodeRangeInfo
    {
        private readonly List<UnicodeSubrange> ranges = new List<UnicodeSubrange>();
        private readonly Dictionary<int, UnicodeSubrange> bitToSubrangeMap = new Dictionary<int, UnicodeSubrange>();

        public UnicodeRangeInfo()
        {
            this.PopulateSubranges();
        }

        protected internal virtual void AddSubrange(char low, char hi, int bit)
        {
            UnicodeSubrange item = new UnicodeSubrange(low, hi, bit);
            this.ranges.Add(item);
        }

        public List<UnicodeSubrange> GetSubranges(BitArray bitArray)
        {
            List<UnicodeSubrange> list = new List<UnicodeSubrange>();
            int length = bitArray.Length;
            for (int i = 0; i < length; i++)
            {
                UnicodeSubrange subrange;
                if (bitArray[i] && this.bitToSubrangeMap.TryGetValue(i, out subrange))
                {
                    list.Add(subrange);
                }
            }
            return list;
        }

        public UnicodeSubrange LookupSubrange(char ch)
        {
            int num = Algorithms.BinarySearchReverseOrder<UnicodeSubrange>(this.ranges, new UnicodeSubrangeAndCharComparer(ch));
            return ((num > 0) ? this.ranges[num] : null);
        }

        protected internal virtual void PopulateSubranges()
        {
            this.AddSubrange('\0', '\x007f', 0);
            this.AddSubrange('\x0080', '\x00ff', 1);
            this.AddSubrange('Ā', 'ſ', 2);
            this.AddSubrange('ƀ', 'ɏ', 3);
            this.AddSubrange('ɐ', 'ʯ', 4);
            this.AddSubrange('ʰ', '˿', 5);
            this.AddSubrange('̀', 'ͯ', 6);
            this.AddSubrange('Ͱ', 'Ͽ', 7);
            this.AddSubrange('Ѐ', 'ӿ', 9);
            this.AddSubrange('Ԁ', 'ԯ', 9);
            this.AddSubrange('԰', '֏', 10);
            this.AddSubrange('֐', '׿', 11);
            this.AddSubrange('؀', 'ۿ', 13);
            this.AddSubrange('܀', 'ݏ', 0x47);
            this.AddSubrange('ݐ', 'ݿ', 13);
            this.AddSubrange('ހ', '޿', 0x48);
            this.AddSubrange('߀', '߿', 14);
            this.AddSubrange('ऀ', 'ॿ', 15);
            this.AddSubrange('ঀ', '৿', 0x10);
            this.AddSubrange('਀', '੿', 0x11);
            this.AddSubrange('઀', '૿', 0x12);
            this.AddSubrange('଀', '୿', 0x13);
            this.AddSubrange('஀', '௿', 20);
            this.AddSubrange('ఀ', '౿', 0x15);
            this.AddSubrange('ಀ', '೿', 0x16);
            this.AddSubrange('ഀ', 'ൿ', 0x17);
            this.AddSubrange('඀', '෿', 0x49);
            this.AddSubrange('฀', '๿', 0x18);
            this.AddSubrange('຀', '໿', 0x19);
            this.AddSubrange('ༀ', '࿿', 70);
            this.AddSubrange('က', '႟', 0x4a);
            this.AddSubrange('Ⴀ', 'ჿ', 0x1a);
            this.AddSubrange('ᄀ', 'ᇿ', 0x1c);
            this.AddSubrange('ሀ', '፿', 0x4b);
            this.AddSubrange('ᎀ', '᎟', 0x4b);
            this.AddSubrange('Ꭰ', '᏿', 0x4c);
            this.AddSubrange('᐀', 'ᙿ', 0x4d);
            this.AddSubrange(' ', '᚟', 0x4e);
            this.AddSubrange('ᚠ', '᛿', 0x4f);
            this.AddSubrange('ᜀ', 'ᜟ', 0x54);
            this.AddSubrange('ᜠ', '᜿', 0x54);
            this.AddSubrange('ᝀ', '᝟', 0x54);
            this.AddSubrange('ᝠ', '᝿', 0x54);
            this.AddSubrange('ក', '៿', 80);
            this.AddSubrange('᠀', '᢯', 0x51);
            this.AddSubrange('ᤀ', '᥏', 0x5d);
            this.AddSubrange('ᥐ', '᥿', 0x5e);
            this.AddSubrange('ᦀ', '᧟', 0x5f);
            this.AddSubrange('᧠', '᧿', 80);
            this.AddSubrange('ᨀ', '᨟', 0x60);
            this.AddSubrange('ᬀ', '᭿', 0x1b);
            this.AddSubrange('ᮀ', 'ᮿ', 0x70);
            this.AddSubrange('ᰀ', 'ᱏ', 0x71);
            this.AddSubrange('᱐', '᱿', 0x72);
            this.AddSubrange('ᴀ', 'ᵿ', 4);
            this.AddSubrange('ᶀ', 'ᶿ', 4);
            this.AddSubrange('᷀', '᷿', 6);
            this.AddSubrange('Ḁ', 'ỿ', 0x1d);
            this.AddSubrange('ἀ', '῿', 30);
            this.AddSubrange(' ', '⁯', 0x1f);
            this.AddSubrange('⁰', '₟', 0x20);
            this.AddSubrange('₠', '⃏', 0x21);
            this.AddSubrange('⃐', '⃿', 0x22);
            this.AddSubrange('℀', '⅏', 0x23);
            this.AddSubrange('⅐', '↏', 0x24);
            this.AddSubrange('←', '⇿', 0x25);
            this.AddSubrange('∀', '⋿', 0x26);
            this.AddSubrange('⌀', '⏿', 0x27);
            this.AddSubrange('␀', '␿', 40);
            this.AddSubrange('⑀', '⑟', 0x29);
            this.AddSubrange('①', '⓿', 0x2a);
            this.AddSubrange('─', '╿', 0x2b);
            this.AddSubrange('▀', '▟', 0x2c);
            this.AddSubrange('■', '◿', 0x2d);
            this.AddSubrange('☀', '⛿', 0x2e);
            this.AddSubrange('✀', '➿', 0x2f);
            this.AddSubrange('⟀', '⟯', 0x26);
            this.AddSubrange('⟰', '⟿', 0x25);
            this.AddSubrange('⠀', '⣿', 0x52);
            this.AddSubrange('⤀', '⥿', 0x25);
            this.AddSubrange('⦀', '⧿', 0x26);
            this.AddSubrange('⨀', '⫿', 0x26);
            this.AddSubrange('⬀', '⯿', 0x25);
            this.AddSubrange('Ⰰ', 'ⱟ', 0x61);
            this.AddSubrange('Ⱡ', 'Ɀ', 0x1d);
            this.AddSubrange('Ⲁ', '⳿', 8);
            this.AddSubrange('ⴀ', '⴯', 0x1a);
            this.AddSubrange('ⴰ', '⵿', 0x62);
            this.AddSubrange('ⶀ', '⷟', 0x4b);
            this.AddSubrange('ⷠ', 'ⷿ', 9);
            this.AddSubrange('⸀', '⹿', 0x1f);
            this.AddSubrange('⺀', '⻿', 0x3b);
            this.AddSubrange('⼀', '⿟', 0x3b);
            this.AddSubrange('⿰', '⿿', 0x3b);
            this.AddSubrange('　', '〿', 0x30);
            this.AddSubrange('぀', 'ゟ', 0x31);
            this.AddSubrange('゠', 'ヿ', 50);
            this.AddSubrange('㄀', 'ㄯ', 0x33);
            this.AddSubrange('㄰', '㆏', 0x34);
            this.AddSubrange('㆐', '㆟', 0x3b);
            this.AddSubrange('ㆠ', 'ㆿ', 0x33);
            this.AddSubrange('㇀', '㇯', 0x3d);
            this.AddSubrange('ㇰ', 'ㇿ', 50);
            this.AddSubrange('㈀', '㋿', 0x36);
            this.AddSubrange('㌀', '㏿', 0x37);
            this.AddSubrange('㐀', '䶿', 0x3b);
            this.AddSubrange('䷀', '䷿', 0x63);
            this.AddSubrange('一', 0x9fff, 0x3b);
            this.AddSubrange(0xa000, 0xa48f, 0x53);
            this.AddSubrange(0xa490, 0xa4cf, 0x53);
            this.AddSubrange(0xa500, 0xa63f, 12);
            this.AddSubrange(0xa640, 0xa69f, 9);
            this.AddSubrange(0xa700, 0xa71f, 5);
            this.AddSubrange(0xa720, 0xa7ff, 0x1d);
            this.AddSubrange(0xa800, 0xa82f, 100);
            this.AddSubrange(0xa840, 0xa87f, 0x35);
            this.AddSubrange(0xa880, 0xa8df, 0x73);
            this.AddSubrange(0xa900, 0xa92f, 0x74);
            this.AddSubrange(0xa930, 0xa95f, 0x75);
            this.AddSubrange(0xaa00, 0xaa5f, 0x76);
            this.AddSubrange(0xac00, 0xd7af, 0x38);
            this.AddSubrange(0xd800, 0xdfff, 0x39);
            this.AddSubrange(0xe000, 0xf8ff, 60);
            this.AddSubrange(0xf900, 0xfaff, 0x3d);
            this.AddSubrange(0xfb00, 0xfb4f, 0x3e);
            this.AddSubrange(0xfb50, 0xfdff, 0x3f);
            this.AddSubrange(0xfe00, 0xfe0f, 0x5b);
            this.AddSubrange(0xfe10, 0xfe1f, 0x41);
            this.AddSubrange(0xfe20, 0xfe2f, 0x40);
            this.AddSubrange(0xfe30, 0xfe4f, 0x41);
            this.AddSubrange(0xfe50, 0xfe6f, 0x42);
            this.AddSubrange(0xfe70, 0xfeff, 0x43);
            this.AddSubrange(0xff00, 0xffef, 0x44);
            this.AddSubrange(0xfff0, 0xffff, 0x45);
        }
    }
}

