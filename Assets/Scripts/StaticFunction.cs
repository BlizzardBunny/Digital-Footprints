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
        "Off on a business trip! Gonna miss 3686 Chandler Drive for the first time!",
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
        "aSTEMGuineaP111g",
        "KnockingYourSocksOff@996",
        "tfGp7i@m@cttpgMda1c",
        "55osF@t!acag@p",
        "afs00th0mOTds@$",
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
    private static int totalErrors = 1; //errors to be found for this stage
    private static bool isChecking = false;
    private static int currFlag = -1;
    private static int profileNum = 0; //profiles completed
    private static int totalProfiles = 3;
    private static int mistakes = 0;
    private static int currentProfile = 0;
    private static int numOfReports = 0;
    private static string currentLevel = "Stage 1";



    public static int instanceCounter = 0; //number of times FlagSystem is run
    public static List<GameObject> reportEntries = new List<GameObject>();
    public static List<GameObject> mistakeMessages = new List<GameObject>();

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
    public static int getNumOfReports()
    {
        return numOfReports;
    }
    public static void setNumOfReports(int reports)
    {
        numOfReports = reports;
    }
}
