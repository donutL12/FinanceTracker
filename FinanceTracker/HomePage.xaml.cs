using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microcharts;
using SkiaSharp;
using System.Linq;

namespace FinanceTracker
{
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
     //   public LineChart BalanceChart { get; set; }

        public HomePage()
        {
            InitializeComponent();
            LoadTransactions();

            MessagingCenter.Subscribe<AddIncomePage, Transaction>(this, "AddTransaction", async (sender, transaction) =>
            {
                await App.Database.SaveTransactionAsync(transaction);
                Transactions.Add(transaction);
                UpdateBalances();
               // GenerateBalanceChart(); // Ensure chart updates
            });

            MessagingCenter.Subscribe<AddExpensePage, Transaction>(this, "AddTransaction", async (sender, transaction) =>
            {
                await App.Database.SaveTransactionAsync(transaction);
                Transactions.Add(transaction);
                UpdateBalances();
               // GenerateBalanceChart(); // Ensure chart updates
            });

            MessagingCenter.Subscribe<EditTransactionPage, Transaction>(this, "UpdateTransaction", async (sender, transaction) =>
            {
                await App.Database.SaveTransactionAsync(transaction);
                var existingTransaction = Transactions.FirstOrDefault(t => t.Id == transaction.Id);
                if (existingTransaction != null)
                {
                    existingTransaction.Date = transaction.Date;
                    existingTransaction.Description = transaction.Description;
                    existingTransaction.Amount = transaction.Amount;
                    existingTransaction.Type = transaction.Type; // Ensure type is updated
                }
                TransactionsCollectionView.ItemsSource = null;
                TransactionsCollectionView.ItemsSource = Transactions;
                UpdateBalances();
               
            });

            MessagingCenter.Subscribe<EditTransactionPage, Transaction>(this, "DeleteTransaction", async (sender, transaction) =>
            {
                await App.Database.DeleteTransactionAsync(transaction);
                Transactions.Remove(transaction);
                UpdateBalances();
             
            });
        }

        private async void LoadTransactions()
        {
            var transactionsList = await App.Database.GetTransactionsAsync();
            Transactions = new ObservableCollection<Transaction>(transactionsList);

            foreach (var transaction in Transactions)
            {
                Console.WriteLine($"Transaction: {transaction.Description}, Amount: {transaction.Amount}, TextColor: {transaction.TextColor}");
            }

            TransactionsCollectionView.ItemsSource = Transactions;
            UpdateBalances();
        
        }

        private async void OnAddIncomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddIncomePage());
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExpensePage());
        }

        private async void OnViewSummaryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SummaryPage());
        }

        private async void OnTransactionSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                var selectedTransaction = e.CurrentSelection[0] as Transaction;
                await Navigation.PushAsync(new EditTransactionPage(selectedTransaction));
            }
        }

        private void UpdateBalances()
        {
            decimal balance = 0m;
            decimal cashOnBank = 0m;
            decimal cashOnHand = 0m;

            foreach (var transaction in Transactions)
            {
                balance += transaction.Amount;
                if (transaction.Type == "Cash on Bank")
                {
                    cashOnBank += transaction.Amount;
                }
                else if (transaction.Type == "Cash on Hand")
                {
                    cashOnHand += transaction.Amount;
                }
            }

            BalanceLabel.Text = $"₱{balance:N2}";
            CashOnBankLabel.Text = $"₱{cashOnBank:N2}";
            CashOnHandLabel.Text = $"₱{cashOnHand:N2}";
        }

       


    }
}
