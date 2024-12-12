using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace FinanceTracker
{
    public partial class SummaryPage : ContentPage
    {
        public SummaryPage()
        {
            InitializeComponent();
            LoadSummary();
        }

        private async void LoadSummary()
        {
            var transactions = await App.Database.GetTransactionsAsync();
            decimal totalIncome = 0m;
            decimal totalExpenses = 0m;
            decimal cashOnBank = 0m;
            decimal cashOnHand = 0m;

            foreach (var transaction in transactions)
            {
                if (transaction.Amount > 0)
                {
                    totalIncome += transaction.Amount;
                }
                else
                {
                    totalExpenses += transaction.Amount;
                }

                if (transaction.Type == "Cash on Bank")
                {
                    cashOnBank += transaction.Amount;
                }
                else if (transaction.Type == "Cash on Hand")
                {
                    cashOnHand += transaction.Amount;
                }
            }

            decimal netBalance = totalIncome + totalExpenses;

            TotalIncomeLabel.Text = $"₱{totalIncome:N2}";
            TotalExpensesLabel.Text = $"₱{totalExpenses:N2}";
            NetBalanceLabel.Text = $"₱{netBalance:N2}";
            CashOnBankLabel.Text = $"₱{cashOnBank:N2}";
            CashOnHandLabel.Text = $"₱{cashOnHand:N2}";
        }
    }
}
