using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //StageManager is singleton who's going to take care of :
    //Read Save OR JSON file of the game or stage
    //Init things
    //Current Turn
    private static StageManager instance = null;

    //Marking current turn number
    private int currentTurn = 1;
    
    //True if there is no savefile of this stage.
    //Save file's (current stage) != (this stage's name)
    private bool newGame = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static StageManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public int getTurn()
    {
        return this.currentTurn;
    }

    public bool isNewGame()
    {
        return this.newGame;
    }
}
