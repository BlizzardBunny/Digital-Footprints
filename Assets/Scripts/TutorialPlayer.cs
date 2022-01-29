using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    public Transform overlay;

    private GameObject pointersPanel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(StaticFunction.dialogueLineCounter);
        pointersPanel = GameObject.FindGameObjectWithTag("PointersPanel");
        StartCoroutine(runThoughts(overlay.Find("Thoughts"), StaticFunction.updateStrings("Alright, let's see what $r's account is like.")));
    }

    IEnumerator runThoughts(Transform textBox, string thought)
    {
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);

        for (int j = 0; j < thought.Length; j++)
        {
            yield return new WaitForSeconds(0.05f);
            textBox.GetComponent<TMPro.TextMeshProUGUI>().text += thought[j];

            if (Input.GetMouseButton(0))
            {
                textBox.GetComponent<TMPro.TextMeshProUGUI>().text += thought.Substring(j + 1);
                yield return new WaitForSeconds(0.5f);
                break;
            }
        }

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

        overlay.SetAsFirstSibling();

        PointerGenerator script = (PointerGenerator)pointersPanel.GetComponent(typeof(PointerGenerator));
        script.tutorialStart();

        yield break;
    }
}
