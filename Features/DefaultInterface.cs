namespace CSharpFeatures.Features
{
    #nullable disable
    public class DefaultInterface : IDefaultInterface
    {
        public T Get<T>()
        {
            return default(T);
        }
    }

    public interface IDefaultInterface
    {
        T Get<T>() => default(T);
    }
    #nullable enable
}