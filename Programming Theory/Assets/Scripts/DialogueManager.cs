using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameObject textBox;
    public GameObject choiceButon;
    public GameObject choicePanel;
    public TMP_InputField textInput;
    public bool isAwaitingInput = false;
    public bool isTalking = false;

    public Story story;
    GameObject namePlate;
    TextMeshProUGUI nametag;
    TextMeshProUGUI message;
    List<string> tags;
    static string choiceSelected;

    private NPCController controller;

    // Start is called before the first frame update
    void Awake()
    {
        namePlate = textBox.transform.GetChild(0).gameObject;
        nametag = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        message = textBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        tags = new List<string>();
        choiceSelected = null;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (story != null)
        {
            if (story.canContinue)
            {
                while (story.currentText == "\n")
                {
                    AdvanceDialogue();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && story.currentChoices.Count == 0)
        {
            //Is there more to the story?
            if (story.canContinue)
            {

                AdvanceDialogue();

                //Are there any choices?
                if (story.currentChoices.Count != 0)
                {
                    StartCoroutine(ShowChoices());
                }
            }
            else
            {
                FinishDialogue();
            }
        }
    }

    public void StartDialogue(NPCController _controller)
    {
        for (int i = 0; i < choicePanel.transform.childCount; i++)
        {
            choicePanel.transform.GetChild(i).gameObject.SetActive(false);
        }

        controller = _controller;

        namePlate.SetActive(true);

        DataManager.instance.npcRelations.AddNPC(controller.npc);
        story = new Story(controller.npc.story.text);

        // Set story variables from local
        story.variablesState["player_name"] = DataManager.instance.player.container.name;
        story.variablesState["player_class"] = DataManager.instance.player.container.playerClass;
        story.variablesState["relationship"] = controller.relationship;
        story.variablesState["isQuestActive"] = controller.isQuestActive;

        if (story != null)
        {
            if (story.canContinue)
            {
                AdvanceDialogue();
            }
        }
    }

    // Finished the Story (Dialogue)
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
        for (int i = 0; i < choicePanel.transform.childCount; i++)
        {
            choicePanel.transform.GetChild(i).gameObject.SetActive(true);
        }
        controller.RelationUpdate();
        namePlate.SetActive(false);
        nametag.text = string.Empty;
        message.text = string.Empty;
    }

    // Advance through the story 
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    // Type out the sentence letter by letter and make character idle if they were talking
    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        /*CharacterScript tempSpeaker = GameObject.FindObjectOfType<CharacterScript>();
        if (tempSpeaker.isTalking)
        {
            SetAnimation("idle");
        }*/
        yield return null;
    }

    // Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            int index = i;
            GameObject temp = Instantiate(choiceButon, choicePanel.transform);
            temp.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = _choices[i].text;
            temp.GetComponent<Button>().onClick.AddListener(() => { SetDecision(index); });
            /*if (_choices[i].text == "Leave")
            {
                temp.GetComponent<Button>().onClick.AddListener(() => { SetRelationship(int.Parse(story.state.variablesState["relationship"].ToString())); });
            }*/
        }

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    // Tells the story which branch to go to
    public void SetDecision(int index)
    {
        choiceSelected = index.ToString();
        story.ChooseChoiceIndex(index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        for (int i = 0; i < choicePanel.transform.childCount; i++)
        {
            if (choicePanel.transform.GetChild(i).gameObject.activeSelf)
            {
                Destroy(choicePanel.transform.GetChild(i).gameObject);
            }
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    /*** Tag Parser ***/
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];
            string sub = t.Split(' ')[2];

            switch (prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;
                case "name":
                    SetSpeakerName(param, sub);
                    break;
                case "relationship":
                    SetRelationship(int.Parse(param));
                    break;
                case "quest":
                    SetQuest(param);
                    break;
                case "await":
                    AwaitInput(param);
                    break;
            }
        }
    }
    void SetAnimation(string _name)
    {
        //CharacterScript cs = GameObject.FindObjectOfType<CharacterScript>();
        //cs.PlayAnimation(_name);
    }
    void SetTextColor(string _color)
    {
        switch (_color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }

    void SetSpeakerName(string first, string last)
    {
        if (first != "null")
        {
            if (last != "null")
            {
                namePlate.SetActive(true);
                nametag.text = first + " " + last;
            }
            else
            {
                namePlate.SetActive(true);
                nametag.text = first;
            }
        }
        else
        {
            namePlate.SetActive (false);
            nametag.text = "";
        }
    }

    public void SetRelationship(int amount)
    {
        controller.relationship += amount;
    }

    public void SetQuest(string type)
    {

    }

    void AwaitInput(string _input)
    {
        if (_input == "text")
        {
            isAwaitingInput = true;
            TMP_InputField input = Instantiate(textInput, textBox.transform.parent);
            input.Select();
            input.onEndEdit.AddListener(delegate {  story.state.variablesState["player_name"] = input.text; FinishInput(input.gameObject); });
        }
    }

    void FinishInput(GameObject _input)
    {
        isAwaitingInput = false;
        Destroy(_input.gameObject);
        AdvanceDialogue();
    }
}