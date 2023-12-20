string username = Console.ReadLine();
Console.WriteLine($"Hello, {username}");
Console.WriteLine("---------------------");
ConcatLogic concatenationLogic = new();
Console.WriteLine(concatenationLogic.Concat(username));
Console.WriteLine("---------------------");
Console.WriteLine(concatenationLogic.ConcatTypeTwo(username));