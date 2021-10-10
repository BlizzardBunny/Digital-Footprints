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
        "73 Lebanon St. Ibadan, Nigeria",
        "38 Riverlawn Drive, Cedar Falls, Iowa",
        "3967 Main Street, Redwood, Washington",
        "2A Snake Road, Los Angeles, California"
    };

    private static string[] goodAddress = new string[]
    {
        "Toronto, Canada",
        "Ibadan, Nigeria",
        "Cedar Falls, Iowa",
        "Redwood, Washington",
        "Los Angeles, California"
    };

    private static string[] names = new string[]
    {
        "Bruno Bourdeaux",
        "Ekene Cuevas",
        "Destiny Stuart",
        "Karen Thompson",
        "Norma Lecter"
    };

    private static string[] bios = new string[]
    {
        "Living that sweet life #YouOnlyLiveOnce #LiveLaughLove",
        "Have virus problem? Try my applet here www.LegitVirusRemover.com",
        "Walk as if you are kissing the Earth with your feet #MantraOfTheDay #SpirituallyPure",
        "46 Years young and single mom of 3. Always get what I want. #Feisty #IfYouCantHandleMeAtMyWorst",
        "Lover of all things snake/serpent related! Pls DM me if you have anything related to the best danger noodle<3 #NaginiDidNothingWrong"
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

    public static string[] getNames()
    {
        return names;
    }

    public static string[] getBios()
    {
        return bios;
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
