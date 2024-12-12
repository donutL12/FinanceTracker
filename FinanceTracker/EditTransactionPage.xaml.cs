using System;
using Microsoft.Maui.Controls;

namespace FinanceTracker
{
    public partial class EditTransactionPage : ContentPage
    {
        private Transaction _transaction;

        public EditTransactionPage(Transaction transaction)
        {
            InitializeComponent();
            _transaction = transaction;
            BindingContext = _transaction;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(TransactionAmountEntry.Text, out decimal amount) && !string.IsNullOrEmpty(TransactionDescriptionEntry.Text) && TransactionTypePicker.SelectedIndex != -1)
            {
                _transaction.Amount = amount;
                _transaction.Description = TransactionDescriptionEntry.Text;
                _transaction.Type = TransactionTypePicker.SelectedItem.ToString();

                MessagingCenter.Send(this, "UpdateTransaction", _transaction);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please fill out all fields correctly.", "OK");
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this transaction?", "Yes", "No");
            if (result)
            {
                MessagingCenter.Send(this, "DeleteTransaction", _transaction);
                await Navigation.PopAsync();
            }
        }
    }
}
