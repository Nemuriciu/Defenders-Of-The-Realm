public static class Stats {
    public const int MaxGold = 1000;
    public const int MaxEnergy = 750;
    public const int MaxArtefact = 2000;
    
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
}