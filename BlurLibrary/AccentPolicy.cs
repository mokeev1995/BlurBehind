using System.Runtime.InteropServices;

namespace BlurLibrary
{
	[StructLayout(LayoutKind.Sequential)]
	public struct AccentPolicy
	{
		public AccentState AccentState;
		public int AccentFlags;
		public int GradientColor;
		public int AnimationId;
	}
}