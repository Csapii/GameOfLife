using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Palya
    {

        private int PalyaMeretX { get; init; }

        private int PalyaMeretY { get; init; }

        private readonly List<object>[,] palya;

        private readonly Random rnd;

        public Palya(int palyaMeretX, int palyaMeretY)
        {
            PalyaMeretX = palyaMeretX;
            PalyaMeretY = palyaMeretY;
            palya = new List<object>[palyaMeretX, palyaMeretY];
            rnd = new Random();
        }

        public void FuHozzaadas(int x, int y)
        {
            palya[x, y].Add(new Fu());
        }

        public void NyulHozzaadas(int x, int y)
        {
            palya[x, y].Add(new Nyul());
        }

        public void RokaHozzaadas(int x, int y)
        {
            palya[x, y].Add(new Roka());
        }

        public void PalyaElkeszites()
        {
            for (int x = 0; x < PalyaMeretX; x++)
            {
                for (int y = 0; y < PalyaMeretY; y++)
                {
                    int rolled = rnd.Next(0, 5);

                    palya[x,y] = new List<object>();

                    FuHozzaadas(x,y);

                    if (rolled == 0)
                    {
                        NyulHozzaadas(x,y);
                    } else if (rolled == 1)
                    {
                        RokaHozzaadas(x,y);
                    }

                }
            }
        }

        public void PalyaMegjelenites()
        {
            Console.Clear();

            for (int x = 0; x < PalyaMeretX; x++)
            {
                Console.WriteLine();
                for (int y = 0; y < PalyaMeretY; y++)
                {
                    if (palya[x, y].Exists(x => x is Roka))
                    {
                        Console.Write("R ");
                    }
                    else if (palya[x, y].Exists(x => x is Nyul))
                    {
                        Console.Write("N ");
                    }
                    else if (palya[x, y].Exists(x => x is Fu))
                    {
                        Fu fu = (Fu) palya[x, y].First(x => x is Fu);

                        if (fu.Tapertek == 0)
                        {
                            Console.Write(". ");
                        } else if (fu.Tapertek == 1)
                        {
                            Console.Write(", ");
                        } else
                        {
                            Console.Write("; ");
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }

            Console.WriteLine("\n");
        }

        public void PalyaValtoztatasok()
        {
            for (int x = 0; x < PalyaMeretX; x++)
            {
                for (int y = 0; y < PalyaMeretY; y++)
                {
                    if (palya[x, y].Exists(x => x is Fu))
                    {
                        FuValtoztatasok(x, y);
                    }

                    if (palya[x, y].Exists(x => x is Nyul))
                    {
                        NyulValtoztatasok(x, y);
                    }

                    if (palya[x, y].Exists(x => x is Roka))
                    {
                        RokaValtoztatasok(x, y);
                    }

                }
            }
        }

        public void FuValtoztatasok(int x, int y)
        {
            Fu fu = (Fu)palya[x, y].First(x => x is Fu);
            fu.NovekedesiAllapotvaltozasNoveles();
        }

        public void NyulValtoztatasok(int x, int y)
        {
            // Nyúl változtatások
        }

        public void RokaValtoztatasok(int x, int y)
        {
            // Róka változtatások
        }
    }
}
