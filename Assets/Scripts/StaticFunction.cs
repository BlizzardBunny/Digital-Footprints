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
        "Just got my new credit card!!! #GOALS",
        "I love my car!!!",
        "New phone plannnnn!!! Hmu at 209-470-0522",
        "Doing some window shopping! Won’t be home until 5 :3",
        "Off on a business trip! Gonna be at 3686 Chandler Drive for the first time!",
        "My son’s finally enrolled! Enjoy school!!!",
        "Thanks @Ray for taking care of my home while I’m out!"
    };

    private static string[] captionFlags = new string[]
    {
        "Personal Information",
        "Location Data",
        "Location Data",
        "Personal Information",
        "Personal Information",
        "Personal Information",
        "Personal Information",
        "Personal Information",
        "Location Data",
        "Location Data",
        "Personal Information",
        "Location Data"
    };

    private static string[] goodCaptions = new string[]
    {
        "Memories of #Moby2019! Take me back to the pre-pandemic days….",
        " ",
        "Buy 1 take 1 promo on some Maccas!!! Get them while they’re hot!",
        "Craving for some egg tarts! #bestsnackever",
        " ",
        "Listening to Pink Floyd",
        "Amazing how far video games have gone!",
        " ",
        "Tsuki ga kireidesu ne",
        "Do you come from a land down under",
        "I <3 Pizza :>",
        "Just like the simulations"
    };

    private static string[] badAddress = new string[]
    {
        "319 Jadewood Drive, Chicago, Indiana",
        "236 Osler St, Toronto, Canada",
        "73 Lebanon St, Ibadan, Nigeria",
        "38 Riverlawn Drive, Cedar Falls, Iowa",
        "3967 Main Street, Redwood, Washington",
        "2A Snake Road, Los Angeles, California",
        "295 Karen Lane, Portland, Oregon",
        "273 Blind Bay Road, Revelstoke, Canada",
        "104 Bouverie Road, Westnewton, UK",
        "137 Front St, Hengoed, United Kingdom",
        "129 Settlement Road, Victoria, Australia",
        "95 Walder Crescent, Queensland, Australia",
        "111 Caxton Place, Byley, United Kingdom",
        "2235 Diamond St, Isabela, Puerto Rico",
        "4696 rue Saint-Antoine, Quebec, Canada"
    };

    private static string[] goodAddress = new string[]
    {
        "Chicago, Indiana",
        "Toronto, Canada",
        "Ibadan, Nigeria",
        "Cedar Falls, Iowa",
        "Redwood, Washington",
        "Los Angeles, California",
        "Portland, Oregon",
        "Revelstoke, Canada",
        "Westnewton, UK",
        "Hengoed, United Kingdom",
        "Victoria, Australia",
        "Queensland, Australia",
        "Byley, United Kingdom",
        "Isabela, Puerto Rico",
        "Quebec, Canada"
    };

    private static string[] names = new string[]
    {
        "Auntie Denise",
        "Bruno Bourdeaux",
        "Ekene Cuevas",
        "Destiny Stuart",
        "Karen Thompson",
        "Norma Lecter",
        "Anne Warren",
        "Cassie Klein",
        "Emma Larsen",
        "Fergus Bishop",
        "Isla McCall",
        "Leonie Sampson",
        "Lia Riggs",
        "Philip Ruiz",
        "Tommy English"
    };

    private static string[] bios = new string[]
    {
        "My motto in life is to Live Laugh Love as best as I can.",
        "Living that sweet life #YouOnlyLiveOnce #LiveLaughLove",
        "Have virus problem? Try my applet here www.LegitVirusRemover.com",
        "Walk as if you are kissing the Earth with your feet #MantraOfTheDay #SpirituallyPure",
        "46 Years young and single mom of 3. Always get what I want. #Feisty #IfYouCantHandleMeAtMyWorst",
        "Lover of all things snake/serpent related! Pls DM me if you have anything related to the best danger noodle<3 #NaginiDidNothingWrong",
        "Hot and dangerous.",
        "Namast’ay in bed.",
        "Making history.",
        "Life is what happens to you while you scroll through Photogram.",
        "Born to express, not impress.",
        "VVV Check out my life VVV",
        "I don’t look like this in real life.",
        "L<3VE is in the air.",
        "Spreading smiles."
    };

    private static string[] privacySettingsFlags = new string[]
    {
        "Messages",
        "Friend Requests",
        "Content Visibility",
        "Contact Details",
        "Contact Details",
        "Searchable Online"
    };

    private static string[] privacySettingsChoices = new string[]
    {
        "Message Me",
        "Add As Friend",
        "View Posts",
        "View Email",
        "View Phone#",
        "Search me Online"
    };

    private static string[] badPasswords = new string[]
    {
        "PasswordPassword",
        "[PROFILENAME]_2021",
        "[ADDRESS]21!",
        "Im-Gr8t",
        "IDONTWANTNEWPASSWORD",
        "FleaM@rketAddict%%",
        "DnKw$1",
        "@msDh@&3"
    };

    private static string[] passwordFlags = new string[]
    {
        "Complexity",
        "Personal Information",
        "Personal Information",
        "Password Length",
        "Complexity",
        "Complexity",
        "Password Length",
        "Password Length"
    };

    private static string[] goodPasswords = new string[]
    {
        "There’sNo1InTe@m",
        "OffOne’sBase#5561",
        "G00dyTwoShoe$$Lad",
        "aSTEMGuineaP!11g",
        "KnockingYourSocksOff@996",
        "tfGp7i@m@cttpgMda1c",
        "55osF@t!acag@p",
        "afs00th0mOTds@$",
    };

    private static int errorNum = 1;
    private static int totalErrors = 1; //errors to be found for this stage
    private static bool isChecking = false;
    private static int currFlag = -1;
    private static int profileNum = 0; //profiles completed
    private static int totalProfiles = 1;
    private static int mistakes = 0;
    private static int currentProfile = 0;
    private static string currentLevel = "AskDialogue";

    //flag system setup
    public static List<GameObject> clickables = new List<GameObject>();
    public static List<int> clickableIDs = new List<int>();
    public static List<int> flagIndexes = new List<int>();
    public static bool roundHasStarted = false;

    public static bool editableIsDrawn = false;
    public static int dialogueLineCounter = 0;
    public static int choiceIndex = 0;
    public static int instanceCounter = 0; //number of times FlagSystem is run
    public static List<GameObject> reportEntries = new List<GameObject>();
    public static List<GameObject> mistakeMessages = new List<GameObject>();
    public static bool tutorialStart = true;
    public static string relativeName = "Auntie";

    //for tutorial
    public static bool tutorialCanSubmit = false;

    //for tutorialplayer
    public static int dialogueIndex = 0;
    public static bool gotoLevelSelect = false;

    //for cutscenes
    public static string parentName = "";
    public static int flagIndex = -1;
    public static bool isFlag = false;
    public static bool reloadSameStage = false;

    //For the end of the game
    public static bool gameOver = false;

    public static string updateStrings(string s)
    {
        string ret = s;

        if (ret.Contains("$r")) //update string with relativeName
        {
            ret = ret.Replace("$r", relativeName);
        }

        return ret;
    }

    public static void resetVals(int numOfProfiles,int numOfErrors)
    {
        errorNum = numOfErrors;
        totalErrors = numOfErrors;
        isChecking = false;
        currFlag = -1;
        profileNum = 0;
        totalProfiles = numOfProfiles;
        mistakes = 0;
    }

    public static void reset()
    {
        errorNum = 1;
        totalErrors = 1;
        isChecking = false;
        currFlag = -1;
        profileNum = 0;
        totalProfiles = 1;
        mistakes = 0;
        currentProfile = 0;
        editableIsDrawn = false;
        choiceIndex = 0;
        instanceCounter = 0;
        reportEntries = new List<GameObject>();
        mistakeMessages = new List<GameObject>();
    }

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

    public static string[] getBadPasswords()
    {
        return badPasswords;
    }

    public static string[] getPasswordFlags()
    {
        return passwordFlags;
    }

    public static string[] getGoodPasswords()
    {
        return goodPasswords;
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

    public static string[] getPrivacySettingFlags()
    {
        return privacySettingsFlags;
    }

    public static string[] getPrivacySettingChoices()
    {
        return privacySettingsChoices;
    }

    public static int getErrorNum()
    {
        return errorNum;
    }

    public static void setErrorNum(int i)
    {
        errorNum = i;
    }

    public static int getTotalErrors()
    {
        return totalErrors;
    }

    public static void setTotalErrors(int i)
    {
        totalErrors = i;
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
    public static string getCurrentLevel()
    {
        return currentLevel;
    }
    public static void setCurrentLevel(string level)
    {
        currentLevel = level;
    }
    public static int getCurrentProfile()
    {
        return currentProfile;
    }
    public static void setCurrentProfile(int profile)
    {
        currentProfile = profile;
    }
    public static bool getGameOver()
    {
        return gameOver;
    }
    public static void setGameOver()
    {
        gameOver = true;
    }
}
