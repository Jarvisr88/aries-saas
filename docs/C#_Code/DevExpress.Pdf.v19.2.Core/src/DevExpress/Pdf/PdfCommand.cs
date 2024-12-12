namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfCommand
    {
        protected PdfCommand()
        {
        }

        internal static PdfCommand Create(PdfResources resources, string name, PdfStack operands, bool supportType3FontCommands, bool shouldIgnoreUnknownCommands)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
            if (num > 0x6429a72d)
            {
                if (num > 0xe10c2473)
                {
                    if (num > 0xec0c35c4)
                    {
                        if (num > 0xf20c3f36)
                        {
                            if (num > 0xf40c425c)
                            {
                                if (num == 0xf60c4582)
                                {
                                    if (name == "s")
                                    {
                                        return PdfCloseAndStrokePathCommand.Instance;
                                    }
                                }
                                else if (num == 0xf70c4715)
                                {
                                    if (name == "r")
                                    {
                                        goto TR_0066;
                                    }
                                }
                                else if ((num == 0xfc0c4ef4) && (name == "y"))
                                {
                                    return new PdfAppendBezierCurveWithNextControlPointCommand(operands);
                                }
                            }
                            else if (num != 0xf30c40c9)
                            {
                                if ((num == 0xf40c425c) && (name == "q"))
                                {
                                    return PdfSaveGraphicsStateCommand.Instance;
                                }
                            }
                            else if (name == "v")
                            {
                                return new PdfAppendBezierCurveWithPreviousControlPointCommand(operands);
                            }
                        }
                        else if (num <= 0xee0c38ea)
                        {
                            if (num != 0xed0c3757)
                            {
                                if ((num == 0xee0c38ea) && (name == "k"))
                                {
                                    return new PdfSetCMYKColorSpaceForNonStrokingOperationsCommand(operands);
                                }
                            }
                            else if (name == "h")
                            {
                                return PdfClosePathCommand.Instance;
                            }
                        }
                        else if (num != 0xef0c3a7d)
                        {
                            if ((num == 0xf20c3f36) && (name == "w"))
                            {
                                return new PdfSetLineWidthCommand(operands);
                            }
                        }
                        else if (name == "j")
                        {
                            return new PdfSetLineJoinStyleCommand(operands);
                        }
                    }
                    else if (num > 0xe70c2de5)
                    {
                        if (num <= 0xe90c310b)
                        {
                            if (num != 0xe80c2f78)
                            {
                                if ((num == 0xe90c310b) && (name == "l"))
                                {
                                    return new PdfAppendLineSegmentCommand(operands);
                                }
                            }
                            else if (name == "m")
                            {
                                return new PdfBeginPathCommand(operands);
                            }
                        }
                        else if (num != 0xeb0c3431)
                        {
                            if ((num == 0xec0c35c4) && (name == "i"))
                            {
                                return new PdfSetFlatnessToleranceCommand(operands);
                            }
                        }
                        else if (name == "n")
                        {
                            return PdfEndPathWithoutFillingAndStrokingCommand.Instance;
                        }
                    }
                    else if (num > 0xe30c2799)
                    {
                        if (num != 0xe60c2c52)
                        {
                            if ((num == 0xe70c2de5) && (name == "b"))
                            {
                                return PdfCloseFillAndStrokePathUsingNonzeroWindingNumberRuleCommand.Instance;
                            }
                        }
                        else if (name == "c")
                        {
                            return new PdfAppendBezierCurveCommand(operands);
                        }
                    }
                    else if (num == 0xe20c2606)
                    {
                        if (name == "g")
                        {
                            return new PdfSetGrayColorSpaceForNonStrokingOperationsCommand(operands);
                        }
                    }
                    else if ((num == 0xe30c2799) && (name == "f"))
                    {
                        goto TR_008C;
                    }
                }
                else if (num > 0xc30bf539)
                {
                    if (num <= 0xcf0c081d)
                    {
                        if (num <= 0xc80bfd18)
                        {
                            if (num != 0xc70bfb85)
                            {
                                if ((num == 0xc80bfd18) && (name == "M"))
                                {
                                    return new PdfSetMiterLimitCommand(operands);
                                }
                            }
                            else if (name == "B")
                            {
                                return PdfFillAndStrokePathUsingNonzeroWindingNumberRuleCommand.Instance;
                            }
                        }
                        else if (num != 0xce0c068a)
                        {
                            if ((num == 0xcf0c081d) && (name == "J"))
                            {
                                return new PdfSetLineCapStyleCommand(operands);
                            }
                        }
                        else if (name == "K")
                        {
                            return new PdfSetCMYKColorSpaceForStrokingOperationsCommand(operands);
                        }
                    }
                    else if (num <= 0xd40c0ffc)
                    {
                        if (num != 0xd20c0cd6)
                        {
                            if ((num == 0xd40c0ffc) && (name == "Q"))
                            {
                                return PdfRestoreGraphicsStateCommand.Instance;
                            }
                        }
                        else if (name == "W")
                        {
                            return PdfModifyClippingPathUsingNonzeroWindingNumberRuleCommand.Instance;
                        }
                    }
                    else if (num == 0xd60c1322)
                    {
                        if (name == "S")
                        {
                            return PdfStrokePathCommand.Instance;
                        }
                    }
                    else if (num != 0xe0182567)
                    {
                        if ((num == 0xe10c2473) && (name == "d"))
                        {
                            return new PdfSetLineStyleCommand(operands);
                        }
                    }
                    else if (name == "scn")
                    {
                        return new PdfSetColorAdvancedForNonStrokingOperationsCommand(resources, operands);
                    }
                }
                else if (num > 0x7ef5f64b)
                {
                    if (num > 0xa4f870b4)
                    {
                        if (num == 0xc20bf3a6)
                        {
                            if (name == "G")
                            {
                                return new PdfSetGrayColorSpaceForStrokingOperationsCommand(operands);
                            }
                        }
                        else if ((num == 0xc30bf539) && (name == "F"))
                        {
                            goto TR_008C;
                        }
                    }
                    else if (num != 0x872c1cdd)
                    {
                        if ((num == 0xa4f870b4) && (name == "W*"))
                        {
                            return PdfModifyClippingPathUsingEvenOddRuleCommand.Instance;
                        }
                    }
                    else if (name == "b*")
                    {
                        return PdfCloseFillAndStrokePathUsingEvenOddRuleCommand.Instance;
                    }
                }
                else if (num <= 0x65d9f873)
                {
                    if (num != 0x64548834)
                    {
                        if ((num == 0x65d9f873) && (name == "CS"))
                        {
                            return new PdfSetColorSpaceForStrokingOperationsCommand(resources, operands);
                        }
                    }
                    else if (name == "ri")
                    {
                        return new PdfSetRenderingIntentCommand(operands);
                    }
                }
                else if (num != 0x7a04f8f6)
                {
                    if ((num == 0x7ef5f64b) && (name == "T*"))
                    {
                        return PdfStartTextLineCommand.Instance;
                    }
                }
                else if (name == "RG")
                {
                    return new PdfSetRGBColorSpaceForStrokingOperationsCommand(operands);
                }
                goto TR_0007;
            }
            else
            {
                if (num > 0x3af58b3f)
                {
                    if (num > 0x46f59e23)
                    {
                        if (num > 0x560281b3)
                        {
                            if (num > 0x5a547876)
                            {
                                if (num == 0x61ceb934)
                                {
                                    if (name == "Do")
                                    {
                                        return new PdfPaintXObjectCommand(resources, operands);
                                    }
                                }
                                else if (num != 0x62f2a147)
                                {
                                    if ((num == 0x6429a72d) && (name == "cm"))
                                    {
                                        return new PdfModifyTransformationMatrixCommand(new PdfTransformationMatrix(operands));
                                    }
                                }
                                else if (name == "SCN")
                                {
                                    return new PdfSetColorAdvancedForStrokingOperationsCommand(resources, operands);
                                }
                            }
                            else if (num == 0x58547550)
                            {
                                if (name == "re")
                                {
                                    return new PdfAppendRectangleCommand(operands);
                                }
                            }
                            else if ((num == 0x5a547876) && (name == "rg"))
                            {
                                goto TR_0066;
                            }
                        }
                        else if (num > 0x4e208a2f)
                        {
                            if (num == 0x4ef5aabb)
                            {
                                if (name == "Tz")
                                {
                                    goto TR_0045;
                                }
                            }
                            else if ((num == 0x560281b3) && (name == "SC"))
                            {
                                return new PdfSetColorForStrokingOperationsCommand(operands);
                            }
                        }
                        else if (num != 0x49f5a2dc)
                        {
                            if ((num == 0x4e208a2f) && (name == "gs"))
                            {
                                return new PdfSetGraphicsStateParametersCommand(resources, operands);
                            }
                        }
                        else if (name == "Tw")
                        {
                            return new PdfSetWordSpacingCommand(operands);
                        }
                    }
                    else if (num > 0x40f594b1)
                    {
                        if (num <= 0x45f59c90)
                        {
                            if (num != 0x43f5996a)
                            {
                                if ((num == 0x45f59c90) && (name == "Ts"))
                                {
                                    return new PdfSetTextRiseCommand(operands);
                                }
                            }
                            else if (name == "Tm")
                            {
                                return new PdfSetTextMatrixCommand(new PdfTransformationMatrix(operands));
                            }
                        }
                        else if (num != 0x462977f3)
                        {
                            if ((num == 0x46f59e23) && (name == "Tr"))
                            {
                                return new PdfSetTextRenderingModeCommand(operands);
                            }
                        }
                        else if (name == "cs")
                        {
                            return new PdfSetColorSpaceForNonStrokingOperationsCommand(resources, operands);
                        }
                    }
                    else if (num > 0x3ef5918b)
                    {
                        if (num == 0x3f520f5e)
                        {
                            if (name == "sh")
                            {
                                return new PdfPaintShadingPatternCommand(resources, operands);
                            }
                        }
                        else if ((num == 0x40f594b1) && (name == "Th"))
                        {
                            goto TR_0045;
                        }
                    }
                    else if (num != 0x3cf58e65)
                    {
                        if ((num == 0x3ef5918b) && (name == "Tj"))
                        {
                            return new PdfShowTextCommand(operands);
                        }
                    }
                    else if (name == "Td")
                    {
                        return new PdfStartTextLineWithOffsetsCommand(operands);
                    }
                }
                else if (num <= 0x24f5689d)
                {
                    if (num <= 0x1cf55c05)
                    {
                        if (num <= 0x6dd347d)
                        {
                            if (num != 0x241d61c)
                            {
                                if ((num == 0x6dd347d) && (name == "B*"))
                                {
                                    return PdfFillAndStrokePathUsingEvenOddRuleCommand.Instance;
                                }
                            }
                            else if (name == "EMC")
                            {
                                return new PdfUnknownCommand(name, operands);
                            }
                        }
                        else if (num != 0x114b38f2)
                        {
                            if ((num == 0x1cf55c05) && (name == "TD"))
                            {
                                return new PdfStartTextLineWithOffsetsAndLeadingCommand(operands);
                            }
                        }
                        else if (name == "endstream")
                        {
                            return null;
                        }
                    }
                    else if (num <= 0x1f227ec9)
                    {
                        if (num != 0x1ef55f2b)
                        {
                            if ((num == 0x1f227ec9) && (name == "f*"))
                            {
                                return PdfFillPathUsingEvenOddRuleCommand.Instance;
                            }
                        }
                        else if (name == "TJ")
                        {
                            return new PdfShowTextWithGlyphPositioningCommand(operands);
                        }
                    }
                    else if (num != 0x220c8ac6)
                    {
                        if ((num == 0x24f5689d) && (name == "TL"))
                        {
                            return new PdfSetTextLeadingCommand(operands);
                        }
                    }
                    else if (name == "'")
                    {
                        return new PdfShowTextOnNextLineCommand(operands);
                    }
                }
                else if (num <= 0x28dd6a03)
                {
                    if (num <= 0x270c92a5)
                    {
                        if (num != 0x26cc1dbc)
                        {
                            if ((num == 0x270c92a5) && (name == "\""))
                            {
                                return new PdfShowTextOnNextLineWithSpacingCommand(operands);
                            }
                        }
                        else if (name == "ET")
                        {
                            return PdfEndTextCommand.Instance;
                        }
                    }
                    else if (num != 0x28ce5f79)
                    {
                        if ((num == 0x28dd6a03) && (name == "BT"))
                        {
                            return PdfBeginTextCommand.Instance;
                        }
                    }
                    else if (name == "DP")
                    {
                        return new PdfDesignateMarkerContentPointWithParametersCommand(resources, operands);
                    }
                }
                else if (num <= 0x35f58360)
                {
                    if (num != 0x32dfb858)
                    {
                        if ((num == 0x35f58360) && (name == "Tc"))
                        {
                            return new PdfSetCharacterSpacingCommand(operands);
                        }
                    }
                    else if (name == "MP")
                    {
                        return new PdfDesignateMarkedContentPointCommand(operands);
                    }
                }
                else if (num != 0x36520133)
                {
                    if ((num == 0x3af58b3f) && (name == "Tf"))
                    {
                        return new PdfSetTextFontCommand(resources, operands);
                    }
                }
                else if (name == "sc")
                {
                    return new PdfSetColorForNonStrokingOperationsCommand(operands);
                }
                goto TR_0007;
            }
            goto TR_008C;
        TR_0007:
            if (supportType3FontCommands)
            {
                if (name == "d0")
                {
                    return new PdfSetCharWidthCommand(operands);
                }
                if (name == "d1")
                {
                    return new PdfSetCacheDeviceCommand(operands);
                }
            }
            if (!shouldIgnoreUnknownCommands || string.IsNullOrEmpty(name))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return new PdfUnknownCommand(name, operands);
        TR_0045:
            return new PdfSetTextHorizontalScalingCommand(operands);
        TR_0066:
            return new PdfSetRGBColorSpaceForNonStrokingOperationsCommand(operands);
        TR_008C:
            return PdfFillPathUsingNonzeroWindingNumberRuleCommand.Instance;
        }

        protected internal virtual void Execute(PdfCommandInterpreter interpreter)
        {
        }

        internal static bool IsKnownCommand(string name)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
            return ((num > 0x65d9f873) ? ((num > 0xe10c2473) ? ((num > 0xec0c35c4) ? ((num > 0xf20c3f36) ? ((num > 0xf40c425c) ? ((num == 0xf60c4582) ? (name == "s") : ((num == 0xf70c4715) ? (name == "r") : ((num == 0xfc0c4ef4) && (name == "y")))) : ((num == 0xf30c40c9) ? (name == "v") : ((num == 0xf40c425c) && (name == "q")))) : ((num > 0xee0c38ea) ? ((num == 0xef0c3a7d) ? (name == "j") : ((num == 0xf20c3f36) && (name == "w"))) : ((num == 0xed0c3757) ? (name == "h") : ((num == 0xee0c38ea) && (name == "k"))))) : ((num > 0xe70c2de5) ? ((num > 0xe90c310b) ? ((num == 0xeb0c3431) ? (name == "n") : ((num == 0xec0c35c4) && (name == "i"))) : ((num == 0xe80c2f78) ? (name == "m") : ((num == 0xe90c310b) && (name == "l")))) : ((num > 0xe30c2799) ? ((num == 0xe60c2c52) ? (name == "c") : ((num == 0xe70c2de5) && (name == "b"))) : ((num == 0xe20c2606) ? (name == "g") : ((num == 0xe30c2799) && (name == "f")))))) : ((num > 0xc30bf539) ? ((num > 0xcf0c081d) ? ((num > 0xd40c0ffc) ? ((num == 0xd60c1322) ? (name == "S") : ((num == 0xe0182567) ? (name == "scn") : ((num == 0xe10c2473) && (name == "d")))) : ((num == 0xd20c0cd6) ? (name == "W") : ((num == 0xd40c0ffc) && (name == "Q")))) : ((num > 0xc80bfd18) ? ((num == 0xce0c068a) ? (name == "K") : ((num == 0xcf0c081d) && (name == "J"))) : ((num == 0xc70bfb85) ? (name == "B") : ((num == 0xc80bfd18) && (name == "M"))))) : ((num > 0x881d13e6) ? ((num > 0xa4f870b4) ? ((num == 0xc20bf3a6) ? (name == "G") : ((num == 0xc30bf539) && (name == "F"))) : ((num == 0x891d1579) ? (name == "d0") : ((num == 0xa4f870b4) && (name == "W*")))) : ((num > 0x7ef5f64b) ? ((num == 0x872c1cdd) ? (name == "b*") : ((num == 0x881d13e6) && (name == "d1"))) : ((num == 0x7a04f8f6) ? (name == "RG") : ((num == 0x7ef5f64b) && (name == "T*"))))))) : ((num > 0x3ef5918b) ? ((num > 0x4e208a2f) ? ((num > 0x5a547876) ? ((num > 0x62f2a147) ? ((num == 0x6429a72d) ? (name == "cm") : ((num == 0x64548834) ? (name == "ri") : ((num == 0x65d9f873) && (name == "CS")))) : ((num == 0x61ceb934) ? (name == "Do") : ((num == 0x62f2a147) && (name == "SCN")))) : ((num > 0x560281b3) ? ((num == 0x58547550) ? (name == "re") : ((num == 0x5a547876) && (name == "rg"))) : ((num == 0x4ef5aabb) ? (name == "Tz") : ((num == 0x560281b3) && (name == "SC"))))) : ((num > 0x45f59c90) ? ((num > 0x46f59e23) ? ((num == 0x49f5a2dc) ? (name == "Tw") : ((num == 0x4e208a2f) && (name == "gs"))) : ((num == 0x462977f3) ? (name == "cs") : ((num == 0x46f59e23) && (name == "Tr")))) : ((num > 0x40f594b1) ? ((num == 0x43f5996a) ? (name == "Tm") : ((num == 0x45f59c90) && (name == "Ts"))) : ((num == 0x3f520f5e) ? (name == "sh") : ((num == 0x40f594b1) && (name == "Th")))))) : ((num > 0x26cc1dbc) ? ((num > 0x32dfb858) ? ((num > 0x36520133) ? ((num == 0x3af58b3f) ? (name == "Tf") : ((num == 0x3cf58e65) ? (name == "Td") : ((num == 0x3ef5918b) && (name == "Tj")))) : ((num == 0x35f58360) ? (name == "Tc") : ((num == 0x36520133) && (name == "sc")))) : ((num > 0x28ce5f79) ? ((num == 0x28dd6a03) ? (name == "BT") : ((num == 0x32dfb858) && (name == "MP"))) : ((num == 0x270c92a5) ? (name == "\"") : ((num == 0x28ce5f79) && (name == "DP"))))) : ((num > 0x1ef55f2b) ? ((num > 0x220c8ac6) ? ((num == 0x24f5689d) ? (name == "TL") : ((num == 0x26cc1dbc) && (name == "ET"))) : ((num == 0x1f227ec9) ? (name == "f*") : ((num == 0x220c8ac6) && (name == "'")))) : ((num > 0x6dd347d) ? ((num == 0x1cf55c05) ? (name == "TD") : ((num == 0x1ef55f2b) && (name == "TJ"))) : ((num == 0x241d61c) ? (name == "EMC") : ((num == 0x6dd347d) && (name == "B*"))))))));
        }

        protected internal abstract void Write(PdfResources resources, PdfDocumentStream writer);
    }
}

