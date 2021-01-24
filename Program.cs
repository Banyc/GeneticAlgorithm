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
            Console.WriteLine("Hello Genetic Algorithm!");

            Population population = new Population(10, FitnessExpression, 16);

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
            while (population.Generation < 100);
        }

        // the fitness evaluation logic
        private static double FitnessExpression(List<byte> chromosome)
        {
            // decode: bytes -> int16
            List<int> listInt16 = new List<int>();

            int i;
            for (i = 0; i + 2 <= chromosome.Count; i += 2)
            {
                byte[] int16Bytes = chromosome.Skip(i).Take(2).ToArray();
                var int16 = BitConverter.ToUInt16(int16Bytes);
                listInt16.Add(int16);
            }

            // write down your standard of evaluation
            int score = listInt16[0];

            return score;
        }
    }
}
