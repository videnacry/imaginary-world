using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Screenplay screenplay;
    public Vector3 rightTopFrontLimit;
    public Vector3 leftBottomBackLimit;
    public Vector3 questRightTopFrontLimit;
    public Vector3 questLeftBottomBackLimit;
    public Speech[] questDialogue;
    
    public bool questFinished = false;
    
    public void InitQuestDialogue()
    { 
        int speechCount = this.questDialogue.Length;
        if (speechCount > 1) {
            for (short i = 1; i < speechCount - 1; i++) {
                questDialogue[i].prevSpeech = questDialogue[i-1];
                questDialogue[i].nextSpeech = questDialogue[i+1];
            }
            questDialogue[speechCount - 1].prevSpeech = questDialogue[speechCount - 2];
        }
        if (speechCount > 0) {
            questDialogue[0].nextSpeech = questDialogue[speechCount - 1];
            this.StartCoroutine(ShowQuestDialogue());
        }
    }
    public IEnumerator ShowQuestDialogue()
    {
        Transform playerTransform = this.screenplay.player.transform;
        bool changed = false;
        while (changed == false) {
            if (playerTransform.position.x > questLeftBottomBackLimit.x && playerTransform.position.x < questRightTopFrontLimit.x
            && playerTransform.position.y > questLeftBottomBackLimit.y && playerTransform.position.y < questRightTopFrontLimit.y
            && playerTransform.position.z > questLeftBottomBackLimit.z && playerTransform.position.z < questRightTopFrontLimit.z) {
                changed = true;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        this.screenplay.ShowSpeech(this.questDialogue[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual IEnumerator Init()
    {
        // while (this.questFinished == false) {
            yield return new WaitForSeconds(1);
        //     questFinished = true;
        // }
        // this.screenplay.levelIdx++;
        // this.screenplay.currentLevel = this.screenplay.levels[this.screenplay.levelIdx];
        // Destroy(this.gameObject);
    }
    
    public virtual void OnRightTopFrontLimitSurpass ()
    {
        
    }
    public virtual void OnLeftBottomBackLimitSurpass () 
    {
        
    }
}
