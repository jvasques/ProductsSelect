namespace ProductsSelect
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage()); // Usar NavigationPage
        }
    }

}
