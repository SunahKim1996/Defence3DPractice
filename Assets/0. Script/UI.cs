using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : Singleton<UI>
{
    [SerializeField] private List<GameObject> charObj;
    public Transform SpawnParent { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSelect(int index)
    {
        SpawnManager.Instance.SpawnCharacter(charObj[index], SpawnParent);     
    }
}
