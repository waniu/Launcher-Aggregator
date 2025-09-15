using System;

public class Game {
    public string Id { get; set; }
    public string Name { get; set; }
    public string LibraryPath { get; set; }
    
    public Game(string id, string name, string libraryPath) {
        Id = id;
        Name = name;
        LibraryPath = libraryPath;
    }
    
    public override string ToString() {
        return $"{Id} - {Name}";
    }
}
