﻿namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class Win1251BulgarianEncodingDetector : BulgarianEncodingDetector
    {
        private static readonly byte[] charToOrderMap = new byte[] { 
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xfe, 0xff, 0xff, 0xfe, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfc, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xfd, 0x4d, 90, 0x63, 100, 0x48, 0x6d, 0x6b, 0x65, 0x4f, 0xb9, 0x51, 0x66, 0x4c, 0x5e, 0x52,
            110, 0xba, 0x6c, 0x5b, 0x4a, 0x77, 0x54, 0x60, 0x6f, 0xbb, 0x73, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xfd, 0x41, 0x45, 70, 0x42, 0x3f, 0x44, 0x70, 0x67, 0x5c, 0xc2, 0x68, 0x5f, 0x56, 0x57, 0x47,
            0x74, 0xc3, 0x55, 0x5d, 0x61, 0x71, 0xc4, 0xc5, 0xc6, 0xc7, 200, 0xfd, 0xfd, 0xfd, 0xfd, 0xfd,
            0xce, 0xcf, 0xd0, 0xd1, 210, 0xd3, 0xd4, 0xd5, 120, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xdb, 220,
            0xdd, 0x4e, 0x40, 0x53, 0x79, 0x62, 0x75, 0x69, 0xde, 0xdf, 0xe0, 0xe1, 0xe2, 0xe3, 0xe4, 0xe5,
            0x58, 230, 0xe7, 0xe8, 0xe9, 0x7a, 0x59, 0x6a, 0xea, 0xeb, 0xec, 0xed, 0xee, 0x2d, 0xef, 240,
            0x49, 80, 0x76, 0x72, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0x3e, 0x3a, 0xf6, 0xf7, 0xf8, 0xf9, 250,
            0x1f, 0x20, 0x23, 0x2b, 0x25, 0x2c, 0x37, 0x2f, 40, 0x3b, 0x21, 0x2e, 0x26, 0x24, 0x29, 30,
            0x27, 0x1c, 0x22, 0x33, 0x30, 0x31, 0x35, 50, 0x36, 0x39, 0x3d, 0xfb, 0x43, 0xfc, 60, 0x38,
            1, 0x12, 9, 20, 11, 3, 0x17, 15, 2, 0x1a, 12, 10, 14, 6, 4, 13,
            7, 8, 5, 0x13, 0x1d, 0x19, 0x16, 0x15, 0x1b, 0x18, 0x11, 0x4b, 0x34, 0xfd, 0x2a, 0x10
        };

        protected internal override byte[] CharacterToOrderMap =>
            charToOrderMap;

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0x4e3);
    }
}

