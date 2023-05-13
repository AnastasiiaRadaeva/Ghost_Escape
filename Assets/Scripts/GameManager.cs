using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Person[] people;
    public Player player;
    // public Transform memories;

    public int memoryScore;
    
    void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        memoryScore = 0;
        NewRound();
    }

    private void NewRound()
    {
        // foreach (Transform memory in memories)
        //     memory.gameObject.SetActive(true);
        foreach (Ghost ghost in ghosts)
            ghost.gameObject.SetActive(true);
        foreach (Person person in people)
            person.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
    }
}