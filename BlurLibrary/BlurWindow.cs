using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace BlurLibrary
{
	public static class BlurWindow
	{
		public static void SetBlurWindow(Window window)
		{
			if(Environment.OSVersion.Version.Major != 6 && Environment.OSVersion.Version.Major != 10)
				return;

			if (Environment.OSVersion.Version.Major == 6 &&
			    (Environment.OSVersion.Version.Minor == 1 || Environment.OSVersion.Version.Minor == 0))
			{
				SetWinVistaAndWin7Blur(window);
			}
			else if (Environment.OSVersion.Version.Major == 6 &&
			         (Environment.OSVersion.Version.Minor == 3 || Environment.OSVersion.Version.Minor == 2))
			{
				//nothing for win8 - win8.1
				//SetWin8Blur(windowHelper);
			}
			else if (Environment.OSVersion.Version.Major == 10)
			{
				var windowHelper = new WindowInteropHelper(window);
				SetWin10Blur(windowHelper);
			}
        }

		private static void SetWin10Blur(WindowInteropHelper windowHelper)
		{
			var accent = new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND };

			var accentStructSize = Marshal.SizeOf(accent);

			var accentPtr = Marshal.AllocHGlobal(accentStructSize);
			Marshal.StructureToPtr(accent, accentPtr, false);

			var data = new WindowCompositionAttributeData
			{
				Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
				SizeOfData = accentStructSize,
				Data = accentPtr
			};

			NativeMethods.SetWindowCompositionAttribute(windowHelper.Handle, ref data);

			Marshal.FreeHGlobal(accentPtr);
		}

		private static void SetWinVistaAndWin7Blur(Window window)
		{
			if (!NativeMethods.DwmIsCompositionEnabled())
				return;

			var hwnd = new WindowInteropHelper(window).Handle;
			var hs = HwndSource.FromHwnd(hwnd);
			if (hs != null)
			{
				if (hs.CompositionTarget != null)
					hs.CompositionTarget.BackgroundColor = Colors.Transparent;

				hs.AddHook(window.WndProc);
			}
			InitializeGlass(hwnd);
		}

		private static IntPtr WndProc(this Window window, IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg != NativeMethods.WM_DWMCOMPOSITIONCHANGED) return IntPtr.Zero;

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
	}
}