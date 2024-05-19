// See https://aka.ms/new-console-template for more information

int intSleep = 3000;
int intNumberToFor = 1000;
Console.WriteLine(AlignCenterText($"Number to For: {intNumberToFor}"));
Console.WriteLine();

//////////////////////////UNIQUE THREAD/////////////////////////////
Console.WriteLine(AlignCenterText("Using a Unique Thread"));
Thread.Sleep(intSleep);
await MethodWithoutThreads(intNumberToFor);
Console.WriteLine(AlignCenterText($"MethodWithoutThreads finished"));
////////////////////////////////////////////////////////////////////

Console.WriteLine();
Thread.Sleep(intSleep);

/////////////////////////MULTIPLES THREADS///////////////////////////
Console.WriteLine(AlignCenterText("Now.. Using Multiples Threads"));
Thread.Sleep(intSleep);
await MethodWithThreads(intNumberToFor);
Console.WriteLine(AlignCenterText($"MethodWithThreads finished"));
/////////////////////////////////////////////////////////////////////

Console.WriteLine();
Console.WriteLine($"FYI: To see performance test see XXXXXXXXXXXXX");
Console.WriteLine($"Believe me, it's an error trying to test here");
Console.ReadKey();

async Task ForTest(string pText, int pNumber)
{
    Console.WriteLine($"### Begin \"ForTest\" Method with pText: {pText} ");
    Thread.Sleep(intSleep*2);
        
    for (int i = 0; i < pNumber; i++)
    {
        await MyWriteLine($"Called with pText: \"{pText}\" i: {i}");
    }

    Console.WriteLine();
    Console.WriteLine($"### \"ForTest\" Method with pText: {pText} Finished");
    Thread.Sleep(intSleep);
    Console.WriteLine();
}

async Task MyWriteLine(string pText)
{
    Console.WriteLine(pText);
    ////This create a new Task with delay(x) and release the resource from thread to run other code lines
    ////If you uncomment, you can see that even using "MethodWithoutTrhreads" the "ForTest" method will run asynchronously. 
    ////This works due to the use of async/await.
    ////FYI: if you do it, I suggest set the "intNumberToFor" to 1000 or less;
    //await Task.Delay(1); 

    ////Suspends the current thread. Does't release resource from thread to run other code lines
    //Thread.Sleep(1); 

    ////Waits for the Delay(x) to complete execution, in the practic is similar to Thread.Sleep
    //Task.Delay(1000).Wait(); 
}

async Task MethodWithoutThreads(int pNumber)
{
    //If there is an await inside the ForTest method, the code within it will free up resources to go to the next one (it will run asynchronously).
    //If not, the code will run as if it were synchronized.
    Task t1 = ForTest("ELIASDC", pNumber);
    Task t2 = ForTest("DEV", pNumber);

    await Task.WhenAll(t1, t2); //Useful if within the methods above there is a process with await (releases resource)
}
async Task MethodWithThreads(int pNumber)
{
    Task t1 = Task.Run(() => ForTest("ELIASDC", pNumber));
    Task t2 = Task.Run(() => ForTest("DEV", pNumber));

    await Task.WhenAll(t1, t2);
}

string AlignCenterText(string pText)
{
    int intTotalLength = 60;

    string strSpace = new string('-', (intTotalLength - pText.Length) / 2);
    return string.Format("{0}{1}{0}", strSpace, pText);    
}