namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [BrickExporter(typeof(ToggleSwitchBrickExporter))]
    public class ToggleSwitchBrick : VisualBrick
    {
        private string checkText;
        private bool isOnCore;
        private float toDpi;

        public ToggleSwitchBrick() : this(NullBrickOwner.Instance)
        {
        }

        public ToggleSwitchBrick(BrickStyle style) : base(style)
        {
            this.toDpi = 300f;
        }

        public ToggleSwitchBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.toDpi = 300f;
        }

        public ToggleSwitchBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor) : base(sides, borderWidth, borderColor, backColor)
        {
            this.toDpi = 300f;
        }

        public ToggleSwitchBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : this(sides, borderWidth, borderColor, backColor)
        {
            base.Style.ForeColor = foreColor;
        }

        [XtraSerializableProperty, DefaultValue((string) null)]
        public string CheckText
        {
            get => 
                this.checkText;
            set => 
                this.checkText = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal bool ShouldAlignToBottom { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal float ToDpi
        {
            get => 
                this.toDpi;
            set => 
                this.toDpi = value;
        }

        internal string CheckStateText =>
            (this.CheckText != null) ? this.CheckText : (this.IsOn ? "On" : "Off");

        [Description("")]
        public SizeF CheckSize =>
            (SizeF) GraphicsUnitConverter.Convert(new Size(50, 0x13), (float) 96f, (float) 300f);

        [Description(""), XtraSerializableProperty, DefaultValue(false)]
        public bool IsOn
        {
            get => 
                this.isOnCore;
            set
            {
                if (value != this.IsOn)
                {
                    this.isOnCore = value;
                }
            }
        }

        public ArrayList ImageList { get; set; }

        [Description("")]
        public override string BrickType =>
            "ToggleSwitch";
    }
}

