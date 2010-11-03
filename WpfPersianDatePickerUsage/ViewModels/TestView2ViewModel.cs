using System;
using WpfPersianDatePickerUsage.Models;

namespace WpfPersianDatePickerUsage.ViewModels
{
    public class TestView2ViewModel
    {
        public Users Users { set; get; }

        public TestView2ViewModel()
        {
            Users = new Users
           {
                 new User { Name = "Vahid" , AccountValidTo = DateTime.Now},
                 new User { Name = "Farid", AccountValidTo = DateTime.Now.AddDays(-2)}
            };
        }
    }
}
