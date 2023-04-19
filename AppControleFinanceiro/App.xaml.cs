using AppControleFinanceiro.Views;

namespace AppControleFinanceiro;

public partial class App : Application
{
	public App(TransactionListPage listPage)
	{
		InitializeComponent();

		MainPage = new NavigationPage (listPage);
	}
}
