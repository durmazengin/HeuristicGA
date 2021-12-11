using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestMWVCP
{
    public class GraphData
    {
        int numberOfNodes = 0;
        double density = 0;
        double[] nodeWeights = null;
        List<Edge> edges = null;

        public GraphData(int numberOfNodes, double density)
        {
            this.numberOfNodes = numberOfNodes;
            this.density = density;

            nodeWeights = new double[numberOfNodes];
            edges = new List<Edge>();
        }

        public bool setNodeWeight(int index, double value)
        {
            if (nodeWeights == null || index >= nodeWeights.Length)
            {
                return false;
            }
            nodeWeights[index] = value;
            return true;
        }

        public int getNumberOfNodes()
        {
            return numberOfNodes; 
        }

        public void save()
        {
            String strNodeWeights = "";

            for (int i = 0; i < numberOfNodes; i++)
            {
                strNodeWeights += String.Format("{0} {1:0.00}\n", i, nodeWeights[i]);
            }

            String strEdges = "";
            for (int i = 0; i < edges.Count; i++)
            {
                Edge edge = edges[i];
                strEdges += String.Format("{0} {1}\n", edge.GetNode(0), edge.GetNode(1));
            }

            int density100 = (int)(density * 100);
            // save to file; for 50 nodes and 0.5 density, file name is graph_50_0_50.txt
            String fileName = String.Format("graph_{0:D2}_{1:D1}_{2:D2}.txt", numberOfNodes, density100 / 100, density100 % 100); 

            String strContent = "";
            strContent += String.Format("{0}\n", numberOfNodes);
            strContent += String.Format("{0}\n", edges.Count);
            strContent += strNodeWeights;
            strContent += strEdges;

            File.WriteAllText(fileName, strContent);
        }

        public void addEdge(Edge edge)
        {
            edges.Add(edge);
        }

        public int getEdgeCount()
        {
            return edges.Count;
        }
        public List<Edge> getEdges()
        {
            return edges;
        }

        public static GraphData generate(int numberOfNodes, double density)
        {
            GraphData graphData = new GraphData(numberOfNodes, density);
                        
            int maxNumOfEdges = (int)(numberOfNodes * density);
            
            Random rand = new Random((int)DateTime.Now.Ticks);
            
            // Generate mode weights
            for (int i = 0; i < numberOfNodes; i++)
            {
                graphData.setNodeWeight(i, (double)rand.Next(100) / 100);
            }
            
            // generate edges
            for (int i = 0; i < numberOfNodes / 2; i++)
            {
                for (int j = i + 1; j < numberOfNodes; j++)
                {
                    if (graphData.getEdgeCount() >= maxNumOfEdges)
                    {
                        break;
                    }
                    double random = (double)rand.Next(100) / 100;
                    if (random < density)
                    {
                        graphData.addEdge(Edge.Generate(i, j));
                    }
                }
            }

            graphData.save();

            return graphData;
        }
    }
}
