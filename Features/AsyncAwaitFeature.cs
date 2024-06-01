namespace CSharpFeatures;

public class AsyncAwaitFeature
{
    public async Task ExecuteProcess()
    {
        await foreach (var item in ProcessTask().ConfigureAwait(false))
        {
            Console.WriteLine($"Result: {item}");
        }
    }

    public async IAsyncEnumerable<int> ProcessTask()
    {
        var allTask = new List<Task<int>>();
        var random = new Random();
        var list = Enumerable.Range(1, 25);

        /*
        for (var i = 0; i < 25; i++)
        {
            allTask.Add(MyTask(random.Next(0, 9)));
        }
        */

        Parallel.For(1, 25, (i, state) =>
        {
            allTask.Add(MyTask(random.Next(0, 9)));
        });

        /*
        var allTask = new List<Task<int>>
        {
            FirstTask(),
            SecondTask(),
            ThirdTask(),
            FourthTask(),
            FifthTask()
        };
        */

        while (allTask.Count > 0)
        {
            Task<int> finishedTask = await Task.WhenAny(allTask);
            allTask.Remove(finishedTask);

            var result = await finishedTask.ConfigureAwait(false);
            yield return result;
        }
    }

    public async Task<int> MyTask(int i)
    {
        Console.WriteLine($"Task {i} with Thread {System.Environment.CurrentManagedThreadId} started at {new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds()}");

        await Task.Delay(i * 1000);
        return i;
    }

    public async Task<int> FirstTask()
    {
        await Task.Delay(1000);
        return 1;
    }

    public async Task<int> SecondTask()
    {
        await Task.Delay(2000);
        return 2;
    }

    public async Task<int> ThirdTask()
    {
        await Task.Delay(3000);
        return 3;
    }

    public async Task<int> FourthTask()
    {
        await Task.Delay(4000);
        return 4;
    }

    public async Task<int> FifthTask()
    {
        await Task.Delay(5000);
        return 5;
    }
}

