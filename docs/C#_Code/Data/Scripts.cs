// Decompiled with JetBrains decompiler
// Type: DMEWorks.Database.Data.Scripts
// Assembly: DMEWorks.Database, Version=2020.10.2.207, Culture=neutral, PublicKeyToken=null
// MVID: 05D8068A-6DAA-4D2C-9804-9ED26FFEA88C
// Assembly location: C:\Program Files (x86)\DME\Database Manager\DMEWorks.Database.exe

using Devart.Data.MySql;
using DMEWorks.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace DMEWorks.Database.Data
{
  internal static class Scripts
  {
    private const RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
    private static readonly Regex regexInitial = new Regex("^Initial part \\d{2}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex regexUpdate = new Regex("^Update \\d{4}-\\d{2}-\\d{2} part \\d{2}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex regexRoutine = new Regex("^Routines part \\d{2}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex regexExecute = new Regex("^Execute part \\d{2}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex regexVersion = new Regex("^Get version$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex regexSample = new Regex("^Samples part \\d{2}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex regexCompany = new Regex("^Companies part \\d{2}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex regexDoctor = new Regex("^Doctors part \\d{2}$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private const string LineDelimiter = ";\r\n";
    private static Regex DropTriggerRegex = new Regex("^\\s*DROP\\s+TRIGGER\\s", RegexOptions.IgnoreCase | RegexOptions.Singleline);

    public static bool IsUpdate(string version) => Scripts.regexUpdate.IsMatch(version);

    public static bool IsInitial(this Update update)
    {
      return Scripts.regexInitial.IsMatch(update.Version);
    }

    public static bool IsRoutine(this Update update)
    {
      return Scripts.regexRoutine.IsMatch(update.Version);
    }

    public static bool IsExecute(this Update update)
    {
      return Scripts.regexExecute.IsMatch(update.Version);
    }

    public static bool IsVersion(this Update update)
    {
      return Scripts.regexVersion.IsMatch(update.Version);
    }

    public static bool IsSample(this Update update) => Scripts.regexSample.IsMatch(update.Version);

    public static bool IsCompany(this Update update)
    {
      return Scripts.regexCompany.IsMatch(update.Version);
    }

    public static bool IsDoctor(this Update update) => Scripts.regexDoctor.IsMatch(update.Version);

    private static string LoadResource(Assembly assembly, string resource)
    {
      using (Stream manifestResourceStream = assembly.GetManifestResourceStream(resource))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
          return streamReader.ReadToEnd();
      }
    }

    public static ReadOnlyCollection<Update> CreateUpdateCollection(params string[] prefixes)
    {
      Scripts.ResourceNameHelper resourceNameHelper = new Scripts.ResourceNameHelper(prefixes);
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      List<Update> updateList = new List<Update>();
      foreach (string manifestResourceName in executingAssembly.GetManifestResourceNames())
      {
        string updateName;
        if (resourceNameHelper.TryRemovePrefix(manifestResourceName, out updateName))
        {
          string withoutExtension = Path.GetFileNameWithoutExtension(updateName);
          string query = Scripts.LoadResource(executingAssembly, manifestResourceName);
          updateList.Add(new Update(withoutExtension, query));
        }
      }
      updateList.Sort((IComparer<Update>) UpdateComparer.@default);
      for (int index = 1; index < updateList.Count; ++index)
      {
        if (string.Equals(updateList[index - 1].Version, updateList[index].Version, StringComparison.OrdinalIgnoreCase))
          throw new ArgumentException("collection contains two updates with same version '" + updateList[index].Version + "'");
      }
      return new ReadOnlyCollection<Update>((IList<Update>) updateList.ToArray());
    }

    private static bool Ask(string Statement)
    {
      return MessageBox.Show("Error executing query:\r\n" + Statement + "\r\nWould you like to proceed?", "MultistatmentExecute error", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    }

    private static void ExecuteStatement(MySqlCommand cmd, string stmt)
    {
      if (string.IsNullOrWhiteSpace(stmt))
        return;
      cmd.CommandText = stmt;
      try
      {
        DateTime now = DateTime.Now;
        cmd.ExecuteNonQuery();
        TimeSpan timeSpan = DateTime.Now - now;
        if (10 >= timeSpan.Seconds)
          return;
        TraceHelper.TraceInfo("Execution of the following query takes " + timeSpan.ToString() + "\r\n" + stmt);
      }
      catch (Exception ex)
      {
        if (Scripts.DropTriggerRegex.Match(cmd.CommandText).Success)
        {
          TraceHelper.TraceException(ex);
        }
        else
        {
          if (!Scripts.Ask(cmd.CommandText))
            throw new Exception("Error executing query:\r\n" + cmd.CommandText, ex);
          TraceHelper.TraceException(ex);
        }
      }
    }

    public static void ExecuteScript(MySqlCommand cmd, string script)
    {
      int num;
      for (int startIndex = 0; startIndex < script.Length; startIndex = num + ";\r\n".Length)
      {
        num = script.IndexOf(";\r\n", startIndex);
        if (num < 0)
          num = script.Length;
        Scripts.ExecuteStatement(cmd, script.Substring(startIndex, num - startIndex));
      }
    }

    private sealed class ResourceNameHelper
    {
      private readonly string[] prefixes;

      public ResourceNameHelper(string[] prefixes)
      {
        this.prefixes = prefixes ?? throw new ArgumentNullException(nameof (prefixes));
      }

      public bool TryRemovePrefix(string resourceName, out string updateName)
      {
        foreach (string prefix in this.prefixes)
        {
          if (string.Compare(resourceName, 0, prefix, 0, prefix.Length, StringComparison.OrdinalIgnoreCase) == 0)
          {
            updateName = resourceName.Substring(prefix.Length);
            return true;
          }
        }
        updateName = (string) null;
        return false;
      }
    }
  }
}
