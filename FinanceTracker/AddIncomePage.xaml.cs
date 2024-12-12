using System;
using Microsoft.Maui.Controls;

namespace FinanceTracker
{
    public partial class AddIncomePage : ContentPage
    {
        public AddIncomePage()
        {
            InitializeComponent();
        }

        private async void OnSaveIncomeClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(IncomeAmountEntry.Text, out decimal amount) && !string.IsNullOrEmpty(IncomeDescriptionEntry.Text) && IncomeTypePicker.SelectedIndex != -1)
            {
                var transaction = new Transaction
                {
                    Date = DateTime.Now,
                    Description = IncomeDescriptionEntry.Text,
                    Amount = amount, // Income is positive amount
                    Type = IncomeTypePicker.SelectedItem.ToString()
                };

                await App.Database.SaveTransactionAsync(transaction);
                MessagingCenter.Send(this, "AddTransaction", transaction);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please fill out all fields correctly.", "OK");
            }
        }
    }
}
