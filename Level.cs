using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Screenplay screenplay;
    public Vector3 rightTopFrontLimit;
    public Vector3 leftBottomBackLimit;
    public Vector3 questPosition;
    public Speech[] questDialog;
    
    public bool questFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
