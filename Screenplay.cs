using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Screenplay : MonoBehaviour
{
    //dialogue panel
    #region
    public GameObject dialoguePanel;
    public Image speakerIcon;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI speech;
    public GameObject prevMessage;
    public GameObject nextMessage;
    #endregion

    public GameObject controls;

    public Level[] levels;
    public System.Int16 levelIdx;

    //fields that would change pending on the current level of levels field on use
    #region
    public Level currentLevel;
    #endregion

    public GameObject player;
    
    // Start is called before the first frame update
    void Awake() 
    {

    }

    void Start()
    {
        this.currentLevel = this.levels[0];
        this.StartCoroutine(this.currentLevel.Init());
    }
    
    // Update is called once per frame
    void Update()
    {
        this.RestrainPosLimit(this.player, this.player.transform.position, this.currentLevel.rightTopFrontLimit, true, this.currentLevel.OnRightTopFrontLimitSurpass);
        this.RestrainPosLimit(this.player, this.player.transform.position, this.currentLevel.leftBottomBackLimit, false, this.currentLevel.OnLeftBottomBackLimitSurpass);
    }

    ///<summary>
    ///Properties in pPos that exceed the properties in pLimitPos would change its value to the value in pLimitPos respective property
    ///</summary>
    ///<param name="pDirection">if pDirection is true than it returns true if the postion property is bigger than the limit position</param>
    void RestrainPosLimit(GameObject pGameObj, Vector3 pPos, Vector3 pLimitPos, bool pDirection, OnPosLimitSurpass pOnPosLimitSurpass)
    {
        float minimizer = pDirection ? -Time.deltaTime : Time.deltaTime;
        bool changed = false;
        if ((pPos.x > pLimitPos.x) == pDirection) {
            changed = true;
            pPos.x = pLimitPos.x + minimizer;
        }
        if ((pPos.y > pLimitPos.y) == pDirection) {
            changed = true;
            pPos.y = pLimitPos.y + minimizer;
        }
        if ((pPos.z > pLimitPos.z) == pDirection) {
            changed = true;
            pPos.z = pLimitPos.z + minimizer;
        }
        if (changed) {
            pOnPosLimitSurpass();
            pGameObj.transform.position = pPos;
        }
    }

    delegate void OnPosLimitSurpass ();


    public void ShowSpeech (Speech pSpeech)
    {
        this.controls.SetActive(false);
        this.speakerIcon.sprite = pSpeech.icon;
        this.speakerName.text = pSpeech.author;
        this.speech.text = pSpeech.message;
        this.dialoguePanel.SetActive(true);
        if (pSpeech.prevSpeech != null)
        {
            this.prevMessage.SetActive(true);
        }
        else
        {
            this.prevMessage.SetActive(false);
        }
        if (pSpeech.nextSpeech != null)
        {
            this.onClick = ()=>{
                this.ShowSpeech(pSpeech.nextSpeech);Debug.Log("ji1");
            };
        }
        else
        {
            this.onClick = () => {
                this.dialoguePanel.SetActive(false);
                this.controls.SetActive(true);
            };
        }
    }
    public Event showNextSpeech;
    public void OnClick () {Debug.Log("hola"); this.onClick();}
    public delegate void Clicked();
    public Clicked onClick;
}
