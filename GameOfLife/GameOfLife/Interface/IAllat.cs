using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interface
{
    internal interface IAllat
    {
        int JollakottsagiSzint { get; set; }

        int Azonosito { get; set; }
        bool JollakottsagiSzintNovelese(int egyseg);
        bool JollakottsagiSzintCsokkentese(int egyseg);
        void Taplalkozas();
        void Mozgas();
        bool Szaporodas();
        void Elpusztulas();
        void JollakottsagiSzintBeallitas();
    }
}
