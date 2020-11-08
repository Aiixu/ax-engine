using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static class MemoryHelper
    {
        private delegate void MemorySetter(IntPtr arr, byte val, int count);

        private static readonly MemorySetter MemsetDelegate;

        static MemoryHelper()
        {
            // Init memset
            DynamicMethod dynamicMemset = new DynamicMethod("memset",
                MethodAttributes.Public | MethodAttributes.Static,
                CallingConventions.Standard,
                typeof(void), new[] { typeof(IntPtr), typeof(byte), typeof(int) },
                typeof(MemoryHelper), false);

            ILGenerator ilMemset = dynamicMemset.GetILGenerator();
            ilMemset.Emit(OpCodes.Ldarg_0); // addr
            ilMemset.Emit(OpCodes.Ldarg_1); // val
            ilMemset.Emit(OpCodes.Ldarg_2); // count
            ilMemset.Emit(OpCodes.Initblk);
            ilMemset.Emit(OpCodes.Ret);

            MemsetDelegate = (MemorySetter)dynamicMemset.CreateDelegate(typeof(MemorySetter));
        }

        public static void Memset(byte[] arr, int start, int count, byte value)
        {
            GCHandle h = default;
            try
            {
                // alloc
                h = GCHandle.Alloc(arr, GCHandleType.Pinned);
                IntPtr addr = h.AddrOfPinnedObject() + start; // offset address
                MemsetDelegate(addr, value, count);
            }
            finally
            {
                if(h.IsAllocated)
                {
                    h.Free();
                }
            }
        }
    }
}
