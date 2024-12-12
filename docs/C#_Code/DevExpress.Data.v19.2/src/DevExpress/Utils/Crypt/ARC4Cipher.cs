namespace DevExpress.Utils.Crypt
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class ARC4Cipher
    {
        private const int bufferSize = 0x100;
        private readonly byte[] keyBuffer = new byte[0x100];
        private readonly byte[] s = new byte[0x100];
        private int x;
        private int y;

        public ARC4Cipher(byte[] key)
        {
            this.UpdateKey(key);
        }

        public byte[] Decrypt(byte[] input) => 
            this.Encrypt(input);

        public byte Decrypt(byte input) => 
            (byte) (input ^ this.GenerateKeyWord());

        public void Decrypt(byte[] input, int inputOffset, byte[] output, int outputOffset, int count)
        {
            this.Encrypt(input, inputOffset, output, outputOffset, count);
        }

        public byte[] Encrypt(byte[] input)
        {
            Guard.ArgumentNotNull(input, "input");
            int length = input.Length;
            byte[] output = new byte[length];
            this.Encrypt(input, 0, output, 0, length);
            return output;
        }

        public byte Encrypt(byte input) => 
            (byte) (input ^ this.GenerateKeyWord());

        public void Encrypt(byte[] input, int inputOffset, byte[] output, int outputOffset, int count)
        {
            Guard.ArgumentNotNull(input, "input");
            Guard.ArgumentNotNull(output, "output");
            for (int i = 0; i < count; i++)
            {
                output[i + outputOffset] = (byte) (input[i + inputOffset] ^ this.GenerateKeyWord());
            }
        }

        public unsafe void EncryptSelf(byte[] data)
        {
            int length = data.Length;
            for (int i = 0; i < length; i++)
            {
                byte* numPtr1 = &(data[i]);
                numPtr1[0] = (byte) (numPtr1[0] ^ this.GenerateKeyWord());
            }
        }

        private byte GenerateKeyWord()
        {
            this.x = (this.x + 1) & 0xff;
            this.y = (this.y + this.s[this.x]) & 0xff;
            return this.s[Swap(this.s, this.x, this.y)];
        }

        public void Reset()
        {
            this.keyBuffer.CopyTo(this.s, 0);
            this.x = 0;
            this.y = 0;
        }

        public void Reset(int position)
        {
            this.Reset();
            for (int i = 0; i < position; i++)
            {
                this.GenerateKeyWord();
            }
        }

        private static byte Swap(byte[] buffer, int i, int j)
        {
            byte num = buffer[i];
            byte num2 = buffer[j];
            buffer[i] = num2;
            buffer[j] = num;
            return (byte) ((num + num2) & 0xff);
        }

        public void UpdateKey(byte[] key)
        {
            for (int i = 0; i < 0x100; i++)
            {
                this.keyBuffer[i] = (byte) i;
            }
            int num = 0;
            int j = 0;
            int num3 = 0;
            int length = key.Length;
            IEnumerator<byte> enumerator = key.GetEnumerator();
            foreach (byte num7 in this.keyBuffer)
            {
                enumerator.MoveNext();
                j = ((j + num7) + enumerator.Current) & 0xff;
                Swap(this.keyBuffer, num++, j);
                if (++num3 == length)
                {
                    num3 = 0;
                    enumerator.Reset();
                }
            }
            this.Reset();
        }
    }
}

