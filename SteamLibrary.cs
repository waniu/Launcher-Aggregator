using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class SteamLibrary : GameLibrary {
    private readonly string _steamPath;
    public override string Name => "Steam";
    
    public SteamLibrary(string steamPath = @"C:\Program Files (x86)\Steam") {
        _steamPath = steamPath;
    }
    
    public override List<Game> GetInstalledGames() {
        var games = new List<Game>();
        string libraryConfig = Path.Combine(_steamPath, "steamapps", "libraryfolders.vdf");
        
        if (!File.Exists(libraryConfig)) {
            Console.WriteLine($"Steam library config not found: {libraryConfig}");
            return games;
        }
        
        var libraries = GetLibraries(libraryConfig);
        
        foreach (var lib in libraries) {
            string steamappsPath = Path.Combine(lib, "steamapps");
            
            if (Directory.Exists(steamappsPath)) {
                string[] manifests = Directory.GetFiles(steamappsPath, "appmanifest_*.acf");
                
                foreach (string manifest in manifests) {
                    var (id, name) = ParseManifest(manifest);
                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name)) {
                        games.Add(new Game(id, name, lib));
                    }
                }
            }
        }
        
        return games;
    }
    
    public override void LaunchGame(string gameId) {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo($"steam://rungameid/{gameId}") 
        { 
            UseShellExecute = true 
        });
    }
    
    private string[] GetLibraries(string configPath) {
        string text = File.ReadAllText(configPath);
        var matches = Regex.Matches(text, @"\""path\""\s*\t\s*\""([^\""]+)\""");
        string[] libs = new string[matches.Count];
        
        for (int i = 0; i < matches.Count; i++) {
            libs[i] = matches[i].Groups[1].Value.Replace(@"\\", @"\");
        }
        
        return libs;
    }
    
    private (string id, string name) ParseManifest(string manifestPath) {
        string text = File.ReadAllText(manifestPath);
        var idMatch = Regex.Match(text, @"\""appid\""\s*\""(\d+)\""");
        var nameMatch = Regex.Match(text, @"\""name\""\s*\""([^\""]+)\""");
        
        return (idMatch.Groups[1].Value, nameMatch.Groups[1].Value);
    }
}
