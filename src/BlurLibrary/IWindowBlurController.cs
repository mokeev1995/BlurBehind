using System;

namespace mokeev1995.BlurLibrary
{
	internal interface IWindowBlurController
	{
		bool Enabled { get; }
		bool CanBeEnabled { get; }
		void EnableBlur(IntPtr hwnd);
		void DisableBlur(IntPtr hwnd);
	}
}