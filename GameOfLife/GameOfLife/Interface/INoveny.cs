using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal interface INoveny
    {
        string NovekedesiAllapot { get; set; }
        int Tapertek { get; set; }

        void NovekedesiAllapotvaltozasPozitivIranyba();
        void NovekedesiAllapotvaltozasNegativIranyba();
    }
}