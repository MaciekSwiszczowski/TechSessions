using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Diagnostics.Runtime;

namespace ClrmdSpike;

internal partial class Program
{
    private static void Main()
    {
        try
        {
            // Find the target process
            var targetProcess = Process.GetProcesses()
                .FirstOrDefault(static p => p.ProcessName.Contains("hmi", StringComparison.CurrentCultureIgnoreCase));

            if (targetProcess == null)
            {
                Console.WriteLine("Hmi not found");
                return;
            }

            //  Our bitness must much the target, or there will be a runtime error
            Console.WriteLine($"Found process: {targetProcess.ProcessName} (PID: {targetProcess.Id})");
            Console.WriteLine($"Target process is {(IsWow64Process(targetProcess.Handle) ? "32-bit" : "64-bit")}");
            Console.WriteLine($"Our process is {(Environment.Is64BitProcess ? "64-bit" : "32-bit")}");

            // Attach to the process using CLRMD 
            using var dataTarget = DataTarget.AttachToProcess(targetProcess.Id, true); 
            
            // You can also attach to a dump file
            //using var dataTarget = DataTarget.LoadDump()
            
            // Get the runtime, for a C# managed app it's always the first one
            var runtime = dataTarget.ClrVersions.First().CreateRuntime();
            
            // Find the UI thread by checking for STA state
            var uiThread = runtime.Threads.FirstOrDefault(static t => t.State.HasFlag(ClrThreadState.TS_InSTA) && t.ManagedThreadId != 0);
            Console.WriteLine(uiThread != null ? $"Found UI thread - ManagedId: {uiThread.ManagedThreadId}, OS ThreadId: {uiThread.OSThreadId}" : "No STA thread found");

            // Find all ViewModel instances
            var viewModelInstances = runtime.Heap.EnumerateObjects()
                .Where(static obj => obj.Type?.Name?.EndsWith("ViewModel") == true)
                .GroupBy(static obj => obj.Type?.Name)
                .Select(static g => new 
                { 
                    TypeName = g.Key, 
                    Count = g.Count(),
                    TotalSize = g.Select(static obj => (long)obj.Size).Sum(),
                })
                .OrderByDescending(static x => x.TotalSize);

            Console.WriteLine("\nViewModel instances found:");
            foreach (var instance in viewModelInstances)
            {
                Console.WriteLine($"{instance.TypeName}: {instance.Count} instances, Total size: {instance.TotalSize:N0} bytes");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    [System.Runtime.InteropServices.LibraryImport("kernel32.dll", SetLastError = true)]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
    private static partial bool IsWow64Process(IntPtr hProcess, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)] out bool wow64Process);

    private static bool IsWow64Process(IntPtr hProcess)
    {
        return IsWow64Process(hProcess, out bool isWow64) && isWow64;
    }
}