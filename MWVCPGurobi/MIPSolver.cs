using Gurobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWVCPGurobi
{
    public class MIPSolver
    {
        public static String solve(String content)
        {
            String[] lines = content.Split('\n');
            int lineIndex = 0;

            int numOfNodes = Convert.ToInt32(lines[lineIndex++]); // first line is number of nodes
            int numOfEdges = Convert.ToInt32(lines[lineIndex++]); // second line is number of nodes

            double[] weights = new double[numOfNodes];
            for (int i = 0; i < numOfNodes; i++)
            {
                String weightLine = lines[lineIndex++];
                String[] array = weightLine.Split(' ');
                int nodeIndex = Convert.ToInt32(array[0]);
                double weight = Convert.ToDouble(array[1]);
                /*
                 * generally weight lines starts from node zero 
                 * and ends with node n-1
                 * but since line also contains node index,
                 * we should remove that assumption
                 * and assignment is made using node index in line
                 */

                if (nodeIndex < weights.Length)
                {
                    weights[nodeIndex] = weight;
                }
            }

            String strLogFile = String.Format("prb_{0:D4}_{1:D4}.log", numOfNodes, numOfEdges);
            String strModelFile = String.Format("prb_{0:D4}_{1:D4}.lp", numOfNodes, numOfEdges);
            // Create an empty environment, set options and start
            GRBEnv env = new GRBEnv(true);
            env.Set("LogFile", strLogFile);
            env.Start();

            GRBModel model = new GRBModel(env);
            /*
                model.GetVarByName() throws exception and
                model.GetVars() return 0 variables
                so I added my own control to check if variable added before or not
                */
            Dictionary<String, GRBVar> addedVariables = new Dictionary<String, GRBVar>();
            /*
             * following loop adds decision variables and constraints
             */
            for (int i = 0; i < numOfEdges; i++)
            {
                String edgeLine = lines[lineIndex++];
                String[] array = edgeLine.Split(' ');

                int node1 = Convert.ToInt32(array[0]);
                int node2 = Convert.ToInt32(array[1]);
                if(node1 != node2)// ignore if some error which draws edge itself
                {
                    // create or use existing decision variable for node 1
                    String strVarName1 = String.Format("varNode{0:D4}", node1);
                    GRBVar varForNode1 = null;
                    if(addedVariables.ContainsKey(strVarName1))
                    {
                        varForNode1 = addedVariables[strVarName1];
                    }
                    else
                    {
                        varForNode1 = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, strVarName1);
                        addedVariables.Add(strVarName1, varForNode1);
                    }

                    // create or use existing decision variable for node 2
                    String strVarName2 = String.Format("varNode{0:D4}", node2);
                    GRBVar varForNode2 = null;
                    if (addedVariables.ContainsKey(strVarName2))
                    {
                        varForNode2 = addedVariables[strVarName2];
                    }
                    else
                    {
                        varForNode2 = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, strVarName2);
                        addedVariables.Add(strVarName2, varForNode2);
                    }
                    model.AddConstr(varForNode1 + varForNode2 >= 1, String.Format("const_{0:D4}_{1:D4}", node1, node2));
                }
            }

            // set objective function
            GRBLinExpr objectiveFunc = 0.0;
            foreach(String key in addedVariables.Keys)
            {
                String strNode = key.Substring("varNode".Length);
                int nodeIndex = Convert.ToInt32(strNode);
                double weight = weights[nodeIndex];
                objectiveFunc.AddTerm(weight, addedVariables[key]);
            }

            model.SetObjective(objectiveFunc, GRB.MINIMIZE);

            // write model to file
            model.Write(strModelFile);

            DateTime dateStart = DateTime.Now;
            // Optimize model
            model.Optimize();
            DateTime dateEnd = DateTime.Now;

            // Results
            String strResult = "";
            strResult += "Time Elapsed(ms) : " + (dateEnd - dateStart).Milliseconds + "\n";
            strResult += "Find models in   : " + strModelFile + "\n";
            strResult += "Find logs in     : " + strLogFile + "\n";
            strResult += "Objective Value  : " + model.ObjVal + "\n";
            strResult += "Decision Variables\n";
            foreach(GRBVar var in  addedVariables.Values)
            {
                strResult += var.VarName + ": " + var.X + "\n";
            }
            model.Dispose();
            env.Dispose();

            return strResult;
        }
    }
}
