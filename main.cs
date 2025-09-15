using System;
using System.Collections.Generic;

class LauncherPoC {
    static void Main() {
        Console.WriteLine("Launcher Aggregator START");
        
        // Inicjalizuj biblioteki gier
        var steamLibrary = new SteamLibrary();
        var eaLibrary = new EAGamesLibrary();
        
        // Pobierz gry z Steam
        Console.WriteLine("\n=== STEAM GAMES ===");
        var steamGames = steamLibrary.GetInstalledGames();
        foreach (var game in steamGames) {
            Console.WriteLine($"{game.Id} - {game.Name} ({game.LibraryPath})");
        }
        
        // Pobierz gry z EA Games
        Console.WriteLine("\n=== EA GAMES ===");
        var eaGames = eaLibrary.GetInstalledGames();
        foreach (var game in eaGames) {
            Console.WriteLine($"{game.Id} - {game.Name} ({game.LibraryPath})");
        }
        
        // Przykładowe uruchomienie gry Steam
        // if (steamGames.Count > 0) {
        //     Console.WriteLine($"\nUruchamiam pierwszą grę Steam: {steamGames[0].Name}");
        //     steamLibrary.LaunchGame(steamGames[0].Id);
        // }
        
        // Przykładowe uruchomienie gry EA
        // if (eaGames.Count > 0) {
        //     Console.WriteLine($"\nUruchamiam pierwszą grę EA: {eaGames[0].Name}");
        //     eaLibrary.LaunchGame(eaGames[0].Id);
        // }
        
        Console.WriteLine("\nNaciśnij Enter aby zakończyć...");
        Console.ReadLine();
    }
}
