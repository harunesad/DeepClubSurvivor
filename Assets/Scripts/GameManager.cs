using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    [SerializeField] Transform enemies;
    [SerializeField] List<Transform> enemyList;
    [SerializeField] NavMeshSurface surface;
    public Transform player;
    void Start()
    {
        player = Instantiate(characters[DataSave.Instance.characterIndex], Vector3.zero, Quaternion.identity).transform;
        enemyList.Add(enemies.GetChild(0));
        enemyList[0].GetComponent<NavMeshAgent>().SetDestination(player.GetComponent<PlayerControl>().points[0].position);
        enemyList[0].GetComponent<Animator>().SetBool("Walk", true);
        InvokeRepeating("Builder", 0, 2);
        //surface.BuildNavMesh();
    }
    void Builder()
    {
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].GetComponent<NavMeshAgent>().hasPath)
            {
                enemyList[i].transform.LookAt(player.position);
                enemyList[i].GetComponent<Animator>().SetBool("Walk", false);
                bool posX = Mathf.Approximately(player.GetComponent<PlayerControl>().points[i].position.x, enemyList[i].position.x);
                bool posZ = Mathf.Approximately(player.GetComponent<PlayerControl>().points[i].position.z, enemyList[i].position.z);
                Debug.Log(Mathf.Approximately(player.GetComponent<PlayerControl>().points[i].position.z, enemyList[i].position.z));
                if (!posX || !posZ)
                {
                    enemyList[i].GetComponent<NavMeshAgent>().SetDestination(player.GetComponent<PlayerControl>().points[i].position);
                    enemyList[i].GetComponent<Animator>().SetBool("Walk", true);
                }
            }
        }
    }
}
