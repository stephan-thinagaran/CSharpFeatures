namespace CSharpFeatures.Features;

public class RecordFeature
{
    public void Process()
    {
        var person = new Person { FirstName = "John", LastName = "Doe" };
        var person2 = new Person { FirstName = "John", LastName = "Doo" };

        Console.WriteLine(person == person2); // True
        Console.WriteLine(person.Equals(person2)); // True
    }
}
