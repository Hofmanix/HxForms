using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HxForms.Pages;
using Xamarin.Forms;

namespace HxForms.Examples
{
	public partial class MainPage : BottomBarPage
	{
		public MainPage()
		{
			InitializeComponent();
            MorePicker.ItemsSource = new List<string> { "Hello", "World" };
		}
	}
}
