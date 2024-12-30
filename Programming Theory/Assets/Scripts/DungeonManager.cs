using System.Collections;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public DungeonObject dungeon;

    private bool isEnemyPresent;

    // Update is called once per frame
    void Update()
    {
        if (!isEnemyPresent)
        {
            StartCoroutine(EnemySpawn());
        }
    }

    private IEnumerator EnemySpawn()
    {
        isEnemyPresent = true;
        yield return new WaitForSeconds(2);
        GameObject enemy = Instantiate(dungeon.enemyList[Random.Range(0, dungeon.enemyList.Count)], transform);
        yield return new WaitWhile(() => enemy.GetComponent<EnemyController>().enemy.health > 0);
        isEnemyPresent = false;
    }
}
