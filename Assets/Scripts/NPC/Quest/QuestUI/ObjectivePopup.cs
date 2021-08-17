using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePopup : MonoBehaviour
{
    public static ObjectivePopup Instance { get; private set; }
    [HideInInspector] public Transform selectedObjective = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 1.45f, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 0.25f, 0);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.transform != null && hit.transform.GetComponent<UpdateTriggerQuest>() != null)
        {
            selectedObjective = hit.transform;
        }
    }
}
