namespace IFRHoldClearanceTrainer;

public partial class App : Application
{
	public App()
	{
		
		Routing.RegisterRoute("DrawingTest",typeof(DrawingViewTest));
		InitializeComponent();
		MainPage = new AppShell();
	}
}
