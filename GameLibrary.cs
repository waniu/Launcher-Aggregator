using System.Collections.Generic;

public abstract class GameLibrary {
    public abstract string Name { get; }
    public abstract List<Game> GetInstalledGames();
    public abstract void LaunchGame(string gameId);
}
