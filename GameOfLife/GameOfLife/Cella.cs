﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Cella
    {


        public Roka Roka { get; private set; }

        public Nyul Nyul { get; private set; }

        public Fu Fu { get; private set; }




        public void SetRoka()
        {
            if (Roka == null)
            {
                //Roka == new Roka();
            }
        }

        public void SetNyul()
        {
            if (Nyul == null)
            {
                Nyul = new Nyul();
            }
        }

        public void SetFu()
        {
            if (Fu == null)
            {
                Fu = new Fu();
            }
        }

        public bool HasRoka() { return Roka != null; }
        public bool HasNyul() { return Nyul != null; }
        public bool HasFu() { return Fu != null; }

    }
}
