using System;
using System.Globalization;
using System.Windows.Data;
using GongSolutions.Wpf.DragDrop;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Value converter for <see cref="DropTarget"/> view models to <see cref="IDropTarget"/> instances.
    /// </summary>
    public class DropHandlerConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is DropTarget target)
            {
                return target.AsGongHandler();
            }

            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}