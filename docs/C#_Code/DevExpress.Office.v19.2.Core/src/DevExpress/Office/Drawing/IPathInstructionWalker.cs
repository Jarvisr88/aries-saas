namespace DevExpress.Office.Drawing
{
    using System;

    public interface IPathInstructionWalker
    {
        void Visit(PathArc pathArc);
        void Visit(PathClose value);
        void Visit(PathCubicBezier value);
        void Visit(PathLine pathLine);
        void Visit(PathMove pathMove);
        void Visit(PathQuadraticBezier value);
    }
}

