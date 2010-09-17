using System.Windows;
using System.Windows.Media;

namespace SilverlightPersianDatePicker.Brushes
{
    /// <summary>
    /// Different custom brushes for cells of the calendar.
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>    
    public class CustomBrushes
    {
        #region Methods (11)

        // Public Methods (11) 

        /// <summary>
        /// Normal cells background brush.
        /// </summary>
        /// <returns></returns>
        public static LinearGradientBrush BorderBlueBackground()
        {
            var lgb = new LinearGradientBrush { StartPoint = new Point(0, 0), EndPoint = new Point(0, 1) };

            var gs1 = new GradientStop { Color = Color.FromArgb(255, 73, 184, 236), Offset = 0 };
            lgb.GradientStops.Add(gs1);

            var gs2 = new GradientStop { Color = Color.FromArgb(255, 1, 120, 174), Offset = 0.5 };
            lgb.GradientStops.Add(gs2);


            var gs3 = new GradientStop { Color = Color.FromArgb(255, 73, 184, 236), Offset = 1 };
            lgb.GradientStops.Add(gs3);

            return lgb;
        }

        /// <summary>
        /// Normal cells BorederBrush.
        /// </summary>
        /// <returns></returns>
        public static Brush CellsBorederBrush()
        {
            return new SolidColorBrush(Color.FromArgb(255, 29, 152, 196));
        }

        /// <summary>
        /// Normal cells texts Foreground.
        /// </summary>
        /// <returns></returns>
        public static Brush CellsForeground()
        {
            return new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        /// <summary>
        /// Background's brush of header.
        /// </summary>
        /// <returns></returns>
        public static LinearGradientBrush HeaderBackground()
        {
            var lgb = new LinearGradientBrush { StartPoint = new Point(0, 0), EndPoint = new Point(0, 1) };

            var gs1 = new GradientStop { Color = Color.FromArgb(255, 249, 226, 117), Offset = 0 };
            lgb.GradientStops.Add(gs1);

            var gs2 = new GradientStop { Color = Color.FromArgb(255, 234, 195, 8), Offset = 0.5 };
            lgb.GradientStops.Add(gs2);

            var gs3 = new GradientStop { Color = Color.FromArgb(255, 249, 226, 117), Offset = 1 };
            lgb.GradientStops.Add(gs3);

            return lgb;
        }

        /// <summary>
        /// BorderBrush of Header.
        /// </summary>
        /// <returns></returns>
        public static Brush HeaderBorderBrush()
        {
            return new SolidColorBrush(Color.FromArgb(255, 252, 209, 19));
        }

        /// <summary>
        /// Header's Foreground
        /// </summary>
        /// <returns></returns>
        public static Brush HeaderForeground()
        {
            return new SolidColorBrush(Color.FromArgb(255, 145, 86, 8));
        }

        /// <summary>
        /// Background brush of selected cells.
        /// </summary>
        /// <returns></returns>
        public static LinearGradientBrush HighlightBackground()
        {            
            var lgb = new LinearGradientBrush { StartPoint = new Point(0, 0), EndPoint = new Point(0, 1) };

            var gs1 = new GradientStop { Color = Color.FromArgb(255, 183, 217, 140), Offset = 0 };
            lgb.GradientStops.Add(gs1);

            var gs2 = new GradientStop { Color = Color.FromArgb(255, 87, 182, 82), Offset = 0.5 };
            lgb.GradientStops.Add(gs2);


            var gs3 = new GradientStop { Color = Color.FromArgb(255, 183, 217, 140), Offset = 1 };
            lgb.GradientStops.Add(gs3);

            return lgb;
        }

        /// <summary>
        /// BorderBrush of selected cells.
        /// </summary>
        /// <returns></returns>
        public static Brush HightlightBorderBrush()
        {
            return new SolidColorBrush(Color.FromArgb(255, 117, 192, 91));
        }

        /// <summary>
        /// Foreground brush of selected cells.
        /// </summary>
        /// <returns></returns>
        public static Brush HightlightForeground()
        {
            return new SolidColorBrush(Color.FromArgb(255, 237, 244, 230));
        }

        /// <summary>
        /// Today's Background brush.
        /// </summary>
        /// <returns></returns>
        public static LinearGradientBrush TodayBackground()
        {
            var lgb = new LinearGradientBrush { StartPoint = new Point(0, 0), EndPoint = new Point(0, 1) };

            var gs1 = new GradientStop { Color = Color.FromArgb(255, 253, 208, 208), Offset = 0 };
            lgb.GradientStops.Add(gs1);

            var gs2 = new GradientStop { Color = Color.FromArgb(255, 209, 102, 102), Offset = 0.5 };
            lgb.GradientStops.Add(gs2);

            var gs3 = new GradientStop { Color = Color.FromArgb(255, 253, 208, 208), Offset = 1 };
            lgb.GradientStops.Add(gs3);

            return lgb;
        }

        /// <summary>
        /// Today's BorderBrush.
        /// </summary>
        /// <returns></returns>
        public static Brush TodayBorderBrush()
        {
            return new SolidColorBrush(Color.FromArgb(255, 204, 146, 146));
        }

        #endregion Methods
    }
}
