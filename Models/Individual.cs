using System.Linq;
using System;
using System.Collections.Generic;
namespace GeneticAlgorithm.Models
{
    public class Individual
    {
        public string Name { get; set; }
        public List<byte> Chromosome { get; set; } = new List<byte>();
        private readonly Random Random = new Random();
        public Func<List<byte>, double> FitnessExpression { get; set; }
        public double Fitness
        {
            get
            {
                double fitness = FitnessExpression(this.Chromosome);
                if (fitness < 0)
                {
                    throw new Exception($"Fitness {fitness} < 0.");
                }
                return fitness;
            }
        }

        public Individual(Func<List<byte>, double> fitnessExpression, int chromosomeSize = 12)
        {
            this.FitnessExpression = fitnessExpression;
            int i;
            for (i = 0; i < chromosomeSize; i++)
            {
                var randomGene = (byte)this.Random.Next(0, (int)Math.Pow(2, 8));
                this.Chromosome.Add(randomGene);
            }
        }

        public (Individual, Individual) Crossover(Individual otherIndividual)
        {
            // find a point to crossover
            int pointIndex = this.Random.Next(0, this.Chromosome.Count);

            // crossover
            var thisLeftSubChromosome = this.Chromosome.Take(pointIndex).ToList();
            var thisRightSubChromosome = this.Chromosome.Skip(pointIndex).ToList();
            var otherLeftSubChromosome = otherIndividual.Chromosome.Take(pointIndex).ToList();
            var otherRightSubChromosome = otherIndividual.Chromosome.Skip(pointIndex).ToList();

            var child1Chromosome = thisLeftSubChromosome.Concat(otherRightSubChromosome).ToList();
            Individual child1 = new Individual(this.FitnessExpression, child1Chromosome.Count)
            {
                Chromosome = child1Chromosome
            };
            var child2Chromosome = otherLeftSubChromosome.Concat(thisRightSubChromosome).ToList();
            Individual child2 = new Individual(this.FitnessExpression, child2Chromosome.Count)
            {
                Chromosome = child2Chromosome
            };

            return (child1, child2);

            // if (this.Random.Next(0, 2) == 0)
            // {
            //     this.Chromosome = thisLeftSubChromosome.Concat(otherRightSubChromosome).ToList();
            //     otherIndividual.Chromosome = otherLeftSubChromosome.Concat(thisRightSubChromosome).ToList();
            // }
            // else
            // {
            //     otherIndividual.Chromosome = thisLeftSubChromosome.Concat(otherRightSubChromosome).ToList();
            //     this.Chromosome = otherLeftSubChromosome.Concat(thisRightSubChromosome).ToList();
            // }
        }

        public void Mutate()
        {
            // int mutationIndex = this.Random.Next(0, this.Chromosome.Count);
            // // this.Chromosome[mutationIndex] = (this.Chromosome[mutationIndex] + 1) % 2;
            // this.Chromosome[mutationIndex] = (byte)((this.Chromosome[mutationIndex] + 1) % 2);

            int listOffset = this.Random.Next(0, this.Chromosome.Count);
            int byteOffset = this.Random.Next(0, 8);
            byte selectedByte = this.Chromosome[listOffset];
            byte mutatedByte = (byte)(selectedByte ^ ((byte)1 << byteOffset));
            this.Chromosome[listOffset] = mutatedByte;
        }
    }
}
