using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] GameObject enemy;

    float generateInterbal = 0;             // ¶¬Œã‚ÌŒo‰ßŽžŠÔ
    [SerializeField] float generateTime;    // ¶¬‚·‚éŽžŠÔ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        generateInterbal += Time.deltaTime;

        if(generateInterbal >= generateTime)
        {
            generateInterbal = 0;
            var rnd_idx = Random.Range(0, enemyData.enemyStats.Count);

            GameObject obj = Instantiate(enemy, transform.position, Quaternion.identity);
            EnemyController e = obj.GetComponent<EnemyController>();
            e.SetStats(enemyData.enemyStats[rnd_idx]);
        }
    }
}
