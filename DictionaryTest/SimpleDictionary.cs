namespace DictionaryTest;

public class SimpleDictionary<TKey, TValue>: IThingToTest<TKey, TValue> where TKey : notnull
{
    private record KeyAndValue
    {
        public required TKey Key { get; init; }
        public required TValue Value { get; init; }
    }

    private readonly List<KeyAndValue>?[] _buckets = new List<KeyAndValue>[128];

    public void Add(TKey key, TValue value)
    {
        var bucketNumber = Math.Abs(key.GetHashCode()) % _buckets.Length;
        if (_buckets[bucketNumber] is null)
            _buckets[bucketNumber] = new List<KeyAndValue>();

        foreach (var entry in _buckets[bucketNumber]!)
        {
            if (entry.Key.Equals(key))
                throw new Exception($"{key} has already been added"); 
        }
        
        _buckets[bucketNumber]!.Add(new KeyAndValue{ Key = key, Value = value});
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        var bucketNumber = Math.Abs(key.GetHashCode()) % _buckets.Length;
        if (_buckets[bucketNumber] is null)
        {
            value = default;
            return false;
        }

        foreach (var entry in _buckets[bucketNumber]!)
        {
            if (entry.Key.Equals(key))
            {
                value = entry.Value;
                return true;
            }
        }

        value = default;
        return false;
    }
}