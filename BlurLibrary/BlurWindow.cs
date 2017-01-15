using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace BlurLibrary
{
	internal enum OsType
	{
		WindowsVista,
		Windows7,
		Windows8,
		Windows81,
		Windows10,
		Other
	}

	internal static class OsHelper
	{
		public static OsType GetOsType()
		{
			if (Environment.OSVersion.Version.Major != 6 && Environment.OSVersion.Version.Major != 10)
				return OsType.Other;

			if (Environment.OSVersion.Version.Major != 6)
				return Environment.OSVersion.Version.Major == 10
					? OsType.Windows10
					: OsType.Other;

			switch (Environment.OSVersion.Version.Minor)
			{
				case 0:
					return OsType.WindowsVista;
				case 1:
					return OsType.Windows7;
				case 2:
					return OsType.Windows8;
				case 3:
					return OsType.Windows81;
				default:
					return OsType.Other;
			}
		}
	}

	public static class BlurWindow
	{
		public static bool Enabled { get; private set; }

		public static bool CanBeEnabled
		{
			get
			{
				var os = OsHelper.GetOsType();
				return os == OsType.WindowsVista || os == OsType.Windows7 || os == OsType.Windows10;
			}
		}

		public static void SetBlurWindow(Window window)
		{
			var os = OsHelper.GetOsType();
			if(!CanBeEnabled)
				return;
			
			switch (os)
			{
				case OsType.WindowsVista:
				case OsType.Windows7:
					SetWinVistaAndWin7Blur(window);
					break;
				case OsType.Windows10:
					var windowHelper = new WindowInteropHelper(window);
					SetWin10Blur(windowHelper);
					break;

				case OsType.Windows8:
				case OsType.Windows81:
				case OsType.Other:
					return;
				default:
					return;
			}
		}

		private static void SetWin10Blur(WindowInteropHelper windowHelper)
		{
			var accent = new AccentPolicy {AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND};

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
			Enabled = true;
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