using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UTAD.ToDoList.WPF.Models
{
    public class SchedulerViewModel : NotificationObject
    {
        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                RaisePropertyChanged("SelectedDate");
                DisplayDate = SelectedDate.ToString("MMMM yyyy");
            }
        }

        private string displayDate;

        public string DisplayDate
        {
            get { return displayDate; }
            set
            {
                displayDate = value;
                RaisePropertyChanged("DisplayDate");
            }
        }
    }
}
