using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MWVCPHeuristicGA
{
    public class GAHeuristicSolver
    {
        private const int TOURNAMENT_SIZE = 2;
        private int numberOfGenerations = 0;
        private int populationSize = 0;
        public GAHeuristicSolver(int numberOfGenerations, int populationSize)
        {
            this.numberOfGenerations = numberOfGenerations;
            this.populationSize = populationSize;
        }

        public string solve(String fileName, double crossverProbability, double probabilityMutation)
        {
            String strLog = "";

            String fileContent = File.ReadAllText(fileName);
            ProblemModel problemModel = loadProblemModel(fileContent);
            strLog += problemModel.printObjective() + "\n";
            strLog += problemModel.printConstrains() + "\n";
            strLog += "All variables are binary\n";

            int geneSize = problemModel.getVariableCount();
            if (probabilityMutation == 0)
            {
                probabilityMutation = (double)1 / geneSize;
            }

            String averageFileName = fileName.Split('.')[0];
            averageFileName = String.Format("avg_{0}_{1:D4}_{2:D4}_{3:D4}.txt", averageFileName,
                (int)Math.Round(probabilityMutation * 1000), numberOfGenerations, populationSize);

            String bestFileName = averageFileName.Replace("avg_", "best_");

            // Initialize generation
            List<Chromosome> population = new List<Chromosome>();
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < populationSize; i++)
            {
                Chromosome chromosome = new Chromosome();
                chromosome.generateRandom(geneSize, rand);
                population.Add(chromosome);
            }

            population = repair(population, problemModel); // check and repair infeasible solutions

            List<double> averageObjectivePerGeneration = new List<double>();
            averageObjectivePerGeneration.Add(calculateAverageObjective(problemModel, population));

            List<double> bestObjectivePerGeneration = new List<double>();
            bestObjectivePerGeneration.Add(calculateBestObjective(problemModel, population));

            // now produce new generations until reaching number of generations decided by user
            for (int i = 0; i < numberOfGenerations; i++)
            {
                List<Chromosome> matingPool = selectMatingPool(population, TOURNAMENT_SIZE, rand, problemModel);
                List<Chromosome> exchangedChromosomes = crossover(matingPool, crossverProbability, rand);
                List<Chromosome> mutatedGeneration = mutate(exchangedChromosomes, probabilityMutation, rand);
                List<Chromosome> newGeneration = repair(mutatedGeneration, problemModel); // check and repair infeasible solutions

#if !reject_worser
                averageObjectivePerGeneration.Add(calculateAverageObjective(problemModel, newGeneration));
                bestObjectivePerGeneration.Add(calculateBestObjective(problemModel, population));
                population = newGeneration;
#else
                bool accept = true;
                double average = calculateAverageObjective(problemModel, newGeneration);
                double averagePrevious = averageObjectivePerGeneration[averageObjectivePerGeneration.Count - 1];
                if (averagePrevious < 0.75 * average)
                {
                    accept = false;
                }
                else if (averagePrevious < average)
                {
                    if(rand.NextDouble() < 0.5)
                    {
                        accept = false;
                    }
                }
                if (accept)
                {
                    population = newGeneration;
                    averageObjectivePerGeneration.Add(average);
                }
                else
                {
                    i--;
                }
#endif
            }

            String strAverageObjectives = "";
            for (int i = 0; i < averageObjectivePerGeneration.Count; i++)
            {
                strAverageObjectives += averageObjectivePerGeneration[i] + "\n";
            }

            System.IO.File.WriteAllText(averageFileName, strAverageObjectives);

            String strBestObjectives = "";
            for (int i = 0; i < bestObjectivePerGeneration.Count; i++)
            {
                strBestObjectives += bestObjectivePerGeneration[i] + "\n";
            }
            System.IO.File.WriteAllText(bestFileName, strBestObjectives);

            strLog = strLog + "Average Objective Values Per Iteration: \n";
            strLog = strLog + "\n" + strAverageObjectives;

            return strLog;
        }
        private List<Chromosome> selectMatingPool(List<Chromosome> population, int tournamentSize, Random rand, ProblemModel model)
        {
            List<Chromosome> matingPool = new List<Chromosome>();
            for(int i = 0; i < population.Count; i++)
            {
                /*
                 *  Tournament selection
                 *  randomly choose N chromoromes
                 *  put chromosome having best objective value into mating pool
                 */
                Chromosome bestChromosome = null;
                for (int t = 0; t < tournamentSize; t++)
                {
                    int index = rand.Next(0, population.Count - 1);
                    Chromosome random = population[index];
                    bestChromosome = model.selectBetterObjective(random, bestChromosome);
                }
                matingPool.Add(bestChromosome);
            }
            return matingPool;
        }

        private List<Chromosome> crossover(List<Chromosome> chromosomes, double probabilityCrossover, Random rand)
        {
            List<Chromosome> crossover = new List<Chromosome>();
            int pairs = chromosomes.Count / 2;
            for (int i = 0; i < pairs; i++)
            {
                Chromosome partner1 = chromosomes[2 * i];
                Chromosome partner2 = chromosomes[2 * i + 1];
                int geneLength = partner1.getLength();
                for (int p = 0; p < geneLength; p++)
                {
                    if(rand.NextDouble() < probabilityCrossover)
                    {
                        int genePartner1 = partner1.getGeneAt(p);
                        int genePartner2 = partner2.getGeneAt(p);
                        if (genePartner1 != genePartner2) // otherwise it is not required
                        {
                            partner1.setGeneAt(p, genePartner2);
                            partner2.setGeneAt(p, genePartner1);
                        }

                    }
                }
                crossover.Add(partner1);
                crossover.Add(partner2);
            }
            return crossover;
        }
        private List<Chromosome> mutate(List<Chromosome> chromosomes, double probabilityMutation, Random rand)
        {
            List<Chromosome> mutatedChromosomes = new List<Chromosome>();
            foreach(Chromosome chromosome in chromosomes)
            {
                for(int i = 0; i < chromosome.getLength(); i++)
                {
                    double randValue = rand.NextDouble();
                    if(randValue < probabilityMutation)
                    {
                        chromosome.mutate(i);
                    }
                }
                mutatedChromosomes.Add(chromosome);
            }
            return mutatedChromosomes;
        }
        private List<Chromosome> repair(List<Chromosome> rawSolutions, ProblemModel model)
        {
            List<Chromosome> feasibleSolutions = new List<Chromosome>();
            foreach (Chromosome chromosome in rawSolutions)
            {
                Chromosome chromosomeFixed = model.repair(chromosome);
                feasibleSolutions.Add(chromosomeFixed);
            }
            return feasibleSolutions;
        }

        private double calculateAverageObjective(ProblemModel problemModel, List<Chromosome> population)
        {
            double objectiveTotals = 0;
            for (int i = 0; i < population.Count; i++)
            {
                double objective = problemModel.calculateObjectiveVal(population[i]);
                objectiveTotals += objective;
            }
            double average = Math.Round(objectiveTotals / population.Count, 2);

            return average;
        }

        private double calculateBestObjective(ProblemModel problemModel, List<Chromosome> population)
        {
            double best = Double.MaxValue;
            for (int i = 0; i < population.Count; i++)
            {
                double objective = problemModel.calculateObjectiveVal(population[i]);
                if (objective < best)
                {
                    objective = Math.Round(objective, 2);
                    best = objective;
                }
            }
            return best;
        }


        private ProblemModel loadProblemModel(String fileContent)
        {
            ProblemModel problemModel = new ProblemModel();
            String[] lines = fileContent.Split('\n');
            int lineIndex = 0;

            int numOfNodes = Convert.ToInt32(lines[lineIndex++]); // first line is number of nodes
            int numOfEdges = Convert.ToInt32(lines[lineIndex++]); // second line is number of nodes
            problemModel.setNumberOfNodes(numOfNodes);
            problemModel.setNumberOfEdges(numOfEdges);

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
            problemModel.setNodeWeigths(weights);
            
            /*
                add constraints to GA Model
                */
            /*
             * following loop adds decision variables and constraints
             */
            for (int i = 0; i < numOfEdges; i++)
            {
                String edgeLine = lines[lineIndex++];
                String[] array = edgeLine.Split(' ');

                int node1 = Convert.ToInt32(array[0]);
                int node2 = Convert.ToInt32(array[1]);
                if (node1 != node2)// ignore if some error which draws edge itself
                {
                    // create or use existing decision variable for node 1
                    problemModel.addVariable(node1); // it checks whether variable added before
                    problemModel.addVariable(node2); // it checks whether variable added before

                    // create or use existing decision variable for node 2
                    ConstraintBE constraint = new ConstraintBE();
                    constraint.addVariableIndex(node1);
                    constraint.addVariableIndex(node2);
                    constraint.setMinimum(1);
                    problemModel.addConstraint(constraint);
                }
            }

            return problemModel;
        }

        private void printObjectivesValues(int generation, List<Chromosome> newGeneration, ProblemModel model)
        {
            String strLog = "\n";
            strLog += "Objective Values of " + generation + ": ";
            for (int i = 0; i < newGeneration.Count; i++)
            {
                strLog += model.calculateObjectiveVal(newGeneration[i]) + ", ";
            }
            strLog += "\n";
            Console.Write(strLog);
        }

        /* functions after this point are only to see some detail results, they can be removed */

        private void printPopulation(String strLabel, List<Chromosome> population)
        {
            String strGenes = "\nGenes at " + strLabel + "\n";
            for (int i = 0; i < population.Count; i++)
            {
                Chromosome chromosome = population[i];
                for (int j = 0; j < chromosome.getLength(); j++)
                {
                    strGenes += chromosome.getGeneAt(j);
                }
                strGenes += "\r\n";
            }
            Console.Write(strGenes);
        }

        private List<int> getChromosomeOnesCounts(List<Chromosome> population)
        {
            List<int> chromosomeOnes = new List<int>();
            for (int p = 0; p < population.Count; p++)
            {
                Chromosome chromosome = population[p];
                int counter = 0;
                for (int c = 0; c < chromosome.getLength(); c++)
                {
                    counter += chromosome.getGeneAt(c);
                }
                chromosomeOnes.Add(counter);
            }
            return chromosomeOnes;
        }
    }
}
