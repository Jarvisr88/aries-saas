namespace Devart.DbMonitor
{
    using Devart.Common;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    internal class k : Devart.DbMonitor.g, IDisposable
    {
        public const int a = 0x3e8;
        public const int b = 0x1388;
        public const int c = 0x3e8;
        private string d;
        private int e;
        private int f = 0x1388;
        private int g = 0x3e8;
        private TcpClient h;
        private NetworkStream i;
        private int j;
        private bool k;

        public int a() => 
            this.g;

        public void a(Devart.DbMonitor.o A_0)
        {
            try
            {
                this.a((int) A_0.v());
                A_0.a(this);
            }
            catch (IOException)
            {
                this.c();
            }
        }

        public void a(byte A_0)
        {
            this.i.WriteByte(A_0);
        }

        public void a(int A_0)
        {
            byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(A_0));
            if (!BitConverter.IsLittleEndian)
            {
                a(bytes);
            }
            this.i.Write(bytes, 0, 4);
        }

        public void a(string A_0)
        {
            byte[] bytes = new byte[0];
            if (A_0 != null)
            {
                bytes = Encoding.UTF8.GetBytes(A_0);
            }
            if ((bytes == null) || (bytes.Length == 0))
            {
                this.a(0);
            }
            else
            {
                this.a(bytes.Length);
                this.i.Write(bytes, 0, bytes.Length);
            }
        }

        private static void a(byte[] A_0)
        {
            for (int i = 0; i < (A_0.Length / 2); i++)
            {
                byte num2 = A_0[i];
                A_0[i] = A_0[(A_0.Length - i) - 1];
                A_0[(A_0.Length - i) - 1] = num2;
            }
        }

        public bool b() => 
            this.i != null;

        public void b(int A_0)
        {
            this.g = A_0;
        }

        public void b(string A_0)
        {
            this.d = A_0;
        }

        public void c()
        {
            if (this.i != null)
            {
                this.i.Close();
                this.i = null;
            }
            if (this.h != null)
            {
                this.h.Close();
                this.h = null;
            }
        }

        public void c(int A_0)
        {
            this.f = A_0;
        }

        public string d() => 
            this.d;

        public void d(int A_0)
        {
            this.e = A_0;
        }

        public int e() => 
            this.f;

        public int f() => 
            this.e;

        public bool g()
        {
            if (this.k && (Environment.TickCount < (this.j + this.f)))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.d()))
            {
                this.b("localhost");
            }
            if (this.f() == 0)
            {
                this.d(0x3e8);
            }
            try
            {
                IPAddress address;
                this.h = !IPAddress.TryParse(this.d, out address) ? new TcpClient() : new TcpClient(address.AddressFamily);
                this.h.NoDelay = true;
                this.h.SendTimeout = this.a();
                if (!Utils.IsIpAddress(this.d()))
                {
                    this.h.Connect(this.d, this.e);
                }
                else
                {
                    IPAddress address2 = IPAddress.Parse(this.d());
                    this.h.Connect(address2, this.e);
                }
                this.i = this.h.GetStream();
                this.k = false;
                return true;
            }
            catch (SocketException)
            {
                this.c();
                this.k = true;
                this.j = Environment.TickCount;
                return false;
            }
        }

        public void h()
        {
            if (this.b())
            {
                this.c();
            }
        }

        public bool i()
        {
            if (!this.b())
            {
                return false;
            }
            try
            {
                this.a(6);
            }
            catch (IOException)
            {
                this.c();
            }
            return this.b();
        }
    }
}

