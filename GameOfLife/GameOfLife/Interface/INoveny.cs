using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal interface INoveny
    {
        int Tapertek { get; }

        void TapertekBeallitas(int ertek);
        void TapertekNoveles();
        void TapertekCsokkentes();
        void NovekedesiAllapotvaltozasNoveles();
        void NovekedesiAllapotvaltozasCsokkentes();
    }
}