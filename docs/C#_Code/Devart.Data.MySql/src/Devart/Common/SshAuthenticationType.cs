namespace Devart.Common
{
    using System;

    public enum SshAuthenticationType
    {
        public const SshAuthenticationType PublicKey = SshAuthenticationType.PublicKey;,
        public const SshAuthenticationType Password = SshAuthenticationType.Password;,
        public const SshAuthenticationType KeyboardInteractive = SshAuthenticationType.KeyboardInteractive;
    }
}

