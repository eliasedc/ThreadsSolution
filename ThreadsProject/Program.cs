// See https://aka.ms/new-console-template for more information

Console.WriteLine("Use a Unique Thread in 3 seconds");
Thread.Sleep(3000);
DateTime dtBefore = DateTime.Now;
await UseUniqueThread(1000);
double dblTotalSeconds = (DateTime.Now - dtBefore).TotalSeconds;
Console.WriteLine($"Total Time (seconds): {dblTotalSeconds}");

Console.WriteLine("Use Multiples Threads in 3 seconds");
Thread.Sleep(3000);
dtBefore = DateTime.Now;
await UseMultipleThread(1000);
double dblTotalSeconds2 = (DateTime.Now - dtBefore).TotalSeconds;
Console.WriteLine($"Total Time UniqueThread (seconds): {dblTotalSeconds}");
Console.WriteLine($"Total Time MutipleThread (seconds): {dblTotalSeconds2}");
Console.Read();

async Task ForTest(string pText, int pNumber)
{
    for (int i = 0; i < pNumber; i++)
    {
        await MyWriteLine(pText);
    }
    Console.WriteLine("Task Finished");
}

async Task MyWriteLine(string pText)
{
    Console.WriteLine(pText);
    //await Task.Delay(1); //cria uma nova task com delay(x), libera o recurso da thread atual para que outros métodos sejam executados
    //Thread.Sleep(1000); //para toda a thread
    //Task.Delay(1000).Wait(); //para toda a thread
}

async Task UseUniqueThread(int pNumber)
{
    //se dentro este método tiver algum await, vai liberar recurso para ir para o próximo, se não, vai se como se fosse syncrono.
    Task t1 = ForTest("1", pNumber);
    Task t2 = ForTest("2", pNumber);

    await Task.WhenAll(t1, t2); //útil caso dentro dos métodos acima exista algum processo com await (libera recurso)
}
async Task UseMultipleThread(int pNumber)
{
    Task t1 = Task.Run(() => ForTest("1", pNumber));
    Task t2 = Task.Run(() => ForTest("2", pNumber));

    await Task.WhenAll(t1, t2);
}
