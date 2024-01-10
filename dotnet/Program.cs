using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct ArrowSample {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] data;
}

class Program {
    [DllImport("rust_test.dylib")]
    public static extern IntPtr create_arrow_iterator();

    [DllImport("rust_test.dylib")]
    public static extern IntPtr next_arrow(IntPtr iterator);

    [DllImport("rust_test.dylib")]
    public static extern void free_arrow_iterator(IntPtr iterator);

    static void Main() {
        IntPtr iterator = create_arrow_iterator();
        IntPtr samplePtr;
        while ((samplePtr = next_arrow(iterator)) != IntPtr.Zero) {
            ArrowSample sample = Marshal.PtrToStructure<ArrowSample>(samplePtr);
            Console.WriteLine($"ArrowSample: [[{sample.data[0]}, {sample.data[1]}], [{sample.data[2]}, {sample.data[3]}]]");
        }

        free_arrow_iterator(iterator);
    }
}
