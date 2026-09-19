using System;

class Program
{
    static void Main()
    {
        const int haritaBoyutu = 5;
        int oyuncuX = 0;
        int oyuncuY = 0;
        int harcananHamle = 0;
        Random random = new Random();

        int hazineX;
        int hazineY;
        (hazineX, hazineY) = YeniHazineKonumu(random, haritaBoyutu, oyuncuX, oyuncuY);

        Console.Title = "Hazine Avı";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== HAZİNE AVI =====");
            Console.WriteLine("W: Yukarı | S: Aşağı | A: Sol | D: Sağ | Q: Çıkış");
            Console.WriteLine($"Harcanan hamle: {harcananHamle}");
            Console.WriteLine();

            string ipucu = HazineIpucu(oyuncuX, oyuncuY, hazineX, hazineY);
            Console.WriteLine($"İpucu: {ipucu}");
            Console.WriteLine();

            HaritayiGoster(haritaBoyutu, oyuncuX, oyuncuY, hazineX, hazineY);
            Console.WriteLine();
            Console.Write("Hamlenizi girin: ");

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
                    Console.WriteLine("\nGeçersiz tuş! Lütfen W/S/A/D tuşlarından birini kullanın.");
                    Console.WriteLine("Devam etmek için bir tuşa basın.");
                    Console.ReadKey(true);
                    continue;
            }

            if (yeniX < 0 || yeniX >= haritaBoyutu || yeniY < 0 || yeniY >= haritaBoyutu)
            {
                Console.WriteLine("\nBu tarafa gidemezsiniz! Duvar var.");
                Console.WriteLine("Devam etmek için bir tuşa basın.");
                Console.ReadKey(true);
                continue;
            }

            oyuncuX = yeniX;
            oyuncuY = yeniY;
            harcananHamle++;

            if (oyuncuX == hazineX && oyuncuY == hazineY)
            {
                Console.Clear();
                Console.WriteLine("===== TEBRİKLER =====");
                Console.WriteLine("Hazineyi buldunuz!");
                Console.WriteLine($"Toplam harcanan hamle: {harcananHamle}");
                Console.WriteLine();
                Console.Write("Yeniden oynamak ister misin? (E/H): ");
                string cevap = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (cevap == "E")
                {
                    oyuncuX = 0;
                    oyuncuY = 0;
                    harcananHamle = 0;
                    (hazineX, hazineY) = YeniHazineKonumu(random, haritaBoyutu, oyuncuX, oyuncuY);
                    continue;
                }

                Console.WriteLine("\nOyun sona erdi.");
                return;
            }
        }
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

    static string HazineIpucu(int oyuncuX, int oyuncuY, int hazineX, int hazineY)
    {
        int mesafe = Math.Abs(oyuncuX - hazineX) + Math.Abs(oyuncuY - hazineY);

        if (mesafe == 0)
            return "Hazine tam karşında!";
        if (mesafe <= 1)
            return "Çok sıcak! Hazine çok yakın.";
        if (mesafe <= 2)
            return "Sıcak. Biraz daha ilerle.";
        if (mesafe <= 3)
            return "Ilık. Hala uzakta.";
        return "Soğuk. Daha fazla ara.";
    }

    static void HaritayiGoster(int boyut, int oyuncuX, int oyuncuY, int hazineX, int hazineY)
    {
        for (int y = 0; y < boyut; y++)
        {
            for (int x = 0; x < boyut; x++)
            {
                if (x == oyuncuX && y == oyuncuY)
                {
                    Console.Write("[O]");
                }
                else if (Math.Abs(x - hazineX) + Math.Abs(y - hazineY) <= 1)
                {
                    Console.Write("[H]");
                }
                else
                {
                    Console.Write("[ ]");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("[O] = Oyuncu, [H] = Hazineye çok yakın alan");
    }
}
