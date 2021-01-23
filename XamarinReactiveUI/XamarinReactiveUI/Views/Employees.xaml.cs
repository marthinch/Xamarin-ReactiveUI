using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinReactiveUI.ViewModels;

namespace XamarinReactiveUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Employees : ContentPage
    {
        public Employees()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new ListEmployeeViewModel();
        }
    }
}