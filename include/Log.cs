using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KSPBuildTools
{
    internal static class Log
    {
        internal static string Prefix = "[" + Assembly.GetExecutingAssembly().GetName().Name + "] ";

		[System.Diagnostics.Conditional("DEBUG")]
		internal static void Debug(string message)
		{
			UnityEngine.Debug.Log(Prefix + message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Message(string message)
		{
			UnityEngine.Debug.Log(Prefix + message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Warning(string message)
		{
			UnityEngine.Debug.LogWarning(Prefix + message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Error(string message)
		{
			UnityEngine.Debug.LogError(Prefix + message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Exception(Exception ex)
		{
			UnityEngine.Debug.LogException(ex);
		}
	}
}
