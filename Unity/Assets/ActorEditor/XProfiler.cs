using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.Timers;
using System.Diagnostics;
using UDebug = UnityEngine.Debug;

public static class XProfile
{
    static Stopwatch stopwatch = new Stopwatch();
    public static void Begin(string name)
    {
        stopwatch.Start();

        Profiler.BeginSample(name);
    }

    public static void End(bool isReset = false)
    {
        stopwatch.Stop();
        UDebug.LogError(stopwatch.ElapsedMilliseconds);
        if (isReset)
            stopwatch.Reset();
        Profiler.EndSample();
    }

}
