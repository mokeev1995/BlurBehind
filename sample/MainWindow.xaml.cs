using System;
using System.Windows;
using BlurLibrary;

namespace BlurBehindDemo
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			MouseLeftButtonDown += delegate
			{
				try
				{
					DragMove();
				}
				catch (Exception)
				{
					// ignored
				}
			};
			Block.MouseLeftButtonDown += delegate
			{
				try
				{
					DragMove();
				}
				catch (Exception)
				{
					// ignored
				}
			};
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (BlurWindow.CanBeEnabled)
			{
				EnableBtn.IsEnabled = true;
				DisableBtn.IsEnabled = false;

				//BlurWindow.EnableWindowBlur(this);
			}
			else
			{
				EnableBtn.IsEnabled = false;
				DisableBtn.IsEnabled = false;
			}
		}

		private void EnableBtn_OnClick(object sender, RoutedEventArgs e)
		{
			EnableBtn.IsEnabled = false;
			DisableBtn.IsEnabled = true;
			BlurWindow.EnableWindowBlur(this);
		}

		private void DisableBtn_OnClick(object sender, RoutedEventArgs e)
		{
			EnableBtn.IsEnabled = true;
			DisableBtn.IsEnabled = false;
			BlurWindow.DisableWindowBlur(this);
		}
	}
}