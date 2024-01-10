﻿// Dive right in -> we use top level statements, no need for namespace, class, main method, nothing. Imports are done globally in .csproj for clean code.

// create the iterator (calls rust)
IntPtr iterator = create_arrow_iterator();
IntPtr samplePtr;
// call iterator.next() (in rust) until we get a zero pointer indicating that we finished
while ((samplePtr = next_arrow(iterator)) != IntPtr.Zero) {
    // unmarshal the pointer returned from rust into the structure
    ArrowSample sample = Marshal.PtrToStructure<ArrowSample>(samplePtr);
    Console.WriteLine($"ArrowSample: [[{sample.data[0]}, {sample.data[1]}], [{sample.data[2]}, {sample.data[3]}]]");
}
// free the rust iterator
free_arrow_iterator(iterator);


[DllImport("rust_test.dylib")]
static extern IntPtr create_arrow_iterator();

[DllImport("rust_test.dylib")]
static extern IntPtr next_arrow(IntPtr iterator);

[DllImport("rust_test.dylib")]
static extern void free_arrow_iterator(IntPtr iterator);

[StructLayout(LayoutKind.Sequential)]
public struct ArrowSample {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] data;
}







