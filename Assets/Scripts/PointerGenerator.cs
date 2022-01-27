using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerGenerator : MonoBehaviour
{
    public GameObject pointerPrefab;

    private GameObject pointer;
    private Transform[] points;
    private float characterTypingSpeed = 0.03f;

    public enum Directions
    {
        Up, Down, Left, Right, Null
    }

    public class PointerInfo
    {
        public Directions direction { get; }
        public string details { get; }
        public bool allowPlayerControl { get; }

        public PointerInfo(Directions x, string y) => (direction, details, allowPlayerControl) = (x, y, false);
        public PointerInfo(Directions x, string y, bool z) => (direction, details, allowPlayerControl) = (x, y, z);
    }

    private PointerInfo[] tutorial = new PointerInfo[]
    {
        new PointerInfo(Directions.Null, "This is my desktop."),
        new PointerInfo(Directions.Right, "I can't believe I actually downloaded this weird Report App for this."),
        new PointerInfo(Directions.Down, "Well, I better open up Digibook and see what $r did wrong this time.", true)
    };

    private void Start()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.Find("Point " + i);
        }

        StartCoroutine(run(tutorial));
    }

    IEnumerator run(PointerInfo[] pointerInfos)
    {
        transform.SetAsLastSibling();
        for (int i = 0; i < pointerInfos.Length; i++)
        {
            pointer = Instantiate(
                pointerPrefab,
                points[i].position,
                Quaternion.identity,
                transform);

            for (int j = 0; j < pointer.transform.childCount; j++)
            {
                Transform child = pointer.transform.GetChild(j);
                Debug.Log(child.name);
                if (child.name.Equals("BG") || child.name.Equals("Details"))
                {
                    Debug.Log("Okay");
                    //do nothing
                }
                else
                {
                    if (child.name.Equals(pointerInfos[i].direction.ToString()))
                    {
                        Debug.Log("Okay");
                        //do nothing
                    }
                    else
                    {
                        Debug.Log("Boom");
                        Destroy(child.gameObject);
                    }
                }
            }

            Transform textbox = pointer.transform.Find("Details");
            textbox.GetComponent<TMPro.TextMeshProUGUI>().text = "";

            string details = StaticFunction.updateStrings(pointerInfos[i].details);

            for (int j = 0; j < details.Length; j++)
            {
                yield return new WaitForSeconds(characterTypingSpeed);
                textbox.GetComponent<TMPro.TextMeshProUGUI>().text += details[j];

                if (Input.GetMouseButton(0))
                {
                    textbox.GetComponent<TMPro.TextMeshProUGUI>().text += details.Substring(j + 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }

            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

            Destroy(pointer);

            if (pointerInfos[i].allowPlayerControl)
            {
                transform.SetAsFirstSibling();
            }
        }

        yield return null;
    }

    public void mistakeMade()
    {
        PointerInfo[] mistakeMessages = new PointerInfo[]
        {
            new PointerInfo(Directions.Left, "Nah. I'm pretty sure there's nothing wrong with this one."),
            new PointerInfo(Directions.Right, "I better click it again to delete it from the Report App.")
        };
    }
}
