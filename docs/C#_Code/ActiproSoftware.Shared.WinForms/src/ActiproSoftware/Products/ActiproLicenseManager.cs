namespace ActiproSoftware.Products
{
    using #H;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;
    using System.Text;
    using System.Threading;

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Actipro")]
    public static class ActiproLicenseManager
    {
        private static Dictionary<Type, ActiproLicense> #pBb;
        private static Dictionary<Type, #ZFj> #pFj;
        private static List<string> #qBb;
        private static bool? #qFj;
        private static bool? #rFj;
        private static object #a2c = new object();
        private static ActiproSoftware.Products.ActiproLicenseManager.#sFj #sFj;
        private const string #WCb = "C8g128r3";
        private const string #tFj = @"SOFTWARE\Wow6432Node\Actipro Software\{0}\{1}";
        private const string #uFj = @"SOFTWARE\Actipro Software\{0}\{1}";
        private const string #vFj = "WinForms Controls";
        private const int #nBb = 0x100;

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static #ZFj #BFj(LicenseContext #Uj, Type #hBb, int #WCb)
        {
            string savedLicenseKey = null;
            if ((#hBb != null) && ((#Uj != null) && !IsBrowserHosted))
            {
                try
                {
                    savedLicenseKey = #Uj.GetSavedLicenseKey(#hBb, null);
                }
                catch
                {
                }
            }
            return (string.IsNullOrEmpty(savedLicenseKey) ? null : #zFj(savedLicenseKey, #WCb));
        }

        private static #ZFj #CFj(AssemblyInfo #bBb, int #WCb)
        {
            if ((#pFj != null) && (#bBb != null))
            {
                #ZFj fj;
                if (#pFj.TryGetValue(#bBb.GetType(), out fj))
                {
                    #ZFj fj1 = new #ZFj();
                    fj1.Licensee = DecryptString(fj.Licensee, #WCb);
                    fj1.LicenseKey = DecryptString(fj.LicenseKey, #WCb);
                    fj1.SourceLocation = ActiproLicenseSourceLocation.Fixed;
                    return fj1;
                }
                if (#pFj.TryGetValue(typeof(#0Fj), out fj))
                {
                    #ZFj fj2 = new #ZFj();
                    fj2.Licensee = DecryptString(fj.Licensee, #WCb);
                    fj2.LicenseKey = DecryptString(fj.LicenseKey, #WCb);
                    fj2.SourceLocation = ActiproLicenseSourceLocation.Fixed;
                    return fj2;
                }
            }
            return null;
        }

        private static #ZFj #DFj(string #EFj)
        {
            if (!IsBrowserHosted)
            {
                try
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(#EFj);
                    key ??= Registry.CurrentUser.OpenSubKey(#EFj);
                    if (key != null)
                    {
                        string str = (key.GetValue(#G.#eg(0x1753)) as string) ?? string.Empty;
                        string str2 = (key.GetValue(#G.#eg(0x1760)) as string) ?? string.Empty;
                        key.Close();
                        if (!string.IsNullOrEmpty(str2))
                        {
                            if (string.IsNullOrEmpty(str))
                            {
                                str = #G.#eg(0x1771);
                            }
                            #ZFj fj1 = new #ZFj();
                            fj1.Licensee = str.Trim();
                            fj1.LicenseKey = str2.ToUpper(CultureInfo.InvariantCulture).Trim();
                            fj1.SourceLocation = ActiproLicenseSourceLocation.Registry;
                            return fj1;
                        }
                    }
                }
                catch (ArgumentNullException)
                {
                }
                catch (ArgumentException)
                {
                }
                catch (IOException)
                {
                }
                catch (ObjectDisposedException)
                {
                }
                catch (SecurityException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
            return null;
        }

        private static void #IFj(LicenseContext #Uj, Type #hBb, int #WCb, AlgorithmGLicenseDecryptor #JFj)
        {
            if (#Uj == null)
            {
                throw new ArgumentNullException(#G.#eg(0x62d));
            }
            if (#hBb == null)
            {
                throw new ArgumentNullException(#G.#eg(0x178a));
            }
            if (#JFj == null)
            {
                throw new ArgumentNullException(#G.#eg(0x179b));
            }
            #JFj.UsageAllowed = AlgorithmGLicenseDecryptor.#NAb.#8Bb;
            #Uj.SetSavedLicenseKey(#hBb, EncryptString(#JFj.LicenseKey + #G.#eg(0x17a8) + #JFj.Licensee, #WCb));
        }

        internal static void #KFj(ActiproLicense #sBb, ActiproSoftware.Products.LicenseException #LFj, object #gd)
        {
            if (!IsInDesigner)
            {
                if ((#sFj == ActiproSoftware.Products.ActiproLicenseManager.#sFj.#QBb) && (#sBb != null))
                {
                    AssemblyInfo assemblyInfo = #sBb.AssemblyInfo;
                    if ((assemblyInfo != null) && !IsBrowserHosted)
                    {
                        #sFj = ActiproSoftware.Products.ActiproLicenseManager.#sFj.#nve;
                        try
                        {
                            assemblyInfo.ShowLicenseWindow(#sBb);
                        }
                        finally
                        {
                            #sFj = ActiproSoftware.Products.ActiproLicenseManager.#sFj.#mve;
                        }
                    }
                }
                if (((#sFj != ActiproSoftware.Products.ActiproLicenseManager.#sFj.#nve) || (#LFj == null)) && (#LFj != null))
                {
                    throw #LFj;
                }
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId="ActiproSoftware.Products.ActiproLicenseManager+DelayedLicenseUIRenderer")]
        private static void #MFj(ActiproLicense #sBb, Type #4Ub, object #gd)
        {
            ActiproSoftware.Products.LicenseException exception = null;
            bool flag = true;
            if (#sBb != null)
            {
                if ((#sBb.LicenseType == AssemblyLicenseType.Full) && #sBb.IsUnlicensedProduct)
                {
                    flag = false;
                }
                else if (#sBb.IsValid)
                {
                    if (#sBb.LicenseType == AssemblyLicenseType.Full)
                    {
                        return;
                    }
                    flag = false;
                }
            }
            if (flag && ((exception == null) && !IsBrowserHosted))
            {
                object[] args = new object[] { #4Ub };
                string message = string.Format(CultureInfo.CurrentCulture, #G.#eg(0x17ad), args);
                if (#sBb != null)
                {
                    object[] objArray2 = new object[] { Environment.NewLine, #sBb.GetQuickInfo(), #sBb.GetDetails() };
                    message = message + string.Format(CultureInfo.CurrentCulture, #G.#eg(0x18df), objArray2);
                }
                exception = new ActiproSoftware.Products.LicenseException(#4Ub, message);
            }
            #KFj(#sBb, exception, #gd);
        }

        private static int #QId()
        {
            int num = 0;
            for (int i = 0; i < #G.#eg(0x170a).Length; i++)
            {
                num += #G.#eg(0x170a)[i];
            }
            return num;
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static ActiproLicense #wFj(AssemblyInfo #bBb, LicenseContext #Uj)
        {
            if (#bBb == null)
            {
                throw new ArgumentNullException(#G.#eg(0x1717));
            }
            string licensee = string.Empty;
            string str2 = string.Empty;
            ActiproLicenseSourceLocation none = ActiproLicenseSourceLocation.None;
            AlgorithmGLicenseDecryptor decryptor = null;
            AlgorithmGLicenseDecryptor.#MAb ab = AlgorithmGLicenseDecryptor.#MAb.#WBb;
            bool flag = true;
            int num = #QId();
            foreach (#ZFj fj in #xFj(#bBb, #Uj, num))
            {
                string local1 = fj?.Licensee;
                string text1 = local1;
                if (local1 == null)
                {
                    string local2 = local1;
                    text1 = string.Empty;
                }
                string str3 = text1;
                string licenseKey = fj?.LicenseKey;
                string text2 = licenseKey;
                if (licenseKey == null)
                {
                    string local4 = licenseKey;
                    text2 = string.Empty;
                }
                string str4 = text2;
                ActiproLicenseSourceLocation location2 = (fj != null) ? fj.SourceLocation : ActiproLicenseSourceLocation.None;
                AlgorithmGLicenseDecryptor decryptor2 = null;
                AlgorithmGLicenseDecryptor.#MAb exceptionType = AlgorithmGLicenseDecryptor.#MAb.#QBb;
                bool flag2 = true;
                try
                {
                    if (str3.Length == 0)
                    {
                        exceptionType = AlgorithmGLicenseDecryptor.#MAb.#WBb;
                    }
                    else if (str4.Length == 0)
                    {
                        exceptionType = AlgorithmGLicenseDecryptor.#MAb.#SBb;
                    }
                    else
                    {
                        decryptor2 = new AlgorithmGLicenseDecryptor(str3, str4);
                        flag2 = false;
                        if (((#Uj != null) && (#Uj.UsageMode == LicenseUsageMode.Designtime)) && (decryptor2.UsageAllowed == AlgorithmGLicenseDecryptor.#NAb.#8Bb))
                        {
                            exceptionType = AlgorithmGLicenseDecryptor.#MAb.#2Bb;
                        }
                    }
                }
                catch (AlgorithmGLicenseDecryptor.LicenseException exception1)
                {
                    exceptionType = (AlgorithmGLicenseDecryptor.#MAb) exception1.ExceptionType;
                }
                catch
                {
                    exceptionType = AlgorithmGLicenseDecryptor.#MAb.#RBb;
                }
                bool flag3 = (!flag2 && ((exceptionType == AlgorithmGLicenseDecryptor.#MAb.#QBb) && ((decryptor2 != null) && (decryptor2.LicenseType == AssemblyLicenseType.Full)))) && ((decryptor2.ProductCodes & #bBb.ProductId) == #bBb.ProductId);
                if (string.IsNullOrEmpty(str2) | flag3)
                {
                    licensee = str3;
                    str2 = str4;
                    none = location2;
                    decryptor = decryptor2;
                    ab = exceptionType;
                    flag = flag2;
                    if (flag3)
                    {
                        break;
                    }
                }
            }
            ActiproLicense license = !flag ? new ActiproLicense(#bBb, licensee, str2, none, (int) ab, (uint) decryptor.ProductCodes, decryptor.MajorVersion, decryptor.MinorVersion, decryptor.LicenseType, decryptor.LicenseCount, decryptor.Platform, decryptor.OrganizationID, decryptor.Date) : new ActiproLicense(#bBb, licensee, str2, none, (int) ab);
            if ((#Uj != null) && ((decryptor != null) && license.IsValid))
            {
                #IFj(#Uj, LicenseTokenType, num, decryptor);
            }
            if (license.IsValid && ((license.LicenseType == AssemblyLicenseType.Full) && ((license.ProductIDs & #bBb.ProductId) != #bBb.ProductId)))
            {
                license.SetExceptionType(0x2a);
            }
            return license;
        }

        private static IEnumerable<#ZFj> #xFj(AssemblyInfo #bBb, LicenseContext #Uj, int #WCb)
        {
            #gUk uk1 = new #gUk(-2);
            #gUk uk2 = new #gUk(-2);
            uk2.#1Fj = #bBb;
            #gUk local3 = uk2;
            #gUk local4 = uk2;
            local4.#3Fj = #Uj;
            AssemblyInfo local1 = (AssemblyInfo) local4;
            AssemblyInfo local2 = (AssemblyInfo) local4;
            local2.#2Fj = #WCb;
            return (IEnumerable<#ZFj>) local2;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static #ZFj #yFj(Assembly #sxc, Type #hBb, int #WCb)
        {
            string str = null;
            string str2 = null;
            string str3 = null;
            int num;
            if ((#sxc != null) && !#sxc.IsDynamic)
            {
                try
                {
                    string[] manifestResourceNames = #sxc.GetManifestResourceNames();
                    num = 0;
                    goto TR_0026;
                TR_0011:
                    num++;
                TR_0026:
                    while (true)
                    {
                        if (num >= manifestResourceNames.Length)
                        {
                            break;
                        }
                        string name = manifestResourceNames[num];
                        if (name.EndsWith(#G.#eg(0x1728), StringComparison.OrdinalIgnoreCase))
                        {
                            if (IsBrowserHosted)
                            {
                                if (name.IndexOf(#G.#eg(0x1735), StringComparison.OrdinalIgnoreCase) != -1)
                                {
                                    using (Stream stream2 = #sxc.GetManifestResourceStream(name))
                                    {
                                        if (stream2 != null)
                                        {
                                            StreamReader reader1 = new StreamReader(stream2);
                                            str = reader1.ReadLine();
                                            str2 = reader1.ReadLine();
                                            str3 = reader1.ReadToEnd();
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (Stream stream = #sxc.GetManifestResourceStream(name))
                                {
                                    if (stream == null)
                                    {
                                        goto TR_0011;
                                    }
                                    else
                                    {
                                        object obj2 = new BinaryFormatter().Deserialize(stream);
                                        if (obj2 == null)
                                        {
                                            goto TR_0011;
                                        }
                                        else
                                        {
                                            if (#hBb != null)
                                            {
                                                object obj3 = ((Hashtable) ((object[]) obj2)[1])[#hBb.AssemblyQualifiedName];
                                                if (obj3 != null)
                                                {
                                                    str3 = obj3.ToString();
                                                    break;
                                                }
                                            }
                                            goto TR_0011;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        goto TR_0011;
                    }
                }
                catch
                {
                }
            }
            if (str3 == null)
            {
                return null;
            }
            #ZFj fj = #zFj(str3, #WCb);
            if ((fj != null) && IsBrowserHosted)
            {
                bool flag = false;
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                num = 0;
                while (true)
                {
                    if (num < assemblies.Length)
                    {
                        if (!assemblies[num].FullName.StartsWith(fj.RequiredAssemblyName + #G.#eg(0x174e), StringComparison.OrdinalIgnoreCase))
                        {
                            num++;
                            continue;
                        }
                        flag = true;
                    }
                    if (flag && ((fj.Licensee == str) && (fj.RequiredAssemblyName == str2)))
                    {
                        break;
                    }
                    return null;
                }
            }
            return fj;
        }

        private static #ZFj #zFj(string #AFj, int #WCb)
        {
            if (#AFj != null)
            {
                int num;
                #AFj = DecryptString(#AFj, #WCb);
                char[] separator = new char[] { ';' };
                string[] strArray = #AFj.Split(separator);
                int local1 = num = IsBrowserHosted ? 0 : -1;
                int index = local1 + 1;
                int num3 = local1 + 2;
                if (!string.IsNullOrEmpty(strArray[num3]) && !string.IsNullOrEmpty(strArray[index]))
                {
                    #ZFj fj1 = new #ZFj();
                    fj1.Licensee = strArray[num3];
                    fj1.LicenseKey = strArray[index];
                    fj1.SourceLocation = ActiproLicenseSourceLocation.AssemblySavedContext;
                    fj1.RequiredAssemblyName = (num >= 0) ? strArray[num] : null;
                    return fj1;
                }
            }
            return null;
        }

        public static void AddHintAssemblyName(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                #qBb ??= new List<string>();
                assemblyName = assemblyName.Trim();
                if (assemblyName.IndexOf(',') == -1)
                {
                    assemblyName = assemblyName + #G.#eg(0x174e);
                }
                if (!#qBb.Contains(assemblyName))
                {
                    #qBb.Add(assemblyName);
                }
            }
        }

        internal static string DecryptString(string #ql, int #1Nc)
        {
            int num2;
            int length = #ql.Length;
            int[] numArray = new int[length];
            int num3 = 11 + (#1Nc % 0xe9);
            int num4 = 7 + (#1Nc % 0xef);
            int num5 = 5 + (#1Nc % 0xf1);
            int num6 = 3 + (#1Nc % 0xfb);
            for (num2 = 0; num2 < length; num2++)
            {
                numArray[num2] = (#ql[num2] != 'Ā') ? #ql[num2] : 0;
            }
            for (num2 = 0; num2 < (length - 2); num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 2]) ^ ((num6 * numArray[num2 + 1]) % 0x100);
            }
            for (num2 = length - 1; num2 >= 2; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 2]) ^ ((num5 * numArray[num2 - 1]) % 0x100);
            }
            for (num2 = 0; num2 < (length - 1); num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 1]) ^ ((num4 * numArray[num2 + 1]) % 0x100);
            }
            for (num2 = length - 1; num2 >= 1; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 1]) ^ ((num3 * numArray[num2 - 1]) % 0x100);
            }
            StringBuilder builder = new StringBuilder();
            for (num2 = 0; num2 < length; num2++)
            {
                builder.Append((char) numArray[num2]);
            }
            return builder.ToString();
        }

        private static string EncryptString(string #ql, int #1Nc)
        {
            int num2;
            int length = #ql.Length;
            int[] numArray = new int[length];
            int num3 = 11 + (#1Nc % 0xe9);
            int num4 = 7 + (#1Nc % 0xef);
            int num5 = 5 + (#1Nc % 0xf1);
            int num6 = 3 + (#1Nc % 0xfb);
            for (num2 = 0; num2 < length; num2++)
            {
                numArray[num2] = #ql[num2];
            }
            for (num2 = 1; num2 < length; num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 1]) ^ ((num3 * numArray[num2 - 1]) % 0x100);
            }
            for (num2 = length - 2; num2 >= 0; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 1]) ^ ((num4 * numArray[num2 + 1]) % 0x100);
            }
            for (num2 = 2; num2 < length; num2++)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 - 2]) ^ ((num5 * numArray[num2 - 1]) % 0x100);
            }
            for (num2 = length - 3; num2 >= 0; num2--)
            {
                numArray[num2] = (numArray[num2] ^ numArray[num2 + 2]) ^ ((num6 * numArray[num2 + 1]) % 0x100);
            }
            StringBuilder builder = new StringBuilder();
            for (num2 = 0; num2 < length; num2++)
            {
                if (numArray[num2] == 0)
                {
                    builder.Append('Ā');
                }
                else
                {
                    builder.Append((char) numArray[num2]);
                }
            }
            return builder.ToString();
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        internal static ActiproLicense GetLicense(AssemblyInfo #bBb, LicenseContext #Uj)
        {
            if (#bBb == null)
            {
                throw new ArgumentNullException(#G.#eg(0x1717));
            }
            ActiproLicense license = null;
            if ((#pBb == null) || !#pBb.TryGetValue(#bBb.GetType(), out license))
            {
                license = #wFj(#bBb, #Uj);
                #pBb ??= new Dictionary<Type, ActiproLicense>();
                #pBb[#bBb.GetType()] = license;
            }
            return license;
        }

        public static string GetWatermarkText(AssemblyLicenseType licenseType) => 
            (licenseType != AssemblyLicenseType.Evaluation) ? null : #G.#eg(0x1910);

        public static void RegisterLicense(string licensee, string licenseKey)
        {
            RegisterLicense(new #0Fj(), licensee, licenseKey);
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId="licenseKey"), SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId="RegisterLicense"), SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId="ActiproLicenseManager")]
        public static void RegisterLicense(AssemblyInfo assemblyInfo, string licensee, string licenseKey)
        {
            if (assemblyInfo == null)
            {
                throw new ArgumentNullException(#G.#eg(0x1717));
            }
            if (string.IsNullOrEmpty(licensee))
            {
                throw new ArgumentNullException(#G.#eg(0x1921));
            }
            if (string.IsNullOrEmpty(licenseKey))
            {
                throw new ArgumentNullException(#G.#eg(0x192e));
            }
            Type key = assemblyInfo.GetType();
            if ((assemblyInfo is #0Fj) && ((#pFj != null) && #pFj.ContainsKey(key)))
            {
                throw new NotSupportedException(#G.#eg(0x193f));
            }
            int num = #QId();
            #pFj ??= new Dictionary<Type, #ZFj>();
            #ZFj fj1 = new #ZFj();
            fj1.Licensee = EncryptString(licensee, num);
            fj1.LicenseKey = EncryptString(licenseKey, num);
            fj1.SourceLocation = ActiproLicenseSourceLocation.Fixed;
            #pFj[key] = fj1;
        }

        public static AssemblyLicenseType ValidateLicense(AssemblyInfo assemblyInfo, Type type, object instance)
        {
            if (assemblyInfo == null)
            {
                throw new ArgumentNullException(#G.#eg(0x1717));
            }
            if (type == null)
            {
                throw new ArgumentNullException(#G.#eg(0x19cd));
            }
            object obj2 = #a2c;
            lock (obj2)
            {
                ActiproLicense license = GetLicense(assemblyInfo, LicenseManager.CurrentContext);
                AssemblyLicenseType evaluation = (license != null) ? license.LicenseType : AssemblyLicenseType.Invalid;
                if (((evaluation == AssemblyLicenseType.Full) && license.IsUnlicensedProduct) && ((license.ProductIDs & 0x11800) != 0))
                {
                    evaluation = AssemblyLicenseType.Evaluation;
                }
                #MFj(license, type, instance);
                return evaluation;
            }
        }

        private static bool IsBrowserHosted
        {
            get
            {
                if (&#qFj == null)
                {
                    #qFj = false;
                }
                return #qFj.Value;
            }
        }

        private static bool IsInDesigner
        {
            get
            {
                if (&#rFj == null)
                {
                    #rFj = new bool?(LicenseManager.UsageMode == LicenseUsageMode.Designtime);
                }
                return #rFj.Value;
            }
        }

        private static Type LicenseTokenType =>
            Type.GetTypeFromHandle(typeof(ActiproLicenseToken).TypeHandle);

        private class #0Fj : AssemblyInfo
        {
            public override AssemblyLicenseType LicenseType =>
                AssemblyLicenseType.Full;

            public override AssemblyPlatform Platform =>
                AssemblyPlatform.Wpf;

            public sealed override string ProductCode =>
                #G.#eg(0x249f);

            public sealed override int ProductId =>
                0;
        }

        [CompilerGenerated]
        private sealed class #gUk : IDisposable, IEnumerable<ActiproLicenseManager.#ZFj>, IEnumerator<ActiproLicenseManager.#ZFj>, IEnumerable, IEnumerator
        {
            private int #Vo;
            private ActiproLicenseManager.#ZFj #Uo;
            private int #Wo;
            private AssemblyInfo #bBb;
            public AssemblyInfo #1Fj;
            private int #WCb;
            public int #2Fj;
            private LicenseContext #Uj;
            public LicenseContext #3Fj;
            private Assembly[] #knk;
            private Assembly[] #mL;
            private int #nL;
            private Assembly #lnk;
            private string #mnk;
            private List<string>.Enumerator #5zc;

            [DebuggerHidden]
            private IEnumerator<ActiproLicenseManager.#ZFj> #8Fj()
            {
                ActiproLicenseManager.#gUk uk;
                if ((this.#Vo != -2) || (this.#Wo != Thread.CurrentThread.ManagedThreadId))
                {
                    uk = new ActiproLicenseManager.#gUk(0);
                }
                else
                {
                    this.#Vo = 0;
                    uk = this;
                }
                uk.#bBb = this.#1Fj;
                uk.#Uj = this.#3Fj;
                uk.#WCb = this.#2Fj;
                return uk;
            }

            private void #G1i()
            {
                this.#Vo = -1;
                this.#5zc.Dispose();
            }

            private bool #gaf()
            {
                bool flag;
                try
                {
                    ActiproLicenseManager.#ZFj fj;
                    string str;
                    int num = this.#Vo;
                    switch (num)
                    {
                        case 0:
                            this.#Vo = -1;
                            if (this.#bBb == null)
                            {
                                throw new ArgumentNullException(#G.#eg(0x1717));
                            }
                            fj = ActiproLicenseManager.#CFj(this.#bBb, this.#WCb);
                            if (fj != null)
                            {
                                this.#Uo = fj;
                                this.#Vo = 1;
                                return true;
                            }
                            goto TR_002D;

                        case 1:
                            this.#Vo = -1;
                            goto TR_002D;

                        case 2:
                            this.#Vo = -1;
                            goto TR_0029;

                        case 3:
                            this.#Vo = -1;
                            goto TR_0027;

                        case 4:
                            this.#Vo = -3;
                            goto TR_0020;

                        case 5:
                            this.#Vo = -1;
                            break;

                        case 6:
                            this.#Vo = -1;
                            goto TR_0006;

                        default:
                            return false;
                    }
                    goto TR_000D;
                TR_0006:
                    return false;
                TR_0007:
                    str = this.#bBb.Version.Substring(0, 4);
                    object[] args = new object[] { #G.#eg(0x206b), str };
                    object[] objArray2 = new object[] { #G.#eg(0x206b), str };
                    string str3 = string.Format(CultureInfo.InvariantCulture, #G.#eg(0x31c1), objArray2);
                    fj = ActiproLicenseManager.#DFj(string.Format(CultureInfo.InvariantCulture, #G.#eg(0x3184), args)) ?? ActiproLicenseManager.#DFj(str3);
                    if (fj != null)
                    {
                        this.#Uo = fj;
                        this.#Vo = 6;
                        return true;
                    }
                    goto TR_0006;
                TR_000D:
                    this.#nL++;
                TR_0015:
                    while (true)
                    {
                        if (this.#nL >= this.#mL.Length)
                        {
                            this.#mL = null;
                            this.#knk = null;
                            break;
                        }
                        Assembly assembly = this.#mL[this.#nL];
                        try
                        {
                            if ((assembly.ManifestModule != null) && (assembly.ManifestModule.Name == #G.#eg(0x316b)))
                            {
                                goto TR_000D;
                            }
                        }
                        catch
                        {
                        }
                        fj = ActiproLicenseManager.#yFj(assembly, ActiproLicenseManager.LicenseTokenType, this.#WCb);
                        if (fj == null)
                        {
                            goto TR_000D;
                        }
                        else
                        {
                            this.#Uo = fj;
                            this.#Vo = 5;
                            flag = true;
                        }
                        return flag;
                    }
                    goto TR_0007;
                TR_0016:
                    this.#mL = this.#knk;
                    this.#nL = 0;
                    goto TR_0015;
                TR_0018:
                    this.#mnk = null;
                    this.#lnk = null;
                    this.#nL++;
                    goto TR_0025;
                TR_0020:
                    while (true)
                    {
                        if (this.#5zc.MoveNext())
                        {
                            string current = this.#5zc.Current;
                            if (!this.#mnk.StartsWith(current, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            fj = ActiproLicenseManager.#yFj(this.#lnk, ActiproLicenseManager.LicenseTokenType, this.#WCb);
                            if (fj == null)
                            {
                                continue;
                            }
                            this.#Uo = fj;
                            this.#Vo = 4;
                            flag = true;
                        }
                        else
                        {
                            this.#G1i();
                            this.#5zc = new List<string>.Enumerator();
                            goto TR_0018;
                        }
                        break;
                    }
                    return flag;
                TR_0025:
                    while (true)
                    {
                        if (this.#nL < this.#mL.Length)
                        {
                            this.#lnk = this.#mL[this.#nL];
                            this.#mnk = this.#lnk.FullName;
                            if (string.IsNullOrEmpty(this.#mnk))
                            {
                                goto TR_0018;
                            }
                            else
                            {
                                this.#5zc = ActiproLicenseManager.#qBb.GetEnumerator();
                                this.#Vo = -3;
                            }
                        }
                        else
                        {
                            this.#mL = null;
                            goto TR_0016;
                        }
                        break;
                    }
                    goto TR_0020;
                TR_0027:
                    this.#knk = AppDomain.CurrentDomain.GetAssemblies();
                    if (ActiproLicenseManager.#qBb == null)
                    {
                        goto TR_0016;
                    }
                    else
                    {
                        this.#mL = this.#knk;
                        this.#nL = 0;
                    }
                    goto TR_0025;
                TR_0029:
                    fj = ActiproLicenseManager.#yFj(Assembly.GetEntryAssembly(), ActiproLicenseManager.LicenseTokenType, this.#WCb);
                    if (fj == null)
                    {
                        goto TR_0027;
                    }
                    else
                    {
                        this.#Uo = fj;
                        this.#Vo = 3;
                        flag = true;
                    }
                    return flag;
                TR_002D:
                    if ((this.#Uj == null) || (this.#Uj.UsageMode != LicenseUsageMode.Designtime))
                    {
                        fj = ActiproLicenseManager.#BFj(this.#Uj, ActiproLicenseManager.LicenseTokenType, this.#WCb);
                        if (fj != null)
                        {
                            this.#Uo = fj;
                            this.#Vo = 2;
                            return true;
                        }
                    }
                    else
                    {
                        goto TR_0007;
                    }
                    goto TR_0029;
                }
                fault
                {
                    this.#wC();
                }
                return flag;
            }

            [DebuggerHidden]
            private IEnumerator #tC() => 
                this.#8Fj();

            [DebuggerHidden]
            private void #vC()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            private void #wC()
            {
                int num = this.#Vo;
                if ((num == -3) || (num == 4))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.#G1i();
                    }
                }
            }

            [DebuggerHidden]
            public #gUk(int <>1__state)
            {
                this.#Vo = <>1__state;
                this.#Wo = Thread.CurrentThread.ManagedThreadId;
            }

            private ActiproLicenseManager.#ZFj System.Collections.Generic.IEnumerator<ActiproSoftware.Products.ActiproLicenseManager.LicenseInfo>.Current =>
                this.#Uo;

            private object System.Collections.IEnumerator.Current =>
                this.#Uo;
        }

        private enum #sFj
        {
            #QBb,
            #nve,
            #mve
        }

        private class #ZFj
        {
            public string Licensee { get; set; }

            public string LicenseKey { get; set; }

            public string RequiredAssemblyName { get; set; }

            public ActiproLicenseSourceLocation SourceLocation { get; set; }
        }
    }
}

