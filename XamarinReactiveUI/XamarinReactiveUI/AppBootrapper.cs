using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinReactiveUI.Services;

namespace XamarinReactiveUI
{
    public class AppBootrapper
    {
        public AppBootrapper()
        {
            RegisterService();
        }

        public void RegisterService()
        {
            Locator.CurrentMutable.Register(() => new EmployeeService(), typeof(IEmployeeService));
        }
    }
}
