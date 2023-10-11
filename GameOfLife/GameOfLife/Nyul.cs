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

        public bool MostSzaporodott = false;

        public bool MostSzuletett = false;

        public int Tapertek = 3;
     
        private static int azonositohozSzamlalo = 1;
        public int Azonosito { get; } = azonositohozSzamlalo++;

        public Nyul(int jollakottsagiSzint)
        {
            JollakottsagiSzint = jollakottsagiSzint;
        }
        public Nyul()
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

        void IAllat.JollakottsagiSzintBeallitas()
        {
            Random rnd = new();
            int veletlenszam = rnd.Next(1, 5 + 1);

            JollakottsagiSzint = veletlenszam;
        }
        public bool JollakottsagiSzintNovelese(int egyseg)
        {
            if (JollakottsagiSzint+egyseg < 6 && egyseg > 0)
            {
                jollakottsagiSzint+=egyseg;
                return true;
            }
            return false;
        }

        public bool JollakottsagiSzintCsokkentese()
        {
            if (JollakottsagiSzint > 0)
            {
                jollakottsagiSzint--;
                return true;
            }
            return false;
        }

        public void Elpusztulas()
        {
            throw new NotImplementedException();
        }
        public void Mozgas()
        {
            throw new NotImplementedException();
        }

        public bool Szaporodas(Palya palyaClass, Cella cella)
        {
            List<Nyul> kozeliNyulak = new List<Nyul>();
            List<Cella> kozeliUresCellak = new List<Cella>();

            if (cella.X - 1 >= 0) {
                if (palyaClass.palya[cella.X - 1, cella.Y].HasNyul())
                { kozeliNyulak.Add(cella.Nyul); }
                else if (!palyaClass.palya[cella.X - 1, cella.Y].HasRoka())
                { kozeliUresCellak.Add(cella); }
            } // Felfele scan

            if (cella.X + 1 < palyaClass.PalyaMeretX)
            {
                if (palyaClass.palya[cella.X + 1, cella.Y].HasNyul())
                { kozeliNyulak.Add(cella.Nyul); }
                else if (!palyaClass.palya[cella.X + 1, cella.Y].HasRoka())
                { kozeliUresCellak.Add(cella); }
            } // Lefele scan

            if (cella.Y - 1 >= 0)
            {
                if (palyaClass.palya[cella.X, cella.Y - 1].HasNyul())
                { kozeliNyulak.Add(cella.Nyul); }
                else if (!palyaClass.palya[cella.X, cella.Y - 1].HasRoka())
                { kozeliUresCellak.Add(cella); }
            } // Balra scan

            if (cella.Y + 1 < palyaClass.PalyaMeretY)
            {
                if (palyaClass.palya[cella.X, cella.Y + 1].HasNyul())
                { kozeliNyulak.Add(cella.Nyul); }
                else if (!palyaClass.palya[cella.X, cella.Y + 1].HasRoka())
                { kozeliUresCellak.Add(cella); }
            } // Jobbra scan



            if (kozeliNyulak.Count == 0) { return false; } // Ha nem talált nyulat



            if (cella.X - 1 >= 0 && cella.Y - 1 >= 0
                && !palyaClass.palya[cella.X - 1, cella.Y - 1].HasNyul()
                && !palyaClass.palya[cella.X - 1, cella.Y - 1].HasRoka()) {
                kozeliUresCellak.Add(cella);
            } // Bal felső scan

            if (cella.X +1 < palyaClass.PalyaMeretX && cella.Y -1 >= 0
                && !palyaClass.palya[cella.X + 1, cella.Y - 1].HasNyul()
                && !palyaClass.palya[cella.X + 1, cella.Y - 1].HasRoka())
            {
                kozeliUresCellak.Add(cella);
            } // Bal alsó scan

            if (cella.X - 1 >= 0 && cella.Y + 1 < palyaClass.PalyaMeretY
                && !palyaClass.palya[cella.X - 1, cella.Y + 1].HasNyul()
                && !palyaClass.palya[cella.X - 1, cella.Y + 1].HasRoka())
            {
                kozeliUresCellak.Add(cella);
            } // Jobb felső scan

            if (cella.X - 1 >= 0 && cella.Y + 1 < palyaClass.PalyaMeretY
                && !palyaClass.palya[cella.X - 1, cella.Y + 1].HasNyul()
                && !palyaClass.palya[cella.X - 1, cella.Y + 1].HasRoka())
            {
                kozeliUresCellak.Add(cella);
            } // Jobb alsó scan

            if (kozeliUresCellak.Count == 0) { return false; } // Ha nincs közeli üres cella

            // Nyúl születik, állapotok megváltoztatása

            return true;
        }

        public void Taplalkozas(Cella cella)
        {
            int egyseg = cella.Fu.Tapertek;
            if (JollakottsagiSzint + egyseg < 6 && egyseg > 0)
            {
                jollakottsagiSzint += egyseg;
                cella.Fu.NovekedesiAllapotvaltozasCsokkentes();
            }
        }
    }
}
