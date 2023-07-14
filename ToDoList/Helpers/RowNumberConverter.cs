using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System;

namespace ToDoList.Helpers;

public class RowNumberConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DataGridRow row)
        {
            return row.GetIndex() + 1;
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
