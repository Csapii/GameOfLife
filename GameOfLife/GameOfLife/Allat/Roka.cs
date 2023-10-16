using GameOfLife.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Roka : IAllat
    {

        public int SzaporodasVisszaszamlalo { get; set; }

        public bool MostSzaporodott = false;

        public bool Atlepheto = false;

        public bool Taplalkozott = false;

        private int jollakottsagiSzint;

        public int JollakottsagiSzint
        {
            get
            {
                return jollakottsagiSzint;
            }
            set
            {
                if (value > 0 && value <= 10)
                {
                    jollakottsagiSzint = value;
                }
                else
                {
                    jollakottsagiSzint = 10;
                }
            }
        }

        public Roka(int jollakottsagiSzint)
        {
            JollakottsagiSzint = jollakottsagiSzint;
            SzaporodasVisszaszamlalo = 0;
        }

        public Roka()
        {
            ((IAllat)this).JollakottsagiSzintBeallitas();
            SzaporodasVisszaszamlalo = 0;
        }

        void IAllat.JollakottsagiSzintBeallitas()
        {
            Random rnd = new();
            JollakottsagiSzint = rnd.Next(0, 10 + 1);
        }

        public void JollakottsagiSzintNovelese(int egyseg)
        {
            if (JollakottsagiSzint + egyseg < 10 + 1 && egyseg > 0)
            {
                jollakottsagiSzint += egyseg;
            }
        }

        public void JollakottsagiSzintCsokkentese(Cella cella)
        {
            if (JollakottsagiSzint - 1 > 0)
            {
                jollakottsagiSzint--;
            } else
            {
                cella.RemoveRoka();
            }
        }



        public Cella Mozgas(Palya palyaClass, Cella cella)
        {
            if (cella.Roka!.Taplalkozott) { cella.Roka.Taplalkozott = false; return cella; }
            if (cella.Roka.MostSzaporodott) { return cella; }

            List<Cella> lephetoCella = new();

            for (int x = cella.X - 1; x < cella.X + 2; x++)
            {
                for (int y = cella.Y - 1; y < cella.Y + 2; y++)
                {
                    if (x >= 0 && y >= 0 && x < palyaClass.PalyaMeretX && y < palyaClass.PalyaMeretY
                        && !palyaClass.palya[x, y].HasNyul() && !palyaClass.palya[x, y].HasRoka())
                    {
                        lephetoCella.Add(palyaClass.palya[x, y]);
                    }
                }
            }

            if (lephetoCella.Count > 0)
            {
                Random rnd = new();
                Cella lepettCella = lephetoCella[rnd.Next(0, lephetoCella.Count)];
                lepettCella.SetRoka(palyaClass.palya[cella.X, cella.Y].Roka!);
                palyaClass.palya[cella.X, cella.Y].RemoveRoka();
                return lepettCella;
            }
            else { return cella; }
        }

        public void Szaporodas(Palya palyaClass, Cella cella) {
            if (cella.Roka!.MostSzaporodott || cella.Roka.JollakottsagiSzint < 8 ) { return; }

            List<Cella> kozeliRokaCellak = new();
            List<Cella> kozeliUresCellak = new();
            Cella adott;

            if (cella.X - 1 >= 0)
            {
                adott = palyaClass.palya[cella.X - 1, cella.Y];
                if (adott.HasRoka())
                { kozeliRokaCellak.Add(adott); }
                else if (!adott.HasNyul())
                { kozeliUresCellak.Add(adott); }
            } // Felfele scan

            if (cella.X + 1 < palyaClass.PalyaMeretX)
            {
                adott = palyaClass.palya[cella.X + 1, cella.Y];
                if (adott.HasRoka())
                { kozeliRokaCellak.Add(adott); }
                else if (!adott.HasNyul())
                { kozeliUresCellak.Add(adott); }
            } // Lefele scan

            if (cella.Y - 1 >= 0)
            {
                adott = palyaClass.palya[cella.X, cella.Y - 1];
                if (adott.HasRoka())
                { kozeliRokaCellak.Add(adott); }
                else if (!adott.HasNyul())
                { kozeliUresCellak.Add(adott); }
            } // Balra scan

            if (cella.Y + 1 < palyaClass.PalyaMeretY)
            {
                adott = palyaClass.palya[cella.X, cella.Y + 1];
                if (adott.HasRoka())
                { kozeliRokaCellak.Add(adott); }
                else if (!adott.HasNyul())
                { kozeliUresCellak.Add(adott); }
            } // Jobbra scan


            if (kozeliRokaCellak.Count == 0) { return; } // Ha nem talált rókát


            if (cella.X - 1 >= 0 && cella.Y - 1 >= 0
                && !palyaClass.palya[cella.X - 1, cella.Y - 1].HasNyul()
                && !palyaClass.palya[cella.X - 1, cella.Y - 1].HasRoka())
            {
                kozeliUresCellak.Add(palyaClass.palya[cella.X - 1, cella.Y - 1]);
            } // Bal felső scan

            if (cella.X + 1 < palyaClass.PalyaMeretX && cella.Y - 1 >= 0
                && !palyaClass.palya[cella.X + 1, cella.Y - 1].HasNyul()
                && !palyaClass.palya[cella.X + 1, cella.Y - 1].HasRoka())
            {
                kozeliUresCellak.Add(palyaClass.palya[cella.X + 1, cella.Y - 1]);
            } // Bal alsó scan

            if (cella.X - 1 >= 0 && cella.Y + 1 < palyaClass.PalyaMeretY
                && !palyaClass.palya[cella.X - 1, cella.Y + 1].HasNyul()
                && !palyaClass.palya[cella.X - 1, cella.Y + 1].HasRoka())
            {
                kozeliUresCellak.Add(palyaClass.palya[cella.X - 1, cella.Y + 1]);
            } // Jobb felső scan

            if (cella.X + 1 < palyaClass.PalyaMeretX && cella.Y + 1 < palyaClass.PalyaMeretY
                && !palyaClass.palya[cella.X + 1, cella.Y + 1].HasNyul()
                && !palyaClass.palya[cella.X + 1, cella.Y + 1].HasRoka())
            {
                kozeliUresCellak.Add(palyaClass.palya[cella.X + 1, cella.Y + 1]);
            } // Jobb alsó scan


            if (kozeliUresCellak.Count == 0) { return; } // Ha nincs közeli üres cella


            // Róka születik, állapotok megváltoztatása


            Random rnd = new();

            Cella partnerCella = kozeliRokaCellak[rnd.Next(0, kozeliRokaCellak.Count)];
            Cella babaCella = kozeliUresCellak[rnd.Next(0, kozeliUresCellak.Count)];

            cella.Roka.MostSzaporodott = true;
            cella.Roka.SzaporodasVisszaszamlalo = 5;
            partnerCella.Roka!.MostSzaporodott = true;
            partnerCella.Roka.SzaporodasVisszaszamlalo = 5;
            babaCella.SetRoka();
            if (babaCella.X > cella.X || (babaCella.X == cella.X && babaCella.Y > cella.Y))
            {
                babaCella.Roka!.Atlepheto = true;
            }

            palyaClass.palya[partnerCella.X, partnerCella.Y] = partnerCella;
            palyaClass.palya[babaCella.X, babaCella.Y] = babaCella;

            return;
        }



        public Cella Taplalkozas(Palya palyaClass, Cella cella)
        {
            List<Cella> predaLista = new ();
            for (int x = cella.X - 2; x < cella.X + 3; x++)
            {
                for (int y = cella.Y - 2; y < cella.Y + 3; y++)
                {
                    if (x >= 0 && x < palyaClass.PalyaMeretX && y >= 0 && y < palyaClass.PalyaMeretY
                        && palyaClass.palya[x, y].HasNyul())
                    {
                        predaLista.Add(palyaClass.palya[x, y]);
                    }
                }
            }

            if (predaLista.Count > 0 && JollakottsagiSzint + 3 < 10 + 1)
            {
                Random rnd = new();
                Cella predaCella = predaLista[rnd.Next(0, predaLista.Count)];

                JollakottsagiSzintNovelese(predaCella.Nyul!.Tapertek);
                cella.Roka!.Taplalkozott = true;

                palyaClass.palya[predaCella.X, predaCella.Y].RemoveNyul();
                palyaClass.palya[predaCella.X, predaCella.Y].SetRoka(cella.Roka);
                cella.RemoveRoka();

                return predaCella;
            } else
            {
                return cella;
            }
        }



        public void AllapotVizsgalat(Cella cella, Cella eredetiCella)
        {
            if (cella.X > eredetiCella.X || (cella.X == eredetiCella.X && cella.Y > eredetiCella.Y))
            {
                Atlepheto = true;
            }

            if (SzaporodasVisszaszamlalo > 0)
            {
                SzaporodasVisszaszamlalo--;

                if (SzaporodasVisszaszamlalo < 1)
                {
                    MostSzaporodott = false;
                }
            }
        }
    }
}
