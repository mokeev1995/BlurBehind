using System;
using System.Runtime.InteropServices;

namespace BlurLibrary
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WindowCompositionAttributeData
	{
		public WindowCompositionAttribute Attribute;
		public IntPtr Data;
		public int SizeOfData;
	}
}