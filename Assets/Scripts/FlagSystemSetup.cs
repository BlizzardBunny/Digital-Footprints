using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class FlagSystemSetup : MonoBehaviour
{
    public Image[] profilePic;
    public TextMeshProUGUI[] profileName;
    public TextMeshProUGUI[] profileBio;

    // Start is called before the first frame update
    void Start()
    {
        ResetCompletely();
    }

    public void ResetCompletely()
    {
        StaticFunction.setCurrentLevel(SceneManager.GetActiveScene().name);

        if (!StaticFunction.getCurrentLevel().Equals("Tutorial"))
        {
            StaticFunction.tutorialStart = false;
        }

        if (!StaticFunction.roundHasStarted)
        {
            Debug.Log("ResetCompletely()");
            StaticFunction.clickables.Clear();
            StaticFunction.clickables.TrimExcess();
            StaticFunction.clickables = GameObject.FindGameObjectsWithTag("Clickable").ToList<GameObject>();
            foreach (GameObject clickable in StaticFunction.clickables)
            {
                if (!clickable.activeInHierarchy)
                {
                    StaticFunction.clickables.Remove(clickable);
                }
            }

            RandomizeFlags();
            SetStageRequirements();

            SetStage();
        }
    }

    public void ResetRound()
    {
        StaticFunction.roundHasStarted = false;

        StaticFunction.clickables = GameObject.FindGameObjectsWithTag("Clickable").ToList<GameObject>();
        foreach (GameObject clickable in StaticFunction.clickables)
        {
            if (!clickable.activeInHierarchy)
            {
                StaticFunction.clickables.Remove(clickable);
            }
        }

        RandomizeFlags();
        SetStageRequirements();

        SetStage();
    }

    void SetStageRequirements()
    {
        if (SceneManager.GetActiveScene().name.Equals("Stage 1"))
        {
            StaticFunction.setErrorNum(UnityEngine.Random.Range(1, 3)); //1-2 errors
            Debug.Log(StaticFunction.getErrorNum());
            StaticFunction.setTotalProfiles(3);
            StaticFunction.setTotalErrors(StaticFunction.getErrorNum());
        }
        else if (SceneManager.GetActiveScene().name.Equals("Stage 2"))
        {
            StaticFunction.setErrorNum(UnityEngine.Random.Range(2, 4)); //2-3 errors
            StaticFunction.setTotalErrors(StaticFunction.getErrorNum());
            StaticFunction.setTotalProfiles(4);

            foreach (GameObject socialMediaPage in GameObject.FindGameObjectsWithTag("SocialMediaPage"))
            {
                if (socialMediaPage.transform.name == "Photogram (Panel)")
                {
                    socialMediaPage.SetActive(false);
                    break;
                }
            }
            
            foreach (GameObject privacyWindow in GameObject.FindGameObjectsWithTag("PrivacyWindow"))
            {
                privacyWindow.GetComponent<Animator>().SetBool("isMinimized", true);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Stage 3"))
        {
            StaticFunction.setErrorNum(UnityEngine.Random.Range(3, 6)); //3-5 errors
            StaticFunction.setTotalErrors(StaticFunction.getErrorNum());
            StaticFunction.setTotalProfiles(5);

            foreach (GameObject privacyWindow in GameObject.FindGameObjectsWithTag("PrivacyWindow"))
            {
                privacyWindow.GetComponent<Animator>().SetBool("isMinimized", true);
            }

            foreach (GameObject password in GameObject.FindGameObjectsWithTag("PasswordWindow"))
            {
                password.GetComponent<Animator>().SetBool("isMinimized", true);
            }
        }

        //randomize profile
        if (StaticFunction.tutorialStart)
        {
            StaticFunction.setCurrentProfile(0);
        }
        else
        {
            StaticFunction.setCurrentProfile(UnityEngine.Random.Range(1, StaticFunction.getNames().Length));
        }

        foreach (TextMeshProUGUI profileName in profileName)
        {
            profileName.text = StaticFunction.getNames()[StaticFunction.getCurrentProfile()];
        }

        foreach (TextMeshProUGUI profileBio in profileBio)
        {
            profileBio.text = StaticFunction.getBios()[StaticFunction.getCurrentProfile()];
        }
    }

    void SetStage()
    {
        StaticFunction.editableIsDrawn = false;

        Debug.Log("Total Errors: " + StaticFunction.getTotalErrors());

        for (int i = 0; i < StaticFunction.clickables.Count; i++)
        {
            Flag script = (Flag)StaticFunction.clickables[i].GetComponent(typeof(Flag));

            if (i == 0)
            {
                foreach (Image profilePic in profilePic)
                {
                    profilePic.sprite = script.profilePics[StaticFunction.getCurrentProfile()];
                }
            }

            bool isFlaggedItem = false;
            for (int j = 0; j < StaticFunction.getTotalErrors(); j++)
            {
                if (i == StaticFunction.clickableIDs[j])
                {//get the first [errorNum] indexes of the randomized list clickableIDs. These are our flaggedItems
                    script.SetupFlaggedItem(StaticFunction.getCurrentProfile(), i);
                    isFlaggedItem = true;
                    break;
                }
                else
                {
                    continue;
                }
            }

            if (!isFlaggedItem)
            {//is not a flaggedItem
                script.SetupNonFlaggedItem(StaticFunction.getCurrentProfile(), i);
            }
        }

        StaticFunction.roundHasStarted = true;
    }

    void RandomizeFlags()
    {
        //setup list
        StaticFunction.clickableIDs.Clear();
        StaticFunction.clickableIDs.TrimExcess();
        for (int i = 0; i < StaticFunction.clickables.Count; i++)
        {
            StaticFunction.clickableIDs.Add(i);
        }

        //randomize list
        StaticFunction.clickableIDs = StaticFunction.clickableIDs.OrderBy(tvz => System.Guid.NewGuid()).ToList();
    }
}
