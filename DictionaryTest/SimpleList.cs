namespace DictionaryTest;

public class SimpleList<TKey, TValue>: IThingToTest<TKey, TValue> where TKey : notnull
{
    private readonly List<TKey> _keys = new();
    private readonly List<TValue> _values = new();
    
    public void Add(TKey key, TValue value)
    {
        if (_keys.Contains(key))
            throw new Exception($"{key} has already been added");
        
        _keys.Add(key);
        _values.Add(value);
    }
    
    public bool TryGet(TKey key, out TValue? value)
    {
        var index = _keys.IndexOf(key);
        if (index == -1)
        {
            value = default;
            return false;
        }

        value = _values[index];
        return true;
    }
}