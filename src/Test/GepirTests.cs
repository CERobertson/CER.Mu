namespace CER.Test
{
    using CER.Test.org.gs1us.prod.gepir;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class GepirTests
    {
        [TestMethod]
        public void GetInfo()
        {
            CER.Test.org.gs1us.prod.gepir.router r = new router();
            var o = r.GetItemByGTIN(new GetItemByGTIN { requestedGtin = "0788687", requestedLanguages = new string[] { "En-US" }, version = 4.0M });
        }
    }
}
