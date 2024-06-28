using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTAD.ToDoList.WPF.Models
{
    public class SchedulerReminder
    {
        public TimeSpan ReminderTimeInterval { get; set; }
        public DateTime ReminderAlertTime { get; set; }
        public object Data { get; set; }
        public bool Dismissed { get; set; }

        public SchedulerReminder()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("PT-pt");
        }
    }
}
