namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;
    using System.Security.Permissions;

    internal class CRLicenseProvider : LicenseProvider
    {
        private static CRLicenseProvider a;
        private static at b;

        private string a(string A_0)
        {
            Uri uri = new Uri(A_0);
            return (uri.LocalPath + uri.Fragment);
        }

        private string a(object A_0, Type A_1)
        {
            PropertyInfo property = A_1.GetProperty("ProjectItems");
            if (property != null)
            {
                object obj2 = property.GetValue(A_0, null);
                PropertyInfo info2 = property.PropertyType.GetProperty("Count");
                if (info2 != null)
                {
                    int num = (int) info2.GetValue(obj2, null);
                    for (int i = 1; i <= num; i++)
                    {
                        MethodInfo method = property.PropertyType.GetMethod("Item");
                        if (method != null)
                        {
                            object[] parameters = new object[] { i };
                            object obj3 = method.Invoke(obj2, parameters);
                            PropertyInfo info4 = method.ReturnType.GetProperty("Name");
                            if ((info4 == null) || (string.Compare((string) info4.GetValue(obj3, null), "licenses.licx", true, CultureInfo.InvariantCulture) == 0))
                            {
                                string str = null;
                                int num3 = -1;
                                PropertyInfo info5 = method.ReturnType.GetProperty("Properties");
                                if (info5 != null)
                                {
                                    object obj4 = info5.GetValue(obj3, null);
                                    PropertyInfo info6 = info5.PropertyType.GetProperty("Count");
                                    if (info6 != null)
                                    {
                                        int num4 = (int) info6.GetValue(obj4, null);
                                        int num5 = 1;
                                        while (true)
                                        {
                                            if ((num5 > num4) || ((str != null) && (num3 != -1)))
                                            {
                                                if (num3 != 3)
                                                {
                                                    str = null;
                                                }
                                                break;
                                            }
                                            MethodInfo info7 = info5.PropertyType.GetMethod("Item");
                                            if (info7 != null)
                                            {
                                                object[] objArray2 = new object[] { num5 };
                                                object obj5 = info7.Invoke(obj4, objArray2);
                                                PropertyInfo info8 = info7.ReturnType.GetProperty("Name");
                                                if (info8 != null)
                                                {
                                                    string str2 = (string) info8.GetValue(obj5, null);
                                                    if (str2 == "FullPath")
                                                    {
                                                        PropertyInfo info9 = info7.ReturnType.GetProperty("Value");
                                                        if (info9 != null)
                                                        {
                                                            str = (string) info9.GetValue(obj5, null);
                                                        }
                                                    }
                                                    else if (str2 == "BuildAction")
                                                    {
                                                        PropertyInfo info10 = info7.ReturnType.GetProperty("Value");
                                                        if (info10 != null)
                                                        {
                                                            num3 = (int) info10.GetValue(obj5, null);
                                                        }
                                                    }
                                                }
                                            }
                                            num5++;
                                        }
                                    }
                                }
                                return str;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private Stream a(Assembly A_0, string A_1)
        {
            CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            string name = A_0.GetName().Name;
            string[] manifestResourceNames = A_0.GetManifestResourceNames();
            int index = 0;
            while (true)
            {
                if (index < manifestResourceNames.Length)
                {
                    string str2 = manifestResourceNames[index];
                    if ((compareInfo.Compare(str2, A_1, CompareOptions.IgnoreCase) != 0) && ((compareInfo.Compare(str2, name + ".exe.licenses") != 0) && (compareInfo.Compare(str2, name + ".dll.licenses") != 0)))
                    {
                        index++;
                        continue;
                    }
                    A_1 = str2;
                }
                return A_0.GetManifestResourceStream(A_1);
            }
        }

        private void a(out string A_0, out string A_1)
        {
            A_0 = null;
            A_1 = null;
            string path = null;
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            if ((commandLineArgs != null) && (commandLineArgs.Length >= 2))
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                if ((entryAssembly != null) && (entryAssembly.GetName().Name.ToUpper() == "LC"))
                {
                    int index = 1;
                    while (true)
                    {
                        if (index < commandLineArgs.Length)
                        {
                            string str2 = commandLineArgs[index].Trim();
                            if (!str2.EndsWith(".rsp"))
                            {
                                int num2 = -1;
                                int startIndex = 7;
                                if ((str2.Length >= 7) && (str2.Substring(0, 7) == "/target"))
                                {
                                    num2 = 0;
                                    startIndex = 7;
                                }
                                else if ((str2.Length >= 9) && (str2.Substring(0, 9) == "/complist"))
                                {
                                    num2 = 1;
                                    startIndex = 9;
                                }
                                if (num2 != -1)
                                {
                                    int num4 = 0;
                                    string str3 = null;
                                    bool flag = false;
                                    while (true)
                                    {
                                        if ((startIndex >= str2.Length) || flag)
                                        {
                                            if (str3 != null)
                                            {
                                                if (num2 == 0)
                                                {
                                                    A_0 = Path.GetFileNameWithoutExtension(str3);
                                                }
                                                else if (num2 == 1)
                                                {
                                                    A_1 = Path.Combine(Directory.GetCurrentDirectory(), str3);
                                                }
                                            }
                                            break;
                                        }
                                        switch (num4)
                                        {
                                            case 0:
                                                if (str2[startIndex] != ' ')
                                                {
                                                    num4++;
                                                    startIndex--;
                                                }
                                                break;

                                            case 1:
                                                if (str2[startIndex] != ':')
                                                {
                                                    flag = true;
                                                }
                                                num4++;
                                                break;

                                            case 2:
                                                if (str2[startIndex] != ' ')
                                                {
                                                    num4++;
                                                    startIndex--;
                                                }
                                                break;

                                            case 3:
                                                str3 = str2.Substring(startIndex);
                                                flag = true;
                                                break;

                                            default:
                                                break;
                                        }
                                        startIndex++;
                                    }
                                }
                                index++;
                                continue;
                            }
                            path = str2;
                        }
                        if (path != null)
                        {
                            if (path[0] == '@')
                            {
                                path = path.Substring(1);
                            }
                            string str4 = File.ReadAllText(path);
                            int num5 = 0;
                            while (num5 < 2)
                            {
                                num5++;
                                int startIndex = -1;
                                string str5 = (num5 == 1) ? "/target:" : "/complist:";
                                startIndex = str4.IndexOf(str5);
                                if (startIndex != -1)
                                {
                                    startIndex += str5.Length;
                                    int num7 = startIndex;
                                    int num8 = 0;
                                    bool flag2 = false;
                                    string str6 = null;
                                    while (true)
                                    {
                                        if ((num7 >= str4.Length) || (((str4[num7] == ' ') & flag2) && ((num8 % 2) == 0)))
                                        {
                                            str6 = str4.Substring(startIndex, num7 - startIndex);
                                            if (num8 > 0)
                                            {
                                                char[] trimChars = new char[] { '"' };
                                                str6 = str6.Trim(trimChars);
                                            }
                                            if (num5 == 1)
                                            {
                                                A_0 = Path.GetFileNameWithoutExtension(str6);
                                            }
                                            else
                                            {
                                                A_1 = Path.Combine(Directory.GetCurrentDirectory(), str6);
                                            }
                                            break;
                                        }
                                        if (str4[num7] == '"')
                                        {
                                            num8++;
                                        }
                                        else
                                        {
                                            flag2 = true;
                                        }
                                        num7++;
                                    }
                                }
                            }
                        }
                        return;
                    }
                }
            }
        }

        public License a(LicenseContext A_0, Type A_1, bool A_2)
        {
            lock (this)
            {
                at b = null;
                string str = null;
                if ((CRLicenseProvider.b != null) && ((CRLicenseProvider.b.e != 0) && (CRLicenseProvider.b.e != 1)))
                {
                    b = CRLicenseProvider.b;
                }
                else
                {
                    string licenseKey;
                    bool flag;
                    int num = 0;
                    string str3 = null;
                    if ((CRLicenseProvider.b != null) && (CRLicenseProvider.b.e != 1))
                    {
                        licenseKey = CRLicenseProvider.b.LicenseKey;
                        str = CRLicenseProvider.b.d();
                        flag = CRLicenseProvider.b.b();
                    }
                    else
                    {
                        num = this.a(A_0, A_1, out licenseKey, out flag, out str, out str3);
                        if ((A_2 && ((num == 1) || (num == 8))) && ((licenseKey == null) || (licenseKey == "")))
                        {
                            num = ab.f().a(out licenseKey, out str3);
                            flag = false;
                        }
                    }
                    num ??= ab.f().a(licenseKey, str, flag, out str3);
                    CRLicenseProvider.b = b = new at(licenseKey, str, flag, num, str3);
                }
                return b;
            }
        }

        private void a(LicenseContext A_0, out string A_1, out string A_2)
        {
            A_1 = string.Empty;
            A_2 = string.Empty;
            if (ReferenceEquals(A_0.GetType().BaseType, typeof(DesigntimeLicenseContext)) && (string.Compare(A_0.GetType().Name, "HostDesigntimeLicenseContext", true) != 0))
            {
                foreach (FieldInfo info in A_0.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    if (ReferenceEquals(info.FieldType, typeof(string)))
                    {
                        string str = info.GetValue(A_0) as string;
                        if (!string.IsNullOrEmpty(str) && Path.HasExtension(str))
                        {
                            if (Path.GetExtension(str).ToLower() == ".licx")
                            {
                                A_2 = str;
                            }
                            else if ((Path.GetExtension(str).ToLower() == ".exe") || (Path.GetExtension(str).ToLower() == ".dll"))
                            {
                                A_1 = Path.GetFileNameWithoutExtension(str);
                            }
                            if (!string.IsNullOrEmpty(A_2) && !string.IsNullOrEmpty(A_1))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        public override License a(LicenseContext A_0, Type A_1, object A_2, bool A_3)
        {
            if (A_0.UsageMode != LicenseUsageMode.Designtime)
            {
                return ((A_0.UsageMode != LicenseUsageMode.Runtime) ? null : this.a(A_0, A_1, true));
            }
            at b = null;
            string fileNameWithoutExtension = null;
            string str2 = null;
            string licenseKey = null;
            object obj2 = null;
            FieldInfo field = A_0.GetType().GetField("provider", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                object obj3 = field.GetValue(A_0);
                if (obj3 != null)
                {
                    field = obj3.GetType().GetField("typeResolver", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        obj2 = field.GetValue(obj3);
                    }
                }
            }
            if (obj2 == null)
            {
                field = A_0.GetType().GetField("typeResolver", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    obj2 = field.GetValue(A_0);
                }
            }
            if (obj2 != null)
            {
                PropertyInfo property = obj2.GetType().GetProperty("Project", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
                if (property != null)
                {
                    object obj4 = property.GetValue(obj2, null);
                    str2 = this.a(obj4, property.PropertyType);
                    PropertyInfo info3 = property.PropertyType.GetProperty("Properties");
                    if (info3 != null)
                    {
                        object obj5 = info3.GetValue(obj4, null);
                        int num = (int) info3.PropertyType.GetProperty("Count").GetValue(obj5, null);
                        MethodInfo method = info3.PropertyType.GetMethod("Item");
                        for (int i = 1; i <= num; i++)
                        {
                            object[] parameters = new object[] { i };
                            object obj6 = method.Invoke(obj5, parameters);
                            if ((method.ReturnType.GetProperty("Name").GetValue(obj6, null) as string) == "AssemblyName")
                            {
                                fileNameWithoutExtension = method.ReturnType.GetProperty("Value").GetValue(obj6, null) as string;
                            }
                        }
                    }
                }
            }
            else
            {
                field = null;
                FieldInfo[] fields = A_0.GetType().GetFields(BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
                int index = 0;
                while (true)
                {
                    if (index < fields.Length)
                    {
                        if (fields[index].FieldType.Name != "IOTAProject")
                        {
                            index++;
                            continue;
                        }
                        field = fields[index];
                    }
                    if (field != null)
                    {
                        object obj7 = field.GetValue(A_0);
                        if (obj7 != null)
                        {
                            PropertyInfo property = field.FieldType.GetProperty("ProjectOptions");
                            if (property != null)
                            {
                                object obj8 = property.GetValue(obj7, null);
                                PropertyInfo info6 = property.PropertyType.GetProperty("TargetName");
                                if (info6 != null)
                                {
                                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension((string) info6.GetValue(obj8, null));
                                }
                            }
                        }
                    }
                    break;
                }
            }
            if (fileNameWithoutExtension == null)
            {
                this.a(out fileNameWithoutExtension, out str2);
            }
            fileNameWithoutExtension ??= "";
            str2 ??= "";
            if ((fileNameWithoutExtension == "") && (str2 == ""))
            {
                PropertyInfo property = A_0.GetType().GetProperty("OutputFilename");
                if (property != null)
                {
                    fileNameWithoutExtension = property.GetValue(A_0, null) as string;
                    fileNameWithoutExtension ??= "";
                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
                }
                PropertyInfo info8 = A_0.GetType().GetProperty("LicxFilename");
                if (info8 != null)
                {
                    str2 = info8.GetValue(A_0, null) as string;
                    str2 ??= "";
                }
            }
            if ((fileNameWithoutExtension == "") && ((str2 == "") && (CRLicenseProvider.b == null)))
            {
                this.a(A_0, out fileNameWithoutExtension, out str2);
            }
            if ((fileNameWithoutExtension != "") || ((CRLicenseProvider.b == null) || !CRLicenseProvider.b.b()))
            {
                string str4;
                CRLicenseProvider.b = b = new at(licenseKey, fileNameWithoutExtension, true, ab.f().a(fileNameWithoutExtension, str2, out licenseKey, out str4), str4);
            }
            else
            {
                licenseKey = CRLicenseProvider.b.LicenseKey;
                b = CRLicenseProvider.b;
            }
            A_0.SetSavedLicenseKey(A_1, licenseKey);
            return b;
        }

        private int a(LicenseContext A_0, Type A_1, out string A_2, out string A_3, out string A_4)
        {
            int num = 1;
            int num2 = 1;
            A_4 = null;
            string str = null;
            A_3 = null;
            string str2 = null;
            A_2 = null;
            string path = null;
            string licenseFile = null;
            string applicationBase = null;
            try
            {
                licenseFile = AppDomain.CurrentDomain.SetupInformation.LicenseFile;
                applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
            catch
            {
            }
            if ((licenseFile != null) && (applicationBase != null))
            {
                path = Path.Combine(applicationBase, licenseFile);
            }
            if ((path != null) && File.Exists(path))
            {
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                string fileName = Path.GetFileName(path);
                num = this.a(stream, fileName.Substring(0, fileName.LastIndexOf(".")).ToUpper(CultureInfo.InvariantCulture), A_1.AssemblyQualifiedName, out A_2, out A_4);
                if (num == 10)
                {
                    return num;
                }
                if (num == 0)
                {
                    num = ab.f().a(A_2, null, true, out A_3, out A_4);
                    if (num == 0)
                    {
                        return 0;
                    }
                    if (num != 1)
                    {
                        str2 = A_3;
                        str = A_4;
                        num2 = num;
                    }
                }
            }
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if ((entryAssembly != null) && !entryAssembly.GetType().Name.Contains("AssemblyBuilder"))
            {
                string str10;
                Stream manifestResourceStream;
                if (A_0 != null)
                {
                    A_2 = A_0.GetSavedLicenseKey(A_1, entryAssembly);
                    if (A_2 != null)
                    {
                        num = ab.f().a(A_2, entryAssembly, true, out A_3, out A_4);
                        if (num == 0)
                        {
                            return 0;
                        }
                        if (num != 1)
                        {
                            str2 = A_3;
                            str = A_4;
                            num2 = num;
                        }
                    }
                }
                string str7 = ab.a(entryAssembly);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(str7);
                string str9 = Path.GetExtension(str7).ToUpper();
                if ((str9 == ".EXE") || (str9 == ".DLL"))
                {
                    str10 = str7 + ".licenses";
                    manifestResourceStream = entryAssembly.GetManifestResourceStream(str10);
                }
                else
                {
                    str7 = fileNameWithoutExtension + ".exe";
                    str10 = str7 + ".licenses";
                    manifestResourceStream = entryAssembly.GetManifestResourceStream(str10);
                    if (manifestResourceStream == null)
                    {
                        str7 = fileNameWithoutExtension + ".dll";
                        str10 = str7 + ".licenses";
                        manifestResourceStream = entryAssembly.GetManifestResourceStream(str10);
                    }
                }
                if (manifestResourceStream != null)
                {
                    num = this.a(manifestResourceStream, str7.ToUpper(CultureInfo.InvariantCulture), A_1.AssemblyQualifiedName, out A_2, out A_4);
                    if (num == 10)
                    {
                        return num;
                    }
                    if (num != 1)
                    {
                        num = ab.f().a(A_2, entryAssembly, true, out A_3, out A_4);
                        if (num == 0)
                        {
                            return 0;
                        }
                        if (num != 1)
                        {
                            str2 = A_3;
                            str = A_4;
                            num2 = num;
                        }
                    }
                }
                CultureInfo culture = CultureInfo.InvariantCulture;
                str10 = str10.ToUpper(culture);
                string name = entryAssembly.GetName().Name;
                string str11 = (name + ".exe.licenses").ToUpper(culture);
                string str12 = (name + ".dll.licenses").ToUpper(culture);
                foreach (string str13 in entryAssembly.GetManifestResourceNames())
                {
                    string str14 = str13.ToUpper(culture);
                    if (str14.EndsWith(str10) || (str14.EndsWith(str11) || str14.EndsWith(str12)))
                    {
                        manifestResourceStream = entryAssembly.GetManifestResourceStream(str13);
                        if (manifestResourceStream != null)
                        {
                            num = this.a(manifestResourceStream, str7.ToUpper(CultureInfo.InvariantCulture), A_1.AssemblyQualifiedName, out A_2, out A_4);
                            if (num == 10)
                            {
                                return num;
                            }
                            if (num == 0)
                            {
                                num = ab.f().a(A_2, entryAssembly, true, out A_3, out A_4);
                                if (num == 0)
                                {
                                    return 0;
                                }
                                if (num != 1)
                                {
                                    str2 = A_3;
                                    str = A_4;
                                    num2 = num;
                                }
                            }
                        }
                    }
                }
            }
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly resourceAssembly = assemblies[i];
                if ((resourceAssembly != null) && (!resourceAssembly.GetType().Name.Contains("AssemblyBuilder") && resourceAssembly.FullName.StartsWith("App_Licenses,")))
                {
                    if (A_0 != null)
                    {
                        A_2 = A_0.GetSavedLicenseKey(A_1, resourceAssembly);
                        if (A_2 != null)
                        {
                            num = ab.f().a(A_2, resourceAssembly, true, out A_3, out A_4);
                            if (num == 0)
                            {
                                return 0;
                            }
                            if (num != 1)
                            {
                                str2 = A_3;
                                str = A_4;
                                num2 = num;
                            }
                        }
                    }
                    string str15 = "APP_LICENSES.DLL.LICENSES";
                    foreach (string str16 in resourceAssembly.GetManifestResourceNames())
                    {
                        if (str16.ToUpper(invariantCulture).EndsWith(str15))
                        {
                            Stream manifestResourceStream = resourceAssembly.GetManifestResourceStream(str16);
                            manifestResourceStream ??= this.a(resourceAssembly, str16);
                            if (manifestResourceStream != null)
                            {
                                num = this.a(manifestResourceStream, "APP_LICENSES.DLL", A_1.AssemblyQualifiedName, out A_2, out A_4);
                                if (num == 10)
                                {
                                    return num;
                                }
                                if (num == 0)
                                {
                                    num = ab.f().a(A_2, resourceAssembly, true, out A_3, out A_4);
                                    if (num == 0)
                                    {
                                        return 0;
                                    }
                                    if (num != 1)
                                    {
                                        str2 = A_3;
                                        str = A_4;
                                        num2 = num;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < assemblies.Length; j++)
            {
                Assembly assembly3 = assemblies[j];
                if ((assembly3 != null) && !assembly3.GetType().Name.Contains("AssemblyBuilder"))
                {
                    string str18 = ab.a(assembly3);
                    string str17 = (str18 + ".licenses").ToUpper(invariantCulture);
                    foreach (string str19 in assembly3.GetManifestResourceNames())
                    {
                        if (str19.ToUpper(invariantCulture).EndsWith(str17))
                        {
                            Stream manifestResourceStream = assembly3.GetManifestResourceStream(str19);
                            manifestResourceStream ??= this.a(assembly3, str19);
                            if (manifestResourceStream != null)
                            {
                                num = this.a(manifestResourceStream, str18.ToUpper(invariantCulture), A_1.AssemblyQualifiedName, out A_2, out A_4);
                                if (num == 10)
                                {
                                    return num;
                                }
                                if (num == 0)
                                {
                                    num = ab.f().a(A_2, assembly3, true, out A_3, out A_4);
                                    if (num == 0)
                                    {
                                        return 0;
                                    }
                                    if (num != 1)
                                    {
                                        str2 = A_3;
                                        str = A_4;
                                        num2 = num;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (num2 != 1)
            {
                A_3 = str2;
                A_4 = str;
                num = num2;
            }
            return num;
        }

        internal int a(Stream A_0, string A_1, string A_2, out string A_3, out string A_4)
        {
            object obj2;
            int num5;
            A_3 = null;
            A_4 = null;
            IFormatter formatter = new BinaryFormatter();
            try
            {
                try
                {
                    new SecurityPermission(SecurityPermissionFlag.SerializationFormatter).PermitOnly();
                    new SecurityPermission(SecurityPermissionFlag.SerializationFormatter).Assert();
                }
                catch
                {
                }
                try
                {
                    obj2 = formatter.Deserialize(A_0);
                }
                finally
                {
                    try
                    {
                        CodeAccessPermission.RevertAssert();
                        CodeAccessPermission.RevertPermitOnly();
                    }
                    catch
                    {
                    }
                }
                goto TR_0022;
            }
            catch (Exception exception)
            {
                A_4 = exception.Message;
                num5 = 10;
            }
            return num5;
        TR_0022:
            if (!(obj2 is object[]))
            {
                return 1;
            }
            int index = A_2.IndexOf("`");
            if (index != -1)
            {
                A_2 = A_2.Remove(index, (A_2.LastIndexOf("]]") - index) + 2);
            }
            object[] objArray = (object[]) obj2;
            if (!(objArray[0] is string))
            {
                return 1;
            }
            if (Path.GetFileName((string) objArray[0]) != A_1)
            {
                return 1;
            }
            int startIndex = A_2.IndexOf("Version=");
            startIndex = A_2.IndexOf('.', startIndex);
            int num3 = A_2.IndexOf(",", startIndex);
            A_2 = (num3 == -1) ? A_2.Substring(0, startIndex) : (A_2.Substring(0, startIndex) + A_2.Substring(num3));
            IDictionaryEnumerator enumerator = ((Hashtable) objArray[1]).GetEnumerator();
            while (true)
            {
                string key;
                if (enumerator.MoveNext())
                {
                    key = (string) enumerator.Key;
                    startIndex = key.IndexOf('.', key.IndexOf("Version="));
                    num3 = key.IndexOf(",", startIndex);
                    key = (num3 == -1) ? key.Substring(0, startIndex) : (key.Substring(0, startIndex) + key.Substring(num3));
                    if (key != A_2)
                    {
                        continue;
                    }
                    A_3 = (string) enumerator.Value;
                    if (A_3 == null)
                    {
                        return 2;
                    }
                }
                int length = A_2.IndexOf(",");
                if (length != -1)
                {
                    A_2 = A_2.Substring(0, length);
                }
                enumerator.Reset();
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        key = (string) enumerator.Key;
                        length = key.IndexOf(",");
                        if (num3 != -1)
                        {
                            key = key.Substring(0, length);
                        }
                        if (key != A_2)
                        {
                            continue;
                        }
                        A_3 = (string) enumerator.Value;
                    }
                    return ((A_3 != null) ? 0 : 1);
                }
            }
        }

        private int a(LicenseContext A_0, Type A_1, out string A_2, out bool A_3, out string A_4, out string A_5)
        {
            A_3 = true;
            int num = this.a(A_0, A_1, out A_2, out A_4, out A_5);
            if ((num != 0) && (((A_2 == null) || (A_2 == "")) && (Assembly.GetEntryAssembly() == null)))
            {
                num = ab.f().a(out A_2, out A_5);
                A_3 = false;
            }
            return num;
        }

        public static CRLicenseProvider SingletonInstance
        {
            get
            {
                lock (typeof(CRLicenseProvider))
                {
                    a ??= new CRLicenseProvider();
                    return a;
                }
            }
        }
    }
}

