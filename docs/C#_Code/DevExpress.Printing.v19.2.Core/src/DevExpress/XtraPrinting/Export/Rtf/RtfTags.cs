namespace DevExpress.XtraPrinting.Export.Rtf
{
    using System;

    public static class RtfTags
    {
        public static bool IsRtfContent(string rtf) => 
            rtf.Contains(RtfHeader) && (rtf.IndexOf(RtfHeader) == 0);

        public static string WrapTextInRtf(string text) => 
            @"{\rtf1 " + text + "}";

        public static string RtfHeader =>
            @"{\rtf";

        public static string ParagraphEnd =>
            @"\par";

        public static string ParagraphDefault =>
            @"\pard";

        public static string RightToLeftParagraph =>
            @"\rtlpar";

        public static string SectionStart =>
            "\\sect \r\n";

        public static string SectionDefault =>
            @"\sectd";

        public static string RightToLeftSection =>
            @"\rtlsect";

        public static string SectionNoBreak =>
            @"\sbknone";

        public static string SpaceBetweenColumns =>
            @"\colsx30";

        public static string ColumnCount =>
            @"\cols{0}";

        public static string PlainText =>
            @"\plain";

        public static string RightIndent =>
            @"\ri{0}";

        public static string LeftIndent =>
            @"\li{0}";

        public static string MirrorIndents =>
            @"\indmirror";

        public static string SpaceBefore =>
            @"\sb{0}";

        public static string SpaceAfter =>
            @"\sa{0}";

        public static string DefaultFrame =>
            @"\frmtxlrtb";

        public static string TopBottomFrame =>
            @"\frmtxtbrl";

        public static string BottomTopFrame =>
            @"\frmtxbtlr";

        public static string RelativeFrameToPage =>
            @"\pvpg";

        public static string ObjectBounds =>
            @"\posx{0}\posy{1}\absw{2}\absh{3}";

        public static string ObjectPosition =>
            @"\posx{0}\posy{1}";

        public static string ObjectSize =>
            @"\absw{0}\absh{1}";

        public static string TextAround =>
            @"\nowrap";

        public static string TextUnderneath =>
            @"\overlay";

        public static string StartWithCodePage =>
            @"{{\rtf1\ansicpg{0}";

        public static string ColorTable =>
            @"\colortbl";

        public static string NumberingListTable =>
            @"\*\listtable";

        public static string ListOverrideTable =>
            @"\*\listoverridetable";

        public static string FontTable =>
            @"\fonttbl";

        public static string DefineFont =>
            @"{{\f{0} {1};}}";

        public static string RGB =>
            @"\red{0}\green{1}\blue{2};";

        public static string TopAlignedInCell =>
            @"\clvertalt";

        public static string VerticalCenteredInCell =>
            @"\clvertalc";

        public static string BottomAlignedInCell =>
            @"\clvertalb";

        public static string Centered =>
            @"\qc";

        public static string Justified =>
            @"\qj";

        public static string LeftAligned =>
            @"\ql";

        public static string RightAligned =>
            @"\qr";

        public static string CellTextFlowBTLR =>
            @"\cltxbtlr";

        public static string CellTextFlowTBRL =>
            @"\cltxtbrl";

        public static string UnicodeCharacter =>
            @"\u{0}";

        public static string WindowsMetafile =>
            @"\wmetafile{0}";

        public static string EnhancedMetafile =>
            @"\emfblip";

        public static string PngPictType =>
            @"\pngblip";

        public static string Picture =>
            @"\pict{0}";

        public static string PictureWidth =>
            @"\picw{0}";

        public static string PictureHeight =>
            @"\pich{0}";

        public static string PictureDesiredWidth =>
            @"\picwgoal{0}";

        public static string PictureDesiredHeight =>
            @"\pichgoal{0}";

        public static string PictureCropTop =>
            @"\piccropt{0}";

        public static string PictureCropBottom =>
            @"\piccropb{0}";

        public static string PictureCropLeft =>
            @"\piccropl{0}";

        public static string PictureCropRight =>
            @"\piccropr{0}";

        public static string HorizontalScaling =>
            @"\picscalex{0}";

        public static string VerticalScaling =>
            @"\picscaley{0}";

        public static string BackgroundPatternBackgroundColor =>
            @"\clcbpat{0}";

        public static string BackgroundPatternColor =>
            @"\cbpat{0}";

        public static string Color =>
            @"\cf{0}";

        public static string SingleBorderWidth =>
            @"\brdrs";

        public static string DoubleBorderWidth =>
            @"\brdrth";

        public static string DotBorderStyle =>
            @"\brdrdot";

        public static string DashBorderStyle =>
            @"\brdrdash";

        public static string DashDotBorderStyle =>
            @"\brdrdashd";

        public static string DashDotDotBorderStyle =>
            @"\brdrdashdd";

        public static string DoubleBorderStyle =>
            @"\brdrdb";

        public static string BorderSpace =>
            @"\brsp{0}";

        public static string TopBorder =>
            @"\brdrt";

        public static string BottomBorder =>
            @"\brdrb";

        public static string LeftBorder =>
            @"\brdrl";

        public static string RightBorder =>
            @"\brdrr";

        public static string BorderColor =>
            @"\brdrcf{0}";

        public static string BorderWidth =>
            @"\brdrw{0}";

        public static string LeftCellBorder =>
            @"\clbrdrl";

        public static string RightCellBorder =>
            @"\clbrdrr";

        public static string TopCellBorder =>
            @"\clbrdrt";

        public static string BottomCellBorder =>
            @"\clbrdrb";

        public static string SuggestToTable =>
            @"\intbl";

        public static string LeftToRightRow =>
            @"\ltrrow";

        public static string RightToLeftRow =>
            @"\rtlrow";

        public static string EndOfRow =>
            @"\intbl\row";

        public static string Hyperlink =>
            "{{\\field{{\\*\\fldinst{{HYPERLINK \"{0}\" }}}}{{\\fldrslt{{{1}}}}}}}";

        public static string LocalHyperlink =>
            "{{\\field{{\\*\\fldinst{{HYPERLINK \\\\l \"{0}\" }}}}{{\\fldrslt{{{1}}}}}}}";

        public static string Bookmark =>
            @"{{\*\bkmkstart {0}}}{{\*\bkmkend {0}}}";

        public static string EndOfLine =>
            @"\line ";

        public static string FirstMergedCell =>
            @"\clmgf ";

        public static string MergedCell =>
            @"\clmrg ";

        public static string FirstVerticalMergedCell =>
            @"\clvmgf ";

        public static string VerticalMergedCell =>
            @"\clvmrg ";

        public static string EmptyCell =>
            @"{\cell}";

        public static string EmptyCellContent =>
            EmptyCell + NewLine;

        public static string EndOfCell =>
            @"\cell";

        public static string ExactlyRowHeight =>
            @"\trrh-{0}";

        public static string AtLeastRowHeight =>
            @"\trrh{0}";

        public static string RowHeight =>
            @"\trrh-{1}";

        public static string DefaultRow =>
            @"\trowd";

        public static string RowAutofit =>
            @"\trautofit1";

        public static string CellRight =>
            @"\cellx";

        public static string FontWithSize =>
            @"\f{0}\fs{1}";

        public static string FontSize =>
            @"\fs{0}";

        public static string Bold =>
            @"\b";

        public static string Italic =>
            @"\i";

        public static string Underline =>
            @"\ul";

        public static string Strikeout =>
            @"\strike";

        public static string PageSize =>
            @"\paperw{0}\paperh{1}";

        public static string PageLandscape =>
            @"\lndscpsxn";

        public static string Margins =>
            @"\margl{0}\margr{1}\margt{2}\margb{3}";

        public static string NoneWrapShape =>
            @"\shpwr3";

        public static string Shape =>
            @"{\shp";

        public static string ShapeInstall =>
            @"{\*\shpinst";

        public static string TextShape =>
            @"{\sp{\sn shapeType}{\sv 136}}";

        public static string ImageShape =>
            @"{\sp{\sn shapeType}{\sv 75}}";

        public static string ShapeRotation =>
            @"{{\sp{{\sn rotation}}{{\sv {0}}}}}";

        public static string ShapeBounds =>
            @"\shpleft{0}\shptop{1}\shpright{2}\shpbottom{3}";

        public static string UnicodeShapeString =>
            @"{{\sp{{\sn gtextUNICODE}}{{\sv {0}}}}}";

        public static string ShapeFont =>
            @"{{\sp{{\sn gtextFont}}{{\sv {0}}}}}";

        public static string ShapeColor =>
            @"{{\sp{{\sn fillColor}}{{\sv {0}}}}}";

        public static string ShapeOpacity =>
            @"{{\sp{{\sn fillOpacity}}{{\sv {0}}}}}";

        public static string NoShapeBorderLine =>
            @"{\sp{\sn fLine}{\sv 0}}";

        public static string ShapeBehind =>
            @"{{\sp{{\sn fBehindDocument}}{{\sv {0}}}}}";

        public static string ShapeBoldText =>
            @"{{\sp{{\sn gtextFBold}}{{\sv {0}}}}}";

        public static string ShapeItalicText =>
            @"{{\sp{{\sn gtextFItalic}}{{\sv {0}}}}}";

        public static string ShapePicture =>
            @"{{\sp{{\sn pib}}{{\sv {0}}}}}";

        public static string ShowBackgroundShape =>
            @"{\viewbksp1";

        public static string DocumentBackground =>
            @"{\*\background";

        public static string NewLine =>
            "\r\n";

        public static string Header =>
            @"\header";

        public static string HeaderY =>
            @"\headery{0}";

        public static string Footer =>
            @"\footer";

        public static string FooterY =>
            @"\footery{0}";

        public static string SpecialFirstPageHeaderFooter =>
            @"\titlepg";

        public static string PageBreakBefore =>
            @"\pagebb";

        public static string FieldInstructionTemplate =>
            @"{{\field{{\*\fldinst{{ {0} }}}}{{\fldrslt {1}}}}}";

        public static string FieldInstructionPageNumber =>
            "PAGE";

        public static string FieldInstructionPageCount =>
            "NUMPAGES";

        public static string FieldInstructionUserName =>
            "USERNAME";

        public static string FieldInstructionDate =>
            "DATE";

        public static string NoGrowAutoFit =>
            @"\nogrowautofit";

        public static string DontUseLineBreakForAsianText =>
            @"\lnbrkrule";

        public static string DefaultDocumentCharacterProperties =>
            @"{\*\defchp }";

        public static string DefaultDocumentParagraphProperties =>
            @"{\*\defpap }";

        public static string DXVersionInfo =>
            @"{{\info{{\*\userprops {{\propname DXVersion}}\proptype30{{\staticval {0}}}}}}}";

        public static string WhitespaceParagraph =>
            @"\pard\plain\fs1 \par";

        public static string ColumnBreakParagraph =>
            @"\pard\fs1{\column}\par";
    }
}

