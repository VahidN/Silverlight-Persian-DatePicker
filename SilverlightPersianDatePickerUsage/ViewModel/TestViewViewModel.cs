using System;
using SilverlightPersianDatePickerUsage.Model;

namespace SilverlightPersianDatePickerUsage.ViewModel
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
