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

        public void NovekedesiAllapotvaltozasPozitivIranyba();
        public void NovekedesiAllapotvaltozasNegativIranyba();
    }
}