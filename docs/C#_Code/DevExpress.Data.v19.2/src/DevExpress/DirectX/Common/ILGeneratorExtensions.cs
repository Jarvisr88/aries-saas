namespace DevExpress.DirectX.Common
{
    using System;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public static class ILGeneratorExtensions
    {
        public static void EmitLoadArg(this ILGenerator generator, int parameterIndex)
        {
            switch (parameterIndex)
            {
                case 0:
                    generator.Emit(OpCodes.Ldarg_0);
                    return;

                case 1:
                    generator.Emit(OpCodes.Ldarg_1);
                    return;

                case 2:
                    generator.Emit(OpCodes.Ldarg_2);
                    return;

                case 3:
                    generator.Emit(OpCodes.Ldarg_3);
                    return;
            }
            generator.Emit(OpCodes.Ldarg, parameterIndex);
        }

        public static void EmitLoadLocal(this ILGenerator generator, int localIndex)
        {
            switch (localIndex)
            {
                case 0:
                    generator.Emit(OpCodes.Ldloc_0);
                    return;

                case 1:
                    generator.Emit(OpCodes.Ldloc_1);
                    return;

                case 2:
                    generator.Emit(OpCodes.Ldloc_2);
                    return;

                case 3:
                    generator.Emit(OpCodes.Ldloc_3);
                    return;
            }
            generator.Emit(OpCodes.Ldloc, localIndex);
        }

        public static void EmitStoreLocal(this ILGenerator generator, int localIndex)
        {
            switch (localIndex)
            {
                case 0:
                    generator.Emit(OpCodes.Stloc_0);
                    return;

                case 1:
                    generator.Emit(OpCodes.Stloc_1);
                    return;

                case 2:
                    generator.Emit(OpCodes.Stloc_2);
                    return;

                case 3:
                    generator.Emit(OpCodes.Stloc_3);
                    return;
            }
            generator.Emit(OpCodes.Stloc, localIndex);
        }
    }
}

