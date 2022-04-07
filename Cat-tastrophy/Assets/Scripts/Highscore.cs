using UnityEngine;

[CreateAssetMenu(fileName = "Highscore", menuName = "ScriptableObjects/HighscoreScriptableObject", order = 1)]
public class Highscore : ScriptableObject
{
    public int[] scoreList = new int[10];

    //Adds a new score but only, if its higher than the 10th score
    //Inserts the new score in the correct place and removes the last
    //now invalid score from the list
    public void AddScore(int score)
    {
        if(score <= scoreList[scoreList.Length - 1]) { return; }
        int place = 0;
        for(int i = 0; i < scoreList.Length; i++)
        {
            if(score >= scoreList[i])
            {
                break;
            }
            place++;
        }
        for(int i = place; i < scoreList.Length; i++)
        {
            int temp = scoreList[i];
            scoreList[i] = score;
            score = temp;
        }
    }
}
