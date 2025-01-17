﻿namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class Koi8rEncodingDetector : CyrillicEncodingDetector
    {
        private static readonly byte[] charToOrderMap = new byte[] { 
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xfe, 0xff, 0xff, 0xfe, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xfd, 0x8e, 0x8f, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 150, 0x97, 0x98, 0x4a, 0x99, 0x4b, 0x9a,
            0x9b, 0x9c, 0x9d, 0x9e, 0x9f, 160, 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xfd, 0x47, 0xac, 0x42, 0xad, 0x41, 0xae, 0x4c, 0xaf, 0x40, 0xb0, 0xb1, 0x4d, 0x48, 0xb2, 0x45,
            0x43, 0xb3, 0x4e, 0x49, 180, 0xb5, 0x4f, 0xb6, 0xb7, 0xb8, 0xb9, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xbf, 0xc0, 0xc1, 0xc2, 0xc3, 0xc4, 0xc5, 0xc6, 0xc7, 200, 0xc9, 0xca, 0xcb, 0xcc, 0xcd, 0xce,
            0xcf, 0xd0, 0xd1, 210, 0xd3, 0xd4, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xdb, 220, 0xdd, 0xde,
            0xdf, 0xe0, 0xe1, 0x44, 0xe2, 0xe3, 0xe4, 0xe5, 230, 0xe7, 0xe8, 0xe9, 0xea, 0xeb, 0xec, 0xed,
            0xee, 0xef, 240, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 250, 0xfb, 0xfc, 0xfd,
            0x1b, 3, 0x15, 0x1c, 13, 2, 0x27, 0x13, 0x1a, 4, 0x17, 11, 8, 12, 5, 1,
            15, 0x10, 9, 7, 6, 14, 0x18, 10, 0x11, 0x12, 20, 0x19, 30, 0x1d, 0x16, 0x36,
            0x3b, 0x25, 0x2c, 0x3a, 0x29, 0x30, 0x35, 0x2e, 0x37, 0x2a, 60, 0x24, 0x31, 0x26, 0x1f, 0x22,
            0x23, 0x2b, 0x2d, 0x20, 40, 0x34, 0x38, 0x21, 0x3d, 0x3e, 0x33, 0x39, 0x2f, 0x3f, 50, 70
        };

        protected internal override byte[] CharacterToOrderMap =>
            charToOrderMap;

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0x5182);
    }
}

