namespace DevExpress.Data.Helpers
{
    using System;

    public class ServerModeServerAndChannelModel
    {
        public readonly double ConstantPart;
        public readonly double TakeCoeff;
        public readonly double ScanCoeff;

        public ServerModeServerAndChannelModel(double constPart, double takeCoeff, double scanCoeff);
        public override string ToString();
    }
}

