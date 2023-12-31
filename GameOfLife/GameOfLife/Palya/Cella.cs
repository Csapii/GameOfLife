﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Cella
    {


        public Roka? Roka { get; private set; }

        public Nyul? Nyul { get; private set; }

        public Fu? Fu { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Cella(int x, int y)
        {
            X = x;
            Y = y;
        }


        public void SetRoka(Roka? roka = null)
        {
            if (roka == null)
            {
                Roka = new Roka();
            } else
            {
                Roka = roka;
            }
        }

        public void SetNyul(Nyul? nyul = null)
        {
            if (nyul == null)
            {
                Nyul = new Nyul();
            } else
            {
                Nyul = nyul;
            }
        }

        public void SetFu(int ertek = -1)
        {
            if (ertek == -1)
            {
                Fu = new Fu();
            } else
            {
                Fu = new Fu(ertek);
            }
        }

        public void RemoveRoka()
        {
            if (Roka != null)
            {
                Roka = null;
            }
        }

        public void RemoveNyul()
        {
            if (Nyul != null)
            {
                Nyul = null;
            }
        }

        public bool HasRoka() { return Roka != null; }
        public bool HasNyul() { return Nyul != null; }
        public bool HasFu() { return Fu != null; }

    }
}
