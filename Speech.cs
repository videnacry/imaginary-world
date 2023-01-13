using UnityEngine;

[System.Serializable]
public class Speech 
{
    public Sprite icon;
    public string author;
    public string message;
    public Speech prevSpeech;
    public Speech nextSpeech;
    public System.Action action;

    public Speech (Sprite pIcon, string pAuthor, string pMessage)
    {
        this.icon = pIcon;
        this.author = pAuthor;
        this.message = pMessage;
    }
}