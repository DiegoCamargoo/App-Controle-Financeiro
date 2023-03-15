namespace AppControleFinanceiro.Views;

public partial class TransactionEditPage : ContentPage
{
	public TransactionEditPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();

    }
}