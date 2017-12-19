using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphx.Graph
{
    public class Vertex
    {
        public int Index;
        public int Color;
        public bool Visited = false;
        public int Degree => ConnectedVertices.Count;

        public List<Vertex> ConnectedVertices;

        public Vertex()
        {
            ConnectedVertices = new List<Vertex>();
        }

    }

}
