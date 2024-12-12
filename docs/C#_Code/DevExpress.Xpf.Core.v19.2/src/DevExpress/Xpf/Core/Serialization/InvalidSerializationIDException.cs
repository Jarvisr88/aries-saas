namespace DevExpress.Xpf.Core.Serialization
{
    using System;

    public class InvalidSerializationIDException : Exception
    {
        private const string messageFormat = "Invalid SerializationID (\"{0}\"). SerializationID must not be null or empty, or contain the '{1}' character.";

        private InvalidSerializationIDException(string id) : base($"Invalid SerializationID ("{id}"). SerializationID must not be null or empty, or contain the '{"$"}' character.")
        {
        }

        public static void Assert(string id)
        {
            if (!string.IsNullOrEmpty(id) && id.Contains("$"))
            {
                throw new InvalidSerializationIDException(id);
            }
        }
    }
}

