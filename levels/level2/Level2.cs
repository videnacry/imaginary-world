using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Level
{
    // Start is called before the first frame update
    void Start()
    {
        this.InitQuestDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnLeftBottomBackLimitSurpass()
    {
        this.screenplay.dialoguePanel.SetActive(true);
        this.screenplay.speech.text = "trobo a faltar...";
    }

    public override void OnRightTopFrontLimitSurpass()
    {
        this.screenplay.dialoguePanel.SetActive(true);
        this.screenplay.speech.text = "res de res";
    }
}
