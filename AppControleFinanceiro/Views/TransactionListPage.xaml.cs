namespace AppControleFinanceiro.Views;

public partial class TransactionListPage : ContentPage
{
	public TransactionListPage()
	{
		InitializeComponent();
	}

	private void OnButtonClicked_To_TransactionAddPage(object sender, EventArgs e)
	{
		App.Current.MainPage = new TransactionAddPage();
	}

    private void OnButtonClicked_To_TransactionEditPage(object sender, EventArgs e)
    {
		App.Current.MainPage = new TransactionEditPage();
    }
}
