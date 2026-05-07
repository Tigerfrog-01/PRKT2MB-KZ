using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Storage;

namespace Projekt2_TARpe24_Kristopher;

public static class FailiHaldur
{
    private static string failiPesa = Path.Combine(FileSystem.AppDataDirectory, "retseptid.txt");

    public static void SalvestaRetsept(Retsept retsept)
    {
        try
        {
            string rida = $"{retsept.Nimi};{retsept.Kategooria};{retsept.PildiLink}{Environment.NewLine}";
            File.AppendAllText(failiPesa, rida);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Viga: {ex.Message}");
        }
    }

    public static List<Retsept> LoeRetseptid()
    {
        var nimekiri = new List<Retsept>();
        if (File.Exists(failiPesa))
        {
            string[] read = File.ReadAllLines(failiPesa);
            foreach (string rida in read)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(rida)) continue;
                    string[] osad = rida.Split(';');
                    if (osad.Length >= 3)
                    {
                        nimekiri.Add(new Retsept { Nimi = osad[0], Kategooria = osad[1], PildiLink = osad[2] });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Viga: {ex.Message}");
                }
            }
        }
        return nimekiri;
    }

    public static void SalvestaKõik(List<Retsept> nimekiri)
    {
        try
        {
            var read = new List<string>();
            foreach (var r in nimekiri) read.Add($"{r.Nimi};{r.Kategooria};{r.PildiLink}");
            File.WriteAllLines(failiPesa, read);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Viga: {ex.Message}");
        }
    }
}