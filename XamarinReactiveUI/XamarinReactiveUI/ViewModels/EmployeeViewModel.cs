using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using Splat;
using System;
using System.Reactive;
using System.Threading.Tasks;
using XamarinReactiveUI.Models;
using XamarinReactiveUI.Services;

namespace XamarinReactiveUI.ViewModels
{
    public class EmployeeViewModel : ReactiveObject
    {
        private int id;
        public int Id
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set => this.RaiseAndSetIfChanged(ref phone, value);
        }

        private string address;
        public string Address
        {
            get => address;
            set => this.RaiseAndSetIfChanged(ref address, value);
        }

        public Employee Employee { get; set; }

        // Commands
        public ReactiveCommand<int, Unit> SaveCommand { get; }
        public ReactiveCommand<int, Unit> DeleteCommand { get; }

        private readonly IEmployeeService employeeService;

        public ValidationContext ValidationContext => new ValidationContext();

        public EmployeeViewModel(Employee employee = null, IEmployeeService employeeService = null)
        {
            if (employee != null)
            {
                id = employee.Id;
                name = employee.Name;
                phone = employee.Phone;
                address = employee.Address;
            }

            // Call employee service by dependency injection
            this.employeeService = employeeService ?? Locator.Current.GetService<IEmployeeService>();

            // Validation
            IObservable<bool> isSaveCommandEnabled = this.WhenAnyValue(x => x.Name, x => x.Phone, x => x.Address,
                                                                     (name, phone, address) =>
                                                                     {
                                                                         if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
                                                                             return false;

                                                                         return true;
                                                                     });
            

            SaveCommand = ReactiveCommand.CreateFromTask<int, Unit>((x) => (Task<Unit>)SaveEmployeeAsync(x), isSaveCommandEnabled);
            SaveCommand.ThrownExceptions.Subscribe(ex => Console.WriteLine(ex.Message));

            DeleteCommand = ReactiveCommand.CreateFromTask<int, Unit>((x) => (Task<Unit>)DeleteEmployeeAsync(x));
            DeleteCommand.ThrownExceptions.Subscribe(ex => Console.WriteLine(ex.Message));
        }

        private async Task SaveEmployeeAsync(int id)
        {
            Employee = new Employee
            {
                Id = Id,
                Name = Name,
                Phone = Phone,
                Address = Address
            };

            if (id == 0)
            {
                this.employeeService.AddEmployee(Employee);
            }
            else
            {
                this.employeeService.EditEmployee(id, Employee);
            }

            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async Task DeleteEmployeeAsync(int id)
        {
            this.employeeService.DeleteEmployee(id);

            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
