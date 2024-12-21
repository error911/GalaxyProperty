namespace Properties
{
    public interface IProperty
    {
        string Key { get; }
    }

    public interface IGetProperty<out T> : IProperty
    {
        T Value { get; }
    }

    public interface ISetProperty<in T> : IProperty
    {
        T Value { set; }
    }
}