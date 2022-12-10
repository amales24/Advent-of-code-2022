using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

var path = "input.txt";
var lines = new List<string>() { };
  
foreach (string line in System.IO.File.ReadLines(path))
{
    lines.Add(line);
}

//______________________________________________________________
var i = 0;
var cycle = 1;
var X = 1;
var addxPhase = 0;

var sumsList = new List<int>();

while(true)
{
    var splitLine = lines[i].Split(" ");

    if (splitLine[0] == "addx")
    {
        addxPhase++;

        if (addxPhase == 2)
        {
            X += int.Parse(lines[i].Split(" ")[1]);
            i++;
            addxPhase = 0;
        }
    }
    else
    {
        addxPhase = 0;
        i++;
    }

    cycle++;

    if ((cycle - 20) % 40 == 0)
        sumsList.Add(X * cycle);

    if (cycle == 220)
        break;
}

Console.WriteLine("Part 1:");
Console.WriteLine(sumsList.Sum());

//_____________________________________________________________________________________-
i = 0;
cycle = 1;
X = 1;
addxPhase = 0;
var output = "#";

while (true)
{
    var splitLine = lines[i].Split(" ");

    if (splitLine[0] == "addx")
    {
        addxPhase++;

        if (addxPhase == 2)
        {
            X += int.Parse(lines[i].Split(" ")[1]);
            i++;
            addxPhase = 0;
        }
    }
    else
    {
        addxPhase = 0;
        i++;
    }

    cycle++;

    if ((cycle - 1) % 40 == X - 1 || (cycle - 1) % 40 == X  || (cycle - 1) % 40 == X + 1)
        output += "#";
    else
        output += ".";

    if (cycle % 40 == 0)
    {
        output += "\n";
    }

    if (cycle == 240)
        break;
}

Console.WriteLine("\nPart 2:");
Console.WriteLine(output);

