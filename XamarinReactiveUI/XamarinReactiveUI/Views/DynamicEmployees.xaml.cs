using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinReactiveUI.ViewModels;

namespace XamarinReactiveUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DynamicEmployees : ContentPage
    {
        public DynamicEmployees()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new DynamicListEmployeeViewModel();
        }
    }
}