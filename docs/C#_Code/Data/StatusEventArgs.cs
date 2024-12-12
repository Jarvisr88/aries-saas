// Decompiled with JetBrains decompiler
// Type: DMEWorks.Database.Data.StatusEventArgs
// Assembly: DMEWorks.Database, Version=2020.10.2.207, Culture=neutral, PublicKeyToken=null
// MVID: 05D8068A-6DAA-4D2C-9804-9ED26FFEA88C
// Assembly location: C:\Program Files (x86)\DME\Database Manager\DMEWorks.Database.exe

using System;

#nullable disable
namespace DMEWorks.Database.Data
{
  public class StatusEventArgs : EventArgs
  {
    public string Status { get; }

    public StatusEventArgs(string status) => this.Status = status;
  }
}
