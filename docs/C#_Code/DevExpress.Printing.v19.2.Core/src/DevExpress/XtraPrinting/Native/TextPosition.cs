namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.CodeDom.Compiler;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TextPosition
    {
        public static TextPosition Create(CompilerError compilerError);
        public int Line { get; private set; }
        public int Column { get; private set; }
        public TextPosition(int line, int column);
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

