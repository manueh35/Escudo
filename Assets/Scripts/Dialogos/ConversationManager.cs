using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationManager : MonoBehaviour
{
    private DialogScript dialog;
    public static bool tutorial_completed = false;

    private Scene currentScene;

    private void Awake()
    {
        dialog = GetComponent<DialogScript>();  
    }

    private void Start()
    {
        if (!tutorial_completed)
        {
            tutorial_completed = true;
            StartIntro();
        }
    }

    private PlayerLevel GetCurrentLevel()
    {
        return ExpTracker.Instance.currentLevel;
    }

    private List<Conversation> GetConversationsForCurrentLevel()
    {
        List<Conversation> conversationsAvaliable = new List<Conversation>();

        foreach (Conversation conversation in dialog.conversations)
        {
            if (conversation.currentLevel == GetCurrentLevel())
            {
                conversationsAvaliable.Add(conversation);
            }
        }

        return conversationsAvaliable;
    }

    public void StartDialogue()
    {
        if (!dialog.dialogOn)
        {
            int rnd = Random.Range(0, GetConversationsForCurrentLevel().Count);
            if (DialogScript.canPressMenu)
            {
                dialog.StartConversation(GetConversationsForCurrentLevel()[rnd]);
            }
            
        }
    }

    private void StartIntro()
    {
        dialog.StartConversation(dialog.intro);
    }

    public void StartTutorial()
    {
        dialog.StartConversation(dialog.tutorial);
    }
}
