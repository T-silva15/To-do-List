using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UTAD.ToDoList.WPF.Models
{
    public class Meeting
    {
        public string Name { get ; set; }
        public string? Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Brush BackgroundColor { get; set; }
        public string? RecurrenceRule { get; set; }
        public string Id { get; set; }
        public ObservableCollection<SchedulerReminder> Reminders { get; set; }
        public bool AllDay { get; set; }

        public Meeting()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("PT-pt");
        }
    }
}
