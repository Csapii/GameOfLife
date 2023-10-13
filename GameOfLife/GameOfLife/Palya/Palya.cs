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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("R ");
                    }
                    else if (palya[x, y].HasNyul())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("N ");
                    }
                    else if (palya[x, y].HasFu())
                    {
                        Fu fu = palya[x, y].Fu!;
                        Console.ForegroundColor = ConsoleColor.Green;
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
            Console.ForegroundColor = ConsoleColor.White;
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
             * 2. Ha nyúl mellett áll és tele van, akkor szaporodik
             * 3. Ha nem szaporodott, akkor megpróbál elmozdulni
             * 4. Tápérték lemegy
            */

            // 0. lépés

            if (cella.Nyul!.MostSzuletett) { cella.Nyul.MostSzuletett = false; return; }

            // 1. lépés

            cella.Nyul.Taplalkozas(this, cella);

            // 2. lépés

            List<Cella> szaporodas;

            if (!cella.Nyul.MostSzaporodott && cella.Nyul.JollakottsagiSzint > 4)
            {
                szaporodas = cella.Nyul.Szaporodas(this, cella);
            } else { cella.Nyul.MostSzaporodott = false; szaporodas = new List<Cella>(); }


            if (szaporodas.Count != 0)
            {
                palya[szaporodas[0].X, szaporodas[0].Y] = szaporodas[0];
                palya[szaporodas[1].X, szaporodas[1].Y] = szaporodas[1];
                return;
            }

            // 3. lépés

            Cella lepettCella = cella.Nyul.Mozgas(this, cella);

            if (lepettCella != null)
            {
                lepettCella.SetNyul(palya[cella.X, cella.Y].Nyul!);
                palya[cella.X, cella.Y].RemoveNyul();
                cella = lepettCella;
            }

            // 4. lépés

            if (!cella.Nyul!.JollakottsagiSzintCsokkentese())
            {
                cella.RemoveNyul();
            }
        }



        public void RokaValtoztatasok(Cella cella)
        {

            /* 1. A róka megpróbál táplálkozni
             * 2. Ha nem táplálkozott, elmozdul
             * 3. Ha róka mellett áll és nem éhes, akkor szaporodik
             * 4. Ha nem szaporodott, akkor megpróbál elmozdulni
             * 5. Tápérték lemegy
            */

            // 0. lépés

            if (cella.Roka!.MostSzuletett) { cella.Roka.MostSzuletett = false; return; }

            // 1. lépés

            Cella preda = cella.Roka.Taplalkozas(this, cella);
            Cella lepettCella;

            if (preda.X != -999)
            {
                palya[preda.X, preda.Y].RemoveNyul();
                palya[preda.X, preda.Y].SetRoka(cella.Roka);
                cella.RemoveRoka();
                cella = preda;

            } else // 2. lépés
            {
                lepettCella = cella.Roka.Mozgas(this, cella);
                if (lepettCella != null)
                {
                    lepettCella.SetRoka(palya[cella.X, cella.Y].Roka!);
                    palya[cella.X, cella.Y].RemoveRoka();
                    cella = lepettCella;
                }
            }

            // 3. lépés

            List<Cella> szaporodas;

            if (!cella.Roka!.MostSzaporodott && cella.Roka.JollakottsagiSzint > 7)
            {
                szaporodas = cella.Roka.Szaporodas(this, cella);
            }
            else { cella.Roka.MostSzaporodott = false; szaporodas = new List<Cella>(); }


            if (szaporodas.Count != 0)
            {
                palya[szaporodas[0].X, szaporodas[0].Y] = szaporodas[0];
                palya[szaporodas[1].X, szaporodas[1].Y] = szaporodas[1];
                return;
            }

            // 4. lépés

            lepettCella = cella.Roka.Mozgas(this, cella);

            if (lepettCella != null)
            {
                lepettCella.SetRoka(palya[cella.X, cella.Y].Roka!);
                palya[cella.X, cella.Y].RemoveRoka();
                cella = lepettCella;
            }

            // 5. lépés

            if (!cella.Roka!.JollakottsagiSzintCsokkentese())
            {
                cella.RemoveRoka();
            }
        }
    }
}
