using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    // 공격력
    public int Power { get; set; } = 0;

    // 공격 속도
    public float AttackSpeed { get; set; } = 1f;

    // 공격 거리
    public float AttackRange { get; set; } = 1f;

    // 코스트 
    public int Cost { get; set; } = 0;

    // 레벨
    public int Level { get; set; } = 1;
}

public class Character : MonoBehaviour
{
    CharacterData data = new CharacterData();
    private float attackTimer = 0;

    private Animator animator;
    private List<Monster> monsterList = new List<Monster>();

    // Start is called before the first frame update
    void Start()
    {
        data.Power = 1;
        data.AttackSpeed = 1;
        data.AttackRange = 3;
        data.Cost = 1;
        data.Level = 1;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, data.AttackRange);
        attackTimer += Time.deltaTime;

        float attackDistance = data.AttackRange;
        Collider collider = null;

        foreach (var monsterCollider in colls)
        {
            if (monsterCollider.tag.Equals("Monster"))
            {
                //공격 처리
                if (attackTimer >= data.AttackSpeed)
                {
                    float distance = Vector3.Distance(transform.position, monsterCollider.transform.position);

                    if (distance <= attackDistance)
                    {
                        attackDistance = distance;
                        collider = monsterCollider;
                    }

                    if (collider != null) 
                    {
                        monsterList.Clear();
                        monsterList.Add(collider.GetComponent<Monster>());

                        attackTimer = 0;
                        animator.SetTrigger("attack");
                    }
                }
            }                
        }

        //해당 몬스터 바라보기
        if (monsterList.Count > 0)
        {
            try
            {
                Transform targetTrans = monsterList[0].transform;
                transform.LookAt(targetTrans, Vector3.up);
            }
            catch (MissingReferenceException e) 
            { 
                //monsterList[0] 이 missing 인 경우에는 위 코드가 실행되지 않게 처리 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.AttackRange);
    }

    public void IdleAnimation()
    {
        animator.SetTrigger("idle");
    }

    /// <summary>
    /// 애니메이션의 이벤트로 함수가 호출됨 
    /// </summary>
    public void Attack()
    {
        if (monsterList.Count > 0)
        {
            foreach (var monster in monsterList)
            {
                monster.Damage(10);
            }            
        }

        monsterList.Clear();
    }
}
