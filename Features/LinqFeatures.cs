using System.Collections;

namespace CSharpFeatures;

class LinqFeatures
{
    public async Task Process()
    {
        await Task.Delay(1000);
        var items = Enumerable.Range(0, 10000);
        Console.WriteLine($"Lib Function {Enumerable.Select(items, i => i * 2).Sum()}");
        Console.WriteLine($"Custom Function {CustomEnumerable.Select(items, i => i * 2).Sum()}");
    }
}

static class CustomEnumerable
{
    public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);

        return new SelectManualEnumerable<TSource, TResult>(source, selector);
    }
}

sealed class SelectManualEnumerable<TSource, TResult> : IEnumerable<TResult>
{
    private IEnumerable<TSource> _source { get; }
    private Func<TSource, TResult> _selector { get; }

    public SelectManualEnumerable(IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        _source = source;
        _selector = selector;
    }

    public IEnumerator<TResult> GetEnumerator()
    {
        return new Enumerator(_source, _selector);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<TResult>
    {
        private IEnumerable<TSource> _source { get; }
        private Func<TSource, TResult> _selector { get; }
        private TResult _current = default!;
#nullable disable
        private IEnumerator<TSource> _enumerator;
#nullable disable
        private int _state = 1;

        public Enumerator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            _selector = selector;
            _source = source;
        }

        public TResult Current => _current;

        object IEnumerator.Current => Current!;

        public bool MoveNext()
        {
            switch (_state)
            {
                case 1:
                    {
                        _enumerator = _source.GetEnumerator();
                        _state = 2;
                        goto case 2;
                    }
                case 2:
                    {
                        try
                        {
                            while (_enumerator.MoveNext())
                            {
                                _current = _selector(_enumerator.Current);
                                return true;
                            }
                        }
                        catch
                        {
                            Dispose();
                            throw;
                        }
                        break;
                    }
            }

            Dispose();
            return false;
        }

        public void Dispose()
        {
            _enumerator?.Dispose();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }


}
