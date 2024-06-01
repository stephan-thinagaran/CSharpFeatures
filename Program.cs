using CSharpFeatures;

internal class Program
{
    public static async Task Main(string[] args)
    {
        /*
        Web Code
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        app.MapGet("/", () => "Hello World!");
        app.Run();
        */



        /*
        var recordFeature = new RecordFeature();
        recordFeature.Process();
        */

        /*
        await AsyncEnumerator.Process();
        */

        /*
        var asyncAwaitFeature = new AsyncAwaitFeature();
        await asyncAwaitFeature.ExecuteProcess();
        */

        var linqFeatures = new LinqFeatures();
        await linqFeatures.Process();

        Console.WriteLine("Completed");
    }
}