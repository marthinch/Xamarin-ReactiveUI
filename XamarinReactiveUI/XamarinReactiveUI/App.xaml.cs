using Xamarin.Forms;
using XamarinReactiveUI.Views;

namespace XamarinReactiveUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Initialize app bootstrapper
            _ = new AppBootrapper();

            //MainPage = new NavigationPage(new Employees());
            MainPage = new NavigationPage(new DynamicEmployees());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
