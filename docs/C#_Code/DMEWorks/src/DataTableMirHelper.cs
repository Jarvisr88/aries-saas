using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

public class DataTableMirHelper
{
    private readonly Hashtable F_MirMessages = CollectionsUtil.CreateCaseInsensitiveHashtable();

    public void Add(string Key, string Message)
    {
        this.F_MirMessages[Key] = Message;
    }

    public string GetErrorMessage(string keys)
    {
        Hashtable hashtable = CollectionsUtil.CreateCaseInsensitiveHashtable();
        if (keys != null)
        {
            char[] separator = new char[] { ',' };
            foreach (string str in keys.Split(separator))
            {
                hashtable[str] = str;
            }
        }
        IDictionaryEnumerator enumerator = this.F_MirMessages.GetEnumerator();
        enumerator.Reset();
        StringBuilder builder = new StringBuilder();
        while (enumerator.MoveNext())
        {
            if (!hashtable.ContainsKey(enumerator.Key))
            {
                continue;
            }
            builder.Append(enumerator.Value);
            builder.Append("\r\n");
        }
        return builder.ToString();
    }
}

