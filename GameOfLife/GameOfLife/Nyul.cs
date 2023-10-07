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

        public int Tapertek = 3;
        int jollakottsagiSzint;
        static int azonositohozSzamlalo = 0;
        public int Azonosito { get; set; }

        public Nyul(int jollakottsagiSzint)
        {
            JollakottsagiSzint = jollakottsagiSzint;
            azonositohozSzamlalo++;
            Azonosito = azonositohozSzamlalo;
        }
        public Nyul()
        {
            ((IAllat)this).JollakottsagiSzintBeallitas();
            azonositohozSzamlalo++;
            Azonosito = azonositohozSzamlalo;
        }

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

        public void Elpusztulas()
        {
            throw new NotImplementedException();
        }

        void IAllat.JollakottsagiSzintBeallitas()
        {
            Random rnd = new();
            int veletlenszam = rnd.Next(1, 6+1);

            JollakottsagiSzint = veletlenszam;
        }

        public bool JollakottsagiSzintCsokkentese(int egyseg)
        {
            throw new NotImplementedException();
        }

        public bool JollakottsagiSzintNovelese(int egyseg)
        {
            throw new NotImplementedException();
        }

        public void Mozgas()
        {
            throw new NotImplementedException();
        }

        public bool Szaporodas()
        {
            throw new NotImplementedException();
        }

        public void Taplalkozas()
        {
            throw new NotImplementedException();
        }
    }
}
