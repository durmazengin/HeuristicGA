using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWVCPHeuristicGA
{
    public class ProblemModel
    {
        private int numberOfNodes = 0;
        private int numberOfEdges = 0;
        private double[] nodeWeights = null;
        private List<int> decisionVariables = null;
        private List<ConstraintBE> constraints = null;
        private int[] edgesCountOfNode = null;
        private Random rand = null;

        public ProblemModel()
        {
            decisionVariables = new List<int>();
            constraints = new List<ConstraintBE>();
            rand = new Random((int)DateTime.Now.Ticks);
        }

        public int getVariableCount()
        {
            return decisionVariables.Count;
        }
        public Chromosome selectBetterObjective(Chromosome chromosome1, Chromosome chromosome2)
        {
            double objectiveVal1 = calculateObjectiveVal(chromosome1);
            double objectiveVal2 = calculateObjectiveVal(chromosome2);
            return objectiveVal1 < objectiveVal2 ? chromosome1 : chromosome2;
        }

        public double calculateObjectiveVal(Chromosome chromosome)
        {
            if(chromosome == null)
            {
                return Double.MaxValue;
            }

            double objectiveValue = 0;
            foreach (int nodeIndex in decisionVariables)
            {
                if(chromosome.getGeneAt(nodeIndex) == 1)
                {
                    objectiveValue += nodeWeights[nodeIndex];
                }
            }
            return objectiveValue;
        }
        public Chromosome repair(Chromosome chromosome)
        {
            foreach (ConstraintBE constraint in constraints)
            {
                /* Looking for constraints if edge is not satisfied at least once
                 *
                 * Calculate centrality as node_weight / node_edges
                 * Then set node having centrality to one
                 */
                List<int> variableIndices = constraint.getVariableIndices();
                int genValue0 = chromosome.getGeneAt(variableIndices[0]);
                int genValue1 = chromosome.getGeneAt(variableIndices[1]);
                if(genValue0 + genValue1 < 1)
                {
                    int indexRepairment = variableIndices[1];
                    bool isRandom = rand.NextDouble() > 0.5;
                    if (isRandom) // decide randomly which will be set 1 
                    {
                        indexRepairment = rand.NextDouble() > 0.5 ? variableIndices[0] : variableIndices[1];
                    }
                    else // decide heuristic
                    {
                        double centrality0 = nodeWeights[variableIndices[0]] / edgesCountOfNode[variableIndices[0]];
                        double centrality1 = nodeWeights[variableIndices[1]] / edgesCountOfNode[variableIndices[1]];
                        if (centrality0 < centrality1)
                        {
                            indexRepairment = variableIndices[0];//more central will be one
                        }
                    }
                    // mutate can also be used change gene value from 0 to 1
                    chromosome.mutate(indexRepairment);
                }
            }
            /*
             * check if node exists with value 1 and with 1 edge
             * in this case select neighbor node which has more than 1 edge
             */
            foreach (ConstraintBE constraint in constraints)
            {
                // remove 1 from node if it has single edge
                List<int> variableIndices = constraint.getVariableIndices();
                int edgeCountNode0 = edgesCountOfNode[variableIndices[0]];
                int edgeCountNode1 = edgesCountOfNode[variableIndices[1]];
                if(edgeCountNode0 == 1)
                {
                    if(chromosome.getGeneAt(variableIndices[0]) == 1)
                    {
                        chromosome.setGeneAt(variableIndices[0], 0);
                        chromosome.setGeneAt(variableIndices[1], 1);
                    }
                }
                else if(edgeCountNode1 == 1)
                {
                    if (chromosome.getGeneAt(variableIndices[1]) == 1)
                    {
                        chromosome.setGeneAt(variableIndices[1], 0);
                        chromosome.setGeneAt(variableIndices[0], 1);
                    }
                }
            }
            return chromosome;
        }

        public string printObjective()
        {

            // set objective function
            String strExpression = "min ";
            foreach (int nodeIndex in decisionVariables)
            {
                String strExpr = String.Format("{0:0.00} *varNode{1:D4}", nodeWeights[nodeIndex], nodeIndex);
                strExpression = strExpression + strExpr + " + ";
            }
            strExpression = strExpression.Substring(0, strExpression.Length - 3); // to remove last "+ "
            return strExpression;
        }
        public string printConstrains()
        {
            String strConstraints = "Subject to ";
            foreach (ConstraintBE constraint in constraints)
            {
                strConstraints = strConstraints + "\n" + constraint.printConstraint();
            }
            return strConstraints;
        }

        public void setNumberOfNodes(int nodesCount)
        {
            numberOfNodes = nodesCount;
            edgesCountOfNode = new int[numberOfNodes];
        }
        
        public void setNumberOfEdges(int edgesCount)
        {
            numberOfEdges = edgesCount;
        }

        public void setNodeWeigths(double[] weights)
        {
            nodeWeights = weights;
        }

        public void addVariable(int nodeIndex)
        {
            edgesCountOfNode[nodeIndex]++;
            for (int i = 0; i < decisionVariables.Count; i++)
            {
                if(decisionVariables[i] == nodeIndex)
                {
                    return;// node added to decision variable before
                }
            }
            decisionVariables.Add(nodeIndex);
        }

        public void addConstraint(ConstraintBE constraint)
        {
            constraints.Add(constraint);
        }

    }
}
