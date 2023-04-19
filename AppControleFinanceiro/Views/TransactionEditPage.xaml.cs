using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;
using System.Transactions;
using Transaction = AppControleFinanceiro.Models.Transaction;

namespace AppControleFinanceiro.Views;

public partial class TransactionEditPage : ContentPage
{
    private Transaction _transaction;
    private ITransactionRepository _repository;
    public TransactionEditPage(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
	}

    public void SetTransactionToEdit(Transaction transaction)
    {
        _transaction = transaction;

        if (transaction.Type == TransactionType.Income)
            RadioIncome.IsChecked = true;
        else
            RadioExpense.IsChecked = true;

        EntryName.Text= transaction.Name;
        DatePickerDate.Date = transaction.Date.Date;
        EntryValue.Text = transaction.Value.ToString();
    }

    private void TapGestureRecognizerTappedToClose
        (object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();

    }

    private void OnButton_Clicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
            return;

        SaveTransactionInDatabase();

        Navigation.PopModalAsync();

        WeakReferenceMessenger.Default.Send(string.Empty);
    }

    private void SaveTransactionInDatabase()
    {
        Transaction transaction = new()
        {
            Id = _transaction.Id,
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expenses,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text)
        };

        _repository.Update(transaction);

    }

    private bool IsValidData()
    {
        bool valid = true;

        StringBuilder sb = new();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("O campo 'Nome' deve ser preenchido!");
            valid = false;
        }

        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            sb.AppendLine("O campo 'Valor' deve ser preenchido!");
            valid = false;
        }

        double result;

        if (!string.IsNullOrEmpty(EntryName.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.AppendLine("O campo 'Valor' é invalido!");
            valid = false;
        }
        if (valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();

        }
        return valid;
    }
}
