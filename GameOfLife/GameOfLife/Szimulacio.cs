﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Szimulacio
    {

        private readonly Palya palya;

        private int KorokSzama { get; set; }

        public int JelenlegiKorSzama { get; private set; }

        public Szimulacio(Palya palya, int korokSzama)
        {

            this.palya = palya;
            KorokSzama = korokSzama;

        }

        public void SzimulacioInditas()
        {

            palya.PalyaElkeszites();

            palya.PalyaMegjelenites();

            Console.WriteLine("Kiinduló állapot");

            Console.ReadKey();

            for (JelenlegiKorSzama = 1; JelenlegiKorSzama <= KorokSzama; JelenlegiKorSzama++)
            {

                palya.PalyaValtoztatasok();

                palya.PalyaMegjelenites();

                Console.WriteLine($"{JelenlegiKorSzama}. kör lefutott");

                Console.ReadKey();
            }
        }

    }
}
