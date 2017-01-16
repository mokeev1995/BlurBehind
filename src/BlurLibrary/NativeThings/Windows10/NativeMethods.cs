using System;
using System.Runtime.InteropServices;
using mokeev1995.BlurLibrary.NativeThings.Windows10;

// ReSharper disable once CheckNamespace

namespace mokeev1995.NativeThings
{
	internal static partial class NativeMethods
	{
		[DllImport("user32.dll")]
		public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
	}
}