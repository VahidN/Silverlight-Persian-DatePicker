using System;
using WpfPersianDatePickerUsage.Models;

namespace WpfPersianDatePickerUsage.ViewModels
{
    public class TestViewViewModel
    {
        public User User { set; get; }

        public TestViewViewModel()
        {
            User = new User
            {
                Name = "Vahid",
                AccountValidTo = DateTime.Now
            };
        }
    }
}
