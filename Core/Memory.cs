using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    internal static class Memory
    {
        private delegate void MemorySetter(IntPtr array, byte value, int count);

        private static MemorySetter MemsetDelegate;

        static Memory()
        {
            // Initialize Memset
            DynamicMethod m = new DynamicMethod("memset", 
                MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, 
                typeof(void), new[] { typeof(IntPtr), typeof(byte), typeof(int) }, typeof(OutputHandler), false);

            ILGenerator il = m.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // address
            il.Emit(OpCodes.Ldarg_1); // initialization value
            il.Emit(OpCodes.Ldarg_2); // number of bytes
            il.Emit(OpCodes.Initblk);
            il.Emit(OpCodes.Ret);

            MemsetDelegate = (MemorySetter)m.CreateDelegate(typeof(MemorySetter));
        }

        public static void Memset(byte[] array, int start, int count, byte value)
        {
            GCHandle h = default;
            try
            {
                h = GCHandle.Alloc(array, GCHandleType.Pinned);
                IntPtr addr = h.AddrOfPinnedObject() + start;
                MemsetDelegate(addr, value, count);
            }
            finally
            {
                if (h.IsAllocated)
                    h.Free();
            }
        }
    }
}
