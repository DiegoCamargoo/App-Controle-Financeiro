using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Platform;
using System.Text;
using Transaction = AppControleFinanceiro.Models.Transaction;

namespace AppControleFinanceiro.Views;

public partial class TransactionAddPage : ContentPage
{
    private ITransactionRepository _repository;
	public TransactionAddPage(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
	}

    private void TapGestureRecognizerTappedToClose(object sender, TappedEventArgs e)
    {

        /* if (Platform.CurrentActivity.CurrentFocus != null)
      {
          Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
      }*/

        Navigation.PopModalAsync();

        EntryName.IsEnabled = false;
        EntryValue.IsEnabled = false;

    }

    private void OnButton_Clicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
            return;

        SaveTransactionInDatabase();


        /* if (Platform.CurrentActivity.CurrentFocus != null)
      {
          Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
      }*/

        Navigation.PopModalAsync();

        EntryName.IsEnabled = false;
        EntryValue.IsEnabled = false;

        WeakReferenceMessenger.Default.Send(string.Empty);
            
    }
    private void SaveTransactionInDatabase()
    {
        Transaction transaction = new()
        {
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expenses,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = Math.Abs(double.Parse(EntryValue.Text))
        };

        _repository.Add(transaction);
         
    }

    private bool IsValidData()
    {
        bool valid = true;

        StringBuilder sb = new();

        if(string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
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

        if(!string.IsNullOrEmpty(EntryName.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.AppendLine("O campo 'Valor' é invalido!");
            valid = false;
        }
        if(valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();

        }
        return valid;
    }
}