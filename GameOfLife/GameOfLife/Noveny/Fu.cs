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
            ((INoveny)this).NovekedesiAllapotBeallitas();
            ((INoveny)this).TapertekBeallitas();
        }
        public Fu(string novekedesiAllapot)
        {
            NovekedesiAllapot = novekedesiAllapot;
            ((INoveny)this).TapertekBeallitas();
        }

        private static int azonositohozSzamlalo = 0;
        public int Azonosito { get; } = ++azonositohozSzamlalo;

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

        private int tapertek;
        public int Tapertek
        {
            get
            {
                return tapertek;
            }
        }

        void INoveny.NovekedesiAllapotBeallitas()
        {
            Random rnd = new();
            int veletlenszam = rnd.Next(0, 3 + 1);

            if (veletlenszam == 0)
            {
                novekedesiAllapot = "Fűkezdemény";
            }
            else if (veletlenszam == 1)
            {
                novekedesiAllapot = "Zsenge fű";
            }
            else
            {
                novekedesiAllapot = "Kifejlett fűcsomó";
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
        public void NovekedesiAllapotvaltozasNoveles()
        {
            if (NovekedesiAllapot == "Fűkezdemény")
            {
                NovekedesiAllapot = "Zsenge fű";
                ((INoveny)this).TapertekNoveles();
            }
            else if (NovekedesiAllapot == "Zsenge fű")
            {
                NovekedesiAllapot = "Kifejlett fűcsomó";
                ((INoveny)this).TapertekNoveles();
            }
        }
        public void NovekedesiAllapotvaltozasCsokkentes()
        {
            if (NovekedesiAllapot == "Kifejlett fűcsomó")
            {
                NovekedesiAllapot = "Zsenge fű";
                ((INoveny)this).TapertekCsokkentes();
            }
            else if (NovekedesiAllapot == "Zsenge fű")
            {
                NovekedesiAllapot = "Fűkezdemény";
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