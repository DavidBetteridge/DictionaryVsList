using System.Diagnostics;
using DictionaryTest;

var sw = new Stopwatch();


var dictionary = new SimpleDictionary<string, int>();
AddData(dictionary);
sw.Start();
ReadData(dictionary);
sw.Stop();
Console.WriteLine($"Dictionary: {sw.ElapsedMilliseconds}ms");


var list = new SimpleList<string, int>();
AddData(list);
sw.Restart();
ReadData(list);
sw.Stop();
Console.WriteLine($"List: {sw.ElapsedMilliseconds}ms");
return;


void AddData(IThingToTest<string, int> sut)
{
// Add 10000 strings from A to Z, then from AA to ZZ, etc.
    var count = 0;
    var nextString = "A";
    while (count < 100000)
    {
        sut.Add(nextString, count);
        count++;
        if (nextString.EndsWith('Z'))
            nextString += 'A';
        else
            nextString = nextString[..^1] + (char)(nextString[^1] + 1);
    }
}

void ReadData(IThingToTest<string, int> sut)
{
    // Try to get all 10000 strings
    var count = 0;
    var nextString = "A";
    while (count < 100000)
    {
        if (!sut.TryGet(nextString, out var value) || value != count)
            throw new Exception($"Failed to get {nextString}");
        count++;
        if (nextString.EndsWith('Z'))
            nextString += 'A';
        else
            nextString = nextString[..^1] + (char)(nextString[^1] + 1);
    }
}