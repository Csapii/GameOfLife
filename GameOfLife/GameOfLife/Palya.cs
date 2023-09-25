using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Palya
    {

        private int PalyaMeretX { get; init; }

        private int PalyaMeretY { get; init; }

        public Palya(int palyaMeretX, int palyaMeretY)
        {
            PalyaMeretX = palyaMeretX;
            PalyaMeretY = palyaMeretY;
        }

    }
}
