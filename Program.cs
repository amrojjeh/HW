Part[] sails = new Part[]{new("Tyvek", 25.5m), new("Mylar", 27.0m), new("Rip-stop nylon", 33.55m)};
Part[] spines = new Part[]{new("Maple", 12.55m), new("Birch spars", 15.00m), new("Fiber glass", 30.00m), new("Carbon spars", 30.00m)};
Part[] cross_spars = new Part[]{new("Maple", 11.55m), new("Birch spars", 14.00m), new("Fiber glass", 28.00m), new("Carbon spars", 27.00m)};
Part[] birdles = new Part[]{new("Single point", 20.00m), new("Two points", 25.50m), new("Three points", 35.00m)};
Part[] lines = new Part[]{new("Cotton", 10.00m), new("Twisted nylon", 15.00m)};
Part[] tails = new Part[]{new("Plastic", 12.00m), new("Rip-stop nylon", 45.00m)};

Console.Clear();

Part sail = GetInput("Which sail would you like?", "Sail", sails);
Part spine = GetInput("Which spine would you like?", "Spine", spines);
Part cross_spar = GetInput("Which cross spar would you like?", "Cross spar", cross_spars);
Part birdle = GetInput("Which birdle would you like?", "Birdle", birdles);
Part line = GetInput("Which line would you like?", "Line", lines);
Part tail = GetInput("Which tail would you like?", "Tail", tails);

Kite kite = new(sail, spine, cross_spar, birdle, line, tail);

Console.WriteLine($"The total cost of your kite is: ${kite.GetCost():F2}");

void ClearRestOfLine()
{
    (int left, int top) = Console.GetCursorPosition();
    Console.Write(new string(' ', (Console.BufferWidth - left)));
    Console.SetCursorPosition(left, top);
}

void Temp(string error)
{
    ClearRestOfLine();
    Console.Write(error);
    (int left, int top) = Console.GetCursorPosition();
    Console.SetCursorPosition(left - error.Length, top);
}

Part GetInput(string prompt, string category, Part[] parts)
{
    (_, int input_top) = Console.GetCursorPosition();
    Console.WriteLine(prompt);
    for (int x = 0; x < parts.Length; ++x)
        Console.WriteLine($"{x + 1}: {parts[x].Name} (${parts[x].Price:F2})");
    Console.Write("Choose a part (press enter to confirm): ");
    return GetChoice(null);

    Part GetChoice(char? _input)
    {
        char input = _input ?? Console.ReadKey(true).KeyChar;
        while (!(Char.IsDigit(input) && input >= '1' && input <= ('0' + parts.Length)))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (!Char.IsDigit(input))
                Temp("YOU MUST ENTER A NUMBER");

            else if (input < '1')
                Temp("YOU CANNOT ENTER 0");
            else
                Temp($"YOU MUST ENTER {parts.Length} OR SMALLER");
            Console.ResetColor();
            input = Console.ReadKey(true).KeyChar;
        }
        int index = (char)input - '1';
        Console.ForegroundColor = ConsoleColor.Green;
        Temp(parts[index].Name);
        Console.ResetColor();
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter)
        {
            (int left, int top) = Console.GetCursorPosition();
            for (int x = 0; x <= (top - input_top); ++x)
            {
                Console.SetCursorPosition(0, top - x);
                ClearRestOfLine();
            }
            Console.Write($"{category}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{parts[index].Name} (${parts[index].Price})");
            Console.ResetColor();
            return parts[index];
        }
        return GetChoice(key.KeyChar);
    }
}

class Part
{
    public string Name;
    public decimal Price;

    public Part(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

class Kite
{
    public Part Sail;
    public Part Spine;
    public Part CrossSpar;
    public Part Birdle;
    public Part Line;
    public Part Tail;

    public Kite(Part sail, Part spine, Part cross_spar, Part birdle, Part line, Part tail)
    {
        Sail = sail;
        Spine = spine;
        CrossSpar = cross_spar;
        Birdle = birdle;
        Line = line;
        Tail = tail;
    }

    public decimal GetCost()
    {
        return  Sail.Price + Spine.Price
                + CrossSpar.Price + Birdle.Price
                + Line.Price + Tail.Price;
    }
}
