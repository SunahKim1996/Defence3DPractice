using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrirorCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        data.Power = 3;
        data.AttackSpeed = 1;
        data.AttackRange = 3;
        data.Cost = 1;
        data.Level = 1;

        animator = GetComponent<Animator>();
    }
}
