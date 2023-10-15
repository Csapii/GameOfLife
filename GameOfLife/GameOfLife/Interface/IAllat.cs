using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interface
{
    internal interface IAllat
    {
        public int SzaporodasVisszaszamlalo { get; set; }
        int JollakottsagiSzint { get; set; }
        void JollakottsagiSzintNovelese(int egyseg);
        void JollakottsagiSzintCsokkentese(Cella cella);
        void JollakottsagiSzintBeallitas();
        Cella Mozgas(Palya palyaClass, Cella cella);
        void Szaporodas(Palya palyaClass, Cella cella);
        Cella Taplalkozas(Palya palyaClass, Cella cella);
        void AllapotVizsgalat(Cella cella, Cella eredetiCella);
    }
}
