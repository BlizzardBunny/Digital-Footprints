using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    public class Dialogue
    {
        public bool isPlayerSpeaking { get; }
        public bool isThought { get; }
        public string line { get; }
        public string sceneToTransitionTo { get; }
        public string[] choices { get; }
        public string[] answers { get; }

        public Dialogue(bool x, string z) => (isPlayerSpeaking, isThought, line) = (x, false, z);
        public Dialogue(bool x, bool y, string z) => (isPlayerSpeaking, isThought, line) = (x, y, z);
        public Dialogue(bool x, string[] a, string[] b) => (isPlayerSpeaking, choices, answers) = (x, a, b);
        public Dialogue(bool x, string y, string[] a, string[] b) => (isPlayerSpeaking, sceneToTransitionTo, choices, answers) = (x, y, a, b);
    }

    public Sprite[] profilePics = new Sprite[StaticFunction.getNames().Length];
    public Sprite relativePic; //profile pic of the player's relative
    public GameObject customerDetails;
    public GameObject overlay;
    public GameObject nextButton;

    public GameObject messagesPanel;
    public GameObject customerLinesPrefab;
    public GameObject playerLinesPrefab;
    public GameObject playerChoicesPrefab;

    private int currProfile;
    private GameObject currChoiceMenu;
    private Dialogue[] currDialogue;
    private int currLine;
    private Coroutine coroutineToBeStopped;
    private float characterTypingSpeed = 0.05f;

    private Dialogue[] introDialogue = new Dialogue[] //bool is true if player is speaking, string is the line to be said
    {
        new Dialogue(true, true, "So, a while back I got a message online from my $r."),
        new Dialogue(true, true, "It was weird... And it called me \"buddy\". \nI immediately knew the account was hacked."),
        new Dialogue(true, true, "After confirming it irl, we contacted Support to restore the account."),
        new Dialogue(true, true, "A few days later, this happened..."),
        new Dialogue(false, "Ok, I managed to recover my account." ),
        new Dialogue(false, "But I’m not entirely sure why I even got hacked in the first place. " ),
        new Dialogue(false, "Can you take a look and see if there’s any issues?" ),
        new Dialogue(true,
            new string[]{"No thanks. I don't really wanna do it today.", "Why? Can't you do it yourself?", "Sure thing, $r. Let me go to your account and check." },
            new string[]{"Well, I'm your $r. You're doing it.", "Well! I don't remember raising you to be that way. You're doing it.", "Thanks, dear! Love you!" }),
        new Dialogue(false, "Oh by the way, I'll be offline for a bit. So could you email me a full report and I'll get back to you?"),
        new Dialogue(true, "A full report??"),
        new Dialogue(false, "You'll be fine! Just download this Report App. It'll make the whole thing a breeze."),
        new Dialogue(true, "Tutorial",
            new string[]{"What?", "What.", "What-"},
            new string[]{"Good luck!", "Good luck!", "Good luck!"}),
        new Dialogue(false, "I just read your email! Thank you so much!" ),
        new Dialogue(false, "You know, I heard that there’s this new company specializing in these social media things." ),
        new Dialogue(false, "I think you can try applying to work for them, if you’re happy doing this for work." ),
        new Dialogue(true, "... This was a ploy to make me get a job, wasn't it?"),
        new Dialogue(false, "Seen."),
        new Dialogue(true, "That's not how seen-zoning works!!!"),
        new Dialogue(true, true, "And that's how I got into the Data Privacy business."),
    };

    //address
    private Dialogue[] goodAddress = new Dialogue[]
    {
        new Dialogue(true,
            new string[]{"Maybe you shouldn't be posting your address online.", "Are you sure it's safe to have your address on your profile?", "I think putting your address online is pretty irresponsible."},
            new string[]{"What do you mean? I didn't post my exact address...", "I'm pretty sure it's fine. I didn't put my exact address.", "*I* think it's fine as long as I don't give my exact address. People don't even know what street I live on!"}),
        new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{"I see! My mistake.", "You're right. I'm sorry.", "You're clearly wrong though."},
            new string[]{"It's no problem.", "It's alright. I'm just glad you're asking!", "You're clearly rude, and I'll be reporting you."})
    };

    private Dialogue[] badAddress = new Dialogue[]
    {
        new Dialogue(true,
            new string[]{"Maybe you shouldn't be posting your address online.", "Are you sure it's safe to have your address on your profile?", "I think putting your address online is pretty irresponsible."},
            new string[]{"Really? But then how would people know where to visit me?", "Is it really that bad? I just wanted the specialized ads.", "Excuse me!? I can do whatever I want, thank you very much!!"}),
        new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{"Showing your exact address online puts you at risk of identity theft, or worse, physical harm.", "You're right. Nevermind. I'm sorry.", "If you won't listen to me, why're you even here?"},
            new string[]{"Oh my god! I didn't know that! Please include it in your report so I don't forget to change it later.", "Whatever, I guess.", "(Your customer has left the chat)"})
    };

    //post
    private Dialogue[][] badPostDialogue = new Dialogue[][]
    {
        new Dialogue[]
        {
            new Dialogue(true,
            new string[]{"Congratulations on your new home! But I don't think it's wise to post a pic of your house number and street name.", "You do realize you technically just posted your address on a public post, right?", "You shouldn't have posted this picture of your house number."},
            new string[]{"Oh really? How so?", "Of course I do! I'm not stupid! I wanted to show off to my friends!", "What's wrong with showing off my new place to my online friends? I worked hard for this house, you know."}),
            new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{"Now anyone could just show up to your house whenever. Don't you think it's kind of a dumb move?", "It shows exactly where your house is and what it looks like, which is really dangerous.", "You know what. Nevermind."},
            new string[]{"Well you don't have to be so rude about it!!", "I never thought of it like that!! Please remind me to change it later.", "?????"})
        },
        new Dialogue[]
        {
            new Dialogue(true,
            new string[]{ "Good choice for your movie! But I don’t think it’s a good idea to let everyone know where you are tonight.", "You shouldn’t have posted a photo of your movie ticket. Now everyone will know where you are tonight.", "Are you sure about posting this? Won’t anyone be able to know where you are and when?"},
            new string[]{"Oh please! Everyone posts stuff like this all the time! Stop being paranoid!", "Uhm. That’s the point??? I WANT people to know I’m seeing this movie. People do it all the time.", "Yeah so? Doesn’t everyone do it?"}),
            new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{ "Even if everyone did do it, which they don’t, that doesn’t mean you should…", "People usually post *after* they’ve seen the movie, not before.", "It’s still dangerous. You could get hurt in real life."},
            new string[]{ "Whatevs!", "Whatevs!", "I never thought of it that way… That’s actually pretty scary. I’ll be sure to take it down."})
        },
        new Dialogue[]
        {
            new Dialogue(true,
            new string[]{ "Looks like you’re ready to play: LET’S GET ROBBED!!!", "You probably shouldn’t tell people you’re going on a trip BEFORE you go.", "I don’t think this post was the best choice."},
            new string[]{ "???? What’s THAT supposed to mean??", "What do you mean?", "Okay? Elaborate?"}),
            new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{ "You just let everyone know you won’t be home for 2 weeks… Leaving your house empty…", "It’s not safe to tell people when exactly you’re going to be leaving your house empty.", "You know what. Nevermind."},
            new string[]{ "OMG!!! That’s not my intention! I’ll fix it ASAP!", "OMG!!! You’re right! I’ll fix it ASAP!", "????????????????"})
        },
        new Dialogue[]
        {
            new Dialogue(true,
            new string[]{ "Why in the world would you post your own license???", "Was it really wise to post your drivers’ license without censoring your details?", "You shouldn’t have posted your license online."},
            new string[]{ "Oh you saw? Pretty neat huh?", "Of course it is! I covered my face!", "Why not!? Aren’t you just being paranoid?"}),
            new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{ "No! No! No! That’s NOT what you’re supposed to post!", "People can still clearly see your details and use them for identity theft.", "Sure… You’re right. Nevermind."},
            new string[]{ "Ok! Ok! I get it! I’ll fix it later!", "OH… Okay. You got me.", "Stop wasting my time."})
        },
        new Dialogue[]
        {
            new Dialogue(true,
            new string[]{ "Why in the world would you actually share those useless quizzes????", 
                "It’s great that you think that those quizzes are fun. But maybe you should avoid the ones that ask for your personal info?", 
                "You shouldn’t have posted this."},
            new string[]{ "Hey! They’re fun and harmless! Why wouldn’t I?",
                "But they need it to save my data! That’s harmless, isn’t it?",
                "???? Elaborate."}),
            new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{ "Seriously?? You just gave them your email address and phone number! That’s in no way harmless!",
                "They can use your data for identity theft.",
                "Not only are you giving away your own data, you’re asking other people to share your data too!! That’s anything but harmless!"},
            new string[]{ "Okokokokok fine! I know better now.",
                "Identity theft!? OMG. I better fix that asap!",
                "Alright, fine!! You don’t have to be rude about it."})
        },
        new Dialogue[]
        {
            new Dialogue(true,
            new string[]{ "What’s wrong with you???????????",
                "What do you think about this post?",
                "You shouldn’t have posted this."},
            new string[]{ "Excuse me???",
                "Oh you mean my credit card? Jealous?",
                "Why not!? I’m pretty proud of this credit card you know!"}),
            new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{ "Never before have I seen someone post their literal credit card online… The CVC’s only 3 digits, people can guess that and use it you know….",
                "I think you should check your bank account…",
                "Anyone can use your card details to steal money from your account."},
            new string[]{ "Let them try! They’re just jelly of my finances hahahaha!",
                "IT’S EMPTY!!!??? I bet YOU had something to do with this!! I’m reporting you to the police!!!",
                "What do you know!? I told my parents this was a waste of time."})
        },
        new Dialogue[]
        {
            new Dialogue(true,
            new string[]{ "Congrats on your new car! But maybe blur out your plate number off this post?",
                "You shouldn’t have posted this.",
                "Why did you post this?"},
            new string[]{ "Why? What can people do with my plate number?",
                "Says you! You’re just jealous of my new car!",
                "Because I wanted to show everyone my new car!"}),
            new Dialogue(true, StaticFunction.getCurrentLevel(),
            new string[]{ "People can use your plate number to find more information about you, like your full name and address.",
                "…Nevermind.",
                "If people know your plate number, you’re more likely to believe their scams!"},
            new string[]{ "Pfft. You’re just paranoid.",
                "Don’t waste my time again.",
                "HAH! You just think I’d be that stupid!"})
        },
        //"New phone plannnnn!!! Hmu at 209-470-0522",
        //"Doing some window shopping! Won’t be home until 5 :3",
        //"Off on a business trip! Gonna be at 3686 Chandler Drive for the first time!",
        //"My son’s finally enrolled! Enjoy school!!!",
        //"Thanks @Ray for taking care of my home while I’m out!"
    };

    private Dialogue[][] goodPostDialogue = new Dialogue[][]
    {
        
    };

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        overlay.transform.Find("Blackscreen").GetComponent<Animator>().SetBool("isFadingIn", true);

        currProfile = StaticFunction.getCurrentProfile();

        //setup customer details
        Transform pic = customerDetails.transform.Find("Pic");
        Transform name = customerDetails.transform.Find("Name");

        if (StaticFunction.tutorialStart)
        {
            pic.GetComponent<Image>().sprite = relativePic;
            name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.relativeName;
            StaticFunction.reset();
            StartCoroutine(run(introDialogue));
        }
        else
        {
            pic.GetComponent<Image>().sprite = profilePics[currProfile];
            name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[currProfile];
            StartCoroutine(run(getCorrectDialogue()));
        }
    }

    Dialogue[] getCorrectDialogue()
    {
        StaticFunction.reloadSameStage = true;

        if (StaticFunction.parentName.Equals("Address"))
        {
            if (StaticFunction.isFlag)
            {
                return badAddress;
            }
            else
            {
                return goodAddress;
            }
        }
        else if (StaticFunction.parentName.StartsWith("Post"))
        {
            if (StaticFunction.isFlag)
            {
                return badPostDialogue[StaticFunction.flagIndex];
            }
            else
            {
                return goodPostDialogue[StaticFunction.flagIndex];
            }
        }
        else if (StaticFunction.parentName.Equals("PrivacyWindow"))
        {
            if (StaticFunction.isFlag)
            {

            }
            else
            {

            }
        }
        else if (StaticFunction.parentName.Equals("Password"))
        {
            if (StaticFunction.isFlag)
            {

            }
            else
            {

            }
        }

        return null;
    }

    IEnumerator run(Dialogue[] dialogue)
    {
        currDialogue = dialogue;

        if (coroutineToBeStopped != null)
        {
            StopCoroutine(coroutineToBeStopped);
        }

        for (int i = StaticFunction.dialogueLineCounter; i < dialogue.Length; i++)
        {
            Debug.Log(i);
            if (dialogue[i].isPlayerSpeaking)
            {
                if (dialogue[i].isThought)
                {
                    if ((i > 0) && (!dialogue[i - 1].isThought))
                    {
                        yield return new WaitForSeconds(3f);
                    }

                    overlay.transform.SetAsLastSibling();

                    Transform thoughts = overlay.transform.Find("Thoughts");
                    thoughts.GetComponent<TMPro.TextMeshProUGUI>().text = "";

                    if (i == 0)
                    {
                        yield return new WaitForSeconds(1f);
                    }

                    string line = StaticFunction.updateStrings(dialogue[i].line);

                    for (int j = 0; j < line.Length; j++)
                    {
                        yield return new WaitForSeconds(characterTypingSpeed);
                        thoughts.GetComponent<TMPro.TextMeshProUGUI>().text += line[j]; 

                        if (Input.GetMouseButton(0))
                        {
                            thoughts.GetComponent<TMPro.TextMeshProUGUI>().text += line.Substring(j+1);
                            yield return new WaitForSeconds(0.5f);
                            break;
                        }
                    }

                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                }
                else if (dialogue[i].choices != null)
                {
                    overlay.transform.SetAsFirstSibling();
                    currLine = i;

                    if (dialogue[i].sceneToTransitionTo != null)
                    {
                        StaticFunction.setCurrentLevel("Tutorial");
                    }

                    yield return StartCoroutine(showChoices(dialogue[i].choices));
                }
                else
                {
                    overlay.transform.SetAsFirstSibling();
                    yield return new WaitForSeconds(3f);
                    GameObject playerMessage = Instantiate(
                        playerLinesPrefab,
                        new Vector3(959.9981079101563f, 198.29241943359376f,0.0f),
                        Quaternion.identity,
                        messagesPanel.transform.parent);

                    playerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.updateStrings(dialogue[i].line);

                    yield return new WaitForFixedUpdate(); //wait for playerMessage to update size acc to text
                    yield return new WaitForFixedUpdate(); //skip frame

                    RectTransform bg = playerMessage.transform.Find("Background").GetComponent<RectTransform>();
                    RectTransform area = playerMessage.transform.GetComponent<RectTransform>();

                    if (bg.sizeDelta.y > area.sizeDelta.y)
                    {
                        playerMessage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 10f);
                    }

                    yield return new WaitForFixedUpdate(); //wait for playerMessage to update size
                    yield return new WaitForFixedUpdate(); //skip frame

                    playerMessage.transform.SetParent(messagesPanel.transform);
                }
            }
            else
            {
                overlay.transform.SetAsFirstSibling();
                yield return StartCoroutine(showTypingStatus());
                GameObject customerMessage = Instantiate(
                    customerLinesPrefab,
                    new Vector3(959.9981079101563f,195.107421875f,0.0f),
                    Quaternion.identity,
                    messagesPanel.transform.parent);

                customerMessage.transform.Find("Pic").GetComponent<Image>().sprite = profilePics[StaticFunction.getCurrentProfile()];
                customerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.updateStrings(dialogue[i].line);
                
                if (dialogue[i].line.StartsWith("("))
                {
                    customerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Italic;
                }

                yield return new WaitForFixedUpdate(); //wait for Background to update size acc to text
                yield return new WaitForFixedUpdate(); //skip frame

                RectTransform bg = customerMessage.transform.Find("Background").GetComponent<RectTransform>();
                RectTransform area = customerMessage.transform.GetComponent<RectTransform>();

                if (bg.sizeDelta.y > area.sizeDelta.y)
                {
                    customerMessage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 10f);
                }

                yield return new WaitForFixedUpdate(); //wait for customerMessage to update size
                yield return new WaitForFixedUpdate(); //skip frame

                customerMessage.transform.SetParent(messagesPanel.transform);
            }       

            StaticFunction.dialogueLineCounter++;
        }

        if (StaticFunction.tutorialStart)
        {
            StaticFunction.setCurrentLevel("Stage 1");
        }

        StaticFunction.tutorialStart = false;
        StaticFunction.dialogueLineCounter = 0;
        yield return StartCoroutine(fadeOut(StaticFunction.getCurrentLevel()));
    }

    IEnumerator fadeOut(string level)
    {

        overlay.transform.SetAsLastSibling();
        overlay.transform.Find("Thoughts").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        overlay.transform.Find("Blackscreen").GetComponent<Animator>().SetBool("isFadingIn", false);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(level);
    }

    IEnumerator showTypingStatus()
    {
        customerDetails.transform.Find("Status").GetComponent<TMPro.TextMeshProUGUI>().text = "typing...";
        yield return new WaitForSeconds(3f);
        customerDetails.transform.Find("Status").GetComponent<TMPro.TextMeshProUGUI>().text = "online";
    }

    IEnumerator showChoices(string[] choices)
    {
        currChoiceMenu = Instantiate(
            playerChoicesPrefab,
            new Vector3(959.9981079101563f, 236.2274169921875f, 0.0f),
            Quaternion.identity,
            messagesPanel.transform.parent);

        for (int i = 1; i - 1 < choices.Length; i++)
        {
            currChoiceMenu.transform.Find("Background").Find("Choice " + i).Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.updateStrings(choices[i - 1]);
            yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
            yield return new WaitForFixedUpdate(); //skip frame
        }

        RectTransform bg = currChoiceMenu.transform.Find("Background").GetComponent<RectTransform>();
        RectTransform area = currChoiceMenu.transform.GetComponent<RectTransform>();

        yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame
        yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame
        yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame


        if (bg.sizeDelta.y > area.sizeDelta.y)
        {
            currChoiceMenu.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 40f);
        }

        yield return new WaitForFixedUpdate(); //wait for currChoiceMenu to update size
        yield return new WaitForFixedUpdate(); //skip frame
        yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame
        yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame

        currChoiceMenu.transform.SetParent(messagesPanel.transform);

        StopAllCoroutines();
    }

    IEnumerator showPlayerMessage()
    {
        GameObject playerMessage = Instantiate(
            playerLinesPrefab,
            new Vector3(959.9981079101563f, 198.29241943359376f, 0.0f),
            Quaternion.identity,
            messagesPanel.transform.parent);

        playerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.updateStrings(currDialogue[currLine].choices[StaticFunction.choiceIndex]);

        yield return new WaitForFixedUpdate(); //wait for Background to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame

        RectTransform bg = playerMessage.transform.Find("Background").GetComponent<RectTransform>();
        RectTransform area = playerMessage.transform.GetComponent<RectTransform>();

        if (bg.sizeDelta.y > area.sizeDelta.y)
        {
            playerMessage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 10f);
        }

        yield return new WaitForFixedUpdate(); //wait for playerMessage to update size
        yield return new WaitForFixedUpdate(); //skip frame

        playerMessage.transform.SetParent(messagesPanel.transform);

        yield return StartCoroutine(showTypingStatus());
        GameObject customerMessage = Instantiate(
            customerLinesPrefab,
            new Vector3(959.9981079101563f, 195.107421875f, 0.0f),
            Quaternion.identity,
            messagesPanel.transform.parent);

        customerMessage.transform.Find("Pic").GetComponent<Image>().sprite = profilePics[StaticFunction.getCurrentProfile()];
        customerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.updateStrings(currDialogue[currLine].answers[StaticFunction.choiceIndex]);

        if (currDialogue[currLine].answers[StaticFunction.choiceIndex].StartsWith("("))
        {
            customerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Italic;
        }

        yield return new WaitForFixedUpdate(); //wait for Background to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame

        bg = customerMessage.transform.Find("Background").GetComponent<RectTransform>();
        area = customerMessage.transform.GetComponent<RectTransform>();

        if (bg.sizeDelta.y > area.sizeDelta.y)
        {
            customerMessage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 10f);
        }

        yield return new WaitForFixedUpdate(); //wait for customerMessage to update size
        yield return new WaitForFixedUpdate(); //skip frame

        customerMessage.transform.SetParent(messagesPanel.transform);

        if (currDialogue[currLine].sceneToTransitionTo != null)
        {
            StaticFunction.dialogueLineCounter++;
            StaticFunction.setCurrentLevel(currDialogue[currLine].sceneToTransitionTo);
            nextButton.transform.SetAsLastSibling();
            StopAllCoroutines();
        }
        else
        {
            StaticFunction.dialogueLineCounter++;
            StartCoroutine(run(currDialogue));
        }
    }

    public void acceptChoice()
    {
        Destroy(currChoiceMenu);

        coroutineToBeStopped = StartCoroutine(showPlayerMessage());
    }

    public void nextScene()
    {
        StartCoroutine(fadeOut(currDialogue[currLine].sceneToTransitionTo));
    }

    private void Update()
    {
        transform.SetAsLastSibling();
    }
}


