namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal abstract class TTFStream
    {
        protected const string positionError = "error when working with .ttf file";
        public const int SizeOf_Byte = 1;
        public const int SizeOf_Char = 1;
        public const int SizeOf_UShort = 2;
        public const int SizeOf_Short = 2;
        public const int SizeOf_ULong = 4;
        public const int SizeOf_Long = 4;
        public const int SizeOf_Fixed = 4;
        public const int SizeOf_FWord = 2;
        public const int SizeOf_UFWord = 2;
        public const int SizeOf_F2Dot14 = 2;
        public const int SizeOf_InternationalDate = 8;
        public const int SizeOf_PANOSE = 10;

        protected TTFStream()
        {
        }

        protected abstract byte _read();
        protected abstract void _seek(int newPosition);
        protected abstract void _write(byte value);
        public static float FixedToFloat(byte[] value)
        {
            if (value.Length != 4)
            {
                return 0f;
            }
            byte[] buffer = new byte[4];
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < 4; i++)
                {
                    buffer[i] = value[(4 - i) - 1];
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    buffer[i] = value[i];
                }
            }
            short num = BitConverter.ToInt16(buffer, 2);
            ushort num2 = BitConverter.ToUInt16(buffer, 0);
            if (num2 == 0)
            {
                return Convert.ToSingle(num);
            }
            double num5 = Math.Pow(10.0, Math.Ceiling(Math.Log10((double) num2)));
            return Convert.ToSingle((double) (num + ((((double) num2) / num5) * Math.Sign(num))));
        }

        public void Move(int offset)
        {
            this.Seek(this.Position + offset);
        }

        public void Pad4()
        {
            int num = this.Position % 4;
            for (int i = 4; i > num; i--)
            {
                this._write(0);
            }
        }

        public byte ReadByte()
        {
            if ((this.Position < 0) || (this.Position >= this.Length))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            return this._read();
        }

        public byte[] ReadBytes(int count)
        {
            if ((this.Position < 0) || (this.Position >= ((this.Length - count) + 1)))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i++)
            {
                buffer[i] = this._read();
            }
            return buffer;
        }

        public sbyte ReadChar() => 
            Convert.ToSByte(this.ReadByte());

        public byte[] ReadF2Dot14()
        {
            if ((this.Position < 0) || (this.Position >= (this.Length - 1)))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            return new byte[] { this._read(), this._read() };
        }

        public short ReadFWord() => 
            this.ReadShort();

        public int ReadLong()
        {
            if ((this.Position < 0) || (this.Position >= (this.Length - 3)))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            return (!BitConverter.IsLittleEndian ? (((this._read() + (this._read() << 8)) + (this._read() << 0x10)) + (this._read() << 0x18)) : ((((this._read() << 0x18) + (this._read() << 0x10)) + (this._read() << 8)) + this._read()));
        }

        public TTFPanose ReadPanose()
        {
            if ((this.Position < 0) || (this.Position >= (this.Length - 9)))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            return new TTFPanose { 
                bFamilyType = this._read(),
                bSerifType = this._read(),
                bWeight = this._read(),
                bProportion = this._read(),
                bContrast = this._read(),
                bStrokeVariation = this._read(),
                bArmStyle = this._read(),
                bLetterForm = this._read(),
                bMidline = this._read(),
                bXHeight = this._read()
            };
        }

        public short ReadShort()
        {
            if ((this.Position < 0) || (this.Position >= (this.Length - 1)))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            return (!BitConverter.IsLittleEndian ? ((short) (this._read() + (this._read() << 8))) : ((short) ((this._read() << 8) + this._read())));
        }

        public ushort ReadUFWord() => 
            this.ReadUShort();

        public uint ReadULong()
        {
            if ((this.Position < 0) || (this.Position >= (this.Length - 3)))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            return (!BitConverter.IsLittleEndian ? ((uint) (((this._read() + (this._read() << 8)) + (this._read() << 0x10)) + (this._read() << 0x18))) : ((uint) ((((this._read() << 0x18) + (this._read() << 0x10)) + (this._read() << 8)) + this._read())));
        }

        public string ReadUnicodeString(int lengthInBytes)
        {
            string str = "";
            for (int i = 0; i < lengthInBytes; i += 2)
            {
                str = str + ((char) this.ReadUShort()).ToString();
            }
            return str;
        }

        public ushort ReadUShort()
        {
            if ((this.Position < 0) || (this.Position >= (this.Length - 1)))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            return (!BitConverter.IsLittleEndian ? ((ushort) (this._read() + (this._read() << 8))) : ((ushort) ((this._read() << 8) + this._read())));
        }

        public void Seek(int newPosition)
        {
            if ((newPosition < 0) || (newPosition > this.Length))
            {
                throw new TTFFileException("error when working with .ttf file");
            }
            this._seek(newPosition);
        }

        public void WriteByte(byte value)
        {
            this._write(value);
        }

        public void WriteBytes(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                this._write(buffer[i]);
            }
        }

        public void WriteBytes(byte[] buffer, bool reverse)
        {
            if (!reverse)
            {
                this.WriteBytes(buffer);
            }
            else
            {
                for (int i = buffer.Length - 1; i >= 0; i--)
                {
                    this._write(buffer[i]);
                }
            }
        }

        public void WriteChar(sbyte value)
        {
            this._write((byte) value);
        }

        public void WriteFWord(short value)
        {
            this.WriteShort(value);
        }

        public void WriteLong(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.WriteBytes(bytes, BitConverter.IsLittleEndian);
        }

        public void WritePanose(TTFPanose value)
        {
            this._write(value.bFamilyType);
            this._write(value.bSerifType);
            this._write(value.bWeight);
            this._write(value.bProportion);
            this._write(value.bContrast);
            this._write(value.bStrokeVariation);
            this._write(value.bArmStyle);
            this._write(value.bLetterForm);
            this._write(value.bMidline);
            this._write(value.bXHeight);
        }

        public void WriteShort(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.WriteBytes(bytes, BitConverter.IsLittleEndian);
        }

        public void WriteUFWord(ushort value)
        {
            this.WriteUShort(value);
        }

        public void WriteULong(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.WriteBytes(bytes, BitConverter.IsLittleEndian);
        }

        public void WriteUnicodeString(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                this.WriteUShort(value[i]);
            }
        }

        public void WriteUShort(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.WriteBytes(bytes, BitConverter.IsLittleEndian);
        }

        public abstract int Position { get; }

        public abstract int Length { get; }
    }
}

