using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int generatedPikeman;
    public int generatedArcher;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(generatedArcher==3 && generatedPikeman == 3)
        {
            SceneManager.LoadScene(1);
        }
    }
}
