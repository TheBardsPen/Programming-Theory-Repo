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
        DialogueManagerNewGame.instance.story = new Story(inkFile.text);
        DialogueManagerNewGame.instance.StartDialogue();
    }

    private void Update()
    {
        if (DialogueManagerNewGame.instance.story != null)
        {
            if (!DialogueManagerNewGame.instance.story.canContinue)
            {
                if (DialogueManagerNewGame.instance.story.currentChoices.Count == 0)
                {
                    player.SetClass(DialogueManagerNewGame.instance.story.variablesState["player_class"].ToString(), DialogueManagerNewGame.instance.story.variablesState["player_name"].ToString());
                    StartCoroutine(Continue());
                }
            }
        }
        
    }

    public IEnumerator Continue()
    {
        yield return new WaitForSeconds(3);
        // Start the game with selected class and name
        SceneManager.LoadScene("New Game");
    }
}
