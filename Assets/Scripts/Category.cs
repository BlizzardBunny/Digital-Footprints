using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    struct Details
    {
        public string details, examples;

        public Details(string x, string y) => (details, examples) = (x,y);
    }

    private Dictionary<string,Details> categoryDetails = new Dictionary<string, Details>()
    {
        { "Location Data", new Details(
            "Anything that could potentially be used to locate your current or future location. This is sensitive information that potential threats could use this to either find you, or break into your house while you’re not around.",
            "Examples: Posting about...\n- What time you'll leave the house\n- Where you will be going and when\n- How long you will be away") },
        { "Personal Information", new Details(
            "Any sort of information that could be used to identify you. This is sensitive information as it can be used for phishing or identity theft.",
            "Examples:\n- Address\n- Phone Number\n- Credit Card Details") },
        { "Messages", new Details(
            "Avoiding accepting message requests from unknown contacts prevents people from sending you malicious messages that could put your data at risk.",
            "NOTE: This option should be set to FRIENDS in the Privacy Settings.") },
        { "Friend Requests", new Details(
            "Only accepting friend requests from people you know prevents unknown persons from viewing and recreating your profile.",
            "NOTE: This option should be set to FRIENDS in the Privacy Settings.") },
        { "Content Visibility", new Details(
            "Allowing only your friends to view your posts helps minimize the possibility of unwanted information becoming public.",
            "NOTE: This option should be set to FRIENDS in the Privacy Settings.") },
        { "Contact Details", new Details(
            "Allow only friends to view contact details to prevent entities from using these maliciously, such as spam and identity theft.",
            "Includes:\n- Email Address\n- Phone Number\n") },
        { "Searchable Online", new Details(
            "Preventing the public from looking up your profile online prevents unknown entities from accessing your profile’s content and recreating it to impersonate you.",
            "NOTE: This option should be set to FRIENDS in the Privacy Settings.") },
        { "Password Length", new Details(
            "The longer the password, the stronger it is. 10 characters or more is required to have a strong password.",
            "GOOD Examples:\n- 10+CharactersLong\n- L0ngishPa$$word\n- Exactly10!") },
        { "Complexity", new Details(
            "Mixing and combining the use of numbers, special characters, and letter cases (both upper AND lower case) increases your password’s complexity, making it harder to crack.",
            "GOOD Examples:\n- Aa12!@Bb34\n- CAP&n0ncap\n- Ur$P3C!al") },
        { "Personal Info", new Details(
            "Your personal information should not be within your password because it can make your password easily guessable, especially since entities can gather information about you online.",
            "BAD Examples:\n- 236OslerSt! (has address)\n- NormaLecter2022~ (has name)") },
    };

    private string buttonText;
    private Transform cdPanel, details, title, examples;

    private void Start()
    {
        buttonText = transform.Find("TMP").GetComponent<TMPro.TextMeshProUGUI>().text;
        cdPanel = transform.parent.Find("CategoryDetailsPanel");
        details = cdPanel.Find("Details");
        examples = cdPanel.Find("Examples");
        title = cdPanel.Find("Title");
        cdPanel.gameObject.SetActive(false);
    }

    public void Categorize()
    {
        if (!StaticFunction.isShowingDetails)
        {
            cdPanel.gameObject.SetActive(true);
            details.GetComponent<TMPro.TextMeshProUGUI>().text = categoryDetails[buttonText].details;
            examples.GetComponent<TMPro.TextMeshProUGUI>().text = categoryDetails[buttonText].examples;
            title.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;
            StaticFunction.isShowingDetails = true;
        }
        else
        {
            try
            {
                Transform flag = GameObject.FindGameObjectWithTag("MessageField").transform.Find("Flag");
                string text = flag.GetComponent<TMPro.TextMeshProUGUI>().text;

                if (text.Equals(buttonText))
                {
                    cdPanel.gameObject.SetActive(false);
                    StaticFunction.isShowingDetails = false;
                }
                else
                {
                    details.GetComponent<TMPro.TextMeshProUGUI>().text = categoryDetails[buttonText].details;
                    examples.GetComponent<TMPro.TextMeshProUGUI>().text = categoryDetails[buttonText].examples;
                    title.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;
                }
            }
            catch
            {
                details.GetComponent<TMPro.TextMeshProUGUI>().text = categoryDetails[buttonText].details;
                examples.GetComponent<TMPro.TextMeshProUGUI>().text = categoryDetails[buttonText].examples;
                title.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;
            }
        }

        try
        {
            Transform flag = GameObject.FindGameObjectWithTag("MessageField").transform.Find("Flag");
            string text = flag.GetComponent<TMPro.TextMeshProUGUI>().text;

            if (text.Equals(buttonText))
            {
                flag.GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
            else
            {
                flag.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;

                if (StaticFunction.tutorialStart)
                {
                    PointerGenerator script = (PointerGenerator)(GameObject.FindGameObjectWithTag("PointersPanel")).GetComponent(typeof(PointerGenerator));

                    script.checkCategory(transform, buttonText);
                }
            }
        }
        catch
        {

        }
    }
}
