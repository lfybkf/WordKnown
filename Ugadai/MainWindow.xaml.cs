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

namespace Ugadai
{
	enum Lang {Eng, Rus, None}

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		Train Trn = new Train() { lang = Lang.Eng, iPod = 3 };
	
		public MainWindow()
		{
			InitializeComponent();
			gridTrain.DataContext = Trn;
		}//ctor

		public void OnClick(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			ActivateButton(btn, false);
			Trn.Set(btn.Content.ToString());
		}//func

		void ActivateButton (Button btn, bool active)
		{
			if (active)
				btn.Visibility = System.Windows.Visibility.Visible;
			else
				btn.Visibility = System.Windows.Visibility.Hidden;
		}//func

		void ActivateAllButtons()
		{
			Action<Panel> Do = (pan) =>
				{
					foreach (Button btn in pan.Children)
					{
						ActivateButton(btn, true);
					}//for
				};
			Do(panEng);
			Do(panRus);
		}//func

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Letter.AddButtons(panEng, Letter.Eng);
			Letter.AddButtons(panRus, Letter.Rus);

			string[] ss = Environment.GetCommandLineArgs();
			string path = (ss.Length > 1) ? ss[1] : "my.trn";
			Trn.Load(path);
			Trn.Begin();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Trn.Save();
		}

		private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (Trn.IsDone())
			{
				if (Trn.Begin() == false)
				{
					MessageBox.Show ("Слова закончились");
				}//if				
				ActivateAllButtons();
				Trn.Save();
			}//if

		}//func
	}//class
}//ns
