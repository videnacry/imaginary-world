using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnLeftBottomBackLimitSurpass()
    {
        base.OnLeftBottomBackLimitSurpass();
        Debug.Log("level 1");
    }

}
