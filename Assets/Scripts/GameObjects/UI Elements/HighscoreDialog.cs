using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class HighscoreDialog : BaseDialog
{
    [SerializeField] private TextMeshProUGUI _prefabText;
    [SerializeField] private Transform _pfPanelScore;

    public override void OnCompleteShow()
    {
        base.OnCompleteShow();

        if (this.data != null)
        {
            if (this.data.GetType() == typeof(List<Score>))
            {
                // Sort by score (desc)
                List<Score> descHighscores = ((List<Score>)this.data).OrderByDescending(i => i.score).ToList();

                for (int i = 0; i < descHighscores.Count; i++)
                {
                    int rank = i + 1;
                    int playerScore = descHighscores[i].score;
                    string date = new System.DateTime(descHighscores[i].ticks).ToString("dd/MM/yyyy");

                    TextMeshProUGUI scoreText = Instantiate(_prefabText,_pfPanelScore);
                    scoreText.SetText($"{rank + ": " + playerScore + " - " + date}");
                }
            }
            else
            {
                Debug.LogError(this.GetType().Name + " - Wrong data type!");
                return;
            }
        }
        else
        {
            Debug.Log(this.GetType().Name + " - No data found!");
            return;
        }
    }
}
