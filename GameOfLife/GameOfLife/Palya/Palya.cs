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

        public int NyulakSzazalek { get; private set; }

        public int RokakSzazalek { get; private set; }

        public Cella[,] palya;

        private readonly Random rnd;

        public Palya(int palyaMeretX, int palyaMeretY)
        {
            PalyaMeretX = palyaMeretX;
            PalyaMeretY = palyaMeretY;
            palya = new Cella[palyaMeretX, palyaMeretY];
            rnd = new Random();
        }

        public void SzazalekBeallitas(int nyulakSzazalek, int rokakSzazalek)
        {
            NyulakSzazalek = nyulakSzazalek;
            RokakSzazalek = rokakSzazalek;
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
                    int rolled = rnd.Next(1, 101);

                    palya[x,y] = new Cella(x,y);

                    FuHozzaadas(x,y);

                    if (rolled <= NyulakSzazalek)
                    {
                        NyulHozzaadas(x,y);
                    } else if (rolled <= NyulakSzazalek + RokakSzazalek)
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
            for (int x = 0; x < PalyaMeretX; x++) { for (int y = 0; y < PalyaMeretY; y++) {
                if (palya[x, y].HasFu()) { FuValtoztatasok(palya[x, y]); } }
            }

            for (int x = 0; x < PalyaMeretX; x++) { for (int y = 0; y < PalyaMeretY; y++) {
                if (palya[x, y].HasNyul()) { NyulValtoztatasok(palya[x, y]); } }
            }

            for (int x = 0; x < PalyaMeretX; x++) { for (int y = 0; y < PalyaMeretY; y++) {
                if (palya[x, y].HasRoka()) { RokaValtoztatasok(palya[x, y]); } }
            }
        }



        public static void FuValtoztatasok(Cella cella)
        {
            if (!cella.HasNyul() && !cella.HasRoka()) { cella.Fu!.NovekedesiAllapotvaltozasNoveles(); }
        }



        public void NyulValtoztatasok(Cella cella)
        {

            /* 1. A nyúl táplálkozik
             * 2. Ha nyúl mellett áll és tele van, akkor szaporodik
             * 3. Ha nem szaporodott, akkor megpróbál elmozdulni
             * 4. Állapot vizsgálat és beállítás
             * 4.5 Szaporodást befolyásoló számláló csökkentése
             * 5. Jóllakotsági szint lemegy
            */

            // 0. lépés
            if (cella.Nyul!.Atlepheto) { cella.Nyul.Atlepheto = false; return; }
            Cella eredetiCella = cella;

            // 1. lépés
            cella.Nyul.Taplalkozas(this, cella);

            // 2. lépés
            cella.Nyul.Szaporodas(this, cella);

            // 3. lépés
            cella = cella.Nyul.Mozgas(this, cella);

            // 4. lépés
            cella.Nyul!.AllapotVizsgalat(cella, eredetiCella);

            // 5. lépés
            cella.Nyul.JollakottsagiSzintCsokkentese(cella);

        }



        public void RokaValtoztatasok(Cella cella)
        {

            /* 1. A róka megpróbál táplálkozni
             * 2. Ha nem táplálkozott, elmozdul
             * 3. Ha róka mellett áll és nem éhes, akkor szaporodik
             * 4. Ha nem szaporodott, akkor megpróbál elmozdulni
             * 5. Állapot vizsgálat és beállítás
             * 5.5 Szaporodást befolyásoló számláló csökkentése 
             * 6. Jóllakotsági szint lemegy
            */

            // 0. lépés
            if (cella.Roka!.Atlepheto) { cella.Roka.Atlepheto = false; return; }
            Cella eredetiCella = cella;

            // 1. lépés
            cella = cella.Roka.Taplalkozas(this, cella);

            // 2. lépés
            cella = cella.Roka!.Mozgas(this, cella);

            // 3. lépés
            cella.Roka!.Szaporodas(this, cella);

            // 4. lépés
            cella = cella.Roka!.Mozgas(this, cella);

            // 5. lépés
            cella.Roka!.AllapotVizsgalat(cella, eredetiCella);

            // 6. lépés
            cella.Roka!.JollakottsagiSzintCsokkentese(cella);
        }
    }
}
