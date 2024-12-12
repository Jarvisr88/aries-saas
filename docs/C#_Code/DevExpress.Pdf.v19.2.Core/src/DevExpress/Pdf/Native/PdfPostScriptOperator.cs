namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfPostScriptOperator
    {
        protected PdfPostScriptOperator()
        {
        }

        public abstract void Execute(PdfPostScriptInterpreter interpreter);
        public static object Parse(object obj)
        {
            PdfPostScriptOperator @operator = obj as PdfPostScriptOperator;
            if (@operator != null)
            {
                return @operator;
            }
            PdfComment comment = obj as PdfComment;
            if (comment != null)
            {
                return new PdfPostScriptComment(comment.Text);
            }
            string s = obj as string;
            if (s == null)
            {
                return obj;
            }
            uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
            if (num > 0x697299ee)
            {
                if (num > 0xce0beff7)
                {
                    if (num > 0xe562ab48)
                    {
                        if (num > 0xf14f6a1f)
                        {
                            if (num > 0xf6eef3eb)
                            {
                                if (num == 0xf90c4a3b)
                                {
                                    if (s == "|")
                                    {
                                        goto TR_0017;
                                    }
                                }
                                else if ((num == 0xfdb81dd9) && (s == "eexec"))
                                {
                                    return new PdfPostScriptEexecOperator();
                                }
                            }
                            else if (num != 0xf665d22c)
                            {
                                if ((num == 0xf6eef3eb) && (s == "findfont"))
                                {
                                    return new PdfPostScriptFindFontOperator();
                                }
                            }
                            else if (s == "systemdict")
                            {
                                return new PdfPostScriptSystemDictOperator();
                            }
                        }
                        else if (num == 0xe562ea44)
                        {
                            if (s == "copy")
                            {
                                return new PdfPostScriptCopyOperator();
                            }
                        }
                        else if (num != 0xeb84ed81)
                        {
                            if ((num == 0xf14f6a1f) && (s == "cvi"))
                            {
                                return new PdfPostScriptCviOperator();
                            }
                        }
                        else if (s == "mul")
                        {
                            return new PdfPostScriptMulOperator();
                        }
                    }
                    else if (num <= 0xdc4e3915)
                    {
                        if (num == 0xd330f226)
                        {
                            if (s == "dup")
                            {
                                return new PdfPostScriptDupOperator();
                            }
                        }
                        else if (num != 0xd8379f9c)
                        {
                            if ((num == 0xdc4e3915) && (s == "sub"))
                            {
                                return new PdfPostScriptSubOperator();
                            }
                        }
                        else if (s == "currentfile")
                        {
                            return new PdfPostScriptCurrentFileOperator();
                        }
                    }
                    else if (num <= 0xdf0b4a5c)
                    {
                        if (num != 0xde06cb9b)
                        {
                            if ((num == 0xdf0b4a5c) && (s == "exec"))
                            {
                                return new PdfPostScriptExecOperator();
                            }
                        }
                        else if (s == "exch")
                        {
                            return new PdfPostScriptExchOperator();
                        }
                    }
                    else if (num != 0xe33c3b38)
                    {
                        if ((num == 0xe562ab48) && (s == "div"))
                        {
                            return new PdfPostScriptDivOperator();
                        }
                    }
                    else if (s == "readstring")
                    {
                        return new PdfPostScriptReadStringOperator();
                    }
                }
                else if (num > 0x8a58ad26)
                {
                    if (num <= 0xc02b6f85)
                    {
                        if (num == 0xa476e10a)
                        {
                            if (s == "executeonly")
                            {
                                return new PdfPostScriptExecuteOnlyOperator();
                            }
                        }
                        else if (num != 0xacf38390)
                        {
                            if ((num == 0xc02b6f85) && (s == "StandardEncoding"))
                            {
                                return new PdfPostScriptStandardEncodingOperator();
                            }
                        }
                        else if (s == "for")
                        {
                            return new PdfPostScriptForOperator();
                        }
                    }
                    else if (num <= 0xc77ae4a0)
                    {
                        if (num != 0xc5597e8c)
                        {
                            if ((num == 0xc77ae4a0) && (s == "mark"))
                            {
                                return new PdfPostScriptMarkOperator();
                            }
                        }
                        else if (s == "def")
                        {
                            return new PdfPostScriptDefOperator();
                        }
                    }
                    else if (num != 0xcaaf5d63)
                    {
                        if ((num == 0xce0beff7) && (s == "readonly"))
                        {
                            return new PdfPostScriptReadonlyOperator();
                        }
                    }
                    else if (s == "ifelse")
                    {
                        return new PdfPostScriptIfElseOperator();
                    }
                }
                else if (num > 0x72a68728)
                {
                    if (num == 0x7525b99d)
                    {
                        if (s == "closefile")
                        {
                            return new PdfPostScriptCloseFileOperator();
                        }
                    }
                    else if (num == 0x7904f763)
                    {
                        if (s == "RD")
                        {
                            goto TR_0042;
                        }
                    }
                    else if ((num == 0x8a58ad26) && (s == "array"))
                    {
                        return new PdfPostScriptArrayOperator();
                    }
                }
                else if (num == 0x6a8e75aa)
                {
                    if (s == "end")
                    {
                        return new PdfPostScriptEndOperator();
                    }
                }
                else if (num != 0x6d48538c)
                {
                    if ((num == 0x72a68728) && (s == "exp"))
                    {
                        return new PdfPostScriptExpOperator();
                    }
                }
                else if (s == "known")
                {
                    return new PdfPostScriptKnownOperator();
                }
                goto TR_0004;
            }
            else
            {
                if (num > 0x38e67d8f)
                {
                    if (num > 0x4c37a939)
                    {
                        if (num > 0x540ca757)
                        {
                            if (num <= 0x68348a7e)
                            {
                                if (num != 0x5d31eaed)
                                {
                                    if ((num == 0x68348a7e) && (s == "begin"))
                                    {
                                        return new PdfPostScriptBeginOperator();
                                    }
                                }
                                else if (s == "lt")
                                {
                                    return new PdfPostScriptLtOperator();
                                }
                            }
                            else if (num != 0x688f09fc)
                            {
                                if ((num == 0x697299ee) && (s == "put"))
                                {
                                    return new PdfPostScriptPutOperator();
                                }
                            }
                            else if (s == "noaccess")
                            {
                                return new PdfPostScriptNoAccessOperator();
                            }
                        }
                        else if (num == 0x4fcd670c)
                        {
                            if (s == "-|")
                            {
                                goto TR_0042;
                            }
                        }
                        else if (num != 0x51335fd0)
                        {
                            if ((num == 0x540ca757) && (s == "get"))
                            {
                                return new PdfPostScriptGetOperator();
                            }
                        }
                        else if (s == "pop")
                        {
                            return new PdfPostScriptPopOperator();
                        }
                    }
                    else if (num <= 0x3c206dd9)
                    {
                        if (num == 0x39386e06)
                        {
                            if (s == "if")
                            {
                                return new PdfPostScriptIfOperator();
                            }
                        }
                        else if (num != 0x3b391274)
                        {
                            if ((num == 0x3c206dd9) && (s == "ge"))
                            {
                                return new PdfPostScriptGeOperator();
                            }
                        }
                        else if (s == "add")
                        {
                            return new PdfPostScriptAddOperator();
                        }
                    }
                    else if (num <= 0x4b208576)
                    {
                        if (num != 0x3f3d5aa5)
                        {
                            if ((num == 0x4b208576) && (s == "gt"))
                            {
                                return new PdfPostScriptGtOperator();
                            }
                        }
                        else if (s == "dtransform")
                        {
                            return new PdfPostScriptDtransformOperator();
                        }
                    }
                    else if (num != 0x4c31d02a)
                    {
                        if ((num == 0x4c37a939) && (s == "dict"))
                        {
                            return new PdfPostScriptDictOperator();
                        }
                    }
                    else if (s == "le")
                    {
                        return new PdfPostScriptLeOperator();
                    }
                }
                else if (num > 0x17c16538)
                {
                    if (num > 0x29b19c8a)
                    {
                        if (num > 0x2ec19e9e)
                        {
                            if (num == 0x2eef9426)
                            {
                                if (s == "userdict")
                                {
                                    return new PdfPostScriptUserDictOperator();
                                }
                            }
                            else if ((num == 0x38e67d8f) && (s == "ND"))
                            {
                                goto TR_001A;
                            }
                        }
                        else if (num != 0x2a48023b)
                        {
                            if ((num == 0x2ec19e9e) && (s == "currentdict"))
                            {
                                return new PdfPostScriptCurrentDictOperator();
                            }
                        }
                        else if (s == "abs")
                        {
                            return new PdfPostScriptAbsOperator();
                        }
                    }
                    else if (num == 0x2458a0a2)
                    {
                        if (s == "|-")
                        {
                            goto TR_001A;
                        }
                    }
                    else if (num == 0x24e65e13)
                    {
                        if (s == "NP")
                        {
                            goto TR_0017;
                        }
                    }
                    else if ((num == 0x29b19c8a) && (s == "not"))
                    {
                        return new PdfPostScriptNotOperator();
                    }
                }
                else if (num <= 0xa4f917a)
                {
                    if (num == 0x8082743)
                    {
                        if (s == "definefont")
                        {
                            return new PdfPostScriptDefineFontOperator();
                        }
                    }
                    else if (num != 0x90aa9ab)
                    {
                        if ((num == 0xa4f917a) && (s == "cvr"))
                        {
                            return new PdfPostScriptCvrOperator();
                        }
                    }
                    else if (s == "index")
                    {
                        return new PdfPostScriptIndexOperator();
                    }
                }
                else if (num == 0xcffb773)
                {
                    if (s == "FontDirectory")
                    {
                        return new PdfPostScriptFontDirectoryOperator();
                    }
                }
                else if (num != 0x16ff3d1e)
                {
                    if ((num == 0x17c16538) && (s == "string"))
                    {
                        return new PdfPostScriptStringOperator();
                    }
                }
                else if (s == "roll")
                {
                    return new PdfPostScriptRollOperator();
                }
                goto TR_0004;
            }
            goto TR_0042;
        TR_0004:
            return new PdfPostScriptGetDictionaryElementOperator(s);
        TR_0017:
            return new PdfPostScriptType1FontOperator(PdfPostScriptType1FontOperatorKind.Put);
        TR_001A:
            return new PdfPostScriptType1FontOperator(PdfPostScriptType1FontOperatorKind.Def);
        TR_0042:
            return new PdfPostScriptType1FontOperator(PdfPostScriptType1FontOperatorKind.Read);
        }
    }
}

