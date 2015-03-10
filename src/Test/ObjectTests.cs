namespace CER.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObjectTests
    {
        [TestMethod]
        public void Main()
        {
            foreach (var assertion in this.Assertions)
            {

            }
        }

        public delegate bool Assertion(object objectToTest, object dataContext);
        public Assertion[] Assertions = new Assertion[]
        {
            (obj, dc) => false,
			(obj, dc) => {return false;},
            (object obj, object dc) => false,
            (object obj, object dc) => {return false;},
            delegate(object obj, object dc)
            {
                return false;
            }
        };
    }
}