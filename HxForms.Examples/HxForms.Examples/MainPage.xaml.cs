using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HxForms.Pages;
using Xamarin.Forms;
using System.Diagnostics;

namespace HxForms.Examples
{
    public partial class MainPage : BottomBarPage
	{
		public MainPage()
		{
			InitializeComponent();
            MorePicker.ItemsSource = new List<string> { "Hello", "World" };
            HtmlLabel.Text = "<b>Test</b> HTML <i>Label</i>";
            HtmlShortenedLabel.Text = "<b>Test</b> HTML sh <i>Label</i>";
            CurrentPageChanged += MainPage_CurrentPageChanged;
		}

        void MainPage_CurrentPageChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Current page changed");
        }
    }
}
