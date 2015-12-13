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
			var regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
			var product = regKey?.GetValue("ProductName");
			Text.Content = $"Windows Version: v{Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}, PRODUCT: {product}";
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			BlurWindow.SetBlurWindow(this);
			var regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
			var product = regKey?.GetValue("ProductName");
			Text.Content = $"Windows Version: v{Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}, PRODUCT: {product}";
		}
	}




}
