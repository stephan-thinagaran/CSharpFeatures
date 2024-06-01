namespace CSharpFeatures.Features
{
    public class AsyncEnumerator
    {
        public static async Task Process()
        {
            await foreach (var item in GetItems())
            {
                Console.WriteLine($"Current time is {DateTime.Now:HH:mm:ss} and item is {item}");
            }
        }

        public static async IAsyncEnumerable<int> GetItems()
        {
            for(var i=0; i<10; i++)
            {
                await Task.Delay(2000);
                yield return i;
            }
        }
    }
}