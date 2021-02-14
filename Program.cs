using System.Text;
using System;
using System.Collections.Generic;
using GeneticAlgorithm.Models;
using System.Linq;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            const int generations = 100;

            Population population = new(
                individualCount: 10,
                FitnessExpression,
                chromosomeSize: 16
            );

            Console.WriteLine("Hello Genetic Algorithm!");

            do
            {
                Console.Write($"[{population.Generation}] \t ");

                foreach (var individual in population.Individuals)
                {
                    Console.Write($"{individual.Fitness}, ");
                }
                Console.WriteLine();

                population.NextGeneration();
            }
            while (population.Generation < generations);
        }

        // the fitness evaluation logic
        private static double FitnessExpression(List<byte> chromosome)
        {
            // decode: bytes -> int16
            List<int> listInt16 = new();

            int i;
            for (i = 0; i + 2 <= chromosome.Count; i += 2)
            {
                byte[] int16Bytes = chromosome.Skip(i).Take(2).ToArray();
                var int16 = BitConverter.ToUInt16(int16Bytes);
                listInt16.Add(int16);
            }

            // replace it with your evaluation
            int score = listInt16[0];

            return score;
        }
    }
}
