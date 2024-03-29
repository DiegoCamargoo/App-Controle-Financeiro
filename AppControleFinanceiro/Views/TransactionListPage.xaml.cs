using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;

namespace AppControleFinanceiro.Views;

public partial class TransactionListPage : ContentPage
{
	private TransactionAddPage _transactionAddPage;
	private TransactionEditPage _transactionEditPage;
	private ITransactionRepository _repository;

	public TransactionListPage(ITransactionRepository repository)
	{
		this._repository = repository;

		InitializeComponent();

		Reload();

		WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
		{
			Reload();
		});
	}

	private void Reload()
	{
		var items = _repository.GetAll();
		CollectionViewTransactions.ItemsSource = items;

		double income = items.Where(i => i.Type == Models.TransactionType.Income).Sum(i => i.Value);
		double expense = items.Where(i => i.Type == Models.TransactionType.Expenses).Sum(i => i.Value);
		double balance = income - expense;

		LabelIncome.Text = income.ToString("C");
		LabelExpense.Text = expense.ToString("C");
		LabelBalance.Text = balance.ToString("C");

	}

	private void OnButtonClicked_To_TransactionAddPage(object sender, EventArgs e)
	{
		var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAddPage>();

		Navigation.PushModalAsync(transactionAdd);
	}

	private void TapGestureRecognizerTapped_To_TransactionEdit(object sender, TappedEventArgs e)
	{
		var grid = (Grid)sender;
		var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];

		Transaction transaction = (Transaction)gesture.CommandParameter;

		var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEditPage>();
		transactionEdit.SetTransactionToEdit(transaction);
		Navigation.PushModalAsync(transactionEdit);
	}

	private async void TapGestureRecognizerTappedToDelete(object sender, TappedEventArgs e)
	{
		await AnimationBorder((Border)sender, true);
		bool result = await App.Current.MainPage.DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "N�o");

		if (result)
		{
			Transaction transaction = (Transaction)e.Parameter;
			_repository.Delete(transaction);

			Reload();
		}
		else
			await AnimationBorder((Border)sender, false);
	}
	private Color _borderOriginalBackgroundColor;
	private string _labelOriginaltext;

	private async Task AnimationBorder(Border border, bool IsDeleteAnimation)
	{
		var label = (Label)border.Content;

		if (IsDeleteAnimation)
		{
			_borderOriginalBackgroundColor = border.BackgroundColor;
			_labelOriginaltext = label.Text;

			await border.RotateYTo(90, 500);

			border.BackgroundColor = Colors.Red;

			label.TextColor = Colors.White;
			label.Text = "X";

			await border.RotateYTo(180, 500);
		}

		else
		{
			await border.RotateYTo(90, 500);

			border.BackgroundColor = _borderOriginalBackgroundColor;
            label.TextColor = Colors.Black;
            label.Text = _labelOriginaltext;

            await border.RotateYTo(0, 500);
		}

	}
}

