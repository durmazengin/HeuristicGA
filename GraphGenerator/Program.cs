using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GraphData> graphs = new List<GraphData>();
            /*
            graphs.Add(GraphData.generate(1000, 0.10));
            graphs.Add(GraphData.generate(1000, 0.20));
            graphs.Add(GraphData.generate(2000, 0.10));
            graphs.Add(GraphData.generate(2000, 0.20));
            */

            graphs.Add(GraphData.generate(50, 0.05));
            graphs.Add(GraphData.generate(50, 0.10));
            graphs.Add(GraphData.generate(50, 0.15));
            graphs.Add(GraphData.generate(50, 0.20));
            graphs.Add(GraphData.generate(50, 0.30));

            graphs.Add(GraphData.generate(75, 0.05));
            graphs.Add(GraphData.generate(75, 0.10));
            graphs.Add(GraphData.generate(75, 0.15));
            graphs.Add(GraphData.generate(75, 0.20));
            graphs.Add(GraphData.generate(75, 0.30));

            graphs.Add(GraphData.generate(100, 0.05));
            graphs.Add(GraphData.generate(100, 0.10));
            graphs.Add(GraphData.generate(100, 0.15));
            graphs.Add(GraphData.generate(100, 0.20));

            graphs.Add(GraphData.generate(125, 0.05));
            graphs.Add(GraphData.generate(125, 0.10));

            graphs.Add(GraphData.generate(150, 0.05));
            graphs.Add(GraphData.generate(150, 0.10));

            graphs.Add(GraphData.generate(175, 0.05));
            graphs.Add(GraphData.generate(200, 0.05));

        }
    }
}
