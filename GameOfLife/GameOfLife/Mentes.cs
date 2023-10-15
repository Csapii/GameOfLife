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



        public static void MentPalya(Cella cella, Palya palya, string fajlnev)
        {
            StreamWriter sw = new StreamWriter($"Mentesek/{fajlnev}.txt", false, Encoding.UTF8);

            string menteniKoordinata = $"PalyaMeretX:{palya.PalyaMeretX}\n" +
                                       $"PalyaMeretY:{palya.PalyaMeretY}\n";


            sw.WriteLine(menteniKoordinata);

            for (int i = 0; i < palya.PalyaMeretX; i++)
            {
                for (int j = 0; j < palya.PalyaMeretY; j++)
                {
                    if (cella.HasFu())
                    {
                        sw.Write(cella.Fu);
                    }
                    if (cella.HasNyul())
                    {
                        sw.Write("N");
                    }
                    if (cella.HasRoka())
                    {
                        sw.Write("R");
                    }
                }
                sw.WriteLine();
            }
            sw.Close();

            //Cella cella = new(0, 0);
            //Mentes.MentPalya(cella, palya, "Mentes2");
        }
    }
}
