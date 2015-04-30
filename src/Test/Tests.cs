namespace CER.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;

    [TestClass]
    public class Tests<T, A>
    {
        public static readonly char Separator = '|';
        public static CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public delegate void Assertion(T obj, A fact);
        public Assertion[] Assertions = new Assertion[]
        {
            (obj, f) => new object(),
            (obj, f) => {new object();}
        };
    }
}