using System.Windows;

namespace UTAD.ToDoList.WPF
{
    public class WatermarkService : DependencyObject
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(WatermarkService));

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }


        public static readonly DependencyProperty WatermarkContentVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("WatermarkContentVerticalAlignment", typeof(VerticalAlignment), typeof(WatermarkService), new PropertyMetadata(VerticalAlignment.Top));

        public static VerticalAlignment GetWatermarkContentVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(WatermarkContentVerticalAlignmentProperty);
        }

        public static void SetWatermarkContentVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(WatermarkContentVerticalAlignmentProperty, value);
        }

    }
}
