using GameOfLife.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Nyul : IAllat
    {

        public int SzaporodasVisszaszamlalo { get; set; }

        public bool MostSzaporodott = false;

        public bool Atlepheto = false;

        public int Tapertek = 3;

        private int jollakottsagiSzint;

        public int JollakottsagiSzint
        {
            get
            {
                return jollakottsagiSzint;
            }
            set
            {
                if (value > 0 && value < 6)
                {
                    jollakottsagiSzint = value;
                }
                else
                {
                    jollakottsagiSzint = 5;
                }
            }
        }

        public Nyul(int jollakottsagiSzint)
        {
            JollakottsagiSzint = jollakottsagiSzint;
            SzaporodasVisszaszamlalo = 0;
        }

        public Nyul()
        {
            ((IAllat)this).JollakottsagiSzintBeallitas();
            SzaporodasVisszaszamlalo = 0;
        }

        void IAllat.JollakottsagiSzintBeallitas()
        {
            Random rnd = new();
            JollakottsagiSzint = rnd.Next(1, 5 + 1);
        }

        public void JollakottsagiSzintNovelese(int egyseg)
        {
            if (JollakottsagiSzint + egyseg < 5 + 1 && egyseg > 0)
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
                cella.RemoveNyul();
            }
        }



        public Cella Mozgas(Palya palyaClass, Cella cella)
        {
            if (cella.Nyul!.MostSzaporodott) { return cella; }

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
                Cella lepettCella;
                if (lephetoCella.Exists(x => x.Fu!.Tapertek == 2))
                {
                    lephetoCella = lephetoCella.Where(x => x.Fu!.Tapertek == 2).ToList();
                    lepettCella = lephetoCella[rnd.Next(0, lephetoCella.Count)];
                }
                else if (cella.Fu!.Tapertek == 2) { return cella; }
                else if (lephetoCella.Exists(x => x.Fu!.Tapertek == 1))
                {
                    lephetoCella = lephetoCella.Where(x => x.Fu!.Tapertek == 1).ToList();
                    lepettCella = lephetoCella[rnd.Next(0, lephetoCella.Count)];
                }
                else if (cella.Fu!.Tapertek == 1) { return cella; }
                else { lepettCella = lephetoCella[rnd.Next(0, lephetoCella.Count)]; }

                lepettCella.SetNyul(palyaClass.palya[cella.X, cella.Y].Nyul!);
                palyaClass.palya[cella.X, cella.Y].RemoveNyul();

                return lepettCella;
            }
            else { return cella; }
        }



        public void Szaporodas(Palya palyaClass, Cella cella)
        {
            if (cella.Nyul!.MostSzaporodott || cella.Nyul.JollakottsagiSzint < 5) { return; }

            List<Cella> kozeliNyulCellak = new();
            List<Cella> kozeliUresCellak = new();
            Cella adott;

            if (cella.X - 1 >= 0)
            {
                adott = palyaClass.palya[cella.X - 1, cella.Y];
                if (adott.HasNyul())
                { kozeliNyulCellak.Add(adott); }
                else if (!adott.HasRoka())
                { kozeliUresCellak.Add(adott); }
            } // Felfele scan

            if (cella.X + 1 < palyaClass.PalyaMeretX)
            {
                adott = palyaClass.palya[cella.X + 1, cella.Y];
                if (adott.HasNyul())
                { kozeliNyulCellak.Add(adott); }
                else if (!adott.HasRoka())
                { kozeliUresCellak.Add(adott); }
            } // Lefele scan

            if (cella.Y - 1 >= 0)
            {
                adott = palyaClass.palya[cella.X, cella.Y - 1];
                if (adott.HasNyul())
                { kozeliNyulCellak.Add(adott); }
                else if (!adott.HasRoka())
                { kozeliUresCellak.Add(adott); }
            } // Balra scan

            if (cella.Y + 1 < palyaClass.PalyaMeretY)
            {
                adott = palyaClass.palya[cella.X, cella.Y + 1];
                if (adott.HasNyul())
                { kozeliNyulCellak.Add(adott); }
                else if (!adott.HasRoka())
                { kozeliUresCellak.Add(adott); }
            } // Jobbra scan


            if (kozeliNyulCellak.Count == 0) { return; } // Ha nem talált nyulat


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


            // Nyúl születik, állapotok megváltoztatása


            Random rnd = new();

            Cella partnerCella = kozeliNyulCellak[rnd.Next(0, kozeliNyulCellak.Count)];
            Cella babaCella = kozeliUresCellak[rnd.Next(0, kozeliUresCellak.Count)];

            cella.Nyul.MostSzaporodott = true;
            cella.Nyul.SzaporodasVisszaszamlalo = 3;
            partnerCella.Nyul!.MostSzaporodott = true;
            partnerCella.Nyul.SzaporodasVisszaszamlalo = 3;
            babaCella.SetNyul();
            if (babaCella.X > cella.X || (babaCella.X == cella.X && babaCella.Y > cella.Y))
            {
                babaCella.Nyul!.Atlepheto = true;
            }

            palyaClass.palya[partnerCella.X, partnerCella.Y] = partnerCella;
            palyaClass.palya[babaCella.X, babaCella.Y] = babaCella;

            return;
        }



        public Cella Taplalkozas(Palya palyaClass, Cella cella)
        {
            int egyseg = cella.Fu!.Tapertek;

            if (JollakottsagiSzint + egyseg < 6 && egyseg > 0)
            {
                jollakottsagiSzint += egyseg;
                cella.Fu.NovekedesiAllapotvaltozasCsokkentes();
            }

            return cella;
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
