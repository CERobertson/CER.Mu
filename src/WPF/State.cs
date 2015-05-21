using CER.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CER.WPF
{
    public class State : Node<State>
    {
        public void ActivateChildren()
        {
            foreach (var c in this.children)
            {
                c.Switch();
            }
        }
        public void ActivateParents()
        {
            foreach (var p in this.parents)
            {
                p.Switch();
            }
        }

        public Action Switch { get; set; }

        public void ActivateChild(int index)
        {
            this.children[index].Switch();
        }
        public void ActivateParent(int index)
        {
            this.parents[index].Switch();
        }
    }
}
