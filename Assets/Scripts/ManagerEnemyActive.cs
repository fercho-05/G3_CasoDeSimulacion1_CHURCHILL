using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemyActive : MonoBehaviour
{
    public GameObject enemyToActivate;
    void Start()
    {
        enemyToActivate.SetActive(true);
    }
}
