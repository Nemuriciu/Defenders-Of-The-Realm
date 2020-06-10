using System.Collections;

// ReSharper disable once InconsistentNaming
public static class RNG {
    public static int waveNr = 0;
    
    private static readonly int[] CreatureNr = {100, 0, 0, 0, 0};

    /* Health Modifier */
    /*
    private static readonly double[] FrogmanHealthModifier = {1, 1.5, 2.5, 3.75, 5};
    private static readonly double[] RabbidHealthModifier = {1, 1.5, 2.5, 3.75, 5};
    private static readonly double[] SuccubusHealthModifier = {1, 1.5, 2.5, 3.25, 4};
    private static readonly double[] OgreHealthModifier = {1, 1.5, 2.5, 3.25, 4};
    private static readonly double[] TreantHealthModifier = {1, 1.25, 1.75, 2.5, 3.5};
    private static readonly double[] GolemHealthModifier = {1, 1.25, 1.75, 2.5, 3.5};
    */
    
    /* Gold Ration */
    private static readonly double[] SpiderlingGold = {.03, .025, .02, .015, .007};
    private static readonly double[] SkeletonGold = {.03, .025, .02, .015, .007};
    private static readonly double[] MageGold = {.03, .015, .01, .0075, .004};
    private static readonly double[] OrcGold = {.03, .0125, .0085, .007, .004};
    
    private static readonly double[] TurtleGold = {.03, .025, .02, .015, .007};
    private static readonly double[] BatGold = {.03, .025, .02, .015, .007};
    
    /* Resist Rate */
    /*
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
    */
    
    /*
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
    */
    public static float GoldRatio(string mob) {
        switch (mob) {
            case "Spiderling":
                return (float)SpiderlingGold[waveNr];
            case "Skeleton":
                return (float)SkeletonGold[waveNr];
            case "Mage":
                return (float)MageGold[waveNr];
            case "Orc":
                return (float)OrcGold[waveNr];
            case "Turtle":
                return (float)TurtleGold[waveNr];
            case "Bat":
                return (float)BatGold[waveNr];
        }

        return 1;
    }

    public static ArrayList WaveCreatureList() {
        ArrayList creatureList = new ArrayList();
        int spiderlingNr = 0, skeletonNr = 0, mageNr = 0,
            orcNr = 0, turtleNr = 0, batNr = 0;
        
        /* Static creature number/type depending on wave */
        switch (waveNr) {
            case 0:
                /* 50 per Portal */
                spiderlingNr = 13;
                turtleNr = 10;
                skeletonNr = 12;
                batNr = 8;
                mageNr = 5;
                orcNr = 2;
                break;
            
            /*case 1:
                spiderlingNr = 19;
                skeletonNr = 19;
                mageNr = 1;
                orcNr = 1;
                break;*/
        }
        
        for (int i = 0; i < spiderlingNr; i++)
            creatureList.Add("Spiderling");
        for (int i = 0; i < turtleNr; i++)
            creatureList.Add("Turtle");
        for (int i = 0; i < skeletonNr; i++)
            creatureList.Add("Skeleton");
        for (int i = 0; i < batNr; i++)
            creatureList.Add("Bat");
        for (int i = 0; i < mageNr; i++)
            creatureList.Add("Mage");
        for (int i = 0; i < orcNr; i++)
            creatureList.Add("Orc");

        return creatureList;
    }

    public static int WaveCreatureNr() {
        return CreatureNr[waveNr];
    }
}