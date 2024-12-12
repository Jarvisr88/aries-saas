namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_GRADIENT_MESH_PATCH
    {
        private D2D_POINT_2F point00;
        private D2D_POINT_2F point01;
        private D2D_POINT_2F point02;
        private D2D_POINT_2F point03;
        private D2D_POINT_2F point10;
        private D2D_POINT_2F point11;
        private D2D_POINT_2F point12;
        private D2D_POINT_2F point13;
        private D2D_POINT_2F point20;
        private D2D_POINT_2F point21;
        private D2D_POINT_2F point22;
        private D2D_POINT_2F point23;
        private D2D_POINT_2F point30;
        private D2D_POINT_2F point31;
        private D2D_POINT_2F point32;
        private D2D_POINT_2F point33;
        private D2D1_COLOR_F color00;
        private D2D1_COLOR_F color03;
        private D2D1_COLOR_F color30;
        private D2D1_COLOR_F color33;
        private D2D1_PATCH_EDGE_MODE topEdgeMode;
        private D2D1_PATCH_EDGE_MODE leftEdgeMode;
        private D2D1_PATCH_EDGE_MODE bottomEdgeMode;
        private D2D1_PATCH_EDGE_MODE rightEdgeMode;
        public D2D_POINT_2F Point00
        {
            get => 
                this.point00;
            set => 
                this.point00 = value;
        }
        public D2D_POINT_2F Point01
        {
            get => 
                this.point01;
            set => 
                this.point01 = value;
        }
        public D2D_POINT_2F Point02
        {
            get => 
                this.point02;
            set => 
                this.point02 = value;
        }
        public D2D_POINT_2F Point03
        {
            get => 
                this.point03;
            set => 
                this.point03 = value;
        }
        public D2D_POINT_2F Point10
        {
            get => 
                this.point10;
            set => 
                this.point10 = value;
        }
        public D2D_POINT_2F Point11
        {
            get => 
                this.point11;
            set => 
                this.point11 = value;
        }
        public D2D_POINT_2F Point12
        {
            get => 
                this.point12;
            set => 
                this.point12 = value;
        }
        public D2D_POINT_2F Point13
        {
            get => 
                this.point13;
            set => 
                this.point13 = value;
        }
        public D2D_POINT_2F Point20
        {
            get => 
                this.point20;
            set => 
                this.point20 = value;
        }
        public D2D_POINT_2F Point21
        {
            get => 
                this.point21;
            set => 
                this.point21 = value;
        }
        public D2D_POINT_2F Point22
        {
            get => 
                this.point22;
            set => 
                this.point22 = value;
        }
        public D2D_POINT_2F Point23
        {
            get => 
                this.point23;
            set => 
                this.point23 = value;
        }
        public D2D_POINT_2F Point30
        {
            get => 
                this.point30;
            set => 
                this.point30 = value;
        }
        public D2D_POINT_2F Point31
        {
            get => 
                this.point31;
            set => 
                this.point31 = value;
        }
        public D2D_POINT_2F Point32
        {
            get => 
                this.point32;
            set => 
                this.point32 = value;
        }
        public D2D_POINT_2F Point33
        {
            get => 
                this.point33;
            set => 
                this.point33 = value;
        }
        public D2D1_COLOR_F Color00
        {
            get => 
                this.color00;
            set => 
                this.color00 = value;
        }
        public D2D1_COLOR_F Color03
        {
            get => 
                this.color03;
            set => 
                this.color03 = value;
        }
        public D2D1_COLOR_F Color30
        {
            get => 
                this.color30;
            set => 
                this.color30 = value;
        }
        public D2D1_COLOR_F Color33
        {
            get => 
                this.color33;
            set => 
                this.color33 = value;
        }
        public D2D1_PATCH_EDGE_MODE TopEdgeMode
        {
            get => 
                this.topEdgeMode;
            set => 
                this.topEdgeMode = value;
        }
        public D2D1_PATCH_EDGE_MODE LeftEdgeMode
        {
            get => 
                this.leftEdgeMode;
            set => 
                this.leftEdgeMode = value;
        }
        public D2D1_PATCH_EDGE_MODE BottomEdgeMode
        {
            get => 
                this.bottomEdgeMode;
            set => 
                this.bottomEdgeMode = value;
        }
        public D2D1_PATCH_EDGE_MODE RightEdgeMode
        {
            get => 
                this.rightEdgeMode;
            set => 
                this.rightEdgeMode = value;
        }
    }
}

