using System.ComponentModel;
using System.Runtime.CompilerServices;

var path = "input.txt";
var lines = new List<string>() { };
  
foreach (string line in System.IO.File.ReadLines(path))
{
    lines.Add(line);
}

//______________________________________________________________

var headX = 0;
var headY = 0;
var tailX = 0;
var tailY = 0;

var visited = new List<(int x, int y)>() { (0,0) };

(int x, int y) MoveHead(string direction, int headX, int headY)
{
    if (direction == "D")
        return (headX, headY - 1);
    else if (direction == "U")
        return (headX, headY + 1);
    else if (direction == "L")
        return (headX - 1, headY);
    else
        return (headX + 1, headY);
}

bool IsTouchingHead(int headX, int headY, int tailX, int tailY)
{
    var rangeX = new List<int>() { tailX - 1, tailX, tailX + 1 };
    var rangeY = new List<int>() { tailY - 1, tailY, tailY + 1 };

    if (rangeX.Contains(headX) && rangeY.Contains(headY))
        return true;
    return false;
}

(int x, int y) MoveTail(int headX, int headY, int tailX, int tailY)
{
    int newX = tailX;
    int newY = tailY;

    if (!IsTouchingHead(headX, headY, tailX, tailY))
    {
        if (headX > tailX)
            newX = tailX + 1;
        else if (headX < tailX)
            newX = tailX - 1;

        if (headY > tailY)
            newY = tailY + 1;
        else if (headY < tailY)
            newY = tailY - 1;
    }

    return (newX, newY);
}

foreach (var l in lines)
{
    var splitLine = l.Split(" ");
    var direction = splitLine[0];
    var steps = int.Parse(splitLine[1].ToString());

    for (var i = 0; i < steps; i++)
    {
        var moveHead = MoveHead(direction, headX, headY);
        headX = moveHead.x;
        headY = moveHead.y;
        var moveTail = MoveTail(headX, headY, tailX, tailY);
        tailX = moveTail.x;
        tailY = moveTail.y;

        if (!visited.Contains((tailX, tailY)))
            visited.Add((tailX, tailY));
    }
}

Console.WriteLine(visited.Count);

//_________________________________________________________________________________________

var allNodes = new List<(int x, int y)>();
var visitsForLastOne = new List<(int x, int y)>() { (0,0)};

for (int i = 0; i < 10; i++)
{
    allNodes.Add((0, 0));
}

foreach (var l in lines)
{
    var splitLine = l.Split(" ");
    var direction = splitLine[0];
    var steps = int.Parse(splitLine[1].ToString());
   
    for (var i = 0; i < steps; i++)
    {
        headX = allNodes[0].x;
        headY = allNodes[0].y;
        allNodes[0] = MoveHead(direction, headX, headY);

        for (var j = 1; j < 10; j++)
        {
            headX = allNodes[j - 1].x;
            headY = allNodes[j - 1].y;

            tailX = allNodes[j].x;
            tailY = allNodes[j].y;
            allNodes[j] = MoveTail(headX, headY, tailX, tailY);

            if (j == 9 && !visitsForLastOne.Contains(allNodes[j]))
                visitsForLastOne.Add(allNodes[j]);
        }
    }
}

Console.WriteLine(visitsForLastOne.Count);