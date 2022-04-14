using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectTransitions : MonoBehaviour
{
    bool isFadingIn;

    // Start is called before the first frame update
    void Start()
    {
        isFadingIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn)
        {
            StartCoroutine(fadeIn());
        }
    }

    public void FadeOut()
    {
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeIn()
    {        
        isFadingIn = false;
        yield return new WaitForSeconds(1.0f);
        GameObject.FindGameObjectWithTag("Blackscreen").transform.SetAsFirstSibling();

        PausedMenuFunctions script = (PausedMenuFunctions)this.GetComponent(typeof(PausedMenuFunctions));

        string nextLevel = "";
        string currLevel = StaticFunction.getCurrentLevel();
        switch (currLevel)
        {
            case "Stage 1":
                nextLevel = "Stage 2";
                break;
            case "Stage 2":
                nextLevel = "Stage 3";
                break;
            case "Stage 3":
                nextLevel = "";
                break;
        }

        StaticFunction.setCurrentLevel(nextLevel);

        if (StaticFunction.getCurrentLevel().Equals(""))
        {
            script.ResetSaves();
        }
        else
        {
            script.SaveGame();
        }

        yield return new WaitForSeconds(1.0f);
        yield break;
    }
    IEnumerator fadeOut()
    {
        GameObject.FindGameObjectWithTag("Blackscreen").transform.SetAsLastSibling();
        GameObject.FindGameObjectWithTag("Blackscreen").GetComponent<Animator>().SetBool("isFadingIn", false);
        yield return new WaitForSeconds(1.0f);
        yield break;
    }
}
