using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spawner : MonoBehaviour
{
    [SerializeField]  List<GameObject> spawnedObj;
    [SerializeField]private List<GameObject> destroy;
    [Range(0,5f)][SerializeField] float spawntime = 1f;
    [Range(0, 5f)] [SerializeField] float lifeTime = 3f;
    private float spawnPoint;
    private Vector3 spawnVector = new Vector3(0, 5f, 0);

    private float counter = 0;
    void Start()
    {
        StartCoroutine(GenerateObj());
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
    }
    private IEnumerator GenerateObj()
    {
        while (true)
        {
            spawnPoint = Random.Range(-2.2f, 0);
            yield return new WaitForSeconds(spawntime);
            destroy.Add(Instantiate(spawnedObj[Random.Range(0, spawnedObj.Count)], new Vector3(spawnVector.x, spawnVector.y, spawnPoint), Quaternion.identity));
            StartCoroutine(destroyyy());
            yield return new WaitForFixedUpdate();
        }
       
    }
    private IEnumerator destroyyy()
    {
        while (counter < lifeTime)
        {
            yield return new WaitForFixedUpdate();
        }

        Destroy(destroy[0]);
        destroy.RemoveAt(0);
        counter = 0;
        yield return new WaitForSeconds(0.1f);
    }
}
