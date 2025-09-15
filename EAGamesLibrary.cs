using System;
using System.Collections.Generic;
using System.IO;

public class EAGamesLibrary : GameLibrary {
    private readonly string _eaPath;
    
    public override string Name => "EA Games";
    
    public EAGamesLibrary(string eaPath = @"C:\Program Files\Electronic Arts\EA Desktop\EA Desktop") {
        _eaPath = eaPath;
    }
    
    public override List<Game> GetInstalledGames() {
        var games = new List<Game>();
        
        // EA Desktop przechowuje gry w różnych lokalizacjach
        // Sprawdzamy typowe ścieżki
        string[] possiblePaths = {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "EA Games"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "EA Games"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Origin", "Games"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EA Games", "EA Desktop", "Games")
        };
        
        foreach (string basePath in possiblePaths) {
            if (Directory.Exists(basePath)) {
                ScanDirectoryForGames(basePath, games);
            }
        }
        
        return games;
    }
    
    public override void LaunchGame(string gameId) {
        // EA Desktop używa protokołu origin://
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo($"origin://launchgame/{gameId}") 
        { 
            UseShellExecute = true 
        });
    }
    
    private void ScanDirectoryForGames(string directory, List<Game> games) {
        try {
            var subdirs = Directory.GetDirectories(directory);
            
            foreach (string subdir in subdirs) {
                string dirName = Path.GetFileName(subdir);
                
                // Sprawdzamy czy to katalog gry (zawiera pliki wykonywalne)
                string[] exeFiles = Directory.GetFiles(subdir, "*.exe", SearchOption.AllDirectories);
                
                if (exeFiles.Length > 0) {
                    // Używamy nazwy katalogu jako ID i nazwy gry
                    string gameId = dirName.Replace(" ", "").ToLower();
                    games.Add(new Game(gameId, dirName, subdir));
                }
                
                // Rekurencyjnie skanujemy podkatalogi
                ScanDirectoryForGames(subdir, games);
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error scanning directory {directory}: {ex.Message}");
        }
    }
}


