using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWVCPHeuristicGA
{
    /*
     * This calass for constrains for bigger than or equal to
     *   i.e x0 + x2 >= 3
     *   then contrains is prepared as
     *   addVariableIndex(0);
     *   addVariableIndex(2);
     *   setMinimum(3)
     */
    public class ConstraintBE
    {
        public List<int> variableIndices = null;
        public int minimumVal = 0;
        
        public ConstraintBE()
        {
            variableIndices = new List<int>();
        }
        public void addVariableIndex(int index)
        {
            variableIndices.Add(index);
        }
        public void setMinimum(int minimum)
        {
            minimumVal = minimum;
        }
        public String printConstraint()
        {
            String strConstraint = "";
            String constraintName = "const";
            foreach (int variableIndex in variableIndices)
            {
                constraintName += String.Format("_{0:D4}", variableIndex);
                strConstraint += String.Format("varNode{0:D4}", variableIndex) + " + ";
            }
            strConstraint = strConstraint.Substring(0, strConstraint.Length - 3); // to remove last "+ "
            strConstraint = constraintName + " : " + strConstraint + " >= " + minimumVal;
            return strConstraint;
        }
        public List<int> getVariableIndices()
        {
            return variableIndices;
        }
    }
}
