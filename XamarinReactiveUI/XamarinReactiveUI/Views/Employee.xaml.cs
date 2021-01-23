using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinReactiveUI.ViewModels;

namespace XamarinReactiveUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Employee : ContentPage
    {
        public Employee(Models.Employee employee = null)
        {
            InitializeComponent();

            Title = employee != null ? "Edit" : "Create";

            BindingContext = new EmployeeViewModel(employee);
        }
    }
}