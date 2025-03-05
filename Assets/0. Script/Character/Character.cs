using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    // ���ݷ�
    public int Power { get; set; } = 0;

    // ���� �ӵ�
    public float AttackSpeed { get; set; } = 1f;

    // ���� �Ÿ�
    public float AttackRange { get; set; } = 1f;

    // �ڽ�Ʈ 
    public int Cost { get; set; } = 0;

    // ����
    public int Level { get; set; } = 1;
}

public abstract class Character : MonoBehaviour
{
    protected CharacterData data = new CharacterData();
    private float attackTimer = 0;

    protected private Animator animator;
    private List<Monster> monsterList = new List<Monster>();

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
                //���� ó��
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
                        AttackAnimation();
                    }
                }
            }                
        }

        if (collider == null)
        {
            IdleAnimation();
            return;
        }


        //�ش� ���� �ٶ󺸱�
        if (monsterList.Count > 0)
        {
            try
            {
                Transform targetTrans = monsterList[0].transform;
                transform.LookAt(targetTrans, Vector3.up);
            }
            catch (MissingReferenceException e) 
            { 
                //monsterList[0] �� missing �� ��쿡�� �� �ڵ尡 ������� �ʰ� ó�� 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.AttackRange);
    }

    #region Animation 
    public void IdleAnimation()
    {
        animator.SetTrigger("idle");
    }

    public void AttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    public void ReloadAnimation()
    {
        animator.SetTrigger("reload");
    }
    #endregion

    /// <summary>
    /// �ִϸ��̼��� �̺�Ʈ�� �Լ��� ȣ��� 
    /// </summary>
    public void Attack()
    {
        if (monsterList.Count > 0)
        {
            foreach (var monster in monsterList)
            {
                monster.Damage(data.Power);
            }            
        }

        monsterList.Clear();
    }

    public abstract void Upgrade();
}
