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

        int Azonosito { get;}
        bool JollakottsagiSzintNovelese(int egyseg);
        bool JollakottsagiSzintCsokkentese();
        void JollakottsagiSzintBeallitas();

        Cella Mozgas(Palya palyaClass, Cella cella);
        List<Cella> Szaporodas(Palya palyaClass, Cella cella);
        Cella Taplalkozas(Palya palyaClass, Cella cella);
    }
}
