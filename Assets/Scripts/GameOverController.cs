using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTxt;


    protected virtual void Awake()
    {
        if (scoreTxt != null)
        {
            int score = StateManager.Instance.getScore();
            scoreTxt.text = "" + score;
        }
    }

    public void Reiniciar()
    {
        LevelManager.Instance.FirstScene();
        StateManager.Instance.reiniciar();
    }


}
