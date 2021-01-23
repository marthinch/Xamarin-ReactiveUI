using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinReactiveUI.Services;

namespace XamarinReactiveUI.ViewModels
{
    public class ListEmployeeViewModel : ReactiveObject
    {
        private string search = string.Empty;
        public string Search
        {
            get => search;
            set => this.RaiseAndSetIfChanged(ref search, value);
        }

        private ObservableAsPropertyHelper<string> result;
        public string Result => result.Value;

        private ObservableCollection<Models.Employee> employees;
        public ObservableCollection<Models.Employee> Employees
        {
            get => employees;
            set => this.RaiseAndSetIfChanged(ref employees, value);
        }

        public Models.Employee Employee
        {
            set
            {
                if (value != null)
                    _ = DetailEmployeeAsync(value);

                // Reset selected employee
                value = null;
            }
        }

        public ICommand NewCommand { get; }

        private readonly IEmployeeService employeeService;

        public ListEmployeeViewModel(IEmployeeService employeeService = null)
        {
            // Call employee service by dependency injection
            this.employeeService = employeeService ?? Locator.Current.GetService<IEmployeeService>();
            var listEmployee = this.employeeService.GetEmployees().ToList();

            employees = new ObservableCollection<Models.Employee>(listEmployee);

            this.WhenAnyValue(vm => vm.Search).Subscribe(x =>
            {
                var filtered = listEmployee.Where(a => a.Name.ToLower().Contains(Search.ToLower()) ||
                                                      a.Phone.ToLower().Contains(Search.ToLower()) ||
                                                      a.Address.ToLower().Contains(Search.ToLower())).ToList();

                Employees = new ObservableCollection<Models.Employee>(filtered);
            });

            this.WhenAnyValue(vm => vm.Employees).Select(x =>
            {
                if (listEmployee.Count == Employees.Count)
                    return string.Empty;

                return string.Format("{0} found", Employees.Count);
            }).ToProperty(this, vm => vm.Result, out result);

            NewCommand = new Command(async () => await NewEmployeeAsync());
        }

        private async Task NewEmployeeAsync()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.Employee());
        }

        private async Task DetailEmployeeAsync(Models.Employee employee)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.Employee(employee));
        }
    }
}
