using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        data.Power = 1;
        data.AttackSpeed = 1;
        data.AttackRange = 5;
        data.Cost = 1;
        data.Level = 1;

        animator = GetComponent<Animator>();
    }
}
