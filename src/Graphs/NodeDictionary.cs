namespace CER.Graphs
{
    using CER.Runtime.Serialization;
    using System.Collections.Generic;

    public class NodeDictionary : Dictionary<string, Dictionary<string, string[]>>
    {
        public static readonly string DefaultJson = "{'A':{'A':['A','B'],'B':['A','B']},'B':{'A':{'A':['A','B'],'B':['A','B']}}";
        public NodeDictionary() : base() { }
        public NodeDictionary(string json, string quote = "'", bool throw_exception_on_flaws = false)
            : base(json.ParseJsonToSimple<NodeDictionary>(quote))
        {

        }
    }
}
