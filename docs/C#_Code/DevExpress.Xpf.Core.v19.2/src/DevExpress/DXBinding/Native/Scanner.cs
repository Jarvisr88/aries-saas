namespace DevExpress.DXBinding.Native
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class Scanner : ScannerBase
    {
        private const int maxT = 0x48;
        private const int noSym = 0x48;

        static Scanner()
        {
            for (int i = 0x41; i <= 90; i++)
            {
                ScannerBase.start[i] = 1;
            }
            for (int j = 0x5f; j <= 0x5f; j++)
            {
                ScannerBase.start[j] = 1;
            }
            for (int k = 0x61; k <= 0x7a; k++)
            {
                ScannerBase.start[k] = 1;
            }
            for (int m = 0x31; m <= 0x39; m++)
            {
                ScannerBase.start[m] = 0x10;
            }
            ScannerBase.start[(int) 0x30] = 0x11;
            ScannerBase.start[(int) 0x2e] = 0x94;
            ScannerBase.start[(int) 0x60] = 14;
            ScannerBase.start[(int) 0x3b] = 0x1b;
            ScannerBase.start[(int) 0x3d] = 0x95;
            ScannerBase.start[(int) 0x3f] = 150;
            ScannerBase.start[(int) 0x3a] = 0x1c;
            ScannerBase.start[(int) 0x7c] = 0x97;
            ScannerBase.start[(int) 0x26] = 0x98;
            ScannerBase.start[(int) 0x5e] = 0x20;
            ScannerBase.start[(int) 0x21] = 0x99;
            ScannerBase.start[(int) 60] = 0x9a;
            ScannerBase.start[(int) 0x3e] = 0x9b;
            ScannerBase.start[(int) 0x2b] = 0x27;
            ScannerBase.start[(int) 0x2d] = 40;
            ScannerBase.start[(int) 0x2a] = 0x29;
            ScannerBase.start[(int) 0x2f] = 0x2a;
            ScannerBase.start[(int) 0x25] = 0x2b;
            ScannerBase.start[(int) 0x7e] = 0x2c;
            ScannerBase.start[(int) 40] = 0x2d;
            ScannerBase.start[(int) 0x29] = 0x2e;
            ScannerBase.start[(int) 0x40] = 0x9c;
            ScannerBase.start[(int) 0x2c] = 0x7c;
            ScannerBase.start[(int) 0x24] = 0x7d;
            ScannerBase.start[(int) 0x5b] = 0x7e;
            ScannerBase.start[(int) 0x5d] = 0x7f;
            ScannerBase.start[0x10000] = -1;
        }

        public Scanner(Stream s) : base(s)
        {
        }

        public Scanner(string fileName) : base(fileName)
        {
        }

        protected override void Casing1()
        {
        }

        protected override void Casing2()
        {
            int tlen = base.tlen;
            base.tlen = tlen + 1;
            base.tval[tlen] = (char) base.ch;
        }

        protected override void CheckLiteral()
        {
            string val = base.t.val;
            uint num = <PrivateImplementationDetails>.ComputeStringHash(val);
            if (num <= 0x4db211e5)
            {
                if (num <= 0x3c206dd9)
                {
                    if (num <= 0xf29c2a6)
                    {
                        if (num == 0xb069958)
                        {
                            if (val == "false")
                            {
                                base.t.kind = 0x41;
                            }
                        }
                        else if ((num == 0xf29c2a6) && (val == "and"))
                        {
                            base.t.kind = 13;
                        }
                    }
                    else if (num == 0x28999611)
                    {
                        if (val == "new")
                        {
                            base.t.kind = 0x3d;
                        }
                    }
                    else if ((num == 0x3c206dd9) && (val == "ge"))
                    {
                        base.t.kind = 0x1c;
                    }
                }
                else if (num <= 0x4b208576)
                {
                    if (num == 0x441a6a43)
                    {
                        if (val == "eq")
                        {
                            base.t.kind = 20;
                        }
                    }
                    else if ((num == 0x4b208576) && (val == "gt"))
                    {
                        base.t.kind = 0x18;
                    }
                }
                else if (num == 0x4c31d02a)
                {
                    if (val == "le")
                    {
                        base.t.kind = 0x1a;
                    }
                }
                else if ((num == 0x4db211e5) && (val == "true"))
                {
                    base.t.kind = 0x40;
                }
            }
            else if (num <= 0x5d342984)
            {
                if (num <= 0x5836603c)
                {
                    if (num == 0x4e388f15)
                    {
                        if (val == "is")
                        {
                            base.t.kind = 0x1d;
                        }
                    }
                    else if ((num == 0x5836603c) && (val == "ne"))
                    {
                        base.t.kind = 0x12;
                    }
                }
                else if (num == 0x5d31eaed)
                {
                    if (val == "lt")
                    {
                        base.t.kind = 0x16;
                    }
                }
                else if ((num == 0x5d342984) && (val == "or"))
                {
                    base.t.kind = 11;
                }
            }
            else if (num <= 0x77074ba4)
            {
                if (num == 0x5e25208d)
                {
                    if (val == "as")
                    {
                        base.t.kind = 30;
                    }
                }
                else if ((num == 0x77074ba4) && (val == "null"))
                {
                    base.t.kind = 0x42;
                }
            }
            else if (num == 0x9a90a8a0)
            {
                if (val == "typeof")
                {
                    base.t.kind = 0x3a;
                }
            }
            else if (num == 0xda2de244)
            {
                if (val == "shr")
                {
                    base.t.kind = 0x22;
                }
            }
            else if ((num == 0xe02debb6) && (val == "shl"))
            {
                base.t.kind = 0x20;
            }
        }

        protected override int GetMaxT() => 
            0x48;

        protected override Token NextToken()
        {
            int num;
            int pos;
            Func<char, byte> func1;
            while (true)
            {
                if (((base.ch != 0x20) && ((base.ch < 9) || (base.ch > 10))) && (base.ch != 13))
                {
                    num = 0x48;
                    pos = base.pos;
                    base.t = new Token();
                    base.t.pos = base.pos;
                    base.t.col = base.col;
                    base.t.line = base.line;
                    base.t.charPos = base.charPos;
                    int num3 = !ScannerBase.start.ContainsKey(base.ch) ? 0 : ((int) ScannerBase.start[base.ch]);
                    base.tlen = 0;
                    base.AddCh();
                    switch (num3)
                    {
                        case -1:
                            base.t.kind = 0;
                            goto TR_0002;

                        case 0:
                            goto TR_0007;

                        case 1:
                        {
                            while (true)
                            {
                                pos = base.pos;
                                num = 1;
                                if ((((base.ch < 0x30) || (base.ch > 0x39)) && (((base.ch < 0x41) || (base.ch > 90)) && (base.ch != 0x5f))) && ((base.ch < 0x61) || (base.ch > 0x7a)))
                                {
                                    base.t.kind = 1;
                                    base.t.val = new string(base.tval, 0, base.tlen);
                                    this.CheckLiteral();
                                    return base.t;
                                }
                                base.AddCh();
                            }
                        }
                        case 2:
                            goto TR_0032;

                        case 3:
                            goto TR_002E;

                        case 4:
                            goto TR_0011;

                        case 5:
                            goto TR_004B;

                        case 6:
                            goto TR_0049;

                        case 7:
                            goto TR_0042;

                        case 8:
                            goto TR_003F;

                        case 9:
                            goto TR_003C;

                        case 10:
                            goto TR_0058;

                        case 11:
                            goto TR_0055;

                        case 12:
                            goto TR_0052;

                        case 13:
                            break;

                        case 14:
                            goto TR_0065;

                        case 15:
                            goto TR_005A;

                        case 0x10:
                            goto TR_0088;

                        case 0x11:
                            pos = base.pos;
                            num = 2;
                            if ((base.ch < 0x30) || (base.ch > 0x39))
                            {
                                if (base.ch != 0x55)
                                {
                                    if (base.ch != 0x75)
                                    {
                                        if (base.ch != 0x4c)
                                        {
                                            if (base.ch != 0x6c)
                                            {
                                                if ((base.ch == 0x58) || (base.ch == 120))
                                                {
                                                    base.AddCh();
                                                    goto TR_0032;
                                                }
                                                else if ((base.ch == 0x44) || ((base.ch == 70) || ((base.ch == 0x4d) || ((base.ch == 100) || ((base.ch == 0x66) || (base.ch == 0x6d))))))
                                                {
                                                    base.AddCh();
                                                }
                                                else
                                                {
                                                    if (base.ch != 0x2e)
                                                    {
                                                        if ((base.ch == 0x45) || (base.ch == 0x65))
                                                        {
                                                            base.AddCh();
                                                            goto TR_0058;
                                                        }
                                                        else
                                                        {
                                                            base.t.kind = 2;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        base.AddCh();
                                                        goto TR_004B;
                                                    }
                                                    goto TR_0002;
                                                }
                                            }
                                            else
                                            {
                                                base.AddCh();
                                                goto TR_0078;
                                            }
                                        }
                                        else
                                        {
                                            base.AddCh();
                                            goto TR_0073;
                                        }
                                    }
                                    else
                                    {
                                        base.AddCh();
                                        goto TR_006E;
                                    }
                                }
                                else
                                {
                                    base.AddCh();
                                    goto TR_0069;
                                }
                                break;
                            }
                            else
                            {
                                base.AddCh();
                            }
                            goto TR_0088;

                        case 0x12:
                            goto TR_0069;

                        case 0x13:
                            goto TR_006E;

                        case 20:
                            goto TR_0073;

                        case 0x15:
                            goto TR_0078;

                        case 0x16:
                            goto TR_0013;

                        case 0x17:
                            goto TR_0018;

                        case 0x18:
                            goto TR_001D;

                        case 0x19:
                            goto TR_0022;

                        case 0x1a:
                            goto TR_005E;

                        case 0x1b:
                            base.t.kind = 5;
                            goto TR_0002;

                        case 0x1c:
                            base.t.kind = 8;
                            goto TR_0002;

                        case 0x1d:
                            goto TR_009F;

                        case 30:
                            goto TR_00A0;

                        case 0x1f:
                            goto TR_00A1;

                        case 0x20:
                            base.t.kind = 15;
                            goto TR_0002;

                        case 0x21:
                            goto TR_00A3;

                        case 0x22:
                            goto TR_00A4;

                        case 0x23:
                            goto TR_00A5;

                        case 0x24:
                            goto TR_00A6;

                        case 0x25:
                            goto TR_00A7;

                        case 0x26:
                            goto TR_00A8;

                        case 0x27:
                            base.t.kind = 0x23;
                            goto TR_0002;

                        case 40:
                            base.t.kind = 0x24;
                            goto TR_0002;

                        case 0x29:
                            base.t.kind = 0x25;
                            goto TR_0002;

                        case 0x2a:
                            base.t.kind = 0x26;
                            goto TR_0002;

                        case 0x2b:
                            base.t.kind = 0x27;
                            goto TR_0002;

                        case 0x2c:
                            base.t.kind = 40;
                            goto TR_0002;

                        case 0x2d:
                            base.t.kind = 0x2a;
                            goto TR_0002;

                        case 0x2e:
                            base.t.kind = 0x2b;
                            goto TR_0002;

                        case 0x2f:
                            goto TR_00B1;

                        case 0x30:
                            goto TR_00C6;

                        case 0x31:
                            goto TR_00C4;

                        case 50:
                            goto TR_00C2;

                        case 0x33:
                            goto TR_00C0;

                        case 0x34:
                            goto TR_00BE;

                        case 0x35:
                            goto TR_00BC;

                        case 0x36:
                            goto TR_00BA;

                        case 0x37:
                            goto TR_00B8;

                        case 0x38:
                            goto TR_00B6;

                        case 0x39:
                            goto TR_00B4;

                        case 0x3a:
                            goto TR_00B2;

                        case 0x3b:
                            goto TR_00CB;

                        case 60:
                            goto TR_00C9;

                        case 0x3d:
                            goto TR_00C7;

                        case 0x3e:
                            goto TR_00E8;

                        case 0x3f:
                            goto TR_00E6;

                        case 0x40:
                            goto TR_00E4;

                        case 0x41:
                            goto TR_00E2;

                        case 0x42:
                            goto TR_00E0;

                        case 0x43:
                            goto TR_00DE;

                        case 0x44:
                            goto TR_00DC;

                        case 0x45:
                            goto TR_00DA;

                        case 70:
                            goto TR_00D8;

                        case 0x47:
                            goto TR_00D6;

                        case 0x48:
                            goto TR_00D4;

                        case 0x49:
                            goto TR_00D2;

                        case 0x4a:
                            goto TR_00D0;

                        case 0x4b:
                            goto TR_00CE;

                        case 0x4c:
                            goto TR_00CC;

                        case 0x4d:
                            goto TR_00E9;

                        case 0x4e:
                            goto TR_00FE;

                        case 0x4f:
                            goto TR_00FC;

                        case 80:
                            goto TR_00FA;

                        case 0x51:
                            goto TR_00F8;

                        case 0x52:
                            goto TR_00F6;

                        case 0x53:
                            goto TR_00F4;

                        case 0x54:
                            goto TR_00F2;

                        case 0x55:
                            goto TR_00F0;

                        case 0x56:
                            goto TR_00EE;

                        case 0x57:
                            goto TR_00EC;

                        case 0x58:
                            goto TR_00EA;

                        case 0x59:
                            goto TR_00FF;

                        case 90:
                            goto TR_0118;

                        case 0x5b:
                            goto TR_0116;

                        case 0x5c:
                            goto TR_0114;

                        case 0x5d:
                            goto TR_0112;

                        case 0x5e:
                            goto TR_0110;

                        case 0x5f:
                            goto TR_010E;

                        case 0x60:
                            goto TR_010C;

                        case 0x61:
                            goto TR_010A;

                        case 0x62:
                            goto TR_0108;

                        case 0x63:
                            goto TR_0106;

                        case 100:
                            goto TR_0104;

                        case 0x65:
                            goto TR_0102;

                        case 0x66:
                            goto TR_0100;

                        case 0x67:
                            goto TR_0129;

                        case 0x68:
                            goto TR_0127;

                        case 0x69:
                            goto TR_0125;

                        case 0x6a:
                            goto TR_0123;

                        case 0x6b:
                            goto TR_0121;

                        case 0x6c:
                            goto TR_011F;

                        case 0x6d:
                            goto TR_011D;

                        case 110:
                            goto TR_011B;

                        case 0x6f:
                            goto TR_0119;

                        case 0x70:
                            goto TR_0140;

                        case 0x71:
                            goto TR_013E;

                        case 0x72:
                            goto TR_013C;

                        case 0x73:
                            goto TR_013A;

                        case 0x74:
                            goto TR_0138;

                        case 0x75:
                            goto TR_0136;

                        case 0x76:
                            goto TR_0134;

                        case 0x77:
                            goto TR_0132;

                        case 120:
                            goto TR_0130;

                        case 0x79:
                            goto TR_012E;

                        case 0x7a:
                            goto TR_012C;

                        case 0x7b:
                            goto TR_012A;

                        case 0x7c:
                            base.t.kind = 0x39;
                            goto TR_0002;

                        case 0x7d:
                            base.t.kind = 60;
                            goto TR_0002;

                        case 0x7e:
                            base.t.kind = 0x3e;
                            goto TR_0002;

                        case 0x7f:
                            base.t.kind = 0x3f;
                            goto TR_0002;

                        case 0x80:
                            goto TR_014B;

                        case 0x81:
                            goto TR_0149;

                        case 130:
                            goto TR_0147;

                        case 0x83:
                            goto TR_0145;

                        case 0x84:
                            goto TR_015A;

                        case 0x85:
                            goto TR_0158;

                        case 0x86:
                            goto TR_0156;

                        case 0x87:
                            goto TR_0154;

                        case 0x88:
                            goto TR_0152;

                        case 0x89:
                            goto TR_0150;

                        case 0x8a:
                            goto TR_014E;

                        case 0x8b:
                            goto TR_014C;

                        case 140:
                            goto TR_0163;

                        case 0x8d:
                            goto TR_0161;

                        case 0x8e:
                            goto TR_015F;

                        case 0x8f:
                            goto TR_015D;

                        case 0x90:
                            goto TR_015B;

                        case 0x91:
                            goto TR_0168;

                        case 0x92:
                            goto TR_0166;

                        case 0x93:
                            goto TR_0164;

                        case 0x94:
                            pos = base.pos;
                            num = 0x3b;
                            if ((base.ch < 0x30) || (base.ch > 0x39))
                            {
                                base.t.kind = 0x3b;
                                goto TR_0002;
                            }
                            else
                            {
                                base.AddCh();
                            }
                            goto TR_0049;

                        case 0x95:
                            pos = base.pos;
                            num = 6;
                            if (base.ch != 0x3d)
                            {
                                base.t.kind = 6;
                                goto TR_0002;
                            }
                            else
                            {
                                base.AddCh();
                            }
                            goto TR_00A4;

                        case 150:
                            pos = base.pos;
                            num = 7;
                            if (base.ch != 0x3f)
                            {
                                base.t.kind = 7;
                                goto TR_0002;
                            }
                            else
                            {
                                base.AddCh();
                            }
                            goto TR_009F;

                        case 0x97:
                            pos = base.pos;
                            num = 14;
                            if (base.ch != 0x7c)
                            {
                                base.t.kind = 14;
                                goto TR_0002;
                            }
                            else
                            {
                                base.AddCh();
                            }
                            goto TR_00A0;

                        case 0x98:
                            pos = base.pos;
                            num = 0x10;
                            if (base.ch != 0x26)
                            {
                                base.t.kind = 0x10;
                                goto TR_0002;
                            }
                            else
                            {
                                base.AddCh();
                            }
                            goto TR_00A1;

                        case 0x99:
                            pos = base.pos;
                            num = 0x29;
                            if (base.ch != 0x3d)
                            {
                                base.t.kind = 0x29;
                                goto TR_0002;
                            }
                            else
                            {
                                base.AddCh();
                            }
                            goto TR_00A3;

                        case 0x9a:
                            pos = base.pos;
                            num = 0x15;
                            if (base.ch != 0x3d)
                            {
                                if (base.ch != 60)
                                {
                                    base.t.kind = 0x15;
                                    goto TR_0002;
                                }
                                else
                                {
                                    base.AddCh();
                                }
                            }
                            else
                            {
                                base.AddCh();
                                goto TR_00A5;
                            }
                            goto TR_00A7;

                        case 0x9b:
                            pos = base.pos;
                            num = 0x17;
                            if (base.ch != 0x3d)
                            {
                                if (base.ch != 0x3e)
                                {
                                    base.t.kind = 0x17;
                                    goto TR_0002;
                                }
                                else
                                {
                                    base.AddCh();
                                }
                            }
                            else
                            {
                                base.AddCh();
                                goto TR_00A6;
                            }
                            goto TR_00A8;

                        case 0x9c:
                            if (base.ch != 0x63)
                            {
                                if (base.ch != 0x44)
                                {
                                    if (base.ch != 0x73)
                                    {
                                        if (base.ch != 0x53)
                                        {
                                            if (base.ch != 0x70)
                                            {
                                                if (base.ch != 0x54)
                                                {
                                                    if (base.ch != 0x65)
                                                    {
                                                        if (base.ch != 0x45)
                                                        {
                                                            if (base.ch != 0x72)
                                                            {
                                                                if (base.ch != 0x52)
                                                                {
                                                                    if (base.ch != 0x61)
                                                                    {
                                                                        if (base.ch != 70)
                                                                        {
                                                                            if (base.ch == 0x76)
                                                                            {
                                                                                base.AddCh();
                                                                                goto TR_01AB;
                                                                            }
                                                                            goto TR_0007;
                                                                        }
                                                                        else
                                                                        {
                                                                            base.AddCh();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        base.AddCh();
                                                                        goto TR_01A5;
                                                                    }
                                                                    goto TR_0140;
                                                                }
                                                                else
                                                                {
                                                                    base.AddCh();
                                                                    goto TR_0129;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                base.AddCh();
                                                                goto TR_00FF;
                                                            }
                                                            goto TR_01AB;
                                                        }
                                                        else
                                                        {
                                                            base.AddCh();
                                                            goto TR_00FE;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        base.AddCh();
                                                        goto TR_00E9;
                                                    }
                                                    goto TR_0007;
                                                }
                                                else
                                                {
                                                    base.AddCh();
                                                    goto TR_00E8;
                                                }
                                            }
                                            else
                                            {
                                                base.AddCh();
                                                goto TR_019B;
                                            }
                                            goto TR_01AB;
                                        }
                                        else
                                        {
                                            base.AddCh();
                                            goto TR_0197;
                                        }
                                    }
                                    else
                                    {
                                        base.AddCh();
                                        goto TR_0191;
                                    }
                                    goto TR_0007;
                                }
                                else
                                {
                                    base.AddCh();
                                    goto TR_00C6;
                                }
                            }
                            else
                            {
                                base.AddCh();
                                goto TR_00B1;
                            }
                            goto TR_01AB;

                        case 0x9d:
                            goto TR_0191;

                        case 0x9e:
                            goto TR_0197;

                        case 0x9f:
                            goto TR_019B;

                        case 160:
                            goto TR_01A5;

                        case 0xa1:
                            goto TR_01AB;

                        default:
                            goto TR_0002;
                    }
                    goto TR_0037;
                }
                base.NextCh();
            }
            goto TR_01AB;
        TR_0002:
            func1 = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<char, byte> local1 = <>c.<>9__9_0;
                func1 = <>c.<>9__9_0 = x => Convert.ToByte(x);
            }
            byte[] bytes = base.tval.Select<char, byte>(func1).ToArray<byte>();
            base.t.val = Encoding.UTF8.GetString(bytes, 0, base.tlen);
            return base.t;
        TR_0007:
            if (num != 0x48)
            {
                base.tlen = pos - base.t.pos;
                base.SetScannerBehindT();
            }
            base.t.kind = num;
            goto TR_0002;
        TR_0011:
            base.t.kind = 2;
            goto TR_0002;
        TR_0013:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x4c) || (base.ch == 0x6c))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_0018:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x4c) || (base.ch == 0x6c))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_001D:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x55) || (base.ch == 0x75))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_0022:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x55) || (base.ch == 0x75))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_002E:
            while (true)
            {
                pos = base.pos;
                num = 2;
                if ((((base.ch < 0x30) || (base.ch > 0x39)) && ((base.ch < 0x41) || (base.ch > 70))) && ((base.ch < 0x61) || (base.ch > 0x66)))
                {
                    if (base.ch != 0x55)
                    {
                        if (base.ch != 0x75)
                        {
                            if (base.ch != 0x4c)
                            {
                                if (base.ch != 0x6c)
                                {
                                    base.t.kind = 2;
                                    goto TR_0002;
                                }
                                else
                                {
                                    base.AddCh();
                                }
                            }
                            else
                            {
                                base.AddCh();
                                goto TR_001D;
                            }
                        }
                        else
                        {
                            base.AddCh();
                            goto TR_0018;
                        }
                    }
                    else
                    {
                        base.AddCh();
                        goto TR_0013;
                    }
                    break;
                }
                base.AddCh();
            }
            goto TR_0022;
        TR_0032:
            if ((((base.ch >= 0x30) && (base.ch <= 0x39)) || ((base.ch >= 0x41) && (base.ch <= 70))) || ((base.ch >= 0x61) && (base.ch <= 0x66)))
            {
                base.AddCh();
                goto TR_002E;
            }
            goto TR_0007;
        TR_0037:
            base.t.kind = 3;
            goto TR_0002;
        TR_003C:
            while (true)
            {
                pos = base.pos;
                num = 3;
                if ((base.ch >= 0x30) && (base.ch <= 0x39))
                {
                    base.AddCh();
                    continue;
                }
                if ((base.ch == 0x44) || ((base.ch == 70) || ((base.ch == 0x4d) || ((base.ch == 100) || ((base.ch == 0x66) || (base.ch == 0x6d))))))
                {
                    base.AddCh();
                }
                else
                {
                    base.t.kind = 3;
                    goto TR_0002;
                }
                break;
            }
            goto TR_0037;
        TR_003F:
            if ((base.ch >= 0x30) && (base.ch <= 0x39))
            {
                base.AddCh();
                goto TR_003C;
            }
            goto TR_0007;
        TR_0042:
            if ((base.ch < 0x30) || (base.ch > 0x39))
            {
                if ((base.ch == 0x2b) || (base.ch == 0x2d))
                {
                    base.AddCh();
                }
                else
                {
                    goto TR_0007;
                }
            }
            else
            {
                base.AddCh();
                goto TR_003C;
            }
            goto TR_003F;
        TR_0049:
            while (true)
            {
                pos = base.pos;
                num = 3;
                if ((base.ch >= 0x30) && (base.ch <= 0x39))
                {
                    base.AddCh();
                    continue;
                }
                if ((base.ch == 0x44) || ((base.ch == 70) || ((base.ch == 0x4d) || ((base.ch == 100) || ((base.ch == 0x66) || (base.ch == 0x6d))))))
                {
                    base.AddCh();
                }
                else
                {
                    if ((base.ch == 0x45) || (base.ch == 0x65))
                    {
                        base.AddCh();
                        goto TR_0042;
                    }
                    else
                    {
                        base.t.kind = 3;
                    }
                    goto TR_0002;
                }
                break;
            }
            goto TR_0037;
        TR_004B:
            if ((base.ch >= 0x30) && (base.ch <= 0x39))
            {
                base.AddCh();
                goto TR_0049;
            }
            goto TR_0007;
        TR_0052:
            while (true)
            {
                pos = base.pos;
                num = 3;
                if ((base.ch >= 0x30) && (base.ch <= 0x39))
                {
                    base.AddCh();
                    continue;
                }
                if ((base.ch == 0x44) || ((base.ch == 70) || ((base.ch == 0x4d) || ((base.ch == 100) || ((base.ch == 0x66) || (base.ch == 0x6d))))))
                {
                    base.AddCh();
                }
                else
                {
                    base.t.kind = 3;
                    goto TR_0002;
                }
                break;
            }
            goto TR_0037;
        TR_0055:
            if ((base.ch >= 0x30) && (base.ch <= 0x39))
            {
                base.AddCh();
                goto TR_0052;
            }
            goto TR_0007;
        TR_0058:
            if ((base.ch < 0x30) || (base.ch > 0x39))
            {
                if ((base.ch == 0x2b) || (base.ch == 0x2d))
                {
                    base.AddCh();
                }
                else
                {
                    goto TR_0007;
                }
            }
            else
            {
                base.AddCh();
                goto TR_0052;
            }
            goto TR_0055;
        TR_0059:
            base.AddCh();
            goto TR_0065;
        TR_005A:
            base.t.kind = 4;
            goto TR_0002;
        TR_005E:
            if ((((base.ch == 0x22) || ((base.ch == 0x27) || ((base.ch == 0x30) || ((base.ch == 0x5c) || ((base.ch >= 0x60) && (base.ch <= 0x62)))))) || (base.ch == 0x66)) || ((base.ch == 110) || ((base.ch == 0x72) || ((base.ch == 0x74) || (base.ch == 0x76)))))
            {
                base.AddCh();
            }
            else
            {
                goto TR_0007;
            }
        TR_0065:
            while (true)
            {
                if ((base.ch <= 9) || ((base.ch >= 11) && (base.ch <= 0x5b)))
                {
                    goto TR_0059;
                }
                else
                {
                    if (((base.ch < 0x5d) || (base.ch > 0x5f)) && ((base.ch < 0x61) || (base.ch > 0xffff)))
                    {
                        if (base.ch != 0x60)
                        {
                            if (base.ch != 0x5c)
                            {
                                goto TR_0007;
                            }
                            else
                            {
                                base.AddCh();
                            }
                        }
                        else
                        {
                            base.AddCh();
                            goto TR_005A;
                        }
                        break;
                    }
                    goto TR_0059;
                }
                break;
            }
            goto TR_005E;
        TR_0069:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x4c) || (base.ch == 0x6c))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_006E:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x4c) || (base.ch == 0x6c))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_0073:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x55) || (base.ch == 0x75))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_0078:
            pos = base.pos;
            num = 2;
            if ((base.ch == 0x55) || (base.ch == 0x75))
            {
                base.AddCh();
            }
            else
            {
                base.t.kind = 2;
                goto TR_0002;
            }
            goto TR_0011;
        TR_0088:
            while (true)
            {
                pos = base.pos;
                num = 2;
                if ((base.ch >= 0x30) && (base.ch <= 0x39))
                {
                    base.AddCh();
                    continue;
                }
                if (base.ch != 0x55)
                {
                    if (base.ch != 0x75)
                    {
                        if (base.ch != 0x4c)
                        {
                            if (base.ch != 0x6c)
                            {
                                if ((base.ch == 0x44) || ((base.ch == 70) || ((base.ch == 0x4d) || ((base.ch == 100) || ((base.ch == 0x66) || (base.ch == 0x6d))))))
                                {
                                    base.AddCh();
                                }
                                else
                                {
                                    if (base.ch != 0x2e)
                                    {
                                        if ((base.ch == 0x45) || (base.ch == 0x65))
                                        {
                                            base.AddCh();
                                            goto TR_0058;
                                        }
                                        else
                                        {
                                            base.t.kind = 2;
                                        }
                                    }
                                    else
                                    {
                                        base.AddCh();
                                        goto TR_004B;
                                    }
                                    goto TR_0002;
                                }
                            }
                            else
                            {
                                base.AddCh();
                                goto TR_0078;
                            }
                        }
                        else
                        {
                            base.AddCh();
                            goto TR_0073;
                        }
                    }
                    else
                    {
                        base.AddCh();
                        goto TR_006E;
                    }
                }
                else
                {
                    base.AddCh();
                    goto TR_0069;
                }
                break;
            }
            goto TR_0037;
        TR_009F:
            base.t.kind = 9;
            goto TR_0002;
        TR_00A0:
            base.t.kind = 10;
            goto TR_0002;
        TR_00A1:
            base.t.kind = 12;
            goto TR_0002;
        TR_00A3:
            base.t.kind = 0x11;
            goto TR_0002;
        TR_00A4:
            base.t.kind = 0x13;
            goto TR_0002;
        TR_00A5:
            base.t.kind = 0x19;
            goto TR_0002;
        TR_00A6:
            base.t.kind = 0x1b;
            goto TR_0002;
        TR_00A7:
            base.t.kind = 0x1f;
            goto TR_0002;
        TR_00A8:
            base.t.kind = 0x21;
            goto TR_0002;
        TR_00B1:
            base.t.kind = 0x2c;
            goto TR_0002;
        TR_00B2:
            base.t.kind = 0x2d;
            goto TR_0002;
        TR_00B4:
            if (base.ch == 0x74)
            {
                base.AddCh();
                goto TR_00B2;
            }
            goto TR_0007;
        TR_00B6:
            if (base.ch != 120)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00B4;
        TR_00B8:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00B6;
        TR_00BA:
            if (base.ch != 0x74)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00B8;
        TR_00BC:
            if (base.ch != 110)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00BA;
        TR_00BE:
            if (base.ch != 0x6f)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00BC;
        TR_00C0:
            if (base.ch != 0x43)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00BE;
        TR_00C2:
            if (base.ch != 0x61)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00C0;
        TR_00C4:
            if (base.ch != 0x74)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00C2;
        TR_00C6:
            if (base.ch != 0x61)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00C4;
        TR_00C7:
            base.t.kind = 0x2f;
            goto TR_0002;
        TR_00C9:
            if (base.ch == 0x66)
            {
                base.AddCh();
                goto TR_00C7;
            }
            goto TR_0007;
        TR_00CB:
            if (base.ch != 0x6c)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00C9;
        TR_00CC:
            base.t.kind = 0x31;
            goto TR_0002;
        TR_00CE:
            if (base.ch == 0x74)
            {
                base.AddCh();
                goto TR_00CC;
            }
            goto TR_0007;
        TR_00D0:
            if (base.ch != 110)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00CE;
        TR_00D2:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00D0;
        TR_00D4:
            if (base.ch != 0x72)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00D2;
        TR_00D6:
            if (base.ch != 0x61)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00D4;
        TR_00D8:
            if (base.ch != 80)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00D6;
        TR_00DA:
            if (base.ch != 100)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00D8;
        TR_00DC:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00DA;
        TR_00DE:
            if (base.ch != 0x74)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00DC;
        TR_00E0:
            if (base.ch != 0x61)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00DE;
        TR_00E2:
            if (base.ch != 0x6c)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00E0;
        TR_00E4:
            if (base.ch != 0x70)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00E2;
        TR_00E6:
            if (base.ch != 0x6d)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00E4;
        TR_00E8:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00E6;
        TR_00E9:
            base.t.kind = 50;
            goto TR_0002;
        TR_00EA:
            base.t.kind = 0x33;
            goto TR_0002;
        TR_00EC:
            if (base.ch == 0x65)
            {
                base.AddCh();
                goto TR_00EA;
            }
            goto TR_0007;
        TR_00EE:
            if (base.ch != 0x6d)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00EC;
        TR_00F0:
            if (base.ch != 0x61)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00EE;
        TR_00F2:
            if (base.ch != 0x4e)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00F0;
        TR_00F4:
            if (base.ch != 0x74)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00F2;
        TR_00F6:
            if (base.ch != 110)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00F4;
        TR_00F8:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00F6;
        TR_00FA:
            if (base.ch != 0x6d)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00F8;
        TR_00FC:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00FA;
        TR_00FE:
            if (base.ch != 0x6c)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_00FC;
        TR_00FF:
            base.t.kind = 0x34;
            goto TR_0002;
        TR_0100:
            base.t.kind = 0x35;
            goto TR_0002;
        TR_0102:
            if (base.ch == 0x65)
            {
                base.AddCh();
                goto TR_0100;
            }
            goto TR_0007;
        TR_0104:
            if (base.ch != 0x63)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0102;
        TR_0106:
            if (base.ch != 0x72)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0104;
        TR_0108:
            if (base.ch != 0x75)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0106;
        TR_010A:
            if (base.ch != 0x6f)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0108;
        TR_010C:
            if (base.ch != 0x73)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_010A;
        TR_010E:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_010C;
        TR_0110:
            if (base.ch != 0x52)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_010E;
        TR_0112:
            if (base.ch != 0x63)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0110;
        TR_0114:
            if (base.ch != 0x69)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0112;
        TR_0116:
            if (base.ch != 0x74)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0114;
        TR_0118:
            if (base.ch != 0x61)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0116;
        TR_0119:
            base.t.kind = 0x36;
            goto TR_0002;
        TR_011B:
            if (base.ch == 0x65)
            {
                base.AddCh();
                goto TR_0119;
            }
            goto TR_0007;
        TR_011D:
            if (base.ch != 0x63)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_011B;
        TR_011F:
            if (base.ch != 110)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_011D;
        TR_0121:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_011F;
        TR_0123:
            if (base.ch != 0x72)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0121;
        TR_0125:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0123;
        TR_0127:
            if (base.ch != 0x66)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0125;
        TR_0129:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0127;
        TR_012A:
            base.t.kind = 0x38;
            goto TR_0002;
        TR_012C:
            if (base.ch == 0x72)
            {
                base.AddCh();
                goto TR_012A;
            }
            goto TR_0007;
        TR_012E:
            if (base.ch != 0x6f)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_012C;
        TR_0130:
            if (base.ch != 0x74)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_012E;
        TR_0132:
            if (base.ch != 0x73)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0130;
        TR_0134:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0132;
        TR_0136:
            if (base.ch != 0x63)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0134;
        TR_0138:
            if (base.ch != 110)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0136;
        TR_013A:
            if (base.ch != 0x41)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0138;
        TR_013C:
            if (base.ch != 100)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_013A;
        TR_013E:
            if (base.ch != 110)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_013C;
        TR_0140:
            if (base.ch != 0x69)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_013E;
        TR_0145:
            base.t.kind = 0x44;
            goto TR_0002;
        TR_0147:
            if (base.ch == 0x65)
            {
                base.AddCh();
                goto TR_0145;
            }
            goto TR_0007;
        TR_0149:
            if (base.ch != 0x75)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0147;
        TR_014B:
            if (base.ch != 0x6c)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0149;
        TR_014C:
            base.t.kind = 0x45;
            goto TR_0002;
        TR_014E:
            if (base.ch == 0x72)
            {
                base.AddCh();
                goto TR_014C;
            }
            goto TR_0007;
        TR_0150:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_014E;
        TR_0152:
            if (base.ch != 0x74)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0150;
        TR_0154:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0152;
        TR_0156:
            if (base.ch != 0x6d)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0154;
        TR_0158:
            if (base.ch != 0x61)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0156;
        TR_015A:
            if (base.ch != 0x72)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0158;
        TR_015B:
            base.t.kind = 70;
            goto TR_0002;
        TR_015D:
            if (base.ch == 0x72)
            {
                base.AddCh();
                goto TR_015B;
            }
            goto TR_0007;
        TR_015F:
            if (base.ch != 0x65)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_015D;
        TR_0161:
            if (base.ch != 100)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_015F;
        TR_0163:
            if (base.ch != 110)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0161;
        TR_0164:
            base.t.kind = 0x47;
            goto TR_0002;
        TR_0166:
            if (base.ch == 0x73)
            {
                base.AddCh();
                goto TR_0164;
            }
            goto TR_0007;
        TR_0168:
            if (base.ch != 0x67)
            {
                goto TR_0007;
            }
            else
            {
                base.AddCh();
            }
            goto TR_0166;
        TR_0191:
            pos = base.pos;
            num = 0x2e;
            if (base.ch != 0x65)
            {
                base.t.kind = 0x2e;
            }
            else
            {
                base.AddCh();
                goto TR_0163;
            }
            goto TR_0002;
        TR_0197:
            if (base.ch != 0x65)
            {
                if (base.ch != 0x74)
                {
                    goto TR_0007;
                }
                else
                {
                    base.AddCh();
                }
            }
            else
            {
                base.AddCh();
                goto TR_00CB;
            }
            goto TR_0118;
        TR_019B:
            pos = base.pos;
            num = 0x30;
            if (base.ch != 0x61)
            {
                base.t.kind = 0x30;
            }
            else
            {
                base.AddCh();
                goto TR_015A;
            }
            goto TR_0002;
        TR_01A5:
            pos = base.pos;
            num = 0x37;
            if (base.ch != 0x72)
            {
                base.t.kind = 0x37;
            }
            else
            {
                base.AddCh();
                goto TR_0168;
            }
            goto TR_0002;
        TR_01AB:
            pos = base.pos;
            num = 0x43;
            if (base.ch != 0x61)
            {
                base.t.kind = 0x43;
            }
            else
            {
                base.AddCh();
                goto TR_014B;
            }
            goto TR_0002;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Scanner.<>c <>9 = new Scanner.<>c();
            public static Func<char, byte> <>9__9_0;

            internal byte <NextToken>b__9_0(char x) => 
                Convert.ToByte(x);
        }
    }
}

