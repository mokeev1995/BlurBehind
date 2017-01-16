using System;
using System.Runtime.InteropServices;
using BlurLibrary.NativeThings.Windows10;

// ReSharper disable once CheckNamespace
namespace BlurLibrary.NativeThings
{
	internal static partial class NativeMethods
	{

		[DllImport("user32.dll")]
		public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
	}
}