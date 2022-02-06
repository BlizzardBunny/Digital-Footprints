using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOfStage : MonoBehaviour
{
    public GameObject worldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StaticFunction.gotoLevelSelect = false;

        if (GameObject.FindGameObjectsWithTag("World").Length == 0)
        {
            GameObject world = Instantiate(
                worldPrefab,
                Vector3.zero,
                Quaternion.identity,
                null);

            FlagSystemSetup script = (FlagSystemSetup)world.GetComponent(typeof(FlagSystemSetup));
            script.ResetCompletely();
        }
    }

    public void EndStage()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("World"))
        {
            Destroy(x);
        }

        SceneManager.LoadScene("LevelSelect");
    }
}
