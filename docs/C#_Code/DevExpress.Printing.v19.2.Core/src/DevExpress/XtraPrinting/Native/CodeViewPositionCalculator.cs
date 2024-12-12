namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class CodeViewPositionCalculator
    {
        public static int CalculatePosition(string script, TextPosition orderedPositions);
        [IteratorStateMachine(typeof(CodeViewPositionCalculator.<CalculatePositions>d__1))]
        public static IEnumerable<int> CalculatePositions(string script, IEnumerable<TextPosition> orderedPositions);

    }
}

