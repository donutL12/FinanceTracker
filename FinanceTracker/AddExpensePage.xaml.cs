using System;
using Microsoft.Maui.Controls;

namespace FinanceTracker
{
    public partial class AddExpensePage : ContentPage
    {
        public AddExpensePage()
        {
            InitializeComponent();
        }

        private async void OnSaveExpenseClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(ExpenseAmountEntry.Text, out decimal amount) && !string.IsNullOrEmpty(ExpenseDescriptionEntry.Text) && ExpenseTypePicker.SelectedIndex != -1)
            {
                var transaction = new Transaction
                {
                    Date = DateTime.Now,
                    Description = ExpenseDescriptionEntry.Text,
                    Amount = -amount, // Expenses are negative amounts
                    Type = ExpenseTypePicker.SelectedItem.ToString()
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
