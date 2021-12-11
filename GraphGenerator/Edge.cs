using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphGenerator
{
    public class Edge
    {
        int[] nodes = new int[2];
        public int[] Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
        public void SetNode(int position, int value)
        {
            if (position < 2)
            {
                nodes[position] = value;
            }
        }
        public int GetNode(int position)
        {
            if (position < 2)
            {
                return nodes[position];
            }
            return -1;
        }
        public static Edge Generate(int node1, int node2)
        {
            Edge edge = new Edge();
            edge.SetNode(0, node1);
            edge.SetNode(1, node2);
            return edge;
        }
    }
}
