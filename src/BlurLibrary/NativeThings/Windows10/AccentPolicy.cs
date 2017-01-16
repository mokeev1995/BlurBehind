using System.Runtime.InteropServices;

namespace mokeev1995.BlurLibrary.NativeThings.Windows10
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct AccentPolicy
	{
		public AccentState AccentState;
		public int AccentFlags;
		public int GradientColor;
		public int AnimationId;
	}
}