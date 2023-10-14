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

        public bool MostSzaporodott = false;

        public bool Atlepheto = false;

        private static int azonositohozSzamlalo = 1;
        public int Azonosito { get; } = azonositohozSzamlalo++;

        public Roka(int jollakottsagiSzint)
        {
            JollakottsagiSzint = jollakottsagiSzint;
        }
        public Roka()
        {
            ((IAllat)this).JollakottsagiSzintBeallitas();
        }

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

        void IAllat.JollakottsagiSzintBeallitas()
        {
            Random rnd = new();
            int veletlenszam = rnd.Next(0, 11);

            JollakottsagiSzint = veletlenszam;
        }
        public bool JollakottsagiSzintNovelese(int egyseg)
        {
            if (JollakottsagiSzint + egyseg <= 10 && egyseg > 0)
            {
                jollakottsagiSzint += egyseg;
                return true;
            }
            return false;
        }
        public bool JollakottsagiSzintCsokkentese()
        {
            if (JollakottsagiSzint - 1 > 0)
            {
                jollakottsagiSzint--;
                return true;
            }
            return false;
        }



        public Cella Mozgas(Palya palyaClass, Cella cella)
        {
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

            Random rnd = new();

            Cella? lepettCella = lephetoCella.Count > 0 ? lephetoCella[rnd.Next(0, lephetoCella.Count)] : null;

            return lepettCella!;
        }



        public List<Cella> Szaporodas(Palya palyaClass, Cella cella) {
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



            if (kozeliRokaCellak.Count == 0) { return new List<Cella>(); } // Ha nem talált rókát



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

            if (kozeliUresCellak.Count == 0) { return new List<Cella>(); } // Ha nincs közeli üres cella

            // Róka születik, állapotok megváltoztatása

            Random rnd = new();

            Cella partnerCella = kozeliRokaCellak[rnd.Next(0, kozeliRokaCellak.Count)];
            Cella babaCella = kozeliUresCellak[rnd.Next(0, kozeliUresCellak.Count)];

            MostSzaporodott = true;
            partnerCella.Roka!.MostSzaporodott = true;
            babaCella.SetRoka();
            if (babaCella.X > cella.X || (babaCella.X == cella.X && babaCella.Y > cella.Y))
            {
                babaCella.Roka!.Atlepheto = true;
            }

            List<Cella> visszaadott = new()
            {
                partnerCella,
                babaCella
            };

            return visszaadott;
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

            Random rnd = new ();

            if (predaLista.Count > 0 && JollakottsagiSzint + 3 < 11)
            {
                JollakottsagiSzintNovelese(3);
                return predaLista[rnd.Next(0, predaLista.Count)];
            } else
            {
                return new Cella(-999,0);
            }
        }
    }
}
