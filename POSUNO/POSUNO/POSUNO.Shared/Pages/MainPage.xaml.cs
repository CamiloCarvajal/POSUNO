using POSUNO.Models;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace POSUNO.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public User user { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            user = (User)e.Parameter;
            txtWelcome.Text = $"Bienvenido: {user.Fullname}";
            MyFrame.Navigate(typeof(CustomersPage));
        }

        private async void LogoutImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentDialogResult dialog = await ConfirmLeaveAsync();
            if (dialog == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(LoginPage));
            }
        }

        private async Task<ContentDialogResult> ConfirmLeaveAsync()
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "Confirmación",
                Content = "Estas seguro de salir?",
                PrimaryButtonText = "Sí",
                CloseButtonText = "No"
            };

            return await confirmDialog.ShowAsync();
        }


        private void CustomersNavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(CustomersPage));
        }

        private void ProductsNavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(ProductsPage));
        }

    }
}
