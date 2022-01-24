using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneChoiceRedirect : MonoBehaviour
{
    private GameObject world;

    private void Start()
    {
        world = GameObject.FindGameObjectWithTag("World");
    }

    public void acceptChoice()
    {
        CutscenePlayer script = (CutscenePlayer)world.GetComponent(typeof(CutscenePlayer));

        int choiceIndex = int.Parse(this.name.Replace("Choice ", ""));

        StaticFunction.choiceIndex = choiceIndex - 1;

        script.acceptChoice();
    }

    public void nextScene()
    {
        CutscenePlayer script = (CutscenePlayer)world.GetComponent(typeof(CutscenePlayer));

        script.nextScene();
    }
}
