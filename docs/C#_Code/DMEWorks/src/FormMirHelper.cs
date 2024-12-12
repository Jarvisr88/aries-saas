using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;

public class FormMirHelper
{
    private ArrayList FControls = new ArrayList();

    public void Add(string Key, Control Control, string Message)
    {
        this.FControls.Add(new MirControl(Key, Control, Message));
    }

    private static void AppendString(Hashtable Hash, object key, string value)
    {
        value ??= "";
        object obj2 = Hash[key];
        if (!(obj2 is string) || (Conversions.ToString(obj2) == ""))
        {
            Hash[key] = value;
        }
        else if (value != "")
        {
            Hash[key] = Conversions.ToString(obj2) + "\r\n" + value;
        }
    }

    public void ShowMissingInformation(ErrorProvider MissingProvider, string keys)
    {
        IEnumerator enumerator2;
        Hashtable hashtable = CollectionsUtil.CreateCaseInsensitiveHashtable();
        if (keys != null)
        {
            char[] separator = new char[] { ',' };
            foreach (string str in keys.Split(separator))
            {
                hashtable[str] = str;
            }
        }
        Hashtable hash = new Hashtable();
        try
        {
            enumerator2 = this.FControls.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                MirControl current = (MirControl) enumerator2.Current;
                if (hashtable.ContainsKey(current.Key))
                {
                    AppendString(hash, current.Control, current.Message);
                    continue;
                }
                AppendString(hash, current.Control, "");
            }
        }
        finally
        {
            if (enumerator2 is IDisposable)
            {
                (enumerator2 as IDisposable).Dispose();
            }
        }
        IDictionaryEnumerator enumerator = hash.GetEnumerator();
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
            Control key = (Control) enumerator.Key;
            MissingProvider.SetIconPadding(key, -16);
            MissingProvider.SetError(key, Conversions.ToString(enumerator.Value));
        }
    }

    private static void ShowMissingInformation(ErrorProvider MissingProvider, bool Show, Control control, string Error)
    {
        if (!Show)
        {
            MissingProvider.SetError(control, "");
        }
        else
        {
            MissingProvider.SetIconPadding(control, -16);
            MissingProvider.SetError(control, Error);
        }
    }

    private class MirControl
    {
        public readonly string Key;
        public readonly System.Windows.Forms.Control Control;
        public readonly string Message;

        public MirControl(string Key, System.Windows.Forms.Control Control, string Message)
        {
            this.Key = Key;
            this.Control = Control;
            this.Message = Message;
        }
    }
}

