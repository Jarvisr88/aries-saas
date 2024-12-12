namespace ActiproSoftware.Drawing
{
    using System;

    public abstract class Gradient : BackgroundFill
    {
        public override bool Equals(object obj) => 
            (obj != null) && (obj is Gradient);

        public override int GetHashCode() => 
            this.GetHashCode();
    }
}

