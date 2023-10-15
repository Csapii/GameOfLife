using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Fu : INoveny
    {
        public Fu()
        {
            Random rnd = new Random();
            ((INoveny)this).TapertekBeallitas(rnd.Next(0, 2 + 1));
        }

        public Fu(int ertek)
        {
            ((INoveny)this).TapertekBeallitas(ertek);
        }

        private int tapertek;
        public int Tapertek
        {
            get
            {
                return tapertek;
            }
        }

        void INoveny.TapertekBeallitas(int ertek)
        {
            tapertek = ertek;
        }

        public void NovekedesiAllapotvaltozasNoveles()
        {
            if (Tapertek == 0)
            {
                ((INoveny)this).TapertekNoveles();
            }
            else if (Tapertek == 1)
            {
                ((INoveny)this).TapertekNoveles();
            }
        }
        public void NovekedesiAllapotvaltozasCsokkentes()
        {
            if (Tapertek == 2)
            {
                ((INoveny)this).TapertekCsokkentes();
                ((INoveny)this).TapertekCsokkentes();
            }
            else if (Tapertek == 1)
            {
                ((INoveny)this).TapertekCsokkentes();
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