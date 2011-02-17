using System.ComponentModel;
using System.Globalization;

public class GetPartyByGTINConverter : ExpandableObjectConverter
{
    public override object ConvertTo(ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        System.Type destinationType)
    {
        if (destinationType == typeof(System.String[]) && (value is System.String[]))
        {
            return  System.String.Join(",", (System.String[])value);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}
