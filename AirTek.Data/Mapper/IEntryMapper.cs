namespace AirTek.Data.Mapper
{
    public interface IEntryMapper<T, U>
    {
        T Map(U item);
    }
}