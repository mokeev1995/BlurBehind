using System;
using System.Windows;

namespace BlurLibrary
{
	public static class BlurWindow
	{
		private static readonly IWindowBlurController BlurController;

		static BlurWindow()
		{
			BlurController = Helpers.GetWindowControllerForOs(OsHelper.GetOsType());
		}

		public static bool Enabled => BlurController.Enabled;

		public static bool CanBeEnabled => BlurController.CanBeEnabled;

		public static void EnableWindowBlur(IntPtr hwnd)
		{
			if (!CanBeEnabled)
				return;

			BlurController.EnableBlur(hwnd);
		}

		public static void EnableWindowBlur(Window window)
		{
			EnableWindowBlur(Helpers.GetWindowHandle(window));
		}

		public static void DisableWindowBlur(IntPtr hwnd)
		{
			if (!CanBeEnabled)
				return;

			BlurController.DisableBlur(hwnd);
		}

		public static void DisableWindowBlur(Window window)
		{
			DisableWindowBlur(Helpers.GetWindowHandle(window));
		}
	}
}