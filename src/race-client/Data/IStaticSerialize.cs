namespace SSC.Client.Data
{
    public interface IStaticSerialize<T>
    {
        string Seralize(T input);
        T Deserialize(string input);
    }
}
