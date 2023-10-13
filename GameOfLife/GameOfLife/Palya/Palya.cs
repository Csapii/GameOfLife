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

        public int PalyaMeretX { get; init; }

        public int PalyaMeretY { get; init; }

        public Cella[,] palya;

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

                    palya[x,y] = new Cella(x,y);

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
                        Fu fu = palya[x, y].Fu!;

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
                        FuValtoztatasok(palya[x, y]);
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
            if (!cella.HasNyul() && !cella.HasRoka()) { cella.Fu!.NovekedesiAllapotvaltozasNoveles(); }
        }



        public void NyulValtoztatasok(Cella cella)
        {

            /* 1. A nyúl táplálkozik
             * 2. Ha nyúl mellett áll és tele van, akkor szaparodik
             * 3. Ha nem szaparodott, akkor elmozdul
             * 4. Tápérték lemegy
            */

            if (cella.Nyul!.MostSzuletett) { cella.Nyul.MostSzuletett = false; return; }

            cella.Nyul.Taplalkozas(cella);

            List<Cella> szaporodas;

            if (!cella.Nyul.MostSzaporodott && cella.Nyul.JollakottsagiSzint > 4)
            {
                szaporodas = cella.Nyul.Szaporodas(this, cella);
            } else { cella.Nyul.MostSzaporodott = false; szaporodas = new List<Cella>(); }

            if (szaporodas.Count != 0)
            {
                palya[szaporodas[0].X, szaporodas[0].Y] = szaporodas[0];
                palya[szaporodas[1].X, szaporodas[1].Y] = szaporodas[1];
            } else
            {
                cella.Nyul.Mozgas();
            }

            if (cella.Nyul.JollakottsagiSzintCsokkentese())
            {
                cella.RemoveNyul();
            }
        }



        public void RokaValtoztatasok(Cella cella)
        {
            if (cella.Roka!.JollakottsagiSzintCsokkentese())
            {
                cella.RemoveRoka();
            }
        }
    }
}
