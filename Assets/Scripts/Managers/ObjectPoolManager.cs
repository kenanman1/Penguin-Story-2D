using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }
    [SerializeField] private GameObject objectPrefab;
    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objectPrefab),
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    public GameObject GetObject()
    {
        return pool.Get();
    }

    public void ReturnObject(GameObject obj)
    {
        pool.Release(obj);
    }
}
