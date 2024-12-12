namespace DevExpress.Office.NumberConverters
{
    using System;

    public class BulletNumberConverter : OrdinalBasedNumberConverter
    {
        public override string ConvertNumberCore(long value) => 
            string.Format("•", new object[0]);

        protected internal override NumberingFormat Type =>
            NumberingFormat.Bullet;
    }
}

