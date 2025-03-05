using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : Singleton<TouchEvent>
{
    [SerializeField] private GameObject selectCharacter;
    [SerializeField] private GameObject updateCharacter;

    bool isSelecting = false;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (isSelecting)
            return;

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Pad"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // ĳ���� ����
                    if (hit.transform.childCount == 0)
                    {
                        selectCharacter.transform.position = Camera.main.WorldToScreenPoint(
                            hit.transform.position + new Vector3(0, 1f, 0));
                        selectCharacter.SetActive(true);

                        UI.Instance.SpawnParent = hit.transform;
                    }

                    // ĳ���� ���׷��̵� 
                    else
                    {
                        updateCharacter.transform.position = Camera.main.WorldToScreenPoint(
                            hit.transform.position + new Vector3(0, 1f, 0));
                        updateCharacter.SetActive(true);
                    }

                    isSelecting = true;
                }
            }
        }
    }

    public void EndSelecting()
    {
        isSelecting = false;
    }
}
