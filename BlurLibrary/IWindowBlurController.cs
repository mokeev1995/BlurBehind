using System;
using System.Windows.Media;

namespace BlurLibrary
{
	internal interface IWindowBlurController
	{
		bool Enabled { get; }
		bool CanBeEnabled { get; }
		void EnableBlur(IntPtr hwnd);
		void DisableBlur(IntPtr hwnd);
	}
}