using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Parer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Dwarf dwarf;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void panDst_MouseUp(object sender, MouseButtonEventArgs e)
		{
			IEnumerable<RadioButton> q = panEng.Children
				.OfType<RadioButton>();

			if (q.Any(rb => rb.Visibility == Visibility.Visible))
				return;

			panDst.Children.Clear();
			dwarf.FillPage(panEng, panRus);
		}//func

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			dwarf = new Dwarf(Environment.GetCommandLineArgs()[1]);
			dwarf.handler = rus_Checked;
			dwarf.FillPage(panEng, panRus);
		}//func

		void rus_Checked(object sender, RoutedEventArgs e)
		{
			Func<RadioButton, bool> IsGood = rb => rb.IsChecked == true && rb.IsVisible;

			RadioButton rbEng = panEng.Children.OfType<RadioButton>()
				.FirstOrDefault(IsGood);

			RadioButton rbRus = panRus.Children.OfType<RadioButton>()
				.FirstOrDefault(IsGood);

			if (rbEng == null || rbRus == null)
				return;

			string find = dwarf.Contains(rbEng.Content.ToString(), rbRus.Content.ToString());

			if (string.IsNullOrEmpty(find))
			{
				return;
			}//if

			rbEng.Visibility = System.Windows.Visibility.Collapsed;
			rbRus.Visibility = System.Windows.Visibility.Collapsed;
			panDst.Children.Add(new TextBlock() { Text = find });
		}//func

	}//class
}//ns
