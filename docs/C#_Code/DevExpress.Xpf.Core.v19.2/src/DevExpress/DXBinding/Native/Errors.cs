namespace DevExpress.DXBinding.Native
{
    using System;

    internal class Errors : ErrorsBase
    {
        public Errors(IParserErrorHandler errorHandler) : base(errorHandler)
        {
        }

        internal static string GetDefaultError(int n)
        {
            string str;
            switch (n)
            {
                case 0:
                    str = "EOF expected";
                    break;

                case 1:
                    str = "Ident expected";
                    break;

                case 2:
                    str = "Int expected";
                    break;

                case 3:
                    str = "Float expected";
                    break;

                case 4:
                    str = "String expected";
                    break;

                case 5:
                    str = "\";\" expected";
                    break;

                case 6:
                    str = "\"=\" expected";
                    break;

                case 7:
                    str = "\"?\" expected";
                    break;

                case 8:
                    str = "\":\" expected";
                    break;

                case 9:
                    str = "\"??\" expected";
                    break;

                case 10:
                    str = "\"||\" expected";
                    break;

                case 11:
                    str = "\"or\" expected";
                    break;

                case 12:
                    str = "\"&&\" expected";
                    break;

                case 13:
                    str = "\"and\" expected";
                    break;

                case 14:
                    str = "\"|\" expected";
                    break;

                case 15:
                    str = "\"^\" expected";
                    break;

                case 0x10:
                    str = "\"&\" expected";
                    break;

                case 0x11:
                    str = "\"!=\" expected";
                    break;

                case 0x12:
                    str = "\"ne\" expected";
                    break;

                case 0x13:
                    str = "\"==\" expected";
                    break;

                case 20:
                    str = "\"eq\" expected";
                    break;

                case 0x15:
                    str = "\"<\" expected";
                    break;

                case 0x16:
                    str = "\"lt\" expected";
                    break;

                case 0x17:
                    str = "\">\" expected";
                    break;

                case 0x18:
                    str = "\"gt\" expected";
                    break;

                case 0x19:
                    str = "\"<=\" expected";
                    break;

                case 0x1a:
                    str = "\"le\" expected";
                    break;

                case 0x1b:
                    str = "\">=\" expected";
                    break;

                case 0x1c:
                    str = "\"ge\" expected";
                    break;

                case 0x1d:
                    str = "\"is\" expected";
                    break;

                case 30:
                    str = "\"as\" expected";
                    break;

                case 0x1f:
                    str = "\"<<\" expected";
                    break;

                case 0x20:
                    str = "\"shl\" expected";
                    break;

                case 0x21:
                    str = "\">>\" expected";
                    break;

                case 0x22:
                    str = "\"shr\" expected";
                    break;

                case 0x23:
                    str = "\"+\" expected";
                    break;

                case 0x24:
                    str = "\"-\" expected";
                    break;

                case 0x25:
                    str = "\"*\" expected";
                    break;

                case 0x26:
                    str = "\"/\" expected";
                    break;

                case 0x27:
                    str = "\"%\" expected";
                    break;

                case 40:
                    str = "\"~\" expected";
                    break;

                case 0x29:
                    str = "\"!\" expected";
                    break;

                case 0x2a:
                    str = "\"(\" expected";
                    break;

                case 0x2b:
                    str = "\")\" expected";
                    break;

                case 0x2c:
                    str = "\"@c\" expected";
                    break;

                case 0x2d:
                    str = "\"@DataContext\" expected";
                    break;

                case 0x2e:
                    str = "\"@s\" expected";
                    break;

                case 0x2f:
                    str = "\"@Self\" expected";
                    break;

                case 0x30:
                    str = "\"@p\" expected";
                    break;

                case 0x31:
                    str = "\"@TemplatedParent\" expected";
                    break;

                case 50:
                    str = "\"@e\" expected";
                    break;

                case 0x33:
                    str = "\"@ElementName\" expected";
                    break;

                case 0x34:
                    str = "\"@r\" expected";
                    break;

                case 0x35:
                    str = "\"@StaticResource\" expected";
                    break;

                case 0x36:
                    str = "\"@Reference\" expected";
                    break;

                case 0x37:
                    str = "\"@a\" expected";
                    break;

                case 0x38:
                    str = "\"@FindAncestor\" expected";
                    break;

                case 0x39:
                    str = "\",\" expected";
                    break;

                case 0x3a:
                    str = "\"typeof\" expected";
                    break;

                case 0x3b:
                    str = "\".\" expected";
                    break;

                case 60:
                    str = "\"$\" expected";
                    break;

                case 0x3d:
                    str = "\"new\" expected";
                    break;

                case 0x3e:
                    str = "\"[\" expected";
                    break;

                case 0x3f:
                    str = "\"]\" expected";
                    break;

                case 0x40:
                    str = "\"true\" expected";
                    break;

                case 0x41:
                    str = "\"false\" expected";
                    break;

                case 0x42:
                    str = "\"null\" expected";
                    break;

                case 0x43:
                    str = "\"@v\" expected";
                    break;

                case 0x44:
                    str = "\"@value\" expected";
                    break;

                case 0x45:
                    str = "\"@parameter\" expected";
                    break;

                case 70:
                    str = "\"@sender\" expected";
                    break;

                case 0x47:
                    str = "\"@args\" expected";
                    break;

                case 0x48:
                    str = "??? expected";
                    break;

                case 0x49:
                    str = "invalid DXBinding";
                    break;

                case 0x4a:
                    str = "invalid Back_ExprRoot";
                    break;

                case 0x4b:
                    str = "invalid Assign_Expr";
                    break;

                case 0x4c:
                    str = "invalid TypeExpr";
                    break;

                case 0x4d:
                    str = "invalid AtomRootExpr";
                    break;

                case 0x4e:
                    str = "invalid AtomExpr";
                    break;

                case 0x4f:
                    str = "invalid AtomExpr";
                    break;

                case 80:
                    str = "invalid Back_AtomExpr";
                    break;

                case 0x51:
                    str = "invalid Execute_AtomExpr";
                    break;

                case 0x52:
                    str = "invalid Execute_AtomExpr";
                    break;

                case 0x53:
                    str = "invalid Event_AtomExpr";
                    break;

                case 0x54:
                    str = "invalid Event_AtomExpr";
                    break;

                case 0x55:
                    str = "invalid ConstExpr";
                    break;

                case 0x56:
                    str = "invalid RelativeExpr";
                    break;

                case 0x57:
                    str = "invalid TypeIdentExpr";
                    break;

                case 0x58:
                    str = "invalid NextIdentExpr";
                    break;

                case 0x59:
                    str = "invalid Back_AssignLeft";
                    break;

                case 90:
                    str = "invalid Back_RelativeValueExpr";
                    break;

                case 0x5b:
                    str = "invalid Execute_RelativeExpr";
                    break;

                case 0x5c:
                    str = "invalid Event_RelativeExpr";
                    break;

                default:
                    str = "error " + n;
                    break;
            }
            return str;
        }

        protected override string GetError(int n) => 
            GetDefaultError(n);
    }
}

