using System;

class Program
{
    static void Main()
    {
        const int haritaBoyutu = 5;
        int oyuncuX = 0;
        int oyuncuY = 0;
        Random random = new Random();

        int hazineX;
        int hazineY;
        (hazineX, hazineY) = YeniHazineKonumu(random, haritaBoyutu, oyuncuX, oyuncuY);

        int hamleSayisi = 0;
        Console.Title = "Hazine Avı";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== HAZİNE AVI =====");
            Console.WriteLine("W: Yukarı | S: Aşağı | A: Sol | D: Sağ | Q: Çıkış");
            Console.WriteLine($"Hamle sayısı: {hamleSayisi}\n");
            HaritayiGoster(haritaBoyutu, oyuncuX, oyuncuY);
            Console.Write("\nHamlenizi girin: ");

            ConsoleKey tus = Console.ReadKey(true).Key;
            int yeniX = oyuncuX;
            int yeniY = oyuncuY;

            switch (tus)
            {
                case ConsoleKey.W: yeniY--; break;
                case ConsoleKey.S: yeniY++; break;
                case ConsoleKey.A: yeniX--; break;
                case ConsoleKey.D: yeniX++; break;
                case ConsoleKey.Q:
                    Console.WriteLine("\nOyundan çıktınız.");
                    return;
                default:
                    continue;
            }

            if (yeniX < 0 || yeniX >= haritaBoyutu || yeniY < 0 || yeniY >= haritaBoyutu)
            {
                Console.WriteLine("\nBuraya gidemezsiniz!");
                Console.WriteLine("Devam etmek için bir tuşa basın.");
                Console.ReadKey(true);
                continue;
            }

            oyuncuX = yeniX;
            oyuncuY = yeniY;
            hamleSayisi++;

            if (oyuncuX == hazineX && oyuncuY == hazineY)
            {
                Console.Clear();
                Console.WriteLine("===== TEBRİKLER! =====");
                Console.WriteLine("Hazineyi buldunuz!");
                Console.WriteLine($"Toplam hamle: {hamleSayisi}\n");
                Console.Write("Tekrar oynamak ister misiniz? (E/H): ");

                if ((Console.ReadLine() ?? "").Trim().ToUpper() != "E")
                    break;

                oyuncuX = 0;
                oyuncuY = 0;
                hamleSayisi = 0;
                (hazineX, hazineY) = YeniHazineKonumu(random, haritaBoyutu, oyuncuX, oyuncuY);
            }
        }

        Console.WriteLine("\nOyun sona erdi.");
    }

    static (int x, int y) YeniHazineKonumu(Random random, int boyut, int oyuncuX, int oyuncuY)
    {
        int x;
        int y;
        do
        {
            x = random.Next(boyut);
            y = random.Next(boyut);
        } while (x == oyuncuX && y == oyuncuY);

        return (x, y);
    }

    static void HaritayiGoster(int boyut, int oyuncuX, int oyuncuY)
    {
        for (int y = 0; y < boyut; y++)
        {
            for (int x = 0; x < boyut; x++)
            {
                Console.Write(x == oyuncuX && y == oyuncuY ? "[O]" : "[ ]");
            }
            Console.WriteLine();
        }
        Console.WriteLine("[O] = Oyuncu");
    }
}
