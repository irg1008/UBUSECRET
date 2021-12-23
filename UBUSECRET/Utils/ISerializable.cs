namespace Utils
{
    public interface ISerializable<T>
    {
        // Devuelve un string en formato JSON.
        string To_JSON();

        // Devuelve una instancia de T.
        T From_JSON(string JSONString);
    }
}
