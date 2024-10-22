namespace DictionaryTest;

public interface IThingToTest<TKey, TValue> where TKey : notnull
{
    void Add(TKey key, TValue value);
    bool TryGet(TKey key, out TValue? value);

}