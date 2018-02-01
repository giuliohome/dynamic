
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dynamic
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}
		bool isOpen = true;
		void btnOpen_Click(object sender, RoutedEventArgs e)
		{
			btnOpen.IsEnabled=false;
			gridPanel.Visibility = Visibility.Visible;
			var group = new TransformGroup();
			var scale = new ScaleTransform();
			var translate = new TranslateTransform();
			group.Children.Add(scale);
			group.Children.Add(translate);
			gridPanel.RenderTransform = group;
			const double start_width = 900;
			const double end_width = 50;
			const double seconds =0.9;
			const double targetScale = end_width / start_width;
			const double targetTranslate = start_width - end_width;
			var animScale = new DoubleAnimation((isOpen ? 1 : targetScale),
			                                    (isOpen ?  targetScale : 1),
			                                TimeSpan.FromSeconds(seconds));
			var animTranslate = new DoubleAnimation((isOpen ? 0 : targetTranslate),
			                                        (isOpen ? targetTranslate : 0),
			                                TimeSpan.FromSeconds(seconds));
			animScale.Completed += animate_Completed;
			scale.BeginAnimation(ScaleTransform.ScaleXProperty, animScale);
			translate.BeginAnimation(TranslateTransform.XProperty, animTranslate);
		}
		private void animate_Completed(object sender, EventArgs e)
		{
			isOpen = !isOpen;
			btnOpen.Content = isOpen ? "Close" : "Open";
			gridPanel.Visibility = isOpen ? Visibility.Visible : Visibility.Collapsed;
			btnOpen.IsEnabled=true;
		}
	}
}