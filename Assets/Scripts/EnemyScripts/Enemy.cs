using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public int damage;
    public float speed = 0;
    private Rigidbody2D rb;
    public EnemyHealth health;

    [HideInInspector] public int randomSpot;
    [HideInInspector] public Vector3 initialPosition;
    public List<Transform> moveSpots = new List<Transform>();
    [HideInInspector] public float delayTimer;
    [SerializeField] private float roamingDelay = 0;
    Transform moveSpot1; 
    Transform moveSpot2; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        CreateInitialPoints();
        randomSpot = Random.Range(0, moveSpots.Count);
        delayTimer = roamingDelay;
    }

    public void Chase(Transform target)
    {
        rb.position = Vector2.MoveTowards(rb.position, target.position, speed * Time.deltaTime);
    }

    public void Roam()
    {
        if (moveSpots.Count != 0)
        {
            if (Vector2.Distance(rb.position, moveSpots[randomSpot].position) > 0.2f)
            {
                rb.position = Vector2.MoveTowards
                     (rb.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            }
            else
            {
                if (delayTimer <= 0)
                {
                    ManageRoamingPoints();
                    randomSpot = Random.Range(0, moveSpots.Count);
                    delayTimer = roamingDelay;
                }
                else
                {
                    delayTimer -= Time.fixedDeltaTime;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        EnemyTargetManager.Instance.target = gameObject;
        TargetNameManager.Instance.DisplayName();
    }
    public void ManageRoamingPoints()
    {
        foreach (Transform spot in moveSpots)
        {
            spot.position = new Vector3(initialPosition.x + Random.Range(-4, 4), initialPosition.y + Random.Range(-4, 4));
        }
    }
    private void CreateInitialPoints()
    {
        initialPosition = gameObject.transform.position;
        Transform storage = GameObject.FindGameObjectWithTag("MovePointStorage").transform;
        moveSpot1 = new GameObject("MoveSpot1").transform;
        moveSpot1.SetParent(storage);
        moveSpot2 = new GameObject("MoveSpot2").transform;
        moveSpot2.SetParent(storage);
        moveSpot1.position = new Vector3(transform.position.x + Random.Range(1,-1), transform.position.y - Random.Range(2, -2));
        moveSpot2.position = new Vector3(transform.position.x - Random.Range(2, -2), transform.position.y + Random.Range(2, -2));
        moveSpots.Add(moveSpot1);
        moveSpots.Add(moveSpot2);
    }
}
