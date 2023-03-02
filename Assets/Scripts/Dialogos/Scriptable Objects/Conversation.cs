using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    [TextArea(2, 5)]
    public string text;
    public enum Mood
    {
        Normal,
        Alegre,
        Confuso,
        Preocupado,
        Afk,
        Celebracion
    };
    public Mood Emotion;
    public AudioClip audioClip;
    public bool Question;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Line[] lines;
    public PlayerLevel currentLevel;
    public Conversation nextConversation;
}
