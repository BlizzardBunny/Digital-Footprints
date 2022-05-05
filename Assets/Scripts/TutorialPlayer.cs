using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPlayer : MonoBehaviour
{
    public Transform overlay;
    public GameObject dialoguePrefab;
    public GameObject pointerPrefab;

    private GameObject world;
    private GameObject pointersPanel;
    private GameObject dialogue;
    private float typingSpeed = 0.03f;
    private GameObject pointer;

    public class Dialogue
    {
        public string line { get; }
        public bool allowPlayerControl { get; }
        public string pointerInfo { get; }
        public Vector3 pointerLocation { get; }

        public Dialogue(string x) => (line, allowPlayerControl) = (x, false);
        public Dialogue(string x, bool y) => (line, allowPlayerControl) = (x, y);
        public Dialogue(string x, string z, Vector3 v) => (line, pointerInfo, pointerLocation) = (x, z, v);
    }

    private Dialogue[] stageOne = new Dialogue[]
    {
        new Dialogue("Greetings, and welcome to Digital Footprints, where your privacy is our priority."),
        new Dialogue("For your first day, we will be letting you handle low-priority customers."),
        new Dialogue("These are customers who only allow us to look through their public profiles."),
        new Dialogue("Please use this time to familiarize yourself with the company’s software and workflow."),
        new Dialogue("If you have any questions to ask the client, you may right-click the item of concern and talk to them about it."),
        new Dialogue("Do be warned that sending false information to the customer will warrant a penalty."),
        new Dialogue("So make sure to double-check your report before you [SUBMIT]."),
        new Dialogue("We hope that your time with us will be informative and fulfilling, good luck!", true)
    };
    
    private Dialogue[] stageTwo = new Dialogue[]
    {
        new Dialogue("Hello, again! It seems like you're doing really well in this company."),
        new Dialogue("So we've decided to let you handle medium-priority customers."),
        new Dialogue("These are customers who allow us to look into their privacy settings."),
        new Dialogue("More details on proper Privacy Settings can be found in your Company Standards."),
        new Dialogue("Also, you now have access to more social media sites. Make sure to check everything on both sites!",
            "You now have access to: CHIRPER!",
            new Vector3(908.0f,267.70001220703127f,0.0f)),
        new Dialogue("As always, make sure to double-check your report before you [SUBMIT]."),
        new Dialogue("Good luck!", true)
    };

    private Dialogue[] stageThree = new Dialogue[]
    {
        new Dialogue("Great news! You've been doing so well in this company that we've decided to give you access to high-priority customers."),
        new Dialogue("These are customers who allow us to look into their passwords."),
        new Dialogue("It's a good thing we had you sign that NDA before joining the company! Haha!"),
        new Dialogue("There are more details on proper Password Strength in your Company Standards."),
        new Dialogue("Also, you now have access to more social media sites. Make sure to check everything on all sites!",
            "You now have access to: |PHOTOGRAM!",
            new Vector3(1125.0f,267.0f,0.0f)),
        new Dialogue("As always, make sure to double-check your report before you [SUBMIT]."),
        new Dialogue("Good luck!", true)
    };

    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.FindGameObjectWithTag("World");

        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            pointersPanel = GameObject.FindGameObjectWithTag("PointersPanel");
            try
            {
                StartCoroutine(runTutorial(overlay.Find("Thoughts"), StaticFunction.updateStrings("Alright, let's see what $r's account is like.")));
            }
            catch
            {
                StopAllCoroutines();
            }
        }
        else
        {
            StaticFunction.tutorialStart = false;
            if (StaticFunction.reloadSameStage)
            {

            }
            else
            {
                try
                {
                    if (SceneManager.GetActiveScene().name.Equals("Stage 1"))
                    {
                        //StaticFunction.setCurrentLevel("Stage 1");
                        StartCoroutine(runStage(stageOne));
                    }
                    else if (SceneManager.GetActiveScene().name.Equals("Stage 2"))
                    {
                        //StaticFunction.setCurrentLevel("Stage 2");
                        StartCoroutine(runStage(stageTwo));
                    }
                    else if (SceneManager.GetActiveScene().name.Equals("Stage 3"))
                    {
                        //StaticFunction.setCurrentLevel("Stage 3");
                        StartCoroutine(runStage(stageThree));
                    }
                }
                catch
                {
                    StopAllCoroutines();
                }
            }
        }
    }

    IEnumerator typingAnim(Transform textBox, string line)
    {
        for (int j = 0; j < line.Length; j++)
        {
            yield return new WaitForSeconds(typingSpeed);
            textBox.GetComponent<TMPro.TextMeshProUGUI>().text += line[j];

            if (Input.GetMouseButton(0))
            {
                textBox.GetComponent<TMPro.TextMeshProUGUI>().text += line.Substring(j + 1);
                yield return new WaitForSeconds(0.5f);
                break;
            }
        }

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield break;
    }

    IEnumerator runTutorial(Transform textBox, string thought)
    {
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(typingAnim(textBox, thought));

        overlay.SetAsFirstSibling();

        PointerGenerator script = (PointerGenerator)pointersPanel.GetComponent(typeof(PointerGenerator));
        script.tutorialStart();

        yield break;
    }

    IEnumerator runStage(Dialogue[] stageDialogue)
    {
        if (StaticFunction.getProfileNum() == 0)
        {
            yield return new WaitForSeconds(1f);
            GameObject.FindGameObjectWithTag("Blackscreen").transform.SetAsFirstSibling();
        }
        if (dialogue == null)
        {
            dialogue = Instantiate(
                dialoguePrefab,
                world.transform.position,
                Quaternion.identity,
                world.transform);

            if (StaticFunction.getProfileNum() > 0)
            {
                dialogue.transform.Find("Close").SetAsFirstSibling();
            }
        }
        Transform textBox = dialogue.transform.Find("BG").Find("Dialogue");

        for (int i = 0; i < stageDialogue.Length; i++)
        {
            if (pointer != null)
            {
                Destroy(pointer);
            }

            textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";

            if (stageDialogue[i].pointerInfo != null)
            {
                pointer = Instantiate(
                    pointerPrefab,
                    stageDialogue[i].pointerLocation,
                    Quaternion.identity,
                    world.transform);

                pointer.transform.Find("Details").GetComponent<TMPro.TextMeshProUGUI>().text = stageDialogue[i].pointerInfo;
            }

            yield return StartCoroutine(typingAnim(textBox, stageDialogue[i].line));

            if (stageDialogue[i].allowPlayerControl)
            {
                StaticFunction.dialogueIndex = i;
                Destroy(dialogue);
                dialogue = null;
                break;
            }            
        }

        if (StaticFunction.gotoLevelSelect)
        {
            GameObject.FindGameObjectWithTag("Blackscreen").transform.SetAsLastSibling();

            StartOfStage script = (StartOfStage)GameObject.FindGameObjectWithTag("MainCamera").GetComponent(typeof(StartOfStage));
            script.EndStage();
        }

        yield break;
    }

    public void run(Dialogue[] dialogue)
    {
        StartCoroutine(runStage(dialogue));
    }

    private void Update()
    {
        
    }
}
