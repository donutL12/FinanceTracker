using System;
using Microsoft.Maui.Controls;

namespace FinanceTracker
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            var existingUser = await App.Database.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                var newUser = new User
                {
                    Username = username,
                    Password = password
                };

                await App.Database.SaveUserAsync(newUser);
                await DisplayAlert("Success", "User registered successfully", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Username already exists", "OK");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
