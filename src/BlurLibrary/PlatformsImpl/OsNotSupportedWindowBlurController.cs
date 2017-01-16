using System;

namespace mokeev1995.BlurLibrary.PlatformsImpl
{
	public class OsNotSupportedWindowBlurController : IWindowBlurController
	{
		public void EnableBlur(IntPtr hwnd)
		{
			throw new NotSupportedException();
		}

		public void DisableBlur(IntPtr hwnd)
		{
			throw new NotSupportedException();
		}

		public bool Enabled { get; } = false;
		public bool CanBeEnabled { get; } = false;
	}
}