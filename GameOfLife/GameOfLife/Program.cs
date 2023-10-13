using GameOfLife;

Console.Write("Adja meg a pákya vízszintes méretét (cellákban): ");
int palyaMeretX = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a pákya vízszintes méretét (cellákban): ");
int palyaMeretY = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a körök számát: ");
int korokSzama = Convert.ToInt32(Console.ReadLine());

Palya palya = new Palya(palyaMeretX, palyaMeretY);
Szimulacio szimulacio = new Szimulacio(palya, korokSzama);

szimulacio.SzimulacioInditas();