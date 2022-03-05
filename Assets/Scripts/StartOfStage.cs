using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOfStage : MonoBehaviour
{
    public GameObject worldPrefab;
    private Transform overlay;
    private Coroutine endingCoroutine;

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

    IEnumerator end()
    {
        Animator anim = overlay.GetComponent<Animator>();
        anim.SetBool("isFadingIn", false);

        yield return new WaitForSeconds(1f);
        StaticFunction.roundHasStarted = false;

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("World"))
        {
            Destroy(x);
        }

        SceneManager.LoadScene("LevelSelect");
        StopCoroutine(endingCoroutine);
    }

    public void EndStage()
    {
        overlay = GameObject.FindGameObjectWithTag("Blackscreen").transform;
        endingCoroutine = StartCoroutine(end());
    }
}
