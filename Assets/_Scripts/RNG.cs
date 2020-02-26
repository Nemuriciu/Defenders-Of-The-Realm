using UnityEngine;

// ReSharper disable once InconsistentNaming
public static class RNG {
    public static int waveNr = 0;
    
    private static readonly int[] WaveCreatureNr = {100, 120, 0, 0, 0};

    /* Player Damage */
    private static readonly int[] PlayerDmgMin = {35, 75, 0, 0, 0};
    private static readonly int[] PlayerDmgMax = {40, 90, 0, 0, 0};

    /* Spawn Rate */
    private static readonly double[] FrogmanSpawnRate = {0.28, 0.24};
    private static readonly double[] RabbidSpawnRate = {0.26, 0.24};
    private static readonly double[] SuccubusSpawnRate = {0.24, 0.25};
    private static readonly double[] OgreSpawnRate = {0.18, 0.20,};
    private static readonly double[] TreantSpawnRate = {0.03, 0.045};
    private static readonly double[] GolemSpawnRate = {0.01, 0.025};

    /* CD Timer */
    private static readonly double[] FrogmanTimer = {5, 4.5};
    private static readonly double[] RabbidTimer = {5, 4.5};
    private static readonly double[] SuccubusTimer = {6.5, 6};
    private static readonly double[] OgreTimer = {7.5, 6.5};
    private static readonly double[] TreantTimer = {8, 7};
    private static readonly double[] GolemTimer = {8, 7};

    /* Health Modifier */
    private static readonly double[] FrogmanHealthModifier = {1, 1.5, 0, 0, 0};
    private static readonly double[] RabbidHealthModifier = {1, 1.5, 0, 0, 0};
    private static readonly double[] SuccubusHealthModifier = {1, 1.5, 0, 0, 0};
    private static readonly double[] OgreHealthModifier = {1, 1.5, 0, 0, 0};
    private static readonly double[] TreantHealthModifier = {1, 1.25, 0, 0, 0};
    private static readonly double[] GolemHealthModifier = {1, 1.25, 0, 0, 0};
    
    /* Speed Modifier */
    private static readonly double[] FrogmanSpeedModifier = {1, 1.1, 0, 0, 0};
    private static readonly double[] RabbidSpeedModifier = {1, 1.1, 0, 0, 0};
    private static readonly double[] SuccubusSpeedModifier = {1, 1.15, 0, 0, 0};
    private static readonly double[] OgreSpeedModifier = {1, 1.15, 0, 0, 0};
    private static readonly double[] TreantSpeedModifier = {1, 1.2, 0, 0, 0};
    private static readonly double[] GolemSpeedModifier = {1, 1.2, 0, 0, 0};

    /* Resist Rate */
    private static readonly double[][] FrogmanResist = {
        new[] {0.95, 0.025, 0.025},
        new[] {0.85, 0.075, 0.075}
    };
    private static readonly double[][] RabbidResist = {
        new[] {0.95, 0.025, 0.025},
        new[] {0.85, 0.075, 0.075}
    };
    private static readonly double[][] SuccubusResist = {
        new[] {1.0, 0.0, 0.0},
        new[] {0.9, 0.05, 0.05}
    };
    private static readonly double[][] OgreResist = {
        new[] {1.0, 0.0, 0.0},
        new[] {0.9, 0.05, 0.05}
    };
    private static readonly double[][] TreantResist = {
        new[] {1.0, 0.0, 0.0},
        new[] {0.93, 0.035, 0.035}
    };
    private static readonly double[] GolemResist = {0.5, 0.25, 0.25}; 
    
    /* Spawn Number Rate */
    private static readonly double[][] FrogmanNr = {
        new[] {0.925, 0.05, 0.025},
        new[] {0.865, 0.085, 0.05}
    };
    private static readonly double[][] RabbidNr = {
        new[] {0.925, 0.05, 0.025},
        new[] {0.865, 0.085, 0.05}
    };
    private static readonly double[][] SuccubusNr = {
        new[] {0.97, 0.03},
        new[] {0.925, 0.075}
    };
    private static readonly double[][] OgreNr = {
        new[] {0.97, 0.03},
        new[] {0.925, 0.075}
    };

    public static int GetPlayerDamage() {
        return Random.Range(PlayerDmgMin[waveNr], PlayerDmgMax[waveNr]);
    }

    public static float GetHealthModifier(string mob) {
        switch (mob) {
            case "Frogman":
                return (float)FrogmanHealthModifier[waveNr];
            case "Rabbid":
                return (float)RabbidHealthModifier[waveNr];
            case "Succubus":
                return (float)SuccubusHealthModifier[waveNr];
            case "Ogre":
                return (float)OgreHealthModifier[waveNr];
            case "Treant":
                return (float)TreantHealthModifier[waveNr];
            case "Golem":
                return (float)GolemHealthModifier[waveNr];
        }

        return 1;
    }
    
