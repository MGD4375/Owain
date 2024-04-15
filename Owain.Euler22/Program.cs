

string namesFile = File.ReadAllText("C:\\git\\Owain\\Owain.Euler22\\Names.txt");

var orderedStrings = namesFile
    .Replace("\"", "")
    .Split(',')
    .Order();

var valueOfNames = orderedStrings.Select((name, index) => CalculateValue(name, index));

var sumOfNames = valueOfNames.Sum();

Console.WriteLine(sumOfNames);

//Console.WriteLine(valueOfNames.ToList()[937]);
//Console.WriteLine(CalculateValue("COLIN", 937));

Console.ReadKey();
int CalculateValue(string name, int index)
{
    var numbers = name.Select(ch =>
    {
        return ch - 64;
    });
    var result =  numbers.Sum() * (index + 1);

    return result;
}

