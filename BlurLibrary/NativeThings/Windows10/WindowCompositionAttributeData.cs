using System;
using System.Runtime.InteropServices;

namespace BlurLibrary.NativeThings.Windows10
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct WindowCompositionAttributeData
	{
		public WindowCompositionAttribute Attribute;
		public IntPtr Data;
		public int SizeOfData;
	}
}