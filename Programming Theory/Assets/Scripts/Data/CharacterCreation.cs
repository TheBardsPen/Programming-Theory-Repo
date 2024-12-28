using System.Collections;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles character creation scene after new game start
/// </summary>

public class CharacterCreation : MonoBehaviour
{
    public PlayerObject player;

    public TextAsset inkFile;

    private void Start()
    {
        DialogueManager.instance.story = new Story(inkFile.text);
    }

    private void Update()
    {
        if (!DialogueManager.instance.story.canContinue)
        {
            if (DialogueManager.instance.story.currentChoices.Count == 0)
            {
                player.SetClass(DialogueManager.instance.story.variablesState["player_class"].ToString(), DialogueManager.instance.story.variablesState["player_name"].ToString());
                StartCoroutine(Continue());
            }
        }
    }

    public IEnumerator Continue()
    {
        yield return new WaitForSeconds(5);
        // Start the game with selected class and name
        SceneManager.LoadScene("Town");
    }
}
