namespace kontaktandmed
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Здесь указываем начальную страницу приложения
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
