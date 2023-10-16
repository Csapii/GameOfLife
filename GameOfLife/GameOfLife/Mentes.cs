using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Mentes
    {

        public void MentPalya(Palya palya, string fajlnev = "mentes.txt")
        {
            StreamWriter w = new(fajlnev, false, Encoding.UTF8);

            w.WriteLine($"{palya.PalyaMeretX};{palya.PalyaMeretY}");

            for (int x = 0; x < palya.PalyaMeretX; x++)
            {
                for (int y = 0; y < palya.PalyaMeretY; y++)
                {
                    string cellaTartalom = "";
                    cellaTartalom += $"{x};{y};";
                    cellaTartalom += $"{palya.palya[x, y].Fu!.Tapertek};";
                    cellaTartalom += (palya.palya[x, y].HasNyul() ? palya.palya[x, y].Nyul!.JollakottsagiSzint : null) + ";";
                    cellaTartalom += (palya.palya[x, y].HasNyul() ? palya.palya[x, y].Nyul!.Atlepheto : null) + ";";
                    cellaTartalom += (palya.palya[x, y].HasNyul() ? palya.palya[x, y].Nyul!.MostSzaporodott : null) + ";";
                    cellaTartalom += (palya.palya[x, y].HasRoka() ? palya.palya[x, y].Roka!.JollakottsagiSzint : null) + ";";
                    cellaTartalom += (palya.palya[x, y].HasRoka() ? palya.palya[x, y].Roka!.Atlepheto : null) + ";";
                    cellaTartalom += (palya.palya[x, y].HasRoka() ? palya.palya[x, y].Roka!.MostSzaporodott : null) + ";";
                    cellaTartalom += (palya.palya[x, y].HasRoka() ? palya.palya[x, y].Roka!.Taplalkozott : null);

                    w.WriteLine(cellaTartalom);
                }
            }

            // X;Y;FűÉrték;NyúlJóllak;NyúlÁtlép;NyúlSzap;RókaJóllak;RókaÁtlép;RókaSzap;RókaTápl

            w.Close();
        }

        public Palya BetoltPalya(string fajlnev = "mentes.txt")
        {
            StreamReader r = new (fajlnev);

            string[] palyaMeret = r.ReadLine()!.Split(";");

            Palya palyaClass = new (int.Parse(palyaMeret[0]), int.Parse(palyaMeret[1]));

            string[] sor;
            int x;
            int y;

            while (!r.EndOfStream) {
                sor = r.ReadLine()!.Split(";");
                x = int.Parse(sor[0]);
                y = int.Parse(sor[1]);

                palyaClass.palya[x, y] = new Cella(x, y);
                palyaClass.palya[x, y].SetFu(int.Parse(sor[2]));
                
                if (sor[3] != "")
                {
                    Nyul nyul = new (int.Parse(sor[3]));
                    palyaClass.palya[x, y].SetNyul(nyul);
                    palyaClass.palya[x, y].Nyul!.Atlepheto = sor[4] == "True";
                    palyaClass.palya[x, y].Nyul!.MostSzaporodott = sor[5] == "True";
                }

                if (sor[6] != "")
                {
                    Roka roka = new (int.Parse(sor[6]));
                    palyaClass.palya[x, y].SetRoka(roka);
                    palyaClass.palya[x, y].Roka!.Atlepheto = sor[7] == "True";
                    palyaClass.palya[x, y].Roka!.MostSzaporodott = sor[8] == "True";
                    palyaClass.palya[x, y].Roka!.Taplalkozott = sor[9] == "True";
                }
            }

            r.Close();

            return palyaClass;
        }
    }
}
