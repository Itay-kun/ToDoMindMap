using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindOrgenizerToDo.ToDo.Connectors
{
    public class Connection
    {
        public BubbleControl Source { get; set; }
        public BubbleControl Target { get; set; }

        public Connection(BubbleControl source, BubbleControl target)
        {
            Source = source;
            Target = target;
        }
    }

}
