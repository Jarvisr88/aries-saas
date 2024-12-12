namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MemberVisibilityExtensions
    {
        public static bool IsStrongerThen(this MemberVisibility visibility, MemberVisibility value)
        {
            switch (visibility)
            {
                case MemberVisibility.Public:
                    switch (value)
                    {
                        case MemberVisibility.Public:
                            return false;

                        case MemberVisibility.Protected:
                            return false;

                        case MemberVisibility.Private:
                            return false;

                        case MemberVisibility.Internal:
                            return false;

                        case MemberVisibility.ProtectedInternal:
                            return false;
                    }
                    throw new InvalidOperationException();

                case MemberVisibility.Protected:
                    switch (value)
                    {
                        case MemberVisibility.Public:
                            return false;

                        case MemberVisibility.Protected:
                            return false;

                        case MemberVisibility.Private:
                            return false;

                        case MemberVisibility.Internal:
                            return false;

                        case MemberVisibility.ProtectedInternal:
                            return true;
                    }
                    throw new InvalidOperationException();

                case MemberVisibility.Private:
                    switch (value)
                    {
                        case MemberVisibility.Public:
                            return true;

                        case MemberVisibility.Protected:
                            return true;

                        case MemberVisibility.Private:
                            return false;

                        case MemberVisibility.Internal:
                            return true;

                        case MemberVisibility.ProtectedInternal:
                            return true;
                    }
                    throw new InvalidOperationException();

                case MemberVisibility.Internal:
                    switch (value)
                    {
                        case MemberVisibility.Public:
                            return true;

                        case MemberVisibility.Protected:
                            return true;

                        case MemberVisibility.Private:
                            return false;

                        case MemberVisibility.Internal:
                            return false;

                        case MemberVisibility.ProtectedInternal:
                            return false;
                    }
                    throw new InvalidOperationException();

                case MemberVisibility.ProtectedInternal:
                    switch (value)
                    {
                        case MemberVisibility.Public:
                            return true;

                        case MemberVisibility.Protected:
                            return false;

                        case MemberVisibility.Private:
                            return false;

                        case MemberVisibility.Internal:
                            return false;

                        case MemberVisibility.ProtectedInternal:
                            return false;
                    }
                    throw new InvalidOperationException();
            }
            throw new InvalidOperationException();
        }
    }
}

