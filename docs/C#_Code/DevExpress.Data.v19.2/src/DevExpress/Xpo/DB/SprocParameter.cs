namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Data;

    [Serializable]
    public class SprocParameter : OperandParameter
    {
        private int? size;
        private byte? precision;
        private byte? scale;
        private DBColumnType? dbType;
        private SprocParameterDirection direction;

        public SprocParameter() : this(null)
        {
        }

        public SprocParameter(string parameterName) : this(parameterName, null)
        {
        }

        public SprocParameter(string parameterName, object value) : base(parameterName, value)
        {
            this.direction = SprocParameterDirection.Input;
        }

        public SprocParameter(string parameterName, object value, int size) : this(parameterName, value)
        {
            this.size = new int?(size);
        }

        public OperandParameter Clone()
        {
            ICloneable cloneable = this.Value as ICloneable;
            if (cloneable != null)
            {
                SprocParameter parameter1 = new SprocParameter(base.ParameterName, cloneable.Clone());
                parameter1.Direction = this.Direction;
                parameter1.Size = this.Size;
                parameter1.Scale = this.Scale;
                parameter1.Precision = this.Precision;
                return parameter1;
            }
            SprocParameter parameter2 = new SprocParameter(base.ParameterName, this.Value);
            parameter2.Direction = this.Direction;
            parameter2.Size = this.Size;
            parameter2.Scale = this.Scale;
            parameter2.Precision = this.Precision;
            return parameter2;
        }

        protected override CriteriaOperator CloneCommon() => 
            this.Clone();

        public override bool Equals(object obj)
        {
            SprocParameter criterion = obj as SprocParameter;
            if (!criterion.ReferenceEqualsNull() && base.Equals(obj))
            {
                int? size = criterion.size;
                int? nullable2 = this.size;
                if ((size.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((size != null) == (nullable2 != null)) : false)
                {
                    int? nullable4;
                    int? nullable1;
                    int? nullable7;
                    byte? precision = criterion.precision;
                    if (precision != null)
                    {
                        nullable1 = new int?(precision.GetValueOrDefault());
                    }
                    else
                    {
                        nullable4 = null;
                        nullable1 = nullable4;
                    }
                    nullable2 = nullable1;
                    precision = this.precision;
                    if (precision != null)
                    {
                        nullable7 = new int?(precision.GetValueOrDefault());
                    }
                    else
                    {
                        nullable4 = null;
                        nullable7 = nullable4;
                    }
                    size = nullable7;
                    if ((nullable2.GetValueOrDefault() == size.GetValueOrDefault()) ? ((nullable2 != null) == (size != null)) : false)
                    {
                        int? nullable8;
                        int? nullable9;
                        precision = criterion.scale;
                        if (precision != null)
                        {
                            nullable8 = new int?(precision.GetValueOrDefault());
                        }
                        else
                        {
                            nullable4 = null;
                            nullable8 = nullable4;
                        }
                        size = nullable8;
                        precision = this.scale;
                        if (precision != null)
                        {
                            nullable9 = new int?(precision.GetValueOrDefault());
                        }
                        else
                        {
                            nullable4 = null;
                            nullable9 = nullable4;
                        }
                        nullable2 = nullable9;
                        if (((size.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((size != null) == (nullable2 != null)) : false) && (criterion.direction == this.direction))
                        {
                            DBColumnType? dbType = criterion.dbType;
                            DBColumnType? nullable6 = this.dbType;
                            return ((dbType.GetValueOrDefault() == nullable6.GetValueOrDefault()) ? ((dbType != null) == (nullable6 != null)) : false);
                        }
                    }
                }
            }
            return false;
        }

        public static ParameterDirection GetDataParameterDirection(SprocParameterDirection direction)
        {
            switch (direction)
            {
                case SprocParameterDirection.Input:
                    return ParameterDirection.Input;

                case SprocParameterDirection.Output:
                    return ParameterDirection.Output;

                case SprocParameterDirection.InputOutput:
                    return ParameterDirection.InputOutput;

                case SprocParameterDirection.ReturnValue:
                    return ParameterDirection.ReturnValue;
            }
            throw new InvalidOperationException(direction.ToString());
        }

        public override int GetHashCode() => 
            HashCodeHelper.FinishGeneric<int?, byte?, byte?, SprocParameterDirection, DBColumnType?>(base.GetHashCode(), this.size, this.precision, this.scale, this.direction, this.dbType);

        public int? Size
        {
            get => 
                this.size;
            set => 
                this.size = value;
        }

        public byte? Precision
        {
            get => 
                this.precision;
            set => 
                this.precision = value;
        }

        public byte? Scale
        {
            get => 
                this.scale;
            set => 
                this.scale = value;
        }

        public DBColumnType? DbType
        {
            get => 
                this.dbType;
            set => 
                this.dbType = value;
        }

        public SprocParameterDirection Direction
        {
            get => 
                this.direction;
            set => 
                this.direction = value;
        }
    }
}

