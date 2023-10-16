using GameOfLife;
using System.Runtime.InteropServices;

Console.Write("Adja meg a pálya vízszintes méretét (cellákban): ");
int palyaMeretX = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a pálya függőleges méretét (cellákban): ");
int palyaMeretY = Convert.ToInt32(Console.ReadLine());
Console.Write("\nAdja meg a körök számát: ");
int korokSzama = Convert.ToInt32(Console.ReadLine());

int nyulakSzazalek = 100;
int rokakSzazalek = 100;
while (nyulakSzazalek + rokakSzazalek > 100)
{
    Console.Write("\nAdja meg a nyulak kezdési százalékát: ");
    nyulakSzazalek = Convert.ToInt32(Console.ReadLine());
    Console.Write("\nAdja meg a rókák kezdési százalékát: ");
    rokakSzazalek = Convert.ToInt32(Console.ReadLine());
}

Palya palya = new (palyaMeretX, palyaMeretY);
palya.SzazalekBeallitas(nyulakSzazalek, rokakSzazalek);

Szimulacio szimulacio = new (palya, korokSzama);

szimulacio.SzimulacioInditas();