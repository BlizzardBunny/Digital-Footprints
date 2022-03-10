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
