namespace ActiproSoftware.MarkupLabel
{
    using #g;
    using #H;
    using ActiproSoftware.Drawing;
    using System;
    using System.Drawing;
    using System.Text;

    public class MarkupLabelCssData
    {
        private System.Drawing.Color #tte = System.Drawing.Color.Empty;
        private System.Drawing.Color #eUb = System.Drawing.Color.Empty;
        private string #EEd;
        private float #KK;
        private MarkupLabelFontStyle #ute;
        private MarkupLabelFontWeight #vte;
        private MarkupLabelTextDecoration #wte;

        public static MarkupLabelCssData Parse(MarkupLabelCssData cssData, string css)
        {
            string[] strArray;
            int num;
            if (css == null)
            {
                return cssData;
            }
            else
            {
                char[] separator = new char[] { ';' };
                strArray = css.Split(separator);
                num = 0;
            }
            goto TR_0046;
        TR_0001:
            num++;
            goto TR_0046;
        TR_000E:
            cssData.FontWeight = MarkupLabelFontWeight.Bold;
            goto TR_0001;
        TR_0046:
            while (true)
            {
                if (num < strArray.Length)
                {
                    string str = strArray[num];
                    if (str.Trim().Length <= 0)
                    {
                        goto TR_0001;
                    }
                    else
                    {
                        char[] separator = new char[] { ':' };
                        string[] strArray2 = str.Trim().Split(separator);
                        if (strArray2.Length != 2)
                        {
                            goto TR_0001;
                        }
                        else
                        {
                            string str2 = strArray2[1].Trim().ToLower();
                            if (str2.Length <= 0)
                            {
                                goto TR_0001;
                            }
                            else
                            {
                                string str3 = strArray2[0].Trim();
                                if (str3 == null)
                                {
                                    goto TR_0001;
                                }
                                else
                                {
                                    uint num2 = #r.#g2i(str3);
                                    if (num2 > 0x3d7e6258)
                                    {
                                        if (num2 > 0xc18cc803)
                                        {
                                            if (num2 != 0xc662ae27)
                                            {
                                                if ((num2 == 0xff5ed19e) && (str3 == #G.#eg(0x2c7a)))
                                                {
                                                    try
                                                    {
                                                        cssData.FontSize = ((str2.Length <= 2) || char.IsNumber(str2[str2.Length - 2])) ? Convert.ToSingle(str2) : Convert.ToSingle(str2.Substring(0, str2.Length - 2));
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                            }
                                            else if (str3 == #G.#eg(0x2c47))
                                            {
                                                try
                                                {
                                                    cssData.BackgroundColor = UIColor.FromWebColor(str2).ToColor();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            goto TR_0001;
                                        }
                                        else if (num2 == 0x79a90ea7)
                                        {
                                            if ((str3 == #G.#eg(0x2ca9)) && (str2 != null))
                                            {
                                                if (str2 == #G.#eg(0x2d29))
                                                {
                                                    cssData.TextDecoration = MarkupLabelTextDecoration.LineThrough;
                                                }
                                                else if (str2 == #G.#eg(0x2d3a))
                                                {
                                                    cssData.TextDecoration = MarkupLabelTextDecoration.None;
                                                }
                                                else if (str2 == #G.#eg(0x2d43))
                                                {
                                                    cssData.TextDecoration = MarkupLabelTextDecoration.Underline;
                                                }
                                            }
                                            goto TR_0001;
                                        }
                                        else if ((num2 != 0xc18cc803) || ((str3 != #G.#eg(0x2c87)) || (str2 == null)))
                                        {
                                            goto TR_0001;
                                        }
                                        else
                                        {
                                            num2 = #r.#g2i(str2);
                                            if (num2 > 0x889bd751)
                                            {
                                                if (num2 > 0xad558101)
                                                {
                                                    if (num2 > 0xddcad4b4)
                                                    {
                                                        if (num2 == 0xde96f676)
                                                        {
                                                            if (str2 != #G.#eg(0x2cd2))
                                                            {
                                                                goto TR_0001;
                                                            }
                                                            goto TR_000E;
                                                        }
                                                        else if ((num2 != 0xe68b9c52) || (str2 != #G.#eg(0x2d0a)))
                                                        {
                                                            goto TR_0001;
                                                        }
                                                    }
                                                    else if (num2 == 0xc3e7575d)
                                                    {
                                                        if (str2 != #G.#eg(0x2ce9))
                                                        {
                                                            goto TR_0001;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if ((num2 != 0xddcad4b4) || (str2 != #G.#eg(0x2ccd)))
                                                        {
                                                            goto TR_0001;
                                                        }
                                                        goto TR_000E;
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    if (num2 == 0x9973fce4)
                                                    {
                                                        if (str2 != #G.#eg(0x2cfd))
                                                        {
                                                            goto TR_0001;
                                                        }
                                                    }
                                                    else if (num2 == 0xaa3008db)
                                                    {
                                                        if (str2 != #G.#eg(0x2cf3))
                                                        {
                                                            goto TR_0001;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if ((num2 != 0xad558101) || (str2 != #G.#eg(0x2cdb)))
                                                        {
                                                            goto TR_0001;
                                                        }
                                                        goto TR_000E;
                                                    }
                                                    break;
                                                }
                                                goto TR_000E;
                                            }
                                            else
                                            {
                                                if (num2 > 0x3d9f3cff)
                                                {
                                                    if (num2 == 0x454ff132)
                                                    {
                                                        if (str2 == #G.#eg(0x2cee))
                                                        {
                                                            break;
                                                        }
                                                        goto TR_0001;
                                                    }
                                                    else if (num2 == 0x6733d49c)
                                                    {
                                                        if (str2 == #G.#eg(0x2ce4))
                                                        {
                                                            break;
                                                        }
                                                        goto TR_0001;
                                                    }
                                                    else if ((num2 != 0x889bd751) || (str2 != #G.#eg(0x2cbe)))
                                                    {
                                                        goto TR_0001;
                                                    }
                                                }
                                                else if (num2 == 0x8c73ca6)
                                                {
                                                    if (str2 != #G.#eg(0x2cc3))
                                                    {
                                                        goto TR_0001;
                                                    }
                                                }
                                                else if (num2 == 0x2b98a2b0)
                                                {
                                                    if (str2 == #G.#eg(0x2cf8))
                                                    {
                                                        break;
                                                    }
                                                    goto TR_0001;
                                                }
                                                else if ((num2 != 0x3d9f3cff) || (str2 != #G.#eg(0x2cc8)))
                                                {
                                                    goto TR_0001;
                                                }
                                                goto TR_000E;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (num2 == 0xf0cc3d8)
                                        {
                                            if ((str3 == #G.#eg(0x2c98)) && (str2 != null))
                                            {
                                                if ((str2 == #G.#eg(0x2d13)) || (str2 == #G.#eg(0x2d1c)))
                                                {
                                                    cssData.FontStyle = MarkupLabelFontStyle.Italic;
                                                }
                                                else if (str2 == #G.#eg(0x2d0a))
                                                {
                                                    cssData.FontStyle = MarkupLabelFontStyle.Normal;
                                                }
                                            }
                                        }
                                        else if (num2 == 0x20c4ce3f)
                                        {
                                            if (str3 == #G.#eg(0x2c69))
                                            {
                                                cssData.FontFamily = str2;
                                            }
                                        }
                                        else if ((num2 == 0x3d7e6258) && (str3 == #G.#eg(0x2c60)))
                                        {
                                            try
                                            {
                                                cssData.Color = UIColor.FromWebColor(str2).ToColor();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        goto TR_0001;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return cssData;
                }
                break;
            }
            cssData.FontWeight = MarkupLabelFontWeight.Normal;
            goto TR_0001;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.#eUb != System.Drawing.Color.Empty)
            {
                builder.Append(string.Format(#G.#eg(0x2d50), UIColor.FromArgb(this.#eUb).ToWebColor()));
            }
            if (this.#tte != System.Drawing.Color.Empty)
            {
                builder.Append(string.Format(#G.#eg(0x2d61), UIColor.FromArgb(this.#tte).ToWebColor()));
            }
            if ((this.#EEd != null) && (this.#EEd != #G.#eg(0x2c25)))
            {
                builder.Append(string.Format(#G.#eg(0x2d82), this.#EEd));
            }
            if (this.#KK != 0f)
            {
                builder.Append(string.Format(#G.#eg(0x2d9b), this.#KK));
            }
            MarkupLabelFontStyle style = this.#ute;
            if (style == MarkupLabelFontStyle.Normal)
            {
                builder.Append(#G.#eg(0x2dd1));
            }
            else if (style == MarkupLabelFontStyle.Italic)
            {
                builder.Append(#G.#eg(0x2db4));
            }
            MarkupLabelFontWeight weight = this.#vte;
            if (weight == MarkupLabelFontWeight.Normal)
            {
                builder.Append(#G.#eg(0x2e0b));
            }
            else if (weight == MarkupLabelFontWeight.Bold)
            {
                builder.Append(#G.#eg(0x2dee));
            }
            switch (this.#wte)
            {
                case MarkupLabelTextDecoration.None:
                    builder.Append(#G.#eg(0x2e4d));
                    break;

                case MarkupLabelTextDecoration.Underline:
                    builder.Append(#G.#eg(0x2e6a));
                    break;

                case MarkupLabelTextDecoration.LineThrough:
                    builder.Append(#G.#eg(0x2e28));
                    break;

                default:
                    break;
            }
            return builder.ToString().Trim();
        }

        public System.Drawing.Color BackgroundColor
        {
            get => 
                this.#tte;
            set => 
                this.#tte = value;
        }

        public System.Drawing.Color Color
        {
            get => 
                this.#eUb;
            set => 
                this.#eUb = value;
        }

        public string FontFamily
        {
            get => 
                this.#EEd;
            set => 
                this.#EEd = value;
        }

        public float FontSize
        {
            get => 
                this.#KK;
            set => 
                this.#KK = value;
        }

        public MarkupLabelFontStyle FontStyle
        {
            get => 
                this.#ute;
            set => 
                this.#ute = value;
        }

        public MarkupLabelFontWeight FontWeight
        {
            get => 
                this.#vte;
            set => 
                this.#vte = value;
        }

        public MarkupLabelTextDecoration TextDecoration
        {
            get => 
                this.#wte;
            set => 
                this.#wte = value;
        }
    }
}

