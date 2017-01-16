using System;
using System.Windows.Interop;
using BlurLibrary.NativeThings;

namespace BlurLibrary.PlatformsImpl
{
	internal class WindowsVistaWindowBlurController : IWindowBlurController
	{
		public void EnableBlur(IntPtr hwnd)
		{
			if (!NativeMethods.DwmIsCompositionEnabled())
				return;

			HwndSource.FromHwnd(hwnd)?.AddHook(WndProc);

			InitializeGlass(hwnd);
			Enabled = true;
		}

		public void DisableBlur(IntPtr hwnd)
		{
			if (!NativeMethods.DwmIsCompositionEnabled())
				return;

			HwndSource.FromHwnd(hwnd)?.RemoveHook(WndProc);

			DeinitializeGlass(hwnd);
			Enabled = false;
		}

		public bool Enabled { get; private set; }

		public bool CanBeEnabled { get; } = true;

		private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg != NativeMethods.WM_DWMCOMPOSITIONCHANGED)
				return IntPtr.Zero;

			InitializeGlass(hwnd);
			handled = false;

			return IntPtr.Zero;
		}

		private static void InitializeGlass(IntPtr hwnd)
		{
			// fill the background with glass
			var margins = new NativeMethods.MARGINS();
			margins.cxLeftWidth = margins.cxRightWidth = margins.cyBottomHeight = margins.cyTopHeight = -1;
			NativeMethods.DwmExtendFrameIntoClientArea(hwnd, ref margins);

			// initialize blur for the window
			var bbh = new NativeMethods.DWM_BLURBEHIND
			{
				fEnable = true,
				dwFlags = NativeMethods.DWM_BB.DWM_BB_ENABLE
			};

			NativeMethods.DwmEnableBlurBehindWindow(hwnd, ref bbh);
		}

		private static void DeinitializeGlass(IntPtr hwnd)
		{
			// fill the background with glass
			var margins = new NativeMethods.MARGINS();
			margins.cxLeftWidth = margins.cxRightWidth = margins.cyBottomHeight = margins.cyTopHeight = -1;
			NativeMethods.DwmExtendFrameIntoClientArea(hwnd, ref margins);

			// initialize blur for the window
			var bbh = new NativeMethods.DWM_BLURBEHIND
			{
				fEnable = false,
				dwFlags = NativeMethods.DWM_BB.DWM_BB_ENABLE
			};

			NativeMethods.DwmEnableBlurBehindWindow(hwnd, ref bbh);
		}
	}
}