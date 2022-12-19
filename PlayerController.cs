using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Screenplay screenplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace)) screenplay.dialoguePanel?.SetActive(false);
        if (Input.GetKey(KeyCode.A)) {
            Debug.Log("inside");
            screenplay.speech.text = "que hubo";
        }
    }

    public void MoveLeft()
    {
        transform.Translate(new Vector3(-Time.fixedDeltaTime, 0, 0));
    }
    public void MoveRight()
    {
        transform.Translate(new Vector3(Time.fixedDeltaTime, 0, 0));
    }
}
