using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Mentes
    {
        //DateTime currentDateAndTime = DateTime.Now;
        //string fajlnev = currentDateAndTime.ToString();



        public static void MentPalya(Cella cella, Palya palya, string fajlnev = "teszt.txt")
        {
            StreamWriter sw = new StreamWriter($"Mentesek/{fajlnev}.txt", false, Encoding.UTF8);

            string menteniValoDolgok = $"PalyaMeretX:{palya.PalyaMeretX}\n" +
                                       $"PalyaMeretY:{palya.PalyaMeretY}\n";


            sw.WriteLine(menteniValoDolgok);
            sw.Close();

        }
    }
}
