using System.Linq;
using System.Collections.Generic;
using System;

namespace GeneticAlgorithm.Models
{
    public class Population
    {
        private readonly Random Random = new Random();
        public List<Individual> Individuals { get; set; } = new List<Individual>();
        public int Generation { get; set; } = 0;

        public Population(int individualCount, Func<List<byte>, double> fitnessExpression, int chromosomeSize)
        {
            int i;
            for (i = 0; i < individualCount; i++)
            {
                Individual newIndividual = new Individual(fitnessExpression, chromosomeSize);
                this.Individuals.Add(newIndividual);
            }
        }

        public void NextGeneration()
        {
            double sumFitness = 0;
            this.Individuals = this.Individuals.OrderByDescending(xxxx => xxxx.Fitness).ToList();
            foreach (var individual in this.Individuals)
            {
                sumFitness += individual.Fitness;
            }

            List<Individual> children = new List<Individual>();
            int i;
            for (i = 0; i < this.Individuals.Count / 4; i++)
            {
                // select parents
                // Stochastic Universal Selection method
                var firstParent = SelectParent(sumFitness);
                var secondParent = SelectParent(sumFitness);

                var firstChild = (Individual)firstParent.Clone();
                var secondChild = (Individual)secondParent.Clone();

                // crossover
                while (this.Random.NextDouble() < 0.5f)
                {
                    (firstChild, secondChild) = firstChild.Crossover(secondChild);
                }
                // mutate children
                while (this.Random.NextDouble() < 0.5f)
                {
                    firstChild.Mutate();
                }
                while (this.Random.NextDouble() < 0.5f)
                {
                    secondChild.Mutate();
                }

                // save new children
                children.Add(firstChild);
                children.Add(secondChild);
            }
            // kill dump asses
            this.Individuals = this.Individuals.Take(this.Individuals.Count / 2).ToList();
            // add new children
            this.Individuals = this.Individuals.Concat(children).ToList();
            // generation counter
            this.Generation++;
        }

        private Individual SelectParent(double sumFitness)
        {
            double firstFixedPoint = this.Random.NextDouble() % sumFitness;
            double firstSumSoFar = 0;
            Individual firstParent = null;
            foreach (var individual in this.Individuals)
            {
                firstSumSoFar += individual.Fitness;
                if (firstSumSoFar > firstFixedPoint)
                {
                    firstParent = individual;
                    break;
                }
            }
            return firstParent;
        }
    }
}
