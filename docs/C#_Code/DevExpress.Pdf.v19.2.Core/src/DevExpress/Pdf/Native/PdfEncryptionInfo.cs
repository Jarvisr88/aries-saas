namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Utils;
    using DevExpress.Utils.Crypt;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Numerics;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    public class PdfEncryptionInfo : PdfObject
    {
        [ThreadStatic]
        private static Aes providerValue = null;
        private const int hashLength = 0x20;
        private const int validationSaltLength = 8;
        private const int keySaltLength = 8;
        private const int keySaltPosition = 40;
        private const int aesV3HashLength = 0x30;
        private const int passwordLimit = 0x7f;
        private const int encryptedPermissionsLength = 0x10;
        private const int initializationVectorLength = 0x10;
        private const int defaultKeyLength = 40;
        private const string filterNameDictionaryKey = "Filter";
        private const string algorithmTypeDictionaryKey = "V";
        private const string cryptFiltersDictionaryKey = "CF";
        private const string streamFilterDictionaryKey = "StmF";
        private const string stringFilterDictionaryKey = "StrF";
        private const string securityHandlerRevisionDictionaryKey = "R";
        private const string ownerPasswordHashDictionaryKey = "O";
        private const string userPasswordHashDictionaryKey = "U";
        private const string ownerPasswordEncryptedKeyDictionaryKey = "OE";
        private const string userPasswordEncryptedKeyDictionaryKey = "UE";
        private const string encryptedPermissionsDictionaryKey = "Perms";
        private const string encryptionFlagsDictionaryKey = "P";
        private const string encryptMetadataDictionaryKey = "EncryptMetadata";
        private const string cryptFilterMethodDictionaryKey = "CFM";
        private const string keyLengthDictionaryKey = "Length";
        private const string standardFilterName = "Standard";
        private const string identityCryptFilterName = "Identity";
        private const string standardCryptFilterName = "StdCF";
        private static readonly byte[] passwordPadding = new byte[] { 
            40, 0xbf, 0x4e, 0x5e, 0x4e, 0x75, 0x8a, 0x41, 100, 0, 0x4e, 0x56, 0xff, 250, 1, 8,
            0x2e, 0x2e, 0, 0xb6, 0xd0, 0x68, 0x3e, 0x80, 0x2f, 12, 0xa9, 0xfe, 100, 0x53, 0x69, 0x7a
        };
        private readonly EncryptionAlgorithm algorithm;
        private readonly int securityHandlerRevision;
        private readonly byte[] ownerPasswordHash;
        private readonly byte[] ownerValidationSalt;
        private readonly byte[] ownerKeySalt;
        private readonly byte[] userPasswordHash;
        private readonly byte[] userValidationSalt;
        private readonly byte[] userKeySalt;
        private readonly bool encryptMetadata;
        private readonly byte[][] documentID;
        private readonly long encryptionFlags;
        private readonly PdfDocumentPermissionFlags permissionFlags;
        private readonly int extendedKeyLength;
        private readonly int actualKeyLength;
        private int keyLength;
        private string streamFilterName;
        private string stringFilterName;
        private string embeddedFileFilterName;
        private byte[] ownerPasswordEncryptedKey;
        private byte[] userPasswordEncryptedKey;
        private byte[] encryptedPermissions;
        private CryptMethod cryptMethod;
        private byte[] encryptionKey;

        public PdfEncryptionInfo(byte[][] documentID, PdfEncryptionParameters encryptionParameters)
        {
            this.documentID = documentID;
            this.encryptionFlags = encryptionParameters.Flags;
            this.permissionFlags = (PdfDocumentPermissionFlags) this.encryptionFlags;
            this.encryptMetadata = true;
            this.streamFilterName = "StdCF";
            this.stringFilterName = "StdCF";
            PdfEncryptionAlgorithm algorithm = encryptionParameters.Algorithm;
            if (algorithm != PdfEncryptionAlgorithm.AES256)
            {
                this.algorithm = EncryptionAlgorithm.Pdf15;
                this.securityHandlerRevision = 4;
                this.keyLength = 0x10;
                this.extendedKeyLength = 0x15;
                this.actualKeyLength = 0x10;
                this.cryptMethod = (algorithm == PdfEncryptionAlgorithm.ARC4) ? CryptMethod.V2 : CryptMethod.AESV2;
                byte[] input = PadOrTruncatePassword(encryptionParameters.UserPassword);
                string ownerPassword = encryptionParameters.OwnerPassword;
                byte[] key = this.ComputeOwnerEncryptionKey(string.IsNullOrEmpty(ownerPassword) ? input : PadOrTruncatePassword(ownerPassword));
                this.ownerPasswordHash = new ARC4Cipher(key).Encrypt(input);
                for (int i = 1; i <= 0x13; i++)
                {
                    this.ownerPasswordHash = new ARC4Cipher(this.XorKey(key, i)).Encrypt(this.ownerPasswordHash);
                }
                this.userPasswordHash = this.ComputeUserPasswordHash(input);
            }
            else
            {
                this.algorithm = EncryptionAlgorithm.Pdf20;
                this.securityHandlerRevision = 6;
                this.keyLength = 0x20;
                this.cryptMethod = CryptMethod.AESV3;
                byte[] userKey = new byte[0];
                byte[] initializationVector = new byte[0x10];
                this.encryptionKey = GenerateRandomData(this.keyLength);
                this.userValidationSalt = GenerateRandomData(8);
                this.userKeySalt = GenerateRandomData(8);
                this.ownerValidationSalt = GenerateRandomData(8);
                this.ownerKeySalt = GenerateRandomData(8);
                byte[] collection = TruncatePassword(encryptionParameters.UserPassword);
                List<byte> list = new List<byte>(collection);
                list.AddRange(this.userValidationSalt);
                byte[] sourceArray = this.ComputeHash(collection, list.ToArray(), userKey);
                this.userPasswordHash = new byte[0x30];
                Array.Copy(sourceArray, this.userPasswordHash, 0x20);
                Array.Copy(this.userValidationSalt, 0, this.userPasswordHash, 0x20, 8);
                Array.Copy(this.userKeySalt, 0, this.userPasswordHash, 40, 8);
                list = new List<byte>(collection);
                list.AddRange(this.userKeySalt);
                this.userPasswordEncryptedKey = EncryptAesData(CipherMode.CBC, PaddingMode.None, this.ComputeHash(collection, list.ToArray(), userKey), initializationVector, this.encryptionKey);
                byte[] buffer5 = TruncatePassword(encryptionParameters.OwnerPassword);
                if (buffer5.Length == 0)
                {
                    buffer5 = collection;
                }
                list = new List<byte>(buffer5);
                list.AddRange(this.ownerValidationSalt);
                list.AddRange(this.userPasswordHash);
                byte[] buffer6 = this.ComputeHash(buffer5, list.ToArray(), this.userPasswordHash);
                this.ownerPasswordHash = new byte[0x30];
                Array.Copy(buffer6, this.ownerPasswordHash, 0x20);
                Array.Copy(this.ownerValidationSalt, 0, this.ownerPasswordHash, 0x20, 8);
                Array.Copy(this.ownerKeySalt, 0, this.ownerPasswordHash, 40, 8);
                list = new List<byte>(buffer5);
                list.AddRange(this.ownerKeySalt);
                list.AddRange(this.userPasswordHash);
                this.ownerPasswordEncryptedKey = EncryptAesData(CipherMode.CBC, PaddingMode.None, this.ComputeHash(buffer5, list.ToArray(), this.userPasswordHash), initializationVector, this.encryptionKey);
                Array.Resize<byte>(ref this.userPasswordHash, 0x7f);
                Array.Resize<byte>(ref this.ownerPasswordHash, 0x7f);
                byte[] permissionsToEncrypt = this.PermissionsToEncrypt;
                Array.Resize<byte>(ref permissionsToEncrypt, 0x10);
                Array.Copy(GenerateRandomData(4), 0, permissionsToEncrypt, 12, 4);
                this.encryptedPermissions = EncryptAesData(CipherMode.ECB, PaddingMode.None, this.encryptionKey, initializationVector, permissionsToEncrypt);
            }
        }

        [SecuritySafeCritical]
        public PdfEncryptionInfo(PdfReaderDictionary dictionary, byte[][] documentID, PdfGetPasswordAction getPasswordAction)
        {
            int num;
            this.documentID = documentID;
            string name = dictionary.GetName("Filter");
            int? integer = dictionary.GetInteger("V");
            int? nullable4 = dictionary.GetInteger("Length");
            this.keyLength = (nullable4 != null) ? nullable4.GetValueOrDefault() : 40;
            int? nullable2 = dictionary.GetInteger("R");
            this.ownerPasswordHash = dictionary.GetBytes("O");
            this.userPasswordHash = dictionary.GetBytes("U");
            int? nullable3 = dictionary.GetInteger("P");
            bool? boolean = dictionary.GetBoolean("EncryptMetadata");
            this.encryptMetadata = (boolean != null) ? boolean.GetValueOrDefault() : true;
            if ((name != "Standard") || ((integer == null) || ((nullable2 == null) || ((this.ownerPasswordHash == null) || ((this.userPasswordHash == null) || (nullable3 == null))))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.algorithm = integer.Value;
            if ((this.algorithm < EncryptionAlgorithm.Pdf20) && (documentID == null))
            {
                this.documentID = new byte[][] { new byte[0], new byte[0] };
            }
            this.securityHandlerRevision = nullable2.Value;
            this.encryptionFlags = (long) nullable3.Value;
            this.permissionFlags = (PdfDocumentPermissionFlags) this.encryptionFlags;
            this.ownerPasswordHash = this.ValidateHash(this.ownerPasswordHash);
            this.userPasswordHash = this.ValidateHash(this.userPasswordHash);
            switch (this.algorithm)
            {
                case EncryptionAlgorithm.KeyLength40:
                    if ((this.securityHandlerRevision < 2) || (this.securityHandlerRevision > 3))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.keyLength = 40;
                    break;

                case EncryptionAlgorithm.KeyLengthGreaterThan40:
                    if ((this.keyLength < 40) || ((this.keyLength > 0x80) || (((this.keyLength % 8) != 0) || ((this.securityHandlerRevision < 2) || (this.securityHandlerRevision > 3)))))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    break;

                case EncryptionAlgorithm.Pdf15:
                    if (this.securityHandlerRevision != 4)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.InitializeCryptFilter(dictionary);
                    break;

                case EncryptionAlgorithm.Pdf20:
                    if ((this.securityHandlerRevision != 5) && (this.securityHandlerRevision != 6))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.InitializeCryptFilter(dictionary);
                    this.ownerValidationSalt = new byte[8];
                    this.ownerKeySalt = new byte[8];
                    Array.Copy(this.ownerPasswordHash, 0x20, this.ownerValidationSalt, 0, 8);
                    Array.Copy(this.ownerPasswordHash, 40, this.ownerKeySalt, 0, 8);
                    this.userValidationSalt = new byte[8];
                    this.userKeySalt = new byte[8];
                    Array.Copy(this.userPasswordHash, 0x20, this.userValidationSalt, 0, 8);
                    Array.Copy(this.userPasswordHash, 40, this.userKeySalt, 0, 8);
                    break;

                default:
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    break;
            }
            this.keyLength /= 8;
            this.extendedKeyLength = this.keyLength + 5;
            this.actualKeyLength = Math.Min(this.extendedKeyLength, 0x10);
            if (this.CheckUserPassword((this.algorithm == EncryptionAlgorithm.Pdf20) ? new byte[0] : passwordPadding))
            {
                goto TR_0004;
            }
            else
            {
                if (getPasswordAction == null)
                {
                    throw new PdfIncorrectPasswordException();
                }
                num = 1;
            }
            while (true)
            {
                string passwordString = getPasswordAction(num);
                if (passwordString == null)
                {
                    throw new PdfIncorrectPasswordException();
                }
                byte[] buffer = (this.algorithm != EncryptionAlgorithm.Pdf20) ? PadOrTruncatePassword(passwordString) : TruncatePassword(passwordString);
                if (this.CheckUserPassword(buffer))
                {
                    break;
                }
                if (this.algorithm == EncryptionAlgorithm.Pdf20)
                {
                    List<byte> list = new List<byte>(buffer);
                    list.AddRange(this.ownerValidationSalt);
                    list.AddRange(this.userPasswordHash);
                    if (this.CheckPassword(this.ownerPasswordHash, this.ComputeHash(buffer, list.ToArray(), this.userPasswordHash)))
                    {
                        list = new List<byte>(buffer);
                        list.AddRange(this.ownerKeySalt);
                        list.AddRange(this.userPasswordHash);
                        this.encryptionKey = this.DecryptAesData(CipherMode.CBC, this.ComputeHash(buffer, list.ToArray(), this.userPasswordHash), this.ownerPasswordEncryptedKey);
                        if (this.CheckPermissions())
                        {
                            break;
                        }
                    }
                }
                else
                {
                    byte[] ownerPasswordHash;
                    byte[] key = this.ComputeOwnerEncryptionKey(buffer);
                    if (this.securityHandlerRevision < 3)
                    {
                        ownerPasswordHash = new ARC4Cipher(key).Encrypt(this.ownerPasswordHash);
                    }
                    else
                    {
                        ownerPasswordHash = this.ownerPasswordHash;
                        for (int i = 0x13; i >= 0; i--)
                        {
                            ownerPasswordHash = new ARC4Cipher(this.XorKey(key, i)).Encrypt(ownerPasswordHash);
                        }
                    }
                    if (this.CheckUserPassword(ownerPasswordHash))
                    {
                        break;
                    }
                }
                num++;
            }
        TR_0004:
            if (this.securityHandlerRevision < 3)
            {
                this.permissionFlags |= PdfDocumentPermissionFlags.Accessibility | PdfDocumentPermissionFlags.DocumentAssembling | PdfDocumentPermissionFlags.FormFilling | PdfDocumentPermissionFlags.HighQualityPrinting;
            }
        }

        private static bool CheckFilterExistence(PdfDictionary filterDescriptions, string filterName) => 
            (filterName == "Identity") || ((filterName == "StdCF") && ((filterDescriptions != null) && filterDescriptions.ContainsKey("StdCF")));

        private bool CheckPassword(byte[] expectedHash, byte[] actualHash)
        {
            int num = (this.securityHandlerRevision == 2) ? 0x20 : 0x10;
            for (int i = 0; i < num; i++)
            {
                if (actualHash[i] != expectedHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckPermissions()
        {
            if (this.securityHandlerRevision != 6)
            {
                return true;
            }
            byte[] buffer = this.DecryptAesData(CipherMode.ECB, this.encryptionKey, this.encryptedPermissions);
            return ((buffer.Length >= 12) && ((buffer[9] == 0x61) && ((buffer[10] == 100) && ((buffer[11] == 0x62) && (((buffer[8] != 0) == this.encryptMetadata) ? ((((buffer[0] | (buffer[1] << 8)) | (buffer[2] << 0x10)) | (buffer[3] << 0x18)) == this.encryptionFlags) : false)))));
        }

        private bool CheckUserPassword(byte[] passwordString)
        {
            if (this.algorithm != EncryptionAlgorithm.Pdf20)
            {
                return this.CheckPassword(this.userPasswordHash, this.ComputeUserPasswordHash(passwordString));
            }
            List<byte> list = new List<byte>(passwordString);
            list.AddRange(this.userValidationSalt);
            byte[] actualHash = this.ComputeHash(passwordString, list.ToArray(), new byte[0]);
            if (!this.CheckPassword(this.userPasswordHash, actualHash))
            {
                return false;
            }
            list = new List<byte>(passwordString);
            list.AddRange(this.userKeySalt);
            this.encryptionKey = this.DecryptAesData(CipherMode.CBC, this.ComputeHash(passwordString, list.ToArray(), new byte[0]), this.userPasswordEncryptedKey);
            return this.CheckPermissions();
        }

        private byte[] ComputeActualEncryptionKey(int number, int generation)
        {
            byte[] buffer;
            int keyLength = this.keyLength;
            this.encryptionKey[keyLength++] = (byte) (number & 0xff);
            this.encryptionKey[keyLength++] = (byte) ((number & 0xff00) >> 8);
            this.encryptionKey[keyLength++] = (byte) ((number & 0xff0000) >> 0x10);
            this.encryptionKey[keyLength++] = (byte) (generation & 0xff);
            this.encryptionKey[keyLength] = (byte) ((generation & 0xff00) >> 8);
            using (MD5 md = MD5.Create())
            {
                buffer = md.ComputeHash(this.encryptionKey);
            }
            Array.Resize<byte>(ref buffer, this.actualKeyLength);
            return buffer;
        }

        private byte[] ComputeHash(byte[] passwordString, byte[] data, byte[] userKey)
        {
            byte[] buffer2;
            using (SHA256 sha = SHA256.Create())
            {
                byte[] collection = sha.ComputeHash(data);
                if (this.securityHandlerRevision != 5)
                {
                    int num = 0;
                    while (true)
                    {
                        List<byte> list = new List<byte>();
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 < 0x40)
                            {
                                list.AddRange(passwordString);
                                list.AddRange(collection);
                                list.AddRange(userKey);
                                num2++;
                                continue;
                            }
                            byte[] destinationArray = new byte[0x10];
                            byte[] buffer4 = new byte[0x10];
                            Array.Copy(collection, destinationArray, 0x10);
                            Array.Copy(collection, 0x10, buffer4, 0, 0x10);
                            byte[] buffer = EncryptAesData(CipherMode.CBC, PaddingMode.None, destinationArray, buffer4, list.ToArray());
                            BigInteger integer = 0;
                            int index = 0;
                            while (true)
                            {
                                if (index < 0x10)
                                {
                                    integer = (integer * 0x100) + buffer[index];
                                    index++;
                                    continue;
                                }
                                int num4 = (int) (integer % 3);
                                HashAlgorithm algorithm = (num4 == 1) ? ((HashAlgorithm) SHA384.Create()) : ((num4 == 2) ? ((HashAlgorithm) SHA512.Create()) : ((HashAlgorithm) SHA256.Create()));
                                using (algorithm)
                                {
                                    collection = algorithm.ComputeHash(buffer);
                                }
                                num++;
                                if ((num <= 0x3f) || (buffer[buffer.Length - 1] > (num - 0x20)))
                                {
                                    break;
                                }
                                Array.Resize<byte>(ref collection, 0x20);
                                return collection;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    buffer2 = collection;
                }
            }
            return buffer2;
        }

        private byte[] ComputeOwnerEncryptionKey(byte[] ownerPasswordString)
        {
            byte[] buffer = ownerPasswordString;
            using (MD5 md = MD5.Create())
            {
                buffer = md.ComputeHash(buffer);
            }
            Array.Resize<byte>(ref buffer, this.keyLength);
            if (this.securityHandlerRevision >= 3)
            {
                for (int i = 0; i < 50; i++)
                {
                    using (MD5 md2 = MD5.Create())
                    {
                        buffer = md2.ComputeHash(buffer);
                    }
                }
            }
            return buffer;
        }

        private byte[] ComputeUserPasswordHash(byte[] userPasswordString)
        {
            byte[] buffer;
            List<byte> list = new List<byte>(userPasswordString);
            list.AddRange(this.ownerPasswordHash);
            long encryptionFlags = this.encryptionFlags;
            list.Add((byte) (encryptionFlags & 0xffL));
            list.Add((byte) ((encryptionFlags & 0xff00L) >> 8));
            list.Add((byte) ((encryptionFlags & 0xff0000L) >> 0x10));
            list.Add((byte) ((encryptionFlags & 0xff000000UL) >> 0x18));
            list.AddRange(this.documentID[0]);
            if ((this.securityHandlerRevision >= 4) && !this.encryptMetadata)
            {
                byte[] collection = new byte[] { 0xff, 0xff, 0xff, 0xff };
                list.AddRange(collection);
            }
            using (MD5 md = MD5.Create())
            {
                this.encryptionKey = md.ComputeHash(list.ToArray());
            }
            if (this.securityHandlerRevision >= 3)
            {
                for (int j = 0; j < 50; j++)
                {
                    using (MD5 md2 = MD5.Create())
                    {
                        Array.Resize<byte>(ref this.encryptionKey, this.keyLength);
                        this.encryptionKey = md2.ComputeHash(this.encryptionKey);
                    }
                }
            }
            Array.Resize<byte>(ref this.encryptionKey, this.keyLength);
            CryptMethod cryptMethod = this.cryptMethod;
            if (cryptMethod == CryptMethod.V2)
            {
                Array.Resize<byte>(ref this.encryptionKey, this.extendedKeyLength);
            }
            else if (cryptMethod == CryptMethod.AESV2)
            {
                Array.Resize<byte>(ref this.encryptionKey, this.extendedKeyLength + 4);
                int extendedKeyLength = this.extendedKeyLength;
                this.encryptionKey[extendedKeyLength++] = 0x73;
                this.encryptionKey[extendedKeyLength++] = 0x41;
                this.encryptionKey[extendedKeyLength++] = 0x6c;
                this.encryptionKey[extendedKeyLength] = 0x54;
            }
            if (this.securityHandlerRevision < 3)
            {
                byte[] destinationArray = new byte[this.keyLength];
                Array.Copy(this.encryptionKey, 0, destinationArray, 0, this.keyLength);
                return new ARC4Cipher(destinationArray).Encrypt(passwordPadding);
            }
            List<byte> list2 = new List<byte>(passwordPadding);
            list2.AddRange(this.documentID[0]);
            using (MD5 md3 = MD5.Create())
            {
                buffer = md3.ComputeHash(list2.ToArray());
            }
            for (int i = 0; i < 20; i++)
            {
                buffer = new ARC4Cipher(this.XorKey(this.encryptionKey, i)).Encrypt(buffer);
            }
            return PadOrTruncatePassword(buffer);
        }

        private byte[] DecryptAesData(byte[] key, byte[] data)
        {
            byte[] buffer2;
            if ((data.Length - 0x10) < 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            byte[] destinationArray = new byte[0x10];
            Array.Copy(data, destinationArray, 0x10);
            try
            {
                buffer2 = DecryptAesData(CipherMode.CBC, PaddingMode.PKCS7, key, destinationArray, data, 0x10);
            }
            catch
            {
                return DecryptAesData(CipherMode.CBC, PaddingMode.None, key, destinationArray, data, 0x10);
            }
            return buffer2;
        }

        private byte[] DecryptAesData(CipherMode mode, byte[] key, byte[] data) => 
            DecryptAesData(mode, PaddingMode.None, key, new byte[0x10], data, 0);

        private static byte[] DecryptAesData(CipherMode mode, PaddingMode padding, byte[] key, byte[] initializationVector, byte[] data, int dataPosition)
        {
            byte[] buffer4;
            Aes provider = Provider;
            provider.Mode = mode;
            provider.Padding = padding;
            using (ICryptoTransform transform = provider.CreateDecryptor(key, initializationVector))
            {
                using (MemoryStream stream = new MemoryStream(data))
                {
                    stream.Position = dataPosition;
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[data.Length];
                        byte[] src = null;
                        while (true)
                        {
                            if (!stream2.HasFlushedFinalBlock)
                            {
                                int count = stream2.Read(buffer, 0, buffer.Length);
                                if (count != 0)
                                {
                                    int length;
                                    if (src == null)
                                    {
                                        src = new byte[count];
                                        length = 0;
                                    }
                                    else
                                    {
                                        byte[] dst = new byte[count + src.Length];
                                        Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                                        length = src.Length;
                                        src = dst;
                                    }
                                    Buffer.BlockCopy(buffer, 0, src, length, count);
                                    if (count >= buffer.Length)
                                    {
                                        continue;
                                    }
                                }
                            }
                            buffer4 = src ?? new byte[0];
                            break;
                        }
                    }
                }
            }
            return buffer4;
        }

        public byte[] DecryptData(byte[] data, int number, int generation)
        {
            EncryptionAlgorithm algorithm = this.algorithm;
            if (algorithm != EncryptionAlgorithm.Pdf15)
            {
                return ((algorithm != EncryptionAlgorithm.Pdf20) ? new ARC4Cipher(this.ComputeActualEncryptionKey(number, generation)).Encrypt(data) : this.DecryptAesData(this.encryptionKey, data));
            }
            if (this.streamFilterName == "Identity")
            {
                return data;
            }
            byte[] key = this.ComputeActualEncryptionKey(number, generation);
            return ((this.cryptMethod == CryptMethod.AESV2) ? this.DecryptAesData(key, data) : new ARC4Cipher(key).Encrypt(data));
        }

        private static byte[] EncryptAesData(CipherMode mode, PaddingMode padding, byte[] key, byte[] initializationVector, byte[] data)
        {
            byte[] buffer;
            Aes provider = Provider;
            provider.Mode = mode;
            provider.Padding = padding;
            using (ICryptoTransform transform = provider.CreateEncryptor(key, initializationVector))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                    {
                        stream2.Write(data, 0, data.Length);
                        stream2.FlushFinalBlock();
                        buffer = stream.ToArray();
                    }
                }
            }
            return buffer;
        }

        public byte[] EncryptData(byte[] data, int number)
        {
            if (this.cryptMethod == CryptMethod.V2)
            {
                return new ARC4Cipher(this.ComputeActualEncryptionKey(number, 0)).Encrypt(data);
            }
            byte[] initializationVector = GenerateRandomData(0x10);
            byte[] sourceArray = EncryptAesData(CipherMode.CBC, PaddingMode.PKCS7, (this.cryptMethod == CryptMethod.AESV3) ? this.encryptionKey : this.ComputeActualEncryptionKey(number, 0), initializationVector, data);
            int length = sourceArray.Length;
            byte[] destinationArray = new byte[0x10 + length];
            Array.Copy(initializationVector, 0, destinationArray, 0, 0x10);
            Array.Copy(sourceArray, 0, destinationArray, 0x10, length);
            return destinationArray;
        }

        private static byte[] GenerateRandomData(int length)
        {
            byte[] data = new byte[length];
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(data);
            }
            return data;
        }

        private void InitializeCryptFilter(PdfReaderDictionary dictionary)
        {
            int num;
            int num2;
            PdfReaderDictionary filterDescriptions = dictionary.GetDictionary("CF");
            this.streamFilterName = dictionary.GetName("StmF");
            this.stringFilterName = dictionary.GetName("StrF");
            this.embeddedFileFilterName = dictionary.GetName("EFF");
            if (this.securityHandlerRevision < 5)
            {
                num = 0x10;
                num2 = 0x80;
            }
            else
            {
                this.ownerPasswordEncryptedKey = dictionary.GetBytes("OE");
                this.userPasswordEncryptedKey = dictionary.GetBytes("UE");
                this.encryptedPermissions = dictionary.GetBytes("Perms");
                if ((this.ownerPasswordEncryptedKey == null) || ((this.ownerPasswordEncryptedKey.Length != 0x20) || ((this.userPasswordEncryptedKey == null) || ((this.userPasswordEncryptedKey.Length != 0x20) || ((this.encryptedPermissions == null) || (this.encryptedPermissions.Length != 0x10))))))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                num = 0x20;
                num2 = 0x100;
            }
            if (!CheckFilterExistence(filterDescriptions, this.streamFilterName) || (!CheckFilterExistence(filterDescriptions, this.stringFilterName) || ((this.embeddedFileFilterName != null) && !CheckFilterExistence(filterDescriptions, this.embeddedFileFilterName))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if ((this.streamFilterName == "StdCF") || (this.stringFilterName == "StdCF"))
            {
                PdfReaderDictionary dictionary3 = filterDescriptions.GetDictionary("StdCF");
                this.cryptMethod = PdfEnumToStringConverter.Parse<CryptMethod>(dictionary3.GetName("CFM"), false);
                CryptMethod cryptMethod = this.cryptMethod;
                if (cryptMethod == CryptMethod.AESV2)
                {
                    this.keyLength = 0x80;
                }
                else if (cryptMethod == CryptMethod.AESV3)
                {
                    this.keyLength = 0x100;
                }
                else
                {
                    int? integer = dictionary3.GetInteger("Length");
                    if (integer == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.keyLength = integer.Value;
                    if ((this.keyLength >= 5) && (this.keyLength <= num))
                    {
                        this.keyLength *= 8;
                    }
                    else if (((this.keyLength < 40) && (this.keyLength > num2)) || ((this.keyLength % 8) != 0))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
            }
        }

        private static byte[] PadOrTruncatePassword(IList<byte> passwordString)
        {
            List<byte> list = new List<byte>(passwordString);
            list.AddRange(passwordPadding);
            list.RemoveRange(0x20, list.Count - 0x20);
            return list.ToArray();
        }

        private static byte[] PadOrTruncatePassword(string passwordString) => 
            PadOrTruncatePassword((passwordString == null) ? new List<byte>() : new List<byte>(DXEncoding.GetEncoding(0).GetBytes(passwordString)));

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Filter", "Standard");
            dictionary.Add("V", (int) this.algorithm);
            dictionary.Add("Length", this.keyLength * 8);
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.AddEnumName<CryptMethod>("CFM", this.cryptMethod);
            dictionary2.Add("Length", this.keyLength);
            PdfWriterDictionary dictionary3 = new PdfWriterDictionary(objects);
            dictionary3.Add("StdCF", dictionary2);
            dictionary.Add("CF", dictionary3);
            dictionary.AddName("StmF", this.streamFilterName);
            dictionary.AddName("StrF", this.stringFilterName);
            dictionary.Add("R", this.securityHandlerRevision);
            dictionary.Add("O", new PdfUnencryptedDataToken(this.ownerPasswordHash));
            dictionary.Add("U", new PdfUnencryptedDataToken(this.userPasswordHash));
            dictionary.Add("P", (int) this.encryptionFlags);
            dictionary.Add("EncryptMetadata", this.encryptMetadata);
            if (this.securityHandlerRevision == 6)
            {
                dictionary.Add("OE", new PdfUnencryptedDataToken(this.ownerPasswordEncryptedKey));
                dictionary.Add("UE", new PdfUnencryptedDataToken(this.userPasswordEncryptedKey));
                dictionary.Add("Perms", new PdfUnencryptedDataToken(this.encryptedPermissions));
            }
            return dictionary;
        }

        private static byte[] TruncatePassword(string passwordString)
        {
            List<byte> list = (passwordString == null) ? new List<byte>() : new List<byte>(Encoding.UTF8.GetBytes(passwordString));
            int count = list.Count - 0x7f;
            if (count > 0)
            {
                list.RemoveRange(0x7f, count);
            }
            return list.ToArray();
        }

        private byte[] ValidateHash(byte[] hash)
        {
            int length = hash.Length;
            int securityHandlerRevision = this.securityHandlerRevision;
            if (securityHandlerRevision != 5)
            {
                if (securityHandlerRevision != 6)
                {
                    if (length != 0x20)
                    {
                        Array.Resize<byte>(ref hash, 0x20);
                    }
                    return hash;
                }
                else if (length == 0x7f)
                {
                    Array.Resize<byte>(ref hash, 0x30);
                    return hash;
                }
            }
            if (length != 0x30)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return hash;
        }

        private byte[] XorKey(byte[] key, int value)
        {
            byte[] buffer = new byte[this.keyLength];
            for (int i = 0; i < this.keyLength; i++)
            {
                buffer[i] = (byte) (key[i] ^ value);
            }
            return buffer;
        }

        private static Aes Provider
        {
            get
            {
                providerValue ??= Aes.Create();
                return providerValue;
            }
        }

        public bool EncryptMetadata =>
            this.encryptMetadata;

        internal PdfDocumentPermissionFlags PermissionFlags =>
            this.permissionFlags;

        private byte[] PermissionsToEncrypt
        {
            get
            {
                byte[] buffer1 = new byte[] { 0, 0, 0, 0, 0xff, 0xff, 0xff, 0xff, 0, 0x61, 100, 0x62 };
                buffer1[0] = (byte) (this.encryptionFlags & 0xffL);
                buffer1[1] = (byte) ((this.encryptionFlags & 0xff00L) >> 8);
                buffer1[2] = (byte) ((this.encryptionFlags & 0xff0000L) >> 0x10);
                buffer1[3] = (byte) ((this.encryptionFlags & 0xff000000UL) >> 0x18);
                buffer1[8] = this.encryptMetadata ? ((byte) 0x54) : ((byte) 70);
                return buffer1;
            }
        }

        private enum CryptMethod
        {
            V2,
            AESV2,
            AESV3
        }

        private enum EncryptionAlgorithm
        {
            Undocumented,
            KeyLength40,
            KeyLengthGreaterThan40,
            Unpublished,
            Pdf15,
            Pdf20
        }
    }
}

