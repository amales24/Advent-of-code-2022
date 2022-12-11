using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

var path = "input.txt";
var lines = new List<string>() { };
 
foreach (string line in System.IO.File.ReadLines(path))
{
    lines.Add(line);
}

//________________________________________________________________________

var maxIndexOfMonkeys = 7; // 3 for test data
var itemsPerMonkey = new List<List<int>>();
var operationPerMonkey = new List<(string operation, int number)>() { };
var testPerMonkey = new List<int>();
var trueFalsePerMonkey = new List<(int t, int f)>();

for (int i = 0; i < lines.Count; i += 7 )
{
    itemsPerMonkey.Add(ListOfItems(lines[i + 1]));
    operationPerMonkey.Add(Operation(lines[i + 2]));
    testPerMonkey.Add(Test(lines[i + 3]));
    trueFalsePerMonkey.Add(TrueFalse(lines[i+4], lines[i+5]));
}

(string operation, int number) Operation(string line)
{
    var lineToList = line.Trim().Split(" ");
    var operation = lineToList[4];
    try
    {
        var number = int.Parse(lineToList[5]);
        return (operation, number);
    }
    catch
    {
        return (operation, -1);
    }
}

List<int> ListOfItems(string line)
{
    var lineToList = line.Trim().Split(" ");
    var listOfItems = new List<int>();

    for (var i = 2; i < lineToList.Count(); i++)
    {
        listOfItems.Add(int.Parse(lineToList[i].Trim(',')));
    }

    return listOfItems;
}

int Test(string line)
{
    var lineToList = line.Trim().Split(" ");

    return int.Parse(lineToList[3]);
}

(int t, int f) TrueFalse(string line1, string line2)
{
    var lineToList1 = line1.Trim().Split(" ");
    var lineToList2 = line2.Trim().Split(" ");

    var t = int.Parse(lineToList1[5]);
    var f = int.Parse(lineToList2[5]);

    return (t, f);
}

var monkeyActivity = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0};
int newWorry = 0;

for (var i = 0; i < 20; i++)
{
    for (var j = 0; j < maxIndexOfMonkeys + 1; j++)
    {
        while (itemsPerMonkey[j].Count() > 0)
        {
            newWorry = DoOperation(itemsPerMonkey[j][0], operationPerMonkey[j]);
            newWorry = newWorry / 3;

            if (newWorry % testPerMonkey[j] == 0)
            {
                var newMonkey = trueFalsePerMonkey[j].t;
                itemsPerMonkey[newMonkey].Add(newWorry);
                itemsPerMonkey[j].RemoveAt(0);
            }
            else
            {
                var newMonkey = trueFalsePerMonkey[j].f;
                itemsPerMonkey[newMonkey].Add(newWorry);
                itemsPerMonkey[j].RemoveAt(0);
            }

            monkeyActivity[j]++;
        }
    }
}

int DoOperation(int old, (string operation, int number) doOperation)
{
    if (doOperation.operation == "+")
    {
        if (doOperation.number != -1)
            return old + doOperation.number;
        else
            return old + old;
    }
    else
    {   if (doOperation.number != -1)
            return old * doOperation.number;
        else
            return old * old;
    }
}

monkeyActivity.Sort();
monkeyActivity.Reverse();

int business = monkeyActivity[0] * monkeyActivity[1];

Console.WriteLine(business);
