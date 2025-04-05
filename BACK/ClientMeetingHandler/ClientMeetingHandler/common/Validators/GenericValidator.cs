namespace ClientMeetingHandler.common.Validators;

public static class GenericValidator
{ 
    public static object? GetDefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
}