    public static float GetSpeedModifier(string mob) {
        switch (mob) {
            case "Frogman":
                return (float)FrogmanSpeedModifier[waveNr];
            case "Rabbid":
                return (float)RabbidSpeedModifier[waveNr];
            case "Succubus":
                return (float)SuccubusSpeedModifier[waveNr];
            case "Ogre":
                return (float)OgreSpeedModifier[waveNr];
            case "Treant":
                return (float)TreantSpeedModifier[waveNr];
            case "Golem":
                return (float)GolemSpeedModifier[waveNr];
        }

        return 1;
    }
    
    public static string GenerateMob() {
        double r = Random.Range(0.0f, 1.0f);
        double t = GolemSpawnRate[waveNr];

        /* Golem */
        if (r <= t)
            return "Golem";

        t += TreantSpawnRate[waveNr];

        /* Treant */
        if (r <= t)
            return "Treant";

        t += OgreSpawnRate[waveNr];

        /* Ogre */
        if (r <= t)
            return "Ogre";
        
        t += SuccubusSpawnRate[waveNr];

        /* Succubus */
        if (r <= t)
            return "Succubus";
        
        t += RabbidSpawnRate[waveNr];

        /* Rabbid */
        if (r <= t)
            return "Rabbid";
        
        t += FrogmanSpawnRate[waveNr];

        /* Frogman */
        if (r <= t)
            return "Frogman";

        return null;
    }

    public static int GenerateNr(string cType) {
        double r = Random.Range(0.0f, 1.0f);
        double t;
        
        switch (cType) {
            case "Frogman":
                t = FrogmanNr[waveNr][2];

                if (r <= t)
                    return 4;

                t += FrogmanNr[waveNr][1];

                if (r <= t)
                    return 3;
                else return 2;
            case "Rabbid":
                t = RabbidNr[waveNr][2];

                if (r <= t)
                    return 4;

                t += RabbidNr[waveNr][1];

                if (r <= t)
                    return 3;
                else return 2;
            case "Succubus":
                t = SuccubusNr[waveNr][1];

                if (r <= t)
                    return 2;
                else return 1;
            case "Ogre":
                t = OgreNr[waveNr][1];

                if (r <= t)
                    return 2;
                else return 1;
            
            default:
                return -1;
        }
    }

    public static string GenerateResist(string cType) {
        double r = Random.Range(0.0f, 1.0f);
        double t;
        
        switch (cType) {
            case "Frogman":
                t = FrogmanResist[waveNr][2];

                if (r <= t)
                    return "Magical";

                t += FrogmanResist[waveNr][1];

                if (r <= t)
                    return "Physical";
                else return "None";
            case "Rabbid":
                t = RabbidResist[waveNr][2];

                if (r <= t)
                    return "Magical";

                t += RabbidResist[waveNr][1];

                if (r <= t)
                    return "Physical";
                else return "None";
            case "Succubus":
                t = SuccubusResist[waveNr][2];

                if (r <= t)
                    return "Magical";

                t += SuccubusResist[waveNr][1];

                if (r <= t)
                    return "Physical";
                else return "None";
            case "Ogre":
                t = OgreResist[waveNr][2];

                if (r <= t)
                    return "Magical";

                t += OgreResist[waveNr][1];

                if (r <= t)
                    return "Physical";
                else return "None";
            case "Treant":
                t = TreantResist[waveNr][2];

                if (r <= t)
                    return "Magical";

                t += TreantResist[waveNr][1];

                if (r <= t)
                    return "Physical";
                else return "None";
            case "Golem":
                t = GolemResist[2];

                if (r <= t)
                    return "Magical_2";

                t += GolemResist[1];

                if (r <= t)
                    return "Magical_1";
                else return "Physical";
            
            default:
                return null;
        }
    }

    public static float GetTimer(string creature) {
        switch (creature) {
            case "Frogman":
                return (float)FrogmanTimer[waveNr];
            case "Rabbid":
                return (float)RabbidTimer[waveNr];
            case "Succubus":
                return (float)SuccubusTimer[waveNr];
            case "Ogre":
                return (float)OgreTimer[waveNr];
            case "Treant":
                return (float)TreantTimer[waveNr];
            case "Golem":
                return (float)GolemTimer[waveNr];
            
            default:
                return -1;
        }
    }

    public static int[] GenerateWaveCreatureNr() {
        var waveCreatureNr = new int[4];
        waveCreatureNr[0] = WaveCreatureNr[waveNr];

        /* Distribute 35%-40% of total creatures to portal_0 */
        float factor = Random.Range(0.35f, 0.40f);
        int amount = Mathf.RoundToInt(waveCreatureNr[0] * factor);
        waveCreatureNr[1] = amount;
        
        /* Split the rest evenly (40%-60%) between portal_1 and portal_2 */
        factor = Random.Range(0.4f, 0.6f);
        amount = Mathf.RoundToInt((WaveCreatureNr[0] - waveCreatureNr[1]) * factor);
        waveCreatureNr[2] = amount;
        waveCreatureNr[3] = waveCreatureNr[0] - (waveCreatureNr[1] + waveCreatureNr[2]);

        return waveCreatureNr;
    }
}