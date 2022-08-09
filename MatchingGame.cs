using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class MatchingGame
    {
        public const short N = 4;
        public const short CommonCount = 100;
        public const short RareCount = 10;

        private readonly Item dummyItem;
        private List<Item> Common = new List<Item>(CommonCount);
        private List<Item> Rare = new List<Item>(RareCount);
        private Item Epic = new Item(CommonCount + RareCount, 1.0, Kind.Epic);
        private Random generator;


        Item[,] MatrixNxN;
        public MatchingGame()
        {
            MatrixNxN = new Item[N, N];
            dummyItem = new Item(-1, 0.0, Kind.Epic);
            for(short i = 0; i < N; i++)
            {
                for (short j = 0; j < N; j++)
                {
                    MatrixNxN[i, j] = dummyItem;
                }
            }
            generator = new Random();
        }

        public void Setup()
        {
            short i;
            double weight = 0.550;
            for (i = 0; i < CommonCount; i++)
            {
                Common.Add(new Item(i, weight, Kind.Common));
                weight -= 0.005;
            }
            weight = 0.55;
            for (i = 0; i < RareCount; i++)
            {
                Rare.Add(new Item(i, weight, Kind.Rare));
                weight -= 0.05;
            }
            ArrangeRandomlyWithWeights(Common, 4);
            ArrangeRandomlyWithWeights(Rare, 3);
            PlaceLast(Epic);
        }

        public void Display()
        {
            for (short i = 0; i < N; i++)
            {
                for (short j = 0; j < N; j++)
                {
                    Console.Write(MatrixNxN[i, j].Serialize());
                }
                Console.WriteLine();
            }
        }

        private void ArrangeRandomlyWithWeights(List<Item> items, int howManyToArrage)
        {
            for (short i = 0; i < howManyToArrage; i++)
            {
                Item item = PickRandomlyUsingWeights(items);
                PlaceRandomlyOnMatrix(item);
                PlaceRandomlyOnMatrix(item); // place same item twice
            }
        }

        // simplistic implementation of weights:
        // pick at random two items
        // out of the two, pick the one with higher weight.
        private Item PickRandomlyUsingWeights(List<Item> items)
        {
            var i1 = generator.Next(0, items.Count);
            var i2 = generator.Next(0, items.Count);
            var item1 = items[i1];
            var item2 = items[i2];
            Item picked = (item1.Weight > item2.Weight) ? item1 : item2;
            items.Remove(picked);
            return picked;
        }

        private void PlaceRandomlyOnMatrix(Item item)
        {
            short max = N * N;
            bool placed = false;

            while (!placed)
            {
                int i = generator.Next(0, max);
                var x = MatrixNxN[i / N, i % N];
                placed = (x.Id == -1);
                if (placed)
                {
                    MatrixNxN[i / N, i % N] = item;
                }
            }
        }

        private void PlaceLast(Item item)
        {
            // search for two empty locations
            var i = SearchForOne();
            if (i == -1)
            {
                throw new ArgumentException("There should be two more empty slots in matrix");
            }
            MatrixNxN[i / N, i % N] = item;
            i = SearchForOne();
            if (i == -1)
            {
                throw new ArgumentException("There should be one more empty slot in matrix");
            }
            MatrixNxN[i / N, i % N] = item;
        }

        private short SearchForOne()
        {
            for (short i = 0; i < N; i++)
            {
                for (short j = 0; j < N; j++)
                {
                    if (MatrixNxN[i, j].Id == -1)
                    {
                        return (short)(i * N + j);
                    }
                }
            }
            return (short)-1;
        }
    }
}
