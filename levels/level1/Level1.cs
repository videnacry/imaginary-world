using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level
{
    // Start is called before the first frame update
    public Sprite playerIcon;
    public string playerName;
    public Speech leftBottomBackLimitSurpass;
    public Speech rightTopFrontLimitSurpass;
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
        this.screenplay.ShowSpeech(leftBottomBackLimitSurpass);
    }

    public override void OnRightTopFrontLimitSurpass()
    {
        this.screenplay.ShowSpeech(rightTopFrontLimitSurpass);
    }
}
