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
        " "
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

    private static string[] tutorialDialogue = new string[]
    {
        "Greetings, and welcome to Digital Footprints, where your privacy is our priority.",
        "For your first day, we would be providing you with some dummy accounts to work on. Use this time to familiarize yourself with the company’s software and workflow.",
        "You will be looking for privacy issues. What privacy issues are is covered by our Company Standards widget on the right.",
        "If you see anything that looks like a privacy issue, click on it and our system would flag it as such.",
        "This would then be reflected in the client’s report where you can double check what you have flagged. Make sure to click send to confirm your flag.",
        "If you are satisfied with your work, click SUBMIT and the client would be notified of your recommendations.",
        "Do be warned that while you are free to make mistakes on the dummy account, doing so with a real customer’s account would warrant a penalty.",
        "We hope that your time with us will be informative and fulfilling, good luck!"
    };

    private static string[] roundMessagesPerfect = new string[]
    {
        "Excellent, it seems that you fully grasp how our system works here at Digital Footprints.",
		"Do be reminded that tomorrow, you will be provided with real accounts and as such, mistakes will be penalized."
    };

    private static string[] roundMessagesGood = new string[]
    {
        "While you did make some mistakes here and there, I hope that this did help you understand how our system works.",
		"Do be reminded that tomorrow, you will be provided with real accounts and as such, mistakes will be penalized."
    };

    private static string[] roundMessagesBad = new string[]
    {
        "I strongly recommend reviewing today’s work to understand what went wrong.",
		"Do remember that tomorrow, you will be provided with real accounts. Mistakes of this level would not be tolerated."
    };

    private static int errorNum = 1;
    private static bool isChecking = false;
    private static int currFlag = -1;
    private static int profileNum = 0; //profiles completed
    private static int totalProfiles = 3;
    private static int mistakes = 0;

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

    public static string[] getTutorial()
    {
        return tutorialDialogue;
    }

    public static string[] getPerfectDialogue()
    {
        return roundMessagesPerfect;
    }

    public static string[] getGoodDialogue()
    {
        return roundMessagesGood;
    }

    public static string[] getBadDialogue()
    {
        return roundMessagesBad;
    }

    public static int getErrorNum()
    {
        return errorNum;
    }

    public static void setErrorNum(int i)
    {
        errorNum = i;
    }

    public static bool getIsChecking()
    {
        return isChecking;
    }

    public static void setIsChecking(bool b)
    {
        isChecking = b;
    }

    public static int getCurrFlag()
    {
        return currFlag;
    }

    public static void setCurrFlag(int i)
    {
        currFlag = i;
    }

    public static int getProfileNum()
    {
        return profileNum;
    }

    public static void setProfileNum(int i)
    {
        profileNum = i;
    }

    public static int getTotalProfiles()
    {
        return totalProfiles;
    }

    public static void setTotalProfiles(int i)
    {
        totalProfiles = i;
    }

    public static int getMistakes()
    {
        return mistakes;
    }

    public static void setMistakes(int i)
    {
        mistakes = i;
    }
}
