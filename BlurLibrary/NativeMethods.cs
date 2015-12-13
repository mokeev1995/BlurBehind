using System;
using System.Runtime.InteropServices;
using System.Security;
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace BlurLibrary
{
	[SuppressUnmanagedCodeSecurity]
	public static class NativeMethods
	{
		public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;

		/*
		 * For Win 10
		 */
		[DllImport("user32.dll")]
		public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);


		/*
		 * For Win 7
		 */
		[StructLayout(LayoutKind.Sequential)]
		public struct DWM_BLURBEHIND
		{
			public DWM_BB dwFlags;
			public bool fEnable;
			public IntPtr hRgnBlur;
			public bool fTransitionOnMaximized;
		}

		[Flags]
		public enum DWM_BB
		{
			DWM_BB_ENABLE = 1,
			DWM_BB_BLURREGION = 2,
			DWM_BB_TRANSITIONONMAXIMIZED = 4
		}

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

		[StructLayout(LayoutKind.Sequential)]
		public struct MARGINS
		{
			public int cxLeftWidth;
			public int cxRightWidth;
			public int cyTopHeight;
			public int cyBottomHeight;
		}

		[DllImport("dwmapi.dll")]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);
	}
}