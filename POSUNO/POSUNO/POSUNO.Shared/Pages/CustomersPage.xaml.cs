using POSUNO.Components;
using POSUNO.Helpers;
using POSUNO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace POSUNO.Pages
{
    public sealed partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<Customer> Customers { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadCustomerAsync();
        }

        private async void LoadCustomerAsync()
        {
            Loader loader = new Loader("Por favor, espere...");
            loader.Show();
            Response response = await ApiService.GetListAsync<Customer>("Customers");
            loader.Close();

            if (!response.IsSuccess)
            {
                MessageDialog dialog = new MessageDialog(response.message, "Error");
                await dialog.ShowAsync();
                return;
            }

            List<Customer> customers = (List<Customer>)response.result;
            Customers = new ObservableCollection<Customer>(customers);
            RefreshList();
        }

        private void RefreshList() {
            lvCustomers.ItemsSource = null;
            lvCustomers.Items.Clear();
            lvCustomers.ItemsSource = Customers;
        }
    }
}
