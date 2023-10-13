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

        private int jollakottsagiSzint = 10;
        public int JollakottsagiSzint
        {
            get
            {
                return jollakottsagiSzint;
            }
            set
            {
                if (value >= 0 && value <= 10)
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
            if (JollakottsagiSzint > 0)
            {
                jollakottsagiSzint--;
                return true;
            }
            return false;
        }


        public void Taplalkozas()
        {
            if (JollakottsagiSzint > 0)
            {
                Console.WriteLine($"A róka ({Azonosito}) táplálkozik.");
                JollakottsagiSzintCsokkentese();
            }
        }
        public void Elpusztulas()
        {
            if (JollakottsagiSzint <= 0)
            {
                Console.WriteLine($"A róka ({Azonosito}) elpusztult.");
            }
        }
        public void Mozgas()
        {
            if (JollakottsagiSzint > 0)
            {
                Console.WriteLine($"A róka ({Azonosito}) mozog.");
                JollakottsagiSzintCsokkentese();
            }
        }

        public List<Cella> Szaporodas(Palya palyaClass, Cella cella)
        {
            if (JollakottsagiSzint >= 5)
            {
                Console.WriteLine($"A róka ({Azonosito}) szaporodik.");
                JollakottsagiSzintCsokkentese();
                return new List<Cella>();
            }
            return new List<Cella>();
        }
        
        public void Taplalkozas(Cella cella)
        {
            throw new NotImplementedException();
        }

        public bool Szaporodas(Palya palyaClass, Cella cella)
        {
            throw new NotImplementedException();
        }
    }
}
