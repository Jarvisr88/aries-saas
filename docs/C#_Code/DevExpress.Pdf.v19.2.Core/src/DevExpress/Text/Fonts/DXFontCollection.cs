namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class DXFontCollection : IDXFontCollection, IDisposable
    {
        private static Dictionary<string, string> fontMapTable;
        private static Dictionary<string, byte[]> fontPanoseTable;
        private static int[] panoseWeights;

        static DXFontCollection()
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("Calibri", "DejaVu Sans");
            dictionary1.Add("Segoe UI", "DejaVu Sans");
            dictionary1.Add("Times New Roman", "Liberation Serif");
            dictionary1.Add("Arial", "Liberation Sans");
            dictionary1.Add("Courier New", "Liberation Mono");
            fontMapTable = dictionary1;
            Dictionary<string, byte[]> dictionary = new Dictionary<string, byte[]>();
            byte[] buffer1 = new byte[] { 2, 11, 6, 4, 2, 2, 2, 2, 2, 4 };
            dictionary.Add("Arial", buffer1);
            byte[] buffer2 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Bahnschrift", buffer2);
            byte[] buffer3 = new byte[] { 2, 15, 5, 2, 2, 2, 4, 3, 2, 4 };
            dictionary.Add("Calibri", buffer3);
            byte[] buffer4 = new byte[] { 2, 4, 5, 3, 5, 4, 6, 3, 2, 4 };
            dictionary.Add("Cambria", buffer4);
            byte[] buffer5 = new byte[] { 2, 4, 5, 3, 5, 4, 6, 3, 2, 4 };
            dictionary.Add("Cambria Math", buffer5);
            byte[] buffer6 = new byte[] { 2, 14, 5, 2, 3, 3, 3, 2, 2, 4 };
            dictionary.Add("Candara", buffer6);
            byte[] buffer7 = new byte[] { 3, 15, 7, 2, 3, 3, 2, 2, 2, 4 };
            dictionary.Add("Comic Sans MS", buffer7);
            byte[] buffer8 = new byte[] { 2, 11, 6, 9, 2, 2, 4, 3, 2, 4 };
            dictionary.Add("Consolas", buffer8);
            byte[] buffer9 = new byte[] { 2, 3, 6, 2, 5, 3, 6, 3, 3, 3 };
            dictionary.Add("Constantia", buffer9);
            byte[] buffer10 = new byte[] { 2, 11, 5, 3, 2, 2, 4, 2, 2, 4 };
            dictionary.Add("Corbel", buffer10);
            byte[] buffer11 = new byte[] { 2, 7, 3, 9, 2, 2, 5, 2, 4, 4 };
            dictionary.Add("Courier New", buffer11);
            byte[] buffer12 = new byte[10];
            buffer12[0] = 2;
            dictionary.Add("Ebrima", buffer12);
            byte[] buffer13 = new byte[] { 2, 11, 6, 3, 2, 1, 2, 2, 2, 4 };
            dictionary.Add("Franklin Gothic", buffer13);
            byte[] buffer14 = new byte[] { 4, 4, 6, 5, 5, 0x10, 2, 2, 13, 2 };
            dictionary.Add("Gabriola", buffer14);
            byte[] buffer15 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Gadugi", buffer15);
            byte[] buffer16 = new byte[] { 2, 4, 5, 2, 5, 4, 5, 2, 3, 3 };
            dictionary.Add("Georgia", buffer16);
            byte[] buffer17 = new byte[] { 2, 11, 8, 6, 3, 9, 2, 5, 2, 4 };
            dictionary.Add("Impact", buffer17);
            byte[] buffer18 = new byte[] { 3, 8, 4, 2, 0, 5, 0, 0, 0, 0 };
            dictionary.Add("Ink Free", buffer18);
            byte[] buffer19 = new byte[10];
            buffer19[0] = 2;
            dictionary.Add("Javanese Text", buffer19);
            byte[] buffer20 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Leelawadee UI", buffer20);
            byte[] buffer21 = new byte[] { 2, 11, 6, 9, 4, 5, 4, 2, 2, 4 };
            dictionary.Add("Lucida Console", buffer21);
            byte[] buffer22 = new byte[] { 2, 11, 6, 2, 3, 5, 4, 2, 2, 4 };
            dictionary.Add("Lucida Sans Unicode", buffer22);
            byte[] buffer23 = new byte[] { 2, 11, 5, 3, 2, 0, 0, 2, 0, 4 };
            dictionary.Add("Malgun Gothic", buffer23);
            byte[] buffer24 = new byte[] { 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 };
            dictionary.Add("Microsoft Himalaya", buffer24);
            byte[] buffer25 = new byte[] { 2, 11, 6, 4, 3, 5, 4, 4, 2, 4 };
            dictionary.Add("Microsoft JhengHei", buffer25);
            byte[] buffer26 = new byte[] { 2, 11, 6, 4, 3, 5, 4, 4, 2, 4 };
            dictionary.Add("Microsoft JhengHei UI", buffer26);
            byte[] buffer27 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Microsoft New Tai Lue", buffer27);
            byte[] buffer28 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Microsoft PhagsPa", buffer28);
            byte[] buffer29 = new byte[] { 2, 11, 6, 4, 2, 2, 2, 2, 2, 4 };
            dictionary.Add("Microsoft Sans Serif", buffer29);
            byte[] buffer30 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Microsoft Tai Le", buffer30);
            byte[] buffer31 = new byte[] { 2, 11, 5, 3, 2, 2, 4, 2, 2, 4 };
            dictionary.Add("Microsoft YaHei", buffer31);
            byte[] buffer32 = new byte[] { 2, 11, 5, 3, 2, 2, 4, 2, 2, 4 };
            dictionary.Add("Microsoft YaHei UI", buffer32);
            byte[] buffer33 = new byte[10];
            buffer33[0] = 3;
            buffer33[2] = 5;
            dictionary.Add("Microsoft Yi Baiti", buffer33);
            byte[] buffer34 = new byte[] { 2, 2, 5, 0, 0, 0, 0, 0, 0, 0 };
            dictionary.Add("MingLiU-ExtB", buffer34);
            byte[] buffer35 = new byte[] { 2, 2, 5, 0, 0, 0, 0, 0, 0, 0 };
            dictionary.Add("PMingLiU-ExtB", buffer35);
            byte[] buffer36 = new byte[] { 2, 2, 5, 0, 0, 0, 0, 0, 0, 0 };
            dictionary.Add("MingLiU_HKSCS-ExtB", buffer36);
            byte[] buffer37 = new byte[10];
            buffer37[0] = 3;
            buffer37[2] = 5;
            dictionary.Add("Mongolian Baiti", buffer37);
            byte[] buffer38 = new byte[] { 2, 11, 6, 9, 7, 2, 5, 8, 2, 4 };
            dictionary.Add("MS Gothic", buffer38);
            byte[] buffer39 = new byte[] { 2, 11, 6, 0, 7, 2, 5, 8, 2, 4 };
            dictionary.Add("MS UI Gothic", buffer39);
            byte[] buffer40 = new byte[] { 2, 11, 6, 0, 7, 2, 5, 8, 2, 4 };
            dictionary.Add("MS PGothic", buffer40);
            byte[] buffer41 = new byte[] { 2, 0, 5, 0, 3, 2, 0, 9, 0, 0 };
            dictionary.Add("MV Boli", buffer41);
            byte[] buffer42 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Myanmar Text", buffer42);
            byte[] buffer43 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Nirmala UI", buffer43);
            byte[] buffer44 = new byte[] { 5, 10, 1, 2, 1, 1, 1, 1, 1, 1 };
            dictionary.Add("Segoe MDL2 Assets", buffer44);
            byte[] buffer45 = new byte[10];
            buffer45[0] = 2;
            buffer45[2] = 6;
            dictionary.Add("Segoe Print", buffer45);
            byte[] buffer46 = new byte[] { 3, 11, 5, 4, 2, 0, 0, 0, 0, 3 };
            dictionary.Add("Segoe Script", buffer46);
            byte[] buffer47 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Segoe UI", buffer47);
            byte[] buffer48 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Segoe UI Emoji", buffer48);
            byte[] buffer49 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Segoe UI Historic", buffer49);
            byte[] buffer50 = new byte[] { 2, 11, 5, 2, 4, 2, 4, 2, 2, 3 };
            dictionary.Add("Segoe UI Symbol", buffer50);
            byte[] buffer51 = new byte[] { 2, 1, 6, 0, 3, 1, 1, 1, 1, 1 };
            dictionary.Add("SimSun", buffer51);
            byte[] buffer52 = new byte[] { 2, 1, 6, 9, 3, 1, 1, 1, 1, 1 };
            dictionary.Add("NSimSun", buffer52);
            byte[] buffer53 = new byte[] { 2, 1, 6, 9, 6, 1, 1, 1, 1, 1 };
            dictionary.Add("SimSun-ExtB", buffer53);
            byte[] buffer54 = new byte[] { 2, 0, 5, 5, 0, 0, 0, 2, 0, 4 };
            dictionary.Add("Sitka Small", buffer54);
            byte[] buffer55 = new byte[] { 2, 0, 5, 5, 0, 0, 0, 2, 0, 4 };
            dictionary.Add("Sitka Text", buffer55);
            byte[] buffer56 = new byte[] { 2, 0, 5, 5, 0, 0, 0, 2, 0, 4 };
            dictionary.Add("Sitka Subheading", buffer56);
            byte[] buffer57 = new byte[] { 2, 0, 5, 5, 0, 0, 0, 2, 0, 4 };
            dictionary.Add("Sitka Heading", buffer57);
            byte[] buffer58 = new byte[] { 2, 0, 5, 5, 0, 0, 0, 2, 0, 4 };
            dictionary.Add("Sitka Display", buffer58);
            byte[] buffer59 = new byte[] { 2, 0, 5, 5, 0, 0, 0, 2, 0, 4 };
            dictionary.Add("Sitka Banner", buffer59);
            byte[] buffer60 = new byte[] { 1, 10, 5, 2, 5, 3, 6, 3, 3, 3 };
            dictionary.Add("Sylfaen", buffer60);
            byte[] buffer61 = new byte[] { 5, 5, 1, 2, 1, 7, 6, 2, 5, 7 };
            dictionary.Add("Symbol", buffer61);
            byte[] buffer62 = new byte[] { 2, 11, 6, 4, 3, 5, 4, 4, 2, 4 };
            dictionary.Add("Tahoma", buffer62);
            byte[] buffer63 = new byte[] { 2, 2, 6, 3, 5, 4, 5, 2, 3, 4 };
            dictionary.Add("Times New Roman", buffer63);
            byte[] buffer64 = new byte[] { 2, 11, 6, 3, 2, 2, 2, 2, 2, 4 };
            dictionary.Add("Trebuchet MS", buffer64);
            byte[] buffer65 = new byte[] { 2, 11, 6, 4, 3, 5, 4, 4, 2, 4 };
            dictionary.Add("Verdana", buffer65);
            byte[] buffer66 = new byte[] { 5, 3, 1, 2, 1, 5, 9, 6, 7, 3 };
            dictionary.Add("Webdings", buffer66);
            byte[] buffer67 = new byte[10];
            buffer67[0] = 5;
            dictionary.Add("Wingdings", buffer67);
            byte[] buffer68 = new byte[] { 2, 11, 4, 0, 0, 0, 0, 0, 0, 0 };
            dictionary.Add("Yu Gothic", buffer68);
            byte[] buffer69 = new byte[] { 2, 11, 5, 0, 0, 0, 0, 0, 0, 0 };
            dictionary.Add("Yu Gothic UI", buffer69);
            byte[] buffer70 = new byte[] { 3, 8, 5, 0, 0, 5, 0, 0, 0, 4 };
            dictionary.Add("Buxton Sketch", buffer70);
            byte[] buffer71 = new byte[] { 3, 8, 6, 2, 4, 3, 2, 2, 2, 4 };
            dictionary.Add("Segoe Marker", buffer71);
            byte[] buffer72 = new byte[] { 2, 11, 6, 4, 2, 2, 2, 2, 2, 4 };
            dictionary.Add("Arial Unicode MS", buffer72);
            byte[] buffer73 = new byte[] { 2, 4, 6, 4, 5, 5, 5, 2, 3, 4 };
            dictionary.Add("Century", buffer73);
            byte[] buffer74 = new byte[] { 5, 2, 1, 2, 1, 5, 7, 7, 7, 7 };
            dictionary.Add("Wingdings 2", buffer74);
            byte[] buffer75 = new byte[] { 5, 4, 1, 2, 1, 8, 7, 7, 7, 7 };
            dictionary.Add("Wingdings 3", buffer75);
            byte[] buffer76 = new byte[] { 4, 2, 7, 5, 4, 10, 2, 6, 7, 2 };
            dictionary.Add("Algerian", buffer76);
            byte[] buffer77 = new byte[] { 2, 2, 6, 2, 8, 5, 5, 2, 3, 3 };
            dictionary.Add("Baskerville Old Face", buffer77);
            byte[] buffer78 = new byte[] { 4, 3, 9, 5, 2, 11, 2, 2, 12, 2 };
            dictionary.Add("Bauhaus 93", buffer78);
            byte[] buffer79 = new byte[] { 2, 2, 5, 3, 6, 3, 5, 2, 3, 3 };
            dictionary.Add("Bell MT", buffer79);
            byte[] buffer80 = new byte[] { 2, 14, 6, 2, 2, 5, 2, 2, 3, 6 };
            dictionary.Add("Berlin Sans FB", buffer80);
            byte[] buffer81 = new byte[] { 2, 5, 8, 6, 6, 9, 5, 2, 4, 4 };
            dictionary.Add("Bernard MT", buffer81);
            byte[] buffer82 = new byte[] { 2, 7, 7, 6, 8, 6, 1, 5, 2, 4 };
            dictionary.Add("Bodoni MT Poster", buffer82);
            byte[] buffer83 = new byte[] { 4, 4, 9, 5, 8, 11, 2, 2, 5, 2 };
            dictionary.Add("Broadway", buffer83);
            byte[] buffer84 = new byte[] { 3, 6, 8, 2, 4, 4, 6, 7, 3, 4 };
            dictionary.Add("Brush Script MT", buffer84);
            byte[] buffer85 = new byte[] { 2, 7, 4, 3, 6, 8, 11, 3, 2, 4 };
            dictionary.Add("Californian FB", buffer85);
            byte[] buffer86 = new byte[] { 2, 3, 5, 4, 5, 2, 5, 2, 3, 4 };
            dictionary.Add("Centaur", buffer86);
            byte[] buffer87 = new byte[] { 2, 11, 5, 2, 2, 2, 2, 2, 2, 4 };
            dictionary.Add("Century Gothic", buffer87);
            byte[] buffer88 = new byte[] { 4, 2, 4, 4, 3, 0x10, 7, 2, 6, 2 };
            dictionary.Add("Chiller", buffer88);
            byte[] buffer89 = new byte[] { 4, 2, 8, 5, 6, 2, 2, 3, 2, 3 };
            dictionary.Add("Colonna MT", buffer89);
            byte[] buffer90 = new byte[] { 2, 8, 9, 4, 4, 3, 11, 2, 4, 4 };
            dictionary.Add("Cooper", buffer90);
            byte[] buffer91 = new byte[] { 2, 4, 6, 2, 6, 3, 10, 2, 3, 4 };
            dictionary.Add("Footlight MT", buffer91);
            byte[] buffer92 = new byte[] { 3, 8, 4, 2, 3, 2, 5, 11, 4, 4 };
            dictionary.Add("Freestyle Script", buffer92);
            byte[] buffer93 = new byte[] { 4, 3, 6, 4, 2, 15, 2, 2, 13, 2 };
            dictionary.Add("Harlow Solid", buffer93);
            byte[] buffer94 = new byte[] { 4, 4, 5, 5, 5, 10, 2, 2, 7, 2 };
            dictionary.Add("Harrington", buffer94);
            byte[] buffer95 = new byte[] { 2, 4, 5, 2, 5, 5, 6, 3, 3, 3 };
            dictionary.Add("High Tower Text", buffer95);
            byte[] buffer96 = new byte[] { 4, 9, 6, 5, 6, 13, 6, 2, 7, 2 };
            dictionary.Add("Jokerman", buffer96);
            byte[] buffer97 = new byte[] { 4, 4, 4, 3, 4, 10, 2, 2, 2, 2 };
            dictionary.Add("Juice ITC", buffer97);
            byte[] buffer98 = new byte[] { 3, 5, 5, 2, 4, 2, 2, 3, 2, 2 };
            dictionary.Add("Kristen ITC", buffer98);
            byte[] buffer99 = new byte[] { 2, 4, 6, 2, 5, 5, 5, 2, 3, 4 };
            dictionary.Add("Lucida Bright", buffer99);
            byte[] buffer100 = new byte[] { 3, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            dictionary.Add("Lucida Calligraphy", buffer100);
            byte[] buffer101 = new byte[] { 2, 6, 6, 2, 5, 5, 5, 2, 2, 4 };
            dictionary.Add("Lucida Fax", buffer101);
            byte[] buffer102 = new byte[] { 3, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            dictionary.Add("Lucida Handwriting", buffer102);
            byte[] buffer103 = new byte[] { 4, 3, 8, 5, 5, 8, 2, 2, 13, 2 };
            dictionary.Add("Magneto", buffer103);
            byte[] buffer104 = new byte[] { 3, 2, 8, 2, 6, 6, 2, 7, 2, 2 };
            dictionary.Add("Matura MT Script Capitals", buffer104);
            byte[] buffer105 = new byte[] { 3, 9, 7, 2, 3, 4, 7, 2, 4, 3 };
            dictionary.Add("Mistral", buffer105);
            byte[] buffer106 = new byte[] { 2, 7, 7, 4, 7, 5, 5, 2, 3, 3 };
            dictionary.Add("Modern No. 20", buffer106);
            byte[] buffer107 = new byte[] { 3, 1, 1, 1, 1, 2, 1, 1, 1, 1 };
            dictionary.Add("Monotype Corsiva", buffer107);
            byte[] buffer108 = new byte[] { 4, 2, 5, 2, 7, 7, 3, 3, 2, 2 };
            dictionary.Add("Niagara Engraved", buffer108);
            byte[] buffer109 = new byte[] { 4, 2, 5, 2, 7, 7, 2, 2, 2, 2 };
            dictionary.Add("Niagara Solid", buffer109);
            byte[] buffer110 = new byte[] { 3, 4, 9, 2, 4, 5, 8, 3, 8, 6 };
            dictionary.Add("Old English Text MT", buffer110);
            byte[] buffer111 = new byte[] { 4, 5, 6, 2, 8, 7, 2, 2, 2, 3 };
            dictionary.Add("Onyx", buffer111);
            byte[] buffer112 = new byte[] { 3, 4, 6, 2, 4, 7, 8, 4, 8, 4 };
            dictionary.Add("Parchment", buffer112);
            byte[] buffer113 = new byte[] { 4, 5, 6, 3, 10, 6, 2, 2, 2, 2 };
            dictionary.Add("Playbill", buffer113);
            byte[] buffer114 = new byte[] { 2, 8, 5, 2, 5, 5, 5, 2, 7, 2 };
            dictionary.Add("Poor Richard", buffer114);
            byte[] buffer115 = new byte[] { 4, 4, 8, 5, 5, 8, 9, 2, 6, 2 };
            dictionary.Add("Ravie", buffer115);
            byte[] buffer116 = new byte[] { 3, 6, 4, 2, 3, 4, 6, 11, 2, 4 };
            dictionary.Add("Informal Roman", buffer116);
            byte[] buffer117 = new byte[] { 4, 2, 9, 4, 2, 1, 2, 2, 6, 4 };
            dictionary.Add("Showcard Gothic", buffer117);
            byte[] buffer118 = new byte[] { 4, 4, 10, 7, 6, 10, 2, 2, 2, 2 };
            dictionary.Add("Snap ITC", buffer118);
            byte[] buffer119 = new byte[] { 4, 4, 9, 5, 13, 8, 2, 2, 4, 4 };
            dictionary.Add("Stencil", buffer119);
            byte[] buffer120 = new byte[] { 4, 2, 4, 4, 3, 13, 7, 2, 2, 2 };
            dictionary.Add("Tempus Sans ITC", buffer120);
            byte[] buffer121 = new byte[] { 3, 7, 5, 2, 3, 5, 2, 2, 2, 3 };
            dictionary.Add("Viner Hand ITC", buffer121);
            byte[] buffer122 = new byte[] { 3, 2, 6, 2, 5, 5, 6, 9, 8, 4 };
            dictionary.Add("Vivaldi", buffer122);
            byte[] buffer123 = new byte[] { 3, 5, 4, 2, 4, 4, 7, 7, 3, 5 };
            dictionary.Add("Vladimir Script", buffer123);
            byte[] buffer124 = new byte[] { 2, 10, 10, 7, 5, 5, 5, 2, 4, 4 };
            dictionary.Add("Wide Latin", buffer124);
            byte[] buffer125 = new byte[] { 2, 11, 6, 2, 2, 1, 4, 2, 6, 3 };
            dictionary.Add("Tw Cen MT", buffer125);
            byte[] buffer126 = new byte[] { 3, 4, 6, 2, 4, 6, 7, 8, 9, 4 };
            dictionary.Add("Script MT", buffer126);
            byte[] buffer127 = new byte[] { 2, 6, 6, 3, 2, 2, 5, 2, 4, 3 };
            dictionary.Add("Rockwell", buffer127);
            byte[] buffer128 = new byte[] { 3, 7, 5, 2, 4, 5, 7, 7, 3, 4 };
            dictionary.Add("Rage", buffer128);
            byte[] buffer129 = new byte[] { 3, 6, 4, 2, 4, 4, 6, 8, 2, 4 };
            dictionary.Add("Pristina", buffer129);
            byte[] buffer130 = new byte[] { 2, 2, 5, 2, 6, 5, 5, 2, 8, 4 };
            dictionary.Add("Perpetua Titling MT", buffer130);
            byte[] buffer131 = new byte[] { 2, 2, 5, 2, 6, 4, 1, 2, 3, 3 };
            dictionary.Add("Perpetua", buffer131);
            byte[] buffer132 = new byte[] { 3, 7, 5, 2, 6, 5, 2, 3, 2, 5 };
            dictionary.Add("Papyrus", buffer132);
            byte[] buffer133 = new byte[] { 3, 3, 3, 2, 2, 6, 7, 12, 11, 5 };
            dictionary.Add("Palace Script MT", buffer133);
            byte[] buffer134 = new byte[] { 2, 1, 5, 9, 2, 1, 2, 1, 3, 3 };
            dictionary.Add("OCR A", buffer134);
            byte[] buffer135 = new byte[] { 2, 14, 5, 2, 3, 3, 8, 2, 2, 4 };
            dictionary.Add("Maiandra GD", buffer135);
            byte[] buffer136 = new byte[] { 2, 11, 5, 9, 3, 5, 4, 3, 2, 4 };
            dictionary.Add("Lucida Sans Typewriter", buffer136);
            byte[] buffer137 = new byte[] { 2, 11, 6, 2, 3, 5, 4, 2, 2, 4 };
            dictionary.Add("Lucida Sans", buffer137);
            byte[] buffer138 = new byte[] { 4, 2, 6, 5, 6, 3, 3, 3, 2, 2 };
            dictionary.Add("Imprint MT Shadow", buffer138);
            byte[] buffer139 = new byte[] { 2, 11, 7, 6, 4, 9, 2, 6, 2, 4 };
            dictionary.Add("Haettenschweiler", buffer139);
            byte[] buffer140 = new byte[] { 2, 2, 9, 4, 7, 3, 11, 2, 4, 1 };
            dictionary.Add("Goudy Stout", buffer140);
            byte[] buffer141 = new byte[] { 2, 2, 5, 2, 5, 3, 5, 2, 3, 3 };
            dictionary.Add("Goudy Old Style", buffer141);
            byte[] buffer142 = new byte[] { 2, 3, 8, 8, 2, 6, 1, 1, 1, 1 };
            dictionary.Add("Gloucester MT", buffer142);
            byte[] buffer143 = new byte[] { 2, 11, 10, 2, 2, 1, 4, 2, 2, 3 };
            dictionary.Add("Gill Sans", buffer143);
            byte[] buffer144 = new byte[] { 2, 11, 5, 2, 2, 1, 4, 2, 2, 3 };
            dictionary.Add("Gill Sans MT", buffer144);
            byte[] buffer145 = new byte[] { 4, 4, 5, 4, 6, 0x10, 7, 2, 13, 2 };
            dictionary.Add("Gigi", buffer145);
            byte[] buffer146 = new byte[] { 2, 2, 4, 4, 3, 3, 1, 1, 8, 3 };
            dictionary.Add("Garamond", buffer146);
            byte[] buffer147 = new byte[] { 3, 2, 4, 2, 4, 6, 7, 4, 6, 5 };
            dictionary.Add("French Script MT", buffer147);
            byte[] buffer148 = new byte[] { 2, 11, 5, 3, 2, 1, 2, 2, 2, 4 };
            dictionary.Add("Franklin Gothic Book", buffer148);
            byte[] buffer149 = new byte[] { 3, 6, 9, 2, 4, 5, 2, 7, 2, 3 };
            dictionary.Add("Forte", buffer149);
            byte[] buffer150 = new byte[] { 4, 6, 5, 5, 6, 2, 2, 2, 10, 4 };
            dictionary.Add("Felix Titling", buffer150);
            byte[] buffer151 = new byte[] { 2, 11, 6, 2, 3, 5, 4, 2, 8, 4 };
            dictionary.Add("Eras ITC", buffer151);
            byte[] buffer152 = new byte[] { 2, 9, 7, 7, 8, 5, 5, 2, 3, 4 };
            dictionary.Add("Engravers MT", buffer152);
            byte[] buffer153 = new byte[] { 2, 2, 9, 4, 9, 5, 5, 2, 3, 3 };
            dictionary.Add("Elephant", buffer153);
            byte[] buffer154 = new byte[] { 3, 3, 3, 2, 4, 7, 7, 13, 8, 4 };
            dictionary.Add("Edwardian Script ITC", buffer154);
            byte[] buffer155 = new byte[] { 4, 4, 4, 4, 5, 7, 2, 2, 2, 2 };
            dictionary.Add("Curlz MT", buffer155);
            byte[] buffer156 = new byte[] { 2, 14, 5, 7, 2, 2, 6, 2, 4, 4 };
            dictionary.Add("Copperplate Gothic", buffer156);
            byte[] buffer157 = new byte[] { 2, 4, 6, 4, 5, 5, 5, 2, 3, 4 };
            dictionary.Add("Century Schoolbook", buffer157);
            byte[] buffer158 = new byte[] { 2, 10, 4, 2, 6, 4, 6, 1, 3, 1 };
            dictionary.Add("Castellar", buffer158);
            byte[] buffer159 = new byte[] { 2, 4, 6, 3, 5, 5, 5, 3, 3, 4 };
            dictionary.Add("Calisto MT", buffer159);
            byte[] buffer160 = new byte[] { 3, 7, 4, 2, 5, 3, 2, 3, 2, 3 };
            dictionary.Add("Bradley Hand ITC", buffer160);
            byte[] buffer161 = new byte[] { 2, 5, 6, 4, 5, 5, 5, 2, 2, 4 };
            dictionary.Add("Bookman Old Style", buffer161);
            byte[] buffer162 = new byte[] { 2, 4, 6, 2, 5, 3, 5, 3, 3, 4 };
            dictionary.Add("Book Antiqua", buffer162);
            byte[] buffer163 = new byte[] { 2, 7, 6, 3, 8, 6, 6, 2, 2, 3 };
            dictionary.Add("Bodoni MT", buffer163);
            byte[] buffer164 = new byte[] { 4, 2, 5, 5, 5, 0x10, 7, 2, 13, 2 };
            dictionary.Add("Blackadder ITC", buffer164);
            byte[] buffer165 = new byte[] { 2, 15, 7, 4, 3, 5, 4, 3, 2, 4 };
            dictionary.Add("Arial Rounded MT", buffer165);
            byte[] buffer166 = new byte[] { 2, 11, 5, 3, 2, 2, 2, 2, 2, 4 };
            dictionary.Add("Agency FB", buffer166);
            byte[] buffer167 = new byte[] { 5, 1, 1, 0, 1, 0, 0, 0, 0, 0 };
            dictionary.Add("MS Outlook", buffer167);
            byte[] buffer168 = new byte[] { 5, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            dictionary.Add("Bookshelf Symbol 7", buffer168);
            byte[] buffer169 = new byte[] { 2, 11, 6, 4, 3, 5, 4, 4, 2, 4 };
            dictionary.Add("MS Reference Sans Serif", buffer169);
            byte[] buffer170 = new byte[10];
            buffer170[0] = 5;
            buffer170[2] = 5;
            dictionary.Add("MS Reference Specialty", buffer170);
            byte[] buffer171 = new byte[] { 2, 11, 5, 2, 2, 2, 4, 2, 3, 3 };
            dictionary.Add("FuturaT", buffer171);
            byte[] buffer172 = new byte[] { 2, 11, 6, 2, 2, 2, 4, 2, 3, 3 };
            dictionary.Add("FuturaTMed", buffer172);
            byte[] buffer173 = new byte[] { 5, 5, 1, 2, 1, 2, 5, 2, 2, 2 };
            dictionary.Add("MT Extra", buffer173);
            byte[] buffer174 = new byte[] { 2, 2, 5, 3, 6, 8, 5, 2, 2, 4 };
            dictionary.Add("Symbola", buffer174);
            fontPanoseTable = dictionary;
            panoseWeights = new int[] { 100, 10, 1, 5, 1, 1, 1, 1, 1, 1 };
        }

        protected DXFontCollection()
        {
        }

        public abstract void Dispose();
        public virtual IDXFontFamily FindFamily(string familyName)
        {
            string str;
            IDXFontFamily family = this.FindFamilyCore(familyName);
            if ((family == null) && fontMapTable.TryGetValue(familyName, out str))
            {
                family = this.FindFamilyCore(str);
            }
            if ((family == null) && (this.DefaultFontFamily != null))
            {
                family = this.FindFamilyCore(this.DefaultFontFamily);
            }
            return ((family != null) ? family : this.Families.FirstOrDefault<IDXFontFamily>());
        }

        private IDXFontFamily FindFamilyCore(string familyName) => 
            this.Families.FirstOrDefault<IDXFontFamily>(family => family.Name == familyName);

        public DXFont FindFirstMatchingFont(string familyName, float sizeInPoints, DXFontWeight weight = 400, DXFontStyle style = 0, DXFontStretch stretch = 5)
        {
            DXFontFace fontFace = this.GetFontFace(familyName, weight, stretch, style);
            return ((fontFace == null) ? null : new DXFont(fontFace, sizeInPoints));
        }

        public DXFontFace FindFirstMatchingFontFace(DXFontDescriptor descriptor) => 
            this.GetFontFace(descriptor.FamilyName, descriptor.Weight, descriptor.Stretch, descriptor.Style);

        private string FindMostMatchingFont(byte[] panose, DXFontWeight weight, DXFontStretch stretch, DXFontStyle style)
        {
            string familyName;
            int num = 0x7fffffff;
            string familyName = null;
            using (IEnumerator<IDXFontFamily> enumerator = this.Families.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DXFontFace face = enumerator.Current.GetFirstMatchingFontFace(weight, stretch, style);
                        if (face == null)
                        {
                            continue;
                        }
                        byte[] second = face.Panose;
                        if ((second == null) || ((panose.Length != second.Length) || (second[0] != 2)))
                        {
                            continue;
                        }
                        if (!panose.SequenceEqual<byte>(second))
                        {
                            int num2 = 0x7fffffff;
                            int index = 1;
                            while (true)
                            {
                                if (index >= panose.Length)
                                {
                                    if (num2 < num)
                                    {
                                        num = num2;
                                        familyName = face.FamilyName;
                                    }
                                    break;
                                }
                                num2 += Math.Abs((int) (panose[index] - second[index])) * panoseWeights[index];
                                index++;
                            }
                            continue;
                        }
                        familyName = face.FamilyName;
                    }
                    else
                    {
                        return familyName;
                    }
                    break;
                }
            }
            return familyName;
        }

        private DXFontFace GetFontFace(string familyName, DXFontWeight weight, DXFontStretch stretch, DXFontStyle style)
        {
            string str;
            byte[] buffer;
            IDXFontFamily family = this.FindFamilyCore(familyName);
            if ((family == null) && fontMapTable.TryGetValue(familyName, out str))
            {
                family = this.FindFamilyCore(str);
            }
            if ((family == null) && fontPanoseTable.TryGetValue(familyName, out buffer))
            {
                string str2 = this.FindMostMatchingFont(buffer, weight, stretch, style);
                family = this.FindFamilyCore(str2);
            }
            if ((family == null) && (this.DefaultFontFamily != null))
            {
                family = this.FindFamilyCore(this.DefaultFontFamily);
            }
            return this.Families.FirstOrDefault<IDXFontFamily>().GetFirstMatchingFontFace(weight, stretch, style);
        }

        public abstract IReadOnlyList<IDXFontFamily> Families { get; }

        public string DefaultFontFamily { get; set; }
    }
}

