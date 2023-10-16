using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Szimulacio
    {

        private Palya palya;

        public int KorokSzama { get; private set; }

        public int JelenlegiKorSzama { get; private set; }

        public Szimulacio(Palya palya, int korokSzama)
        {

            this.palya = palya;
            KorokSzama = korokSzama;

        }

        public void SzimulacioInditas()
        {
            if (palya.palya[0, 0] == null)
            {
                palya.PalyaElkeszites();
            }

            palya.PalyaMegjelenites();

            Console.WriteLine("Kiinduló állapot");

            string settings = "\nBeállítások:\n\tS - Jelenlegi pálya mentése\n\tL - Pálya betöltése\n\tN - Új pálya indítása";

            Console.WriteLine(settings);

            Mentes mentes = new();
            ConsoleKeyInfo pressed = Console.ReadKey(true);

            for (JelenlegiKorSzama = 1; JelenlegiKorSzama <= KorokSzama; JelenlegiKorSzama++)
            {
                if (pressed.Key == ConsoleKey.Escape) { break; }
                else if (pressed.Key == ConsoleKey.S)
                {
                    JelenlegiPalyaMentes(mentes);
                }
                else if (pressed.Key == ConsoleKey.L)
                {
                    if (PalyaBetoltes(mentes)) { pressed = new(); continue; }
                }
                else if (pressed.Key == ConsoleKey.N)
                {
                    if (UjPalyaKezdes(settings)) { pressed = Console.ReadKey(true); continue; }
                }

                palya.PalyaValtoztatasok();

                palya.PalyaMegjelenites();

                Console.WriteLine($"{JelenlegiKorSzama}. kör lefutott");
                Console.WriteLine(settings);

                pressed = Console.ReadKey(true);
            }
        }

        private void JelenlegiPalyaMentes(Mentes mentes)
        {
            Console.Write("\nBiztosan menti a fent lévő pályát? (Y/N): ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                // Mappa tartalmának lekérése
                DirectoryInfo mentesiFajlok = new("./");
                List<FileInfo> fajlok = mentesiFajlok.GetFiles().Where(fajl => fajl.Name.EndsWith(".txt")).ToList();

                // Fájlok kiíratása
                Console.WriteLine("\nLegutóbbi mentések: ");
                int fajlIndex = 1;
                int counter = 0;
                foreach (FileInfo fajl in fajlok)
                {
                    Console.WriteLine($"{fajlIndex} - {fajl.Name}");
                    fajlIndex++;
                    counter++;
                }
                Console.Write("\n");

                Console.Write("\nMi legyen a fájl neve: ");
                string fajlnev = Console.ReadLine()!;
                if (fajlnev.Length > 0)
                {
                    if (fajlok.Exists(x => x.Name == fajlnev))
                    {
                        Console.Write("Már van ilyen nevű mentésed, biztosan felül szeretnéd írni ? Y/N : ");
                        ConsoleKeyInfo dontes = Console.ReadKey();

                        if (dontes.Key == ConsoleKey.Y)
                        {
                            mentes.MentPalya(palya, fajlnev);
                            Console.WriteLine("\nSikeresen felülírtad a fájlt !");
                            Console.WriteLine("\nNyomjon meg egy gombot a folytatáshoz!");
                            _ = Console.ReadKey();
                        }
                    }
                    else
                    {
                        mentes.MentPalya(palya, fajlnev);
                    }
                }
                else
                {
                    fajlnev = DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Second + ".txt";
                    mentes.MentPalya(palya, fajlnev);
                }
            }
        }

        private bool PalyaBetoltes(Mentes mentes)
        {
            Console.Write("\nBiztosan be szeretne tölteni új pályát? (Y/N): ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                // Mappa tartalmának lekérése
                DirectoryInfo mentesiFajlok = new("./");

                List<FileInfo> fajlok = mentesiFajlok.GetFiles().Where(fajl => fajl.Name.EndsWith(".txt")).ToList();

                if (fajlok.Count > 0)
                {
                    // Fájlok kiíratása
                    Console.WriteLine("\n\nLegutóbbi mentések: ");
                    int fajlIndex = 1;
                    int counter = 0;

                    foreach (var fajl in fajlok)
                    {
                        Console.WriteLine($"{fajlIndex} - {fajl.Name}");
                        fajlIndex++;
                        counter++;
                    }

                    Console.Write("\nAdja meg annak a számát, amelyiket be szeretné tölteni: ");
                    int fajlSorszam = Convert.ToInt32(Console.ReadLine());

                    if (counter >= fajlSorszam && fajlSorszam > 0)
                    {
                        string fajlnev = fajlok[fajlSorszam - 1].Name;
                        if (fajlok.Exists(x => x.Name == fajlnev))
                        {
                            Console.WriteLine($"{fajlnev} kiválasztva!");
                            Console.Write("\nAdja meg a körök számát: ");
                            KorokSzama = Convert.ToInt32(Console.ReadLine());
                            palya = mentes.BetoltPalya(fajlnev);
                            JelenlegiKorSzama = 0;
                            return true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyes sorszámú fájl!");
                        Console.WriteLine("\nNyomjon meg egy gombot a folytatáshoz!");
                        _ = Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\nMég nincsen egy mentése sem!");
                    Console.WriteLine("\nNyomjon meg egy gombot a folytatáshoz!");
                    _ = Console.ReadKey();
                }
            }

            return false;
        }

        private bool UjPalyaKezdes(string settings)
        {
            Console.Write("\nBiztosan új pályát szeretne kezdeni? (Y/N): ");
            ConsoleKeyInfo confirm = Console.ReadKey();
            if (confirm.Key == ConsoleKey.Y)
            {
                Console.Write("\n\nAdja meg a körök számát: ");
                KorokSzama = Convert.ToInt32(Console.ReadLine());
                int nyulakSzazalek = 100;
                int rokakSzazalek = 100;
                while (nyulakSzazalek + rokakSzazalek > 100)
                {
                    Console.Write("\nAdja meg a nyulak kezdési százalékát: ");
                    nyulakSzazalek = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nAdja meg a rókák kezdési százalékát: ");
                    rokakSzazalek = Convert.ToInt32(Console.ReadLine());
                }
                palya.SzazalekBeallitas(nyulakSzazalek, rokakSzazalek);
                palya.PalyaElkeszites();
                palya.PalyaMegjelenites();
                Console.WriteLine("Kiinduló állapot");
                Console.WriteLine(settings);
                JelenlegiKorSzama = 0;
                return true;
            }
            return false;
        }
    }
}
