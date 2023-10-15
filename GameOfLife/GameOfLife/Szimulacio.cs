using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Szimulacio
    {

        private Palya palya;

        public int KorokSzama { get; private set; }

        public int JelenlegiKorSzama { get ; private set; }

        public Szimulacio(Palya palya, int korokSzama)
        {

            this.palya = palya;
            KorokSzama = korokSzama;

        }

        public void SzimulacioInditas()
        {
            if (palya.palya[0,0] == null)
            {
                palya.PalyaElkeszites();
            }

            palya.PalyaMegjelenites();

            Console.WriteLine("Kiinduló állapot");

            string settings = "\nBeállítások:\n\tN - Új pálya indítása\n\tS - Jelenlegi pálya mentése\n\tL - Pálya betöltése";

            Console.WriteLine(settings);

            Mentes mentes = new ();
            ConsoleKeyInfo pressed = Console.ReadKey(true);

            for (JelenlegiKorSzama = 1; JelenlegiKorSzama <= KorokSzama; JelenlegiKorSzama++)
            {
                if (pressed.Key == ConsoleKey.Escape) { break; }
                else if (pressed.Key == ConsoleKey.S)
                {
                    Console.Write("\nBiztosan menti a fent lévő pályát? (Y/N): ");
                    ConsoleKeyInfo confirm = Console.ReadKey();
                    if (confirm.Key == ConsoleKey.Y)
                    {
                        Console.Write("\nMi legyen a fájl neve: ");
                        string fajlnev = Console.ReadLine()!;
                        mentes.MentPalya(palya, fajlnev);
                    }
                }
                else if (pressed.Key == ConsoleKey.L)
                {
                    Console.Write("\nBiztosan be szeretne tölteni új pályát? (Y/N): ");
                    ConsoleKeyInfo confirm = Console.ReadKey();
                    if (confirm.Key == ConsoleKey.Y)
                    {
                        Console.Write("\nMi a fájl neve: ");
                        string fajlnev = Console.ReadLine()!;
                        Console.Write("Adja meg a körök számát: ");
                        KorokSzama = Convert.ToInt32(Console.ReadLine());
                        palya = mentes.BetoltPalya(fajlnev);
                        JelenlegiKorSzama = 0;
                        pressed = new ();
                        continue;
                    }
                }
                else if (pressed.Key == ConsoleKey.N)
                {
                    Console.Write("\nBiztosan új pályát szeretne kezdeni? (Y/N): ");
                    ConsoleKeyInfo confirm = Console.ReadKey();
                    if (confirm.Key == ConsoleKey.Y)
                    {
                        Console.Write("\nAdja meg a körök számát: ");
                        KorokSzama = Convert.ToInt32(Console.ReadLine());
                        palya.PalyaElkeszites();
                        palya.PalyaMegjelenites();
                        Console.WriteLine("Kiinduló állapot");
                        Console.WriteLine(settings);
                        JelenlegiKorSzama = 0;
                        pressed = Console.ReadKey(true);
                        continue;
                    }
                }

                palya.PalyaValtoztatasok();

                palya.PalyaMegjelenites();

                Console.WriteLine($"{JelenlegiKorSzama}. kör lefutott");
                Console.WriteLine(settings);

                pressed = Console.ReadKey(true);
            }
        }
    }
}
