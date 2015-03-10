namespace CER.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;

    [TestClass]
    public class Tests<T, D>
    {
        public static readonly char Seperator = '|';
        public static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public delegate void Assertion(T objectToTest, D dataContext);
        public  Assertion[] Assertions = new Assertion[]
        {
            (obj, dc) => new object(),
			(obj, dc) => {new object();}
        };
    }
}