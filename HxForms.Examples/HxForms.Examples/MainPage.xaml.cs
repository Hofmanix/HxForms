using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HxForms.Pages;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace HxForms.Examples
{
    public partial class MainPage : BottomBarPage
	{
        private bool _added = false;
        private Page _lastPage;
        private ObservableCollection<string> RepetitiveTestItems;

        public MainPage()
        {
            InitializeComponent();
            MorePicker.ItemsSource = new List<string> { "Hello", "World", "!" };
            HtmlLabel.Text = "<b>Test</b> HTML <i>Label</i>";
            HtmlShortenedLabel.Text = "<b>Test</b> HTML sh <i>Label</i>";
            CurrentPageChanged += MainPage_CurrentPageChanged;

            RepetitiveTestItems = new ObservableCollection<string>{
                "Hello"
            };
            RepetitiveTest.ItemsSource = RepetitiveTestItems;
		}

        void MainPage_CurrentPageChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Current page changed");
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (Third != null)
            {
                Third.Title = e.NewTextValue;
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (_added)
            {
                Children.Remove(_lastPage);
                _lastPage = null;
                _added = false;
                AddPageButton.Text = "Add page";
            }
            else
            {
                _lastPage = new Page
                {
                    Title = "New page"
                };
                Children.Add(_lastPage);
                _added = true;
                AddPageButton.Text = "Remove page";
            }
        }

        void Handle_NewItemClicked(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewItemEntry.Text))
            {
                RepetitiveTestItems.Add(NewItemEntry.Text);
            }
        }
    }
}
