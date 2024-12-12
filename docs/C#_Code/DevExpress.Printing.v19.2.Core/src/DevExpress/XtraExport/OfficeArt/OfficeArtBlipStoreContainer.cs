namespace DevExpress.XtraExport.OfficeArt
{
    using System;

    internal class OfficeArtBlipStoreContainer : OfficeArtCompositePartBase
    {
        public override int HeaderInstanceInfo =>
            base.Items.Count;

        public override int HeaderTypeCode =>
            0xf001;

        public override int HeaderVersion =>
            15;
    }
}

