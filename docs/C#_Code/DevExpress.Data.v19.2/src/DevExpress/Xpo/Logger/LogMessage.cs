namespace DevExpress.Xpo.Logger
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using DevExpress.Xpo.DB;
    using DevExpress.Xpo.DB.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading;
    using System.Xml.Serialization;

    [Serializable]
    public class LogMessage
    {
        public const string LogParam_ConnectionProvider = "ConnectionProvider";
        public const string LogParam_CacheElement = "CacheElement";
        public const string LogParam_CookieGuid = "CookieGuid";
        public const string LogParam_CookieAge = "CookieAge";
        public const string LogParam_Provider = "Provider";
        private string error;
        private DateTime date;
        private TimeSpan duration;
        private int threadId;
        private string threadName;
        private LogMessageType messageType;
        private string messageText;
        private List<LogMessageParameter> parameterList;
        [OptionalField]
        public int LogSessionId;

        public LogMessage()
        {
            this.parameterList = new List<LogMessageParameter>();
        }

        public LogMessage(LogMessageType messageType, string messageText) : this(messageType, messageText, TimeSpan.Zero)
        {
        }

        public LogMessage(LogMessageType messageType, string messageText, TimeSpan duration)
        {
            this.parameterList = new List<LogMessageParameter>();
            this.date = DateTime.Now;
            this.duration = duration;
            this.threadId = Thread.CurrentThread.ManagedThreadId;
            this.threadName = Thread.CurrentThread.Name;
            this.messageType = messageType;
            this.messageText = messageText;
            this.LogSessionId = LogManager.LogSessionId;
        }

        public static string CollectionToString<T>(ICollection<T> collection, Function<string, T> getString)
        {
            if ((getString == null) || ((collection == null) || (collection.Count <= 0)))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (T local in collection)
            {
                if (builder.Length > 0)
                {
                    builder.Append(";");
                }
                builder.Append(getString(local));
            }
            return builder.ToString();
        }

        public static string CollectionToString(ICollection collection, Function<string, object> getString)
        {
            if ((getString == null) || ((collection == null) || (collection.Count <= 0)))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (object obj2 in collection)
            {
                if (builder.Length > 0)
                {
                    builder.Append(";");
                }
                builder.Append(getString(obj2));
            }
            return builder.ToString();
        }

        public static string CollectionToString(IList<string> nameCollection, IList valueCollection, Function2<string, string, object> getParamString)
        {
            if ((getParamString == null) || ((nameCollection == null) || ((nameCollection.Count <= 0) || ((valueCollection == null) || ((valueCollection.Count <= 0) || (nameCollection.Count != valueCollection.Count))))))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nameCollection.Count; i++)
            {
                if (builder.Length > 0)
                {
                    builder.Append(";");
                }
                builder.Append(getParamString(nameCollection[i], valueCollection[i]));
            }
            return builder.ToString();
        }

        public static LogMessage CreateMessage(object connectionProvider, IDbCommand command, TimeSpan duration)
        {
            string str = string.Empty;
            if (command.Parameters.Count != 0)
            {
                StringBuilder builder = new StringBuilder(command.Parameters.Count * 10);
                int count = command.Parameters.Count;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= count)
                    {
                        str = builder.ToString();
                        break;
                    }
                    IDataParameter parameter = (IDataParameter) command.Parameters[num2];
                    string str3 = (parameter.Value == null) ? "Null" : parameter.Value.ToString();
                    if (str3.Length > 0x40)
                    {
                        str3 = str3.Substring(0, 0x40) + "...";
                    }
                    object[] args = new object[] { str3 };
                    builder.AppendFormat(CultureInfo.InvariantCulture, (num2 == 0) ? "{{{0}}}" : ",{{{0}}}", args);
                    num2++;
                }
            }
            LogMessage message = new LogMessage(LogMessageType.DbCommand, string.Format(!string.IsNullOrEmpty(str) ? "Executing sql '{0}' with parameters {1}" : "Executing sql '{0}'", command.CommandText.Replace('\n', ' '), str), duration) {
                ParameterList = { new LogMessageParameter("ConnectionProvider", connectionProvider.GetType().ToString()) }
            };
            foreach (IDataParameter parameter2 in command.Parameters)
            {
                message.ParameterList.Add(new LogMessageParameter(parameter2.ParameterName, (parameter2.Value is DBNull) ? null : parameter2.Value));
            }
            return message;
        }

        public static LogMessage CreateMessage(object provider, string statementResult, TimeSpan duration) => 
            new LogMessage(LogMessageType.Statement, statementResult, duration) { ParameterList = { new LogMessageParameter("Provider", provider.GetType().FullName) } };

        public static LogMessage CreateMessage(object cacheElement, DataCacheCookie cookie, string statementResult, TimeSpan duration) => 
            new LogMessage(LogMessageType.Statement, statementResult, duration) { ParameterList = { 
                new LogMessageParameter("CacheElement", cacheElement.GetType().FullName),
                new LogMessageParameter("CookieGuid", cookie.Guid),
                new LogMessageParameter("CookieAge", cookie.Age)
            } };

        public static string CriteriaOperatorCollectionToString<T>(IEnumerable<T> collection) where T: CriteriaOperator
        {
            if (collection == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (CriteriaOperator @operator in collection)
            {
                if (builder.Length > 0)
                {
                    builder.Append(";");
                }
                object[] args = new object[] { @operator };
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0}", args);
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            string str = !string.IsNullOrEmpty(this.threadName) ? this.threadName : "NoName";
            return $"Date:{this.date:dd.MM.yy HH:mm:ss.fff} Duration:{this.duration} MessageType:{this.messageType} ThreadId:{this.threadId} ThreadName:{str} MessageText:{this.messageText} ParametersCount:{this.ParameterCount}.";
        }

        public static LogMessage UpdateMessage(LogMessage message, params string[] parameters)
        {
            for (int i = 0; i < (parameters.Length - 1); i += 2)
            {
                message.ParameterList.Add(new LogMessageParameter(parameters[i], parameters[i + 1]));
            }
            return message;
        }

        public static LogMessage UpdateMessageWithTables(LogMessage message, DBTable[] tables)
        {
            if ((tables != null) && (tables.Length != 0))
            {
                StringBuilder builder = new StringBuilder();
                DBTable[] tableArray = tables;
                int index = 0;
                while (true)
                {
                    if (index >= tableArray.Length)
                    {
                        message.ParameterList.Add(new LogMessageParameter("Tables", builder.ToString()));
                        break;
                    }
                    DBTable table = tableArray[index];
                    if (builder.Length > 0)
                    {
                        builder.Append(";");
                    }
                    builder.Append(table.Name);
                    index++;
                }
            }
            return message;
        }

        public DateTime Date
        {
            get => 
                this.date;
            set => 
                this.date = value;
        }

        [XmlIgnore]
        public TimeSpan Duration
        {
            get => 
                this.duration;
            set => 
                this.duration = value;
        }

        public int ThreadId
        {
            get => 
                this.threadId;
            set => 
                this.threadId = value;
        }

        public string ThreadName
        {
            get => 
                this.threadName;
            set => 
                this.threadName = value;
        }

        public LogMessageType MessageType
        {
            get => 
                this.messageType;
            set => 
                this.messageType = value;
        }

        public string MessageText
        {
            get => 
                this.messageText;
            set => 
                this.messageText = value;
        }

        public int ParameterCount =>
            this.parameterList.Count;

        public string Error
        {
            get => 
                this.error;
            set => 
                this.error = value;
        }

        [XmlIgnore]
        public List<LogMessageParameter> ParameterList =>
            this.parameterList;

        public long DurationTicks
        {
            get => 
                this.duration.Ticks;
            set => 
                this.duration = TimeSpan.FromTicks(value);
        }

        [XmlArray("parameters")]
        public LogMessageParameter[] Parameters
        {
            get => 
                ((this.parameterList == null) || (this.parameterList.Count == 0)) ? new LogMessageParameter[0] : this.parameterList.ToArray();
            set
            {
                this.parameterList.Clear();
                if ((value != null) && (value.Length != 0))
                {
                    this.parameterList.AddRange(value);
                }
            }
        }
    }
}

