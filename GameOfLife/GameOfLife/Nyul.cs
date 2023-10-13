﻿using GameOfLife.Interface;
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

        public void Mozgas()
        {
            // mozgas
        }

        public List<Cella> Szaporodas(Palya palyaClass, Cella cella)
        {
            List<Cella> kozeliNyulCellak = new ();
            List<Cella> kozeliUresCellak = new ();
            Cella adott;

            if (cella.X - 1 >= 0) {
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



            if (kozeliNyulCellak.Count == 0) { return new List<Cella>(); } // Ha nem talált nyulat



            if (cella.X - 1 >= 0 && cella.Y - 1 >= 0
                && !palyaClass.palya[cella.X - 1, cella.Y - 1].HasNyul()
                && !palyaClass.palya[cella.X - 1, cella.Y - 1].HasRoka()) {
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
            
            // Nyúl születik, állapotok megváltoztatása

            Random rnd = new ();

            Cella partnerCella = kozeliNyulCellak[rnd.Next(0, kozeliNyulCellak.Count)];
            Cella babaCella = kozeliUresCellak[rnd.Next(0, kozeliUresCellak.Count)];

            MostSzaporodott = true;
            partnerCella.Nyul!.MostSzaporodott = true;
            babaCella.SetNyul();
            babaCella.Nyul!.MostSzuletett = true;

            List<Cella> visszaadott = new ()
            {
                partnerCella,
                babaCella
            };

            return visszaadott;    
        }

        public void Taplalkozas(Cella cella)
        {
            int egyseg = cella.Fu!.Tapertek;
            if (JollakottsagiSzint + egyseg < 6 && egyseg > 0)
            {
                jollakottsagiSzint += egyseg;
                cella.Fu.NovekedesiAllapotvaltozasCsokkentes();
            }
        }
    }
}
