using POSUNO.Helpers;
using POSUNO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = await ValidForm();
            if (!isValid)
            {
                return;
            }

            Response response = await ApiService.LoginAsync(new LoginRequest
            {
                Email = TextEmail.Text,
                Password = txtPassword.Password

            });

            MessageDialog messageDialog;
            if (!response.IsSuccess)
            {
                messageDialog = new MessageDialog(response.message, "Error");
                await messageDialog.ShowAsync();
                return;
            }
            User user = (User)response.result;
            if (user == null)
            {
                messageDialog = new MessageDialog("Usuario o contrasena no existe", "Error");
                await messageDialog.ShowAsync();
                return;
            }

            messageDialog = new MessageDialog($"Bienvenido: {user.Fullname}", "Ok");
            await messageDialog.ShowAsync();
        }

        private async Task<bool> ValidForm()
        {
            MessageDialog messageDialog;

            if (string.IsNullOrEmpty(TextEmail.Text))
            {
                messageDialog = new MessageDialog("Debes ingresar tu email", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (!RegexUtilities.IsValidEmail(TextEmail.Text))
            {
                messageDialog = new MessageDialog("Debes ingresar un email válido", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Password) || txtPassword.Password.Length < 6)
            {
                messageDialog = new MessageDialog("Ingresa una contraseña válida", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            return true;
        }
    }
}
