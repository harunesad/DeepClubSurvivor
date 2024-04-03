using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    [SerializeField] Transform enemies;
    [SerializeField] List<Transform> enemyList;
    Transform player;
    void Start()
    {
        player = Instantiate(characters[DataSave.Instance.characterIndex], Vector3.zero, Quaternion.identity).transform;
        enemyList.Add(enemies.GetChild(0));
        enemyList[0].GetComponent<NavMeshAgent>().SetDestination(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
