using System;
using System.Windows;
using BlurLibrary;
using Microsoft.Win32;

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
			BlurWindow.SetBlurWindow(this);
		}
	}




}
