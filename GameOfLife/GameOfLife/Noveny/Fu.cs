using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Fu : INoveny
    {
        private string? novekedesiAllapot;
        public string NovekedesiAllapot
        {
            get { return novekedesiAllapot!; }
            set
            {
                if (value == "Fűkezdemény" || value == "Zsenge fű" || value == "Kifejlett fűcsomó")
                {
                    novekedesiAllapot = value;
                }
                else
                {
                    ((INoveny)this).NovekedesiAllapotBeallitas();
                }
            }
        }
        void INoveny.TapertekBeallitas()
        {
            if (NovekedesiAllapot == "Fűkezdemény")
            {
                tapertek = 0;
            }
            else if (NovekedesiAllapot == "Zsenge fű")
            {
                tapertek = 1;
            }
            else
            {
                tapertek = 2;
            }
        }

        private int tapertek;
        public int Tapertek
        {
            get
            {
                return tapertek;
            }
        }

        void INoveny.TapertekNoveles()
        {
            if (Tapertek < 2)
            {
                tapertek++;
            }
        }
        void INoveny.TapertekCsokkentes()
        {
            if (Tapertek > 0)
            {
                tapertek--;
            }
        }
    }
}