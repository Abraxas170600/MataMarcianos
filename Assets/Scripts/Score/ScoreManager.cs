using UltEvents;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public class ScoreData
    {
        public int scoreAmount;
    }
    public static ScoreManager Instance;
    private ScoreData score;
    public ScoreData Score { get => score; set => score = value; }
    private UltEvent<int> scoreEvent;
    public UltEvent<int> ScoreEvent { get => scoreEvent; set => scoreEvent = value; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Score = new();
    }
    public void ChangeScore(int scoreAmount)
    {
        Score.scoreAmount += scoreAmount;
        ScoreEvent.Invoke(Score.scoreAmount);
    }
}
