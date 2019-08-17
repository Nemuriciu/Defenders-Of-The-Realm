using UnityEngine;

public static class Stats {
    /* Max Values */
    public const int MaxGold = 5000;
    public const int MaxEnergy = 750;
    public const int MaxArtefact = 2000;
    public static int maxTowers;
    public static int activeTowers;

    /* Game Stats Logs */
    public static int monstersKilled;
    public static int playerDamage;
    public static int towerDamage;
    public static int goldReceived;
    
    /* Player Values */
    public const int PlayerMinDamage = 30;
    public const int PlayerMaxDamage = 50;
    
    private static int _playerGold;
    public static int PlayerGold {
        get => _playerGold;
        set => _playerGold = value > MaxGold ? MaxGold : value;
    }
    
    private static int _playerEnergy = MaxEnergy;
    public static int PlayerEnergy {
        //get => _playerEnergy;
        set => _playerEnergy = value > MaxEnergy ? MaxEnergy : value;
    }

    private static int _artefactHealth;
    public static int ArtefactHealth {
        get => _artefactHealth;
        set => _artefactHealth = value < 0 ? 0 : value;
    }

    public static float savedTimeScale = Time.timeScale;
}