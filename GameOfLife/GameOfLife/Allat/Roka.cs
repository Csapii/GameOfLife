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
            int veletlenszam = rnd.Next(1, 10 + 1);

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
            if (JollakottsagiSzint > 0)
            {
                jollakottsagiSzint--;
                return true;
            }
            return false;
        }


        public void Taplalkozas(Cella cella)
        {

        }
        public void Mozgas()
        {

        }
        public List<Cella> Szaporodas(Palya palyaClass, Cella cella) {
            return new List<Cella>();
        }
    }
}
