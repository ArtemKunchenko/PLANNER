using System;
using System.Globalization;
using System.Windows.Data;

public class AmountToIncomeExpenseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal amount)
        {
            return amount < 0 ? "Expense" : "Income";
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
