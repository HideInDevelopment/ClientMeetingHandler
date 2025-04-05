namespace ClientMeetingHandler.common.Validators;

public static class EntityValidator
{
    public static bool IsNullOrDefault<T>(T value)
    {
        if (value is null) return true;

        var properties = value.GetType().GetProperties();

        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(value);
            var defaultValue = GenericValidator.GetDefaultValue(property.PropertyType);

            Console.WriteLine($"Property: {property.Name}, Value: {propertyValue}, Default: {defaultValue}");

            switch (propertyValue)
            {
                case Guid guidValue when guidValue == Guid.Empty:
                case int and 0:
                case string strValue when string.IsNullOrWhiteSpace(strValue):
                    return true;
            }
            
            if(property.PropertyType.IsEnum) continue;

            if (Equals(propertyValue, defaultValue))
            {
                return true;
            }
        }

        return false;
    }
}