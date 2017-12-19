using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphx
{
    public class DataVertex : VertexBase
    {
        public DataVertex()
        {
        }

        public DataVertex(string text)
        {
            Text = text;
            Color = "Gray";
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
