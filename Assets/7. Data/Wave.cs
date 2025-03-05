using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Data/Wave")]


public class Wave : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyList;

    public List<GameObject> EnemyList { get { return enemyList; } }

    [SerializeField] private float speed;
    public float Speed { get; }

    [SerializeField] private int gold;//TODO: 몬스터 별로 다른 골드 얻을 수 있게 
    public int Gold { get; }
}
