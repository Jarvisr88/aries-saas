namespace DevExpress.XtraSpellChecker.Parser
{
    using System;

    public abstract class Position
    {
        protected Position()
        {
        }

        protected Position(object actualPosition)
        {
            this.ActualPosition = actualPosition;
        }

        public static Position Add(Position position1, Position position2) => 
            (position1 == null) ? ((position2 == null) ? Null : position2.InternalAdd(position1)) : position1.InternalAdd(position2);

        public abstract Position Clone();
        public static int Compare(Position position1, Position position2) => 
            (position1 == null) ? ((position2 == null) ? 0 : -position2.InternalCompare(position1)) : position1.InternalCompare(position2);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            Position position = obj as Position;
            return ((position != null) ? Equals(this, position) : false);
        }

        public static bool Equals(Position position1, Position position2) => 
            Compare(position1, position2) == 0;

        public override int GetHashCode() => 
            this.ActualPosition.GetHashCode();

        protected abstract Position InternalAdd(Position position);
        protected abstract int InternalCompare(Position position);
        protected abstract Position InternalSubtract(Position position);
        protected abstract Position InternalSubtractFromNull();
        public static bool IsGreater(Position position1, Position position2) => 
            Compare(position1, position2) > 0;

        public static bool IsGreaterOrEqual(Position position1, Position position2) => 
            IsGreater(position1, position2) || Equals(position1, position2);

        public static bool IsLess(Position position1, Position position2) => 
            Compare(position1, position2) < 0;

        public static bool IsLessOrEqual(Position position1, Position position2) => 
            IsLess(position1, position2) || Equals(position1, position2);

        protected abstract Position MoveBackward();
        protected abstract Position MoveForward();
        public static Position operator +(Position position1, Position position2) => 
            Add(position1, position2);

        public static Position operator --(Position position) => 
            position.MoveBackward();

        public static Position operator ++(Position position) => 
            position.MoveForward();

        public static Position operator -(Position position1, Position position2) => 
            Subtract(position1, position2);

        public static Position Subtract(Position position1, Position position2) => 
            (position1 == null) ? ((position2 == null) ? Null : position2.InternalSubtractFromNull()) : position1.InternalSubtract(position2);

        public abstract int ToInt();

        public static Position Null =>
            null;

        public static Position Undefined =>
            null;

        protected abstract object ActualPosition { get; set; }

        protected virtual Position Zero =>
            Subtract(this, this);

        public bool IsZero =>
            Equals(this.Zero, this);

        public bool IsNegative =>
            IsLess(this, this.Zero);

        public bool IsPositive =>
            IsGreater(this, this.Zero);
    }
}

