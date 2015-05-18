namespace CER.Graphs
{
    using System;
using System.Linq;

    public class Oscillator : DirectedGraph
    {
        public static readonly string Template = "{'A':['B'],'B':['A']}";

        public Oscillator() : this(Oscillator.Template) { }
        public Oscillator(string json)
            : base(json)
        {
            if (base.IsDirectedAcyclicGraph)
            {
                throw new OscillatorException("There should be a loop.");
            }
        }
    }

    public class OscillatorException : Exception { public OscillatorException(string message) : base(message) { } }
}
