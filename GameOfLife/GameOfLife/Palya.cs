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

        private readonly Cella[,] palya;

        private readonly Random rnd;

        public Palya(int palyaMeretX, int palyaMeretY)
        {
            PalyaMeretX = palyaMeretX;
            PalyaMeretY = palyaMeretY;
            palya = new Cella[palyaMeretX, palyaMeretY];
            rnd = new Random();
        }



        public void FuHozzaadas(int x, int y)
        {
            palya[x, y].SetFu();
        }

        public void NyulHozzaadas(int x, int y)
        {
            palya[x, y].SetNyul();
        }

        public void RokaHozzaadas(int x, int y)
        {
            palya[x, y].SetRoka();
        }



        public void PalyaElkeszites()
        {
            for (int x = 0; x < PalyaMeretX; x++)
            {
                for (int y = 0; y < PalyaMeretY; y++)
                {
                    int rolled = rnd.Next(0, 5);

                    palya[x,y] = new Cella();

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
                    if (palya[x, y].HasRoka())
                    {
                        Console.Write("R ");
                    }
                    else if (palya[x, y].HasNyul())
                    {
                        Console.Write("N ");
                    }
                    else if (palya[x, y].HasFu())
                    {
                        Fu fu = palya[x, y].Fu;

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
                    if (palya[x, y].HasFu())
                    {
                        FuValtoztatasok(palya[x,y]);
                    }

                    if (palya[x, y].HasNyul())
                    {
                        NyulValtoztatasok(palya[x, y]);
                    }

                    if (palya[x, y].HasRoka())
                    {
                        RokaValtoztatasok(palya[x, y]);
                    }

                }
            }
        }



        public void FuValtoztatasok(Cella cella)
        {
            if (!cella.HasNyul() && !cella.HasRoka()) { cella.Fu.NovekedesiAllapotvaltozasNoveles(); }
        }

        public void NyulValtoztatasok(Cella cella)
        {
            if (cella.Nyul.JollakottsagiSzintCsokkentese())
            {
                // action
            } else {
                cella.RemoveNyul();
            }

        }

        public void RokaValtoztatasok(Cella cella)
        {
            if (cella.Roka.JollakottsagiSzintCsokkentese())
            {
                // action
            } else {
                cella.RemoveRoka();
            }
        }



    }
}
