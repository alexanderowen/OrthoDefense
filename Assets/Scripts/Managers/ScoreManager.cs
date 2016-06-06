using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    public static int deaths;        // The player's score.
	int total_enemies;
	public EnemyManager enemyManager;


    Text text;                      // Reference to the Text component.


    void Awake ()
    {
        // Set up the reference.
		total_enemies = enemyManager.GetComponent<WaveManager>().spawnCount;
        text = GetComponent <Text> ();

        // Reset the score.
        deaths = 0;
    }


    void Update ()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
		text.text = "Enemies Left: " + (total_enemies - deaths);
    }
}
