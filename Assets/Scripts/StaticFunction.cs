using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunction
{
    private static string[] badCaptions = new string[]
    {
        "My new home! #Goals #HomeOwner",
        "Off to watch a movie at 8 PM! Gonna be a relaxing afternoon! #SelfCare #MovieNight",
        "Heading to Tokyo for a 2 week vacation! #SushiTime #R&R",
        "Finally got my Driver’s License! #LifeIsAHighway #BeepBeep",
        "Check out Globalquiz.org for free and fun quizzes! Just need email and phone number!",
        "Just got my new credit card!!! #GOALS"
    };

    private static string[] captionFlags = new string[]
    {
        "Personal Information",
        "Location Data",
        "Location Data",
        "Personal Information",
        "Personal Information",
        "Personal Information"
    };

    private static string[] goodCaptions = new string[]
    {
        "Memories of #Moby2019! Take me back to the pre-pandemic days….",
        " ",
        "Buy 1 take 1 promo on some Maccas!!! Get them while they’re hot!",
        "Craving for some egg tarts! #bestsnackever",
        "School life is hardddddddd :(((( Wish I could fast forward to summer"
    };

    private static string[] badAddress = new string[]
    {
        "236 Osler St Toronto, Canada",
        "59 Lbiam St Enugu, Nigeria",
        "7989 Walnut St Cedar Falls, Iowa"
    };

    private static string[] goodAddress = new string[]
    {
        "Toronto, Canada",
        "The Internets",
        "Cedar Falls, Iowa"
    };

    private static int errorNum = 1;
    private static int post1Index;

    public static string[] getBadCaptions()
    {
        return badCaptions;
    }

    public static string[] getCaptionFlags()
    {
        return captionFlags;
    }

    public static string[] getGoodCaptions()
    {
        return goodCaptions;
    }

    public static string[] getBadAddress()
    {
        return badAddress;
    }

    public static string[] getGoodAddress()
    {
        return goodAddress;
    }

    public static int getErrorNum()
    {
        return errorNum;
    }

    public static void setErrorNum(int i)
    {
        errorNum = i;
    }

    public static int getPost1Index()
    {
        return post1Index;
    }

    public static void setPost1Index(int i)
    {
        post1Index = i;
    }
}
