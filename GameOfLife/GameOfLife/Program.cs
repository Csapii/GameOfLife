using GameOfLife;

Console.Write("Adja meg a pálya vízszintes méretét (cellákban): ");
int palyaMeretX = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a pálya vízszintes méretét (cellákban): ");
int palyaMeretY = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a körök számát: ");
int korokSzama = Convert.ToInt32(Console.ReadLine());

Palya palya = new (palyaMeretX, palyaMeretY);
Szimulacio szimulacio = new (palya, korokSzama);

szimulacio.SzimulacioInditas();