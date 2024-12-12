// Decompiled with JetBrains decompiler
// Type: DMEWorks.Database.Data.Update
// Assembly: DMEWorks.Database, Version=2020.10.2.207, Culture=neutral, PublicKeyToken=null
// MVID: 05D8068A-6DAA-4D2C-9804-9ED26FFEA88C
// Assembly location: C:\Program Files (x86)\DME\Database Manager\DMEWorks.Database.exe

#nullable disable
namespace DMEWorks.Database.Data
{
  public sealed class Update
  {
    public readonly string Version;
    public readonly string Query;

    public Update(string version, string query)
    {
      this.Version = version ?? "";
      this.Query = query ?? "";
    }

    public override string ToString() => this.Version;
  }
}
