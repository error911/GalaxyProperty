namespace Properties
{
    public interface IPropertyProvider
    {
        bool TryGetProperty(string key, out IProperty property);
        bool TryGetPropertyValue<T>(string key, out T property, bool silent = false);
    }
}