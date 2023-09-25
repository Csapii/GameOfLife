using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Szimulacio
    {
<<<<<<< Updated upstream
=======

        private readonly Palya palya;

        private int KorokSzama { get; set; }

        private int JelenlegiKorSzama { get; set; }

        public Szimulacio(Palya palya, int korokSzama)
        {

            this.palya = palya;
            KorokSzama = korokSzama;

        }

        public void SzimulacioInditas()
        {
            for (JelenlegiKorSzama = 1; JelenlegiKorSzama <= KorokSzama; JelenlegiKorSzama++)
            {
                Console.WriteLine($"{JelenlegiKorSzama}. kör lefutott");
            }
        }

        public int LekerJelenlegiKorSzama()
        {
            return JelenlegiKorSzama;
        }

>>>>>>> Stashed changes
    }
}
