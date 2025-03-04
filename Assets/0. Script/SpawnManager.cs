using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<Monster> monsters;
    public MonsterHP monsterHPUI;
    public Transform monsterHPParent;
    public List<Flag> flags;

    public float delay;
    private float delayTimer;

    private int spawnCount;
    private int spawnMaxCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = 0;
        spawnMaxCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;

        if (delayTimer >= delay && spawnCount <= spawnMaxCount)
        {
            delayTimer = 0;

            Monster m = Instantiate(monsters[0], flags[0].transform.position, Quaternion.identity);

            MonsterHP hp = Instantiate(monsterHPUI, monsterHPParent);
            hp.SetTarget(m.transform);
            m.HpUI = hp;

            m.SetFlag(flags);
            m.SetDirecion(flags[0].dir);

            spawnCount++;
        }
    }

    public void SpawnCharacter(GameObject spawnObj, Transform parent)
    {
        Transform trans = Instantiate(spawnObj, parent).transform;
        trans.localPosition = Vector3.zero;
        trans.localPosition = new Vector3(0f, 0.5f, 0f);
    }
}
