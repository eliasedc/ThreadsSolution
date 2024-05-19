// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using BenchmarkThreadTest;

Console.WriteLine("### Using BenchmarkDotNet  ###\r\n");
Console.WriteLine("Press Enter to start\r\n");
Console.ReadLine();
var resultado = BenchmarkRunner.Run<BenchmarkThread>();
Console.WriteLine("Finished");
Console.ReadLine();