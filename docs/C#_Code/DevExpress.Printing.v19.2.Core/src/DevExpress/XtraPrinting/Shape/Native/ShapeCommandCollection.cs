namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    public class ShapeCommandCollection : CollectionBase
    {
        private Type[] allowedTypes;

        public ShapeCommandCollection() : this(new Type[0])
        {
        }

        protected ShapeCommandCollection(Type[] allowedTypes)
        {
            this.allowedTypes = allowedTypes;
        }

        public void Add(ShapeCommandBase command)
        {
            if ((this.allowedTypes.Length != 0) && (this.allowedTypes.IndexOf(command.GetType()) == -1))
            {
                throw new ArgumentException();
            }
            base.List.Add(command);
        }

        public void AddBezier(PointF startPoint, PointF startControlPoint, PointF endControlPoint, PointF endPoint)
        {
            this.Add(new ShapeBezierCommand(startPoint, startControlPoint, endControlPoint, endPoint));
        }

        public void AddLine(PointF startPoint, PointF endPoint)
        {
            this.Add(new ShapeLineCommand(startPoint, endPoint));
        }

        public void AddRange(ShapeCommandCollection commands)
        {
            foreach (ShapeCommandBase base2 in commands)
            {
                this.Add(base2);
            }
        }

        public CriticalPointsCalculator GetCriticalPointsCalculator()
        {
            CriticalPointsCalculator visitor = new CriticalPointsCalculator();
            this.Iterate(visitor);
            return visitor;
        }

        public void Iterate(IShapeCommandVisitor visitor)
        {
            foreach (ShapeCommandBase base2 in this)
            {
                base2.Accept(visitor);
            }
        }

        public void Offset(PointF offset)
        {
            this.Iterate(new ShapeCommandsOfsetter(offset));
        }

        public void RotateAt(PointF rotateCenter, int angle)
        {
            this.Iterate(new ShapeCommandsRotator(rotateCenter, angle));
        }

        public void ScaleAt(PointF scaleCenter, float scaleFactorX, float scaleFactorY)
        {
            this.Iterate(new ShapeCommandsScaler(scaleCenter, scaleFactorX, scaleFactorY));
        }

        public ShapeCommandBase this[int index] =>
            (ShapeCommandBase) base.List[index];
    }
}

