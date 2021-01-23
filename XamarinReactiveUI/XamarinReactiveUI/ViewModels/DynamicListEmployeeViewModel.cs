using DynamicData;
using Splat;
using System;
using System.Collections.ObjectModel;
using XamarinReactiveUI.Models;
using XamarinReactiveUI.Services;

namespace XamarinReactiveUI.ViewModels
{
    public class DynamicListEmployeeViewModel : ListEmployeeViewModel
    {
        private SourceList<Employee> employeeSourceList = new SourceList<Employee>();

        private ReadOnlyObservableCollection<Employee> employees;
        public ReadOnlyObservableCollection<Employee> Employees => employees;

        private readonly IEmployeeService employeeService;

        public DynamicListEmployeeViewModel(IEmployeeService employeeService = null)
        {
            this.employeeService = employeeService ?? Locator.Current.GetService<IEmployeeService>();

            employeeSourceList.AddRange(this.employeeService.GetEmployees());

            employeeSourceList.Connect().Bind(out employees).Subscribe();
        }
    }
}
