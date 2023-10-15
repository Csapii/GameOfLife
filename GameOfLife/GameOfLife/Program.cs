using GameOfLife;
using System.Runtime.InteropServices;

Console.Write("Adja meg a pálya vízszintes méretét (cellákban): ");
int palyaMeretX = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a pálya függőleges méretét (cellákban): ");
int palyaMeretY = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a körök számát: ");
int korokSzama = Convert.ToInt32(Console.ReadLine());

Palya palya = new (palyaMeretX, palyaMeretY);
Szimulacio szimulacio = new (palya, korokSzama);

szimulacio.SzimulacioInditas();



Mentes mentes = new ();
mentes.MentPalya(palya);
Palya masodlagos = mentes.BetoltPalya();

Console.Write("\nAdja meg a körök számát: ");
korokSzama = Convert.ToInt32(Console.ReadLine());

szimulacio = new (masodlagos, korokSzama);

szimulacio.SzimulacioInditas();