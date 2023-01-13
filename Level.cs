using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Screenplay screenplay;

    public PlayerController player;
    public Vector3 aimingPoint;

    #region
    public Vector3 rightTopFrontLimit;
    public Vector3 leftBottomBackLimit;
    public Speech leftBottomBackLimitSurpass;
    public Speech rightTopFrontLimitSurpass;
    #endregion

    public Vector3 questRightTopFrontLimit;
    public Vector3 questLeftBottomBackLimit;
    
    public Speech[] questDialogue;
    public bool questFinished = false;

    public void Init()
    { 
        this.screenplay.player = this.screenplay.currentLevel.player;
        this.screenplay.cameraAiming.transform.parent = this.player.transform;
        this.screenplay.cameraAiming.transform.localPosition = aimingPoint;
        int speechCount = this.questDialogue.Length;
        if (speechCount > 1) {
            for (short i = 1; i < speechCount - 1; i++) {
                this.questDialogue[i].prevSpeech = this.questDialogue[i-1];
                this.questDialogue[i].nextSpeech = this.questDialogue[i+1];
            }
            this.questDialogue[speechCount - 1].prevSpeech = questDialogue[speechCount - 2];
            this.questDialogue[speechCount - 1].action = () => {Debug.Log("quest accepted");this.NextLevel();};
        }
        if (speechCount > 0) {
            this.questDialogue[0].action = () => {this.StartCoroutine(this.ShowQuestDialogue());};
            this.questDialogue[0].nextSpeech = this.questDialogue[speechCount - 1];
            this.StartCoroutine(ShowQuestDialogue());
        }
    }
    public IEnumerator ShowQuestDialogue()
    {
        Transform playerTransform = this.screenplay.player.transform;
        Vector3 lastOutPos = playerTransform.position;
        bool changed = false;
        while (changed == false) {
            if (playerTransform.position.x > questLeftBottomBackLimit.x && playerTransform.position.x < questRightTopFrontLimit.x
            && playerTransform.position.y > questLeftBottomBackLimit.y && playerTransform.position.y < questRightTopFrontLimit.y
            && playerTransform.position.z > questLeftBottomBackLimit.z && playerTransform.position.z < questRightTopFrontLimit.z) {
                changed = true;
            } else {
                lastOutPos = playerTransform.position;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        this.screenplay.player.transform.position = lastOutPos;
        this.screenplay.ShowSpeech(this.questDialogue[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void NextLevel()
    {
        this.screenplay.levelIdx++;
        this.screenplay.currentLevel = this.screenplay.levels[this.screenplay.levelIdx];
        this.screenplay.currentLevel.Init();
        Destroy(this.gameObject);
    }
    
    public virtual void OnLeftBottomBackLimitSurpass()
    {
        this.screenplay.ShowSpeech(this.leftBottomBackLimitSurpass);
    }

    public virtual void OnRightTopFrontLimitSurpass()
    {
        this.screenplay.ShowSpeech(this.rightTopFrontLimitSurpass);
    }
}
