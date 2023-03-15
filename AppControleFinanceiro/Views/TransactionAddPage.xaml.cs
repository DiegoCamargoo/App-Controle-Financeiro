namespace AppControleFinanceiro.Views;

public partial class TransactionAddPage : ContentPage
{
	public TransactionAddPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();

    }
}