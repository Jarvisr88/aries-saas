namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingConnectionPointsType : OfficeDrawingIntPropertyBase
    {
        public DrawingConnectionPointsType()
        {
            base.Value = 1;
        }

        public DrawingConnectionPointsType(ConnectionPointsType type)
        {
            base.Value = (int) type;
        }

        public override bool Complex =>
            false;

        public ConnectionPointsType PointsType
        {
            get => 
                (ConnectionPointsType) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

