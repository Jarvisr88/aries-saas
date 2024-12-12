// Decompiled with JetBrains decompiler
// Type: DMEWorks.Database.Data.UpdateComparer
// Assembly: DMEWorks.Database, Version=2020.10.2.207, Culture=neutral, PublicKeyToken=null
// MVID: 05D8068A-6DAA-4D2C-9804-9ED26FFEA88C
// Assembly location: C:\Program Files (x86)\DME\Database Manager\DMEWorks.Database.exe

using System;
using System.Collections.Generic;

#nullable disable
namespace DMEWorks.Database.Data
{
  public sealed class UpdateComparer : IComparer<Update>
  {
    public static readonly UpdateComparer @default = new UpdateComparer();

    private UpdateComparer()
    {
    }

    public static int CompareVersions(string x, string y)
    {
      return string.Compare(x ?? "", y ?? "", StringComparison.OrdinalIgnoreCase);
    }

    public static int CompareVersions(Update x, Update y)
    {
      if (x == y)
        return 0;
      if (x == null)
        return -1;
      return y == null ? 1 : UpdateComparer.CompareVersions(x.Version, y.Version);
    }

    int IComparer<Update>.Compare(Update x, Update y) => UpdateComparer.CompareVersions(x, y);
  }
}
