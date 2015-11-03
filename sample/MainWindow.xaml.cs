using System;
using System.Windows;
using BlurLibrary;

namespace BlurBehindDemo
{
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			MouseLeftButtonDown += delegate {
				try
				{
					DragMove();
				}
				catch (Exception)
				{
					// ignored
				}
			};
			Block.MouseLeftButtonDown += delegate {
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
			EnableBlur();
		}

		private void EnableBlur()
		{
			BlurWindow.SetBlurWindow(this);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			EnableBlur();
		}
	}




}
