using UnityEngine;

// ReSharper disable once InconsistentNaming
public static class RNG {
    public static int waveNr = 0;
    
    private static readonly int[] WaveCreatureNr = {80, 100, 110, 120, 140};

    /* Player Damage */
    private static readonly int[] PlayerDmgMin = {30, 50, 75, 120, 175};
    private static readonly int[] PlayerDmgMax = {35, 60, 80, 135, 200};

    /* Spawn Rate */
    private static readonly double[] FrogmanSpawnRate = {.28, .25, .23, .2, .2};
    private static readonly double[] RabbidSpawnRate = {.28, .25, .23, .2, .2};
    private static readonly double[] SuccubusSpawnRate = {.24, .245, .22, .225, .2};
    private static readonly double[] OgreSpawnRate = {.175, .19, .22, .225, .2};
    private static readonly double[] TreantSpawnRate = {.02, .04, .06, .075, .1};
    private static readonly double[] GolemSpawnRate = {.005, .025, .04, .075, .1};

    /* CD Timer */
    private static readonly double[] FrogmanTimer = {6, 6, 5, 5, 4.5};
    private static readonly double[] RabbidTimer = {6, 6, 5, 5, 4.5};
    private static readonly double[] SuccubusTimer = {7, 7, 6, 6, 5};
    private static readonly double[] OgreTimer = {7, 7, 6, 6, 5};
    private static readonly double[] TreantTimer = {8, 7, 6.5, 6, 5};
    private static readonly double[] GolemTimer = {8, 7, 6.5, 6, 5};

    /* Health Modifier */
    private static readonly double[] FrogmanHealthModifier = {1, 1.5, 2.5, 3.75, 5};
    private static readonly double[] RabbidHealthModifier = {1, 1.5, 2.5, 3.75, 5};
    private static readonly double[] SuccubusHealthModifier = {1, 1.5, 2.5, 3.25, 4};
    private static readonly double[] OgreHealthModifier = {1, 1.5, 2.5, 3.25, 4};
    private static readonly double[] TreantHealthModifier = {1, 1.25, 1.75, 2.5, 3.5};
    private static readonly double[] GolemHealthModifier = {1, 1.25, 1.75, 2.5, 3.5};
    
    /* Gold Ration */
    private static readonly double[] FrogmanGoldRatio = {.025, .025, .02, .015, .007};
    private static readonly double[] RabbidGoldRatio = {.025, .025, .02, .015, .007};
    private static readonly double[] SuccubusGoldRatio = {.015, .015, .01, .0075, .004};
    private static readonly double[] OgreGoldRatio = {.0125, .0125, .0085, .007, .004};
    private static readonly double[] TreantGoldRatio = {.0075, .0075, .007, .005, .002};
    private static readonly double[] GolemGoldRatio = {.01, .01, .0075, .007, .004};
    
    /* Resist Rate */
    private static readonly double[][] FrogmanResist = {
        new[] {.95, .025, .025},
        new[] {.85, .075, .075},
        new []{.8, .1, .1},
        new []{.7, .15, .15},
        new []{.5, .25, .25}
    };
    private static readonly double[][] RabbidResist = {
        new[] {0.95, 0.025, 0.025},
        new[] {0.85, 0.075, 0.075},
        new []{.8, .1, .1},
        new []{.7, .15, .15},
        new []{.5, .25, .25}
    };
    private static readonly double[][] SuccubusResist = {
        new[] {1.0, 0.0, 0.0},
        new[] {0.9, 0.05, 0.05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.6, .2, .2}
    };
    private static readonly double[][] OgreResist = {
        new[] {1.0, 0.0, 0.0},
        new[] {0.9, 0.05, 0.05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.6, .2, .2}
    };
    private static readonly double[][] TreantResist = {
        new[] {1.0, 0.0, 0.0},
        new[] {0.93, 0.035, 0.035},
        new []{.9, .05, .05},
        new []{.85, .075, .075},
        new []{.7, .15, .15}
    };
    private static readonly double[] GolemResist = {0.5, 0.25, 0.25}; 
    
    /* Spawn Number Rate */
    private static readonly double[][] FrogmanNr = {
        new[] {.925, .05, .025},
        new[] {.865, .085, .05},
        new []{.83, .1, .07},
        new []{.75, .15, .1},
        new []{.4, .35, .25}
    };
    private static readonly double[][] RabbidNr = {
        new[] {.925, .05, .025},
        new[] {.865, .085, .05},
        new []{.83, .1, .07},
        new []{.75, .15, .1},
        new []{.4, .35, .25}
    };
    private static readonly double[][] SuccubusNr = {
        new[] {.97, .03},
        new[] {.925, .075},
        new []{.9, .1},
        new []{.85, .15},
        new []{.7, .3}
    };
    private static readonly double[][] OgreNr = {
        new[] {.97, .03},
        new[] {.925, .075},
        new []{.9, .1},
        new []{.85, .15},
        new []{.7, .3}
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
    
    public static float GetGoldRatio(string mob) {
        switch (mob) {
            case "Frogman":
                return (float)FrogmanGoldRatio[waveNr];
            case "Rabbid":
                return (float)RabbidGoldRatio[waveNr];
            case "Succubus":
                return (float)SuccubusGoldRatio[waveNr];
            case "Ogre":
                return (float)OgreGoldRatio[waveNr];
            case "Treant":
                return (float)TreantGoldRatio[waveNr];
            case "Golem":
                return (float)GolemGoldRatio[waveNr];
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
        
        /* Split the rest evenly (45%-55%) between portal_1 and portal_2 */
        factor = Random.Range(0.45f, 0.55f);
        amount = Mathf.RoundToInt((waveCreatureNr[0] - waveCreatureNr[1]) * factor);
        waveCreatureNr[2] = amount;
        waveCreatureNr[3] = waveCreatureNr[0] - (waveCreatureNr[1] + waveCreatureNr[2]);

        return waveCreatureNr;
    }
}