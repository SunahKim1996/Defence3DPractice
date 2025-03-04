using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : Singleton<TouchEvent>
{
    [SerializeField] private GameObject selectCharacter;
    private bool isSelecting = false;

    [SerializeField] private GameObject updateCharacter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Pad"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (isSelecting == false)
                    {
                        // 캐릭터 생성
                        if (hit.transform.childCount == 0)
                        {
                            isSelecting = true;
                            selectCharacter.transform.position = Camera.main.WorldToScreenPoint(
                                hit.transform.position + new Vector3(0, 1f, 0));
                            selectCharacter.SetActive(true);

                            UI.Instance.SpawnParent = hit.transform;
                        }

                        // 캐릭터 업그레이드 
                        else
                        {
                            updateCharacter.transform.position = Camera.main.WorldToScreenPoint(
                                hit.transform.position + new Vector3(0, 1f, 0));
                            updateCharacter.SetActive(true);
                        }
                    }                    
                }
            }
        }
    }

    public void EndSelecting()
    {
        isSelecting = false;
    }
}
