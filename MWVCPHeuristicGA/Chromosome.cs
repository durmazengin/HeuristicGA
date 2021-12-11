using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWVCPHeuristicGA
{
    public class Chromosome
    {
        private List<bool> genes = null;
        public Chromosome()
        {
            genes = new List<bool>();
        }
        public void generateRandom(int size, Random rand)
        {
            genes = new List<bool>();
            for (int i = 0; i < size; i++)
            {
                bool decision = rand.NextDouble() > 0.50;
                genes.Add(decision);
            }
        }
        public int getLength()
        {
            return genes.Count;
        }

        public int getGeneAt(int index)
        {
            if(index < genes.Count)
            {
                return genes[index] ? 1 : 0;
            }
            return -1;
        }
        public void setGeneAt(int index, int newGene)
        {
            if (index < genes.Count)
            {
                genes[index] = newGene == 1;
            }
        }

        public List<bool> getAllGenes()
        {
            return genes;
        }

        public void mutate(int index)
        {
            genes[index] = !genes[index];
        }
    }
}
