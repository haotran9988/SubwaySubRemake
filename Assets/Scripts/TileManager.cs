using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform player;
    private float zSpawn = 0;
    private float tileLength = 50;
    private int lastPrefabsIndex = 0;
    private int amnTileOnScreen = 3; //so prefabs hien thi tren khung hinh la 3
    private List<GameObject> activeTile = new List<GameObject>();//tao 1 list de add cac tilePrefabs sinh ra
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player").transform;
         for(int i=0; i<=amnTileOnScreen; i++)
         {
             if (i == 0)
             {
                 SpawnTile(0);
             }
             else
             {
                 SpawnTile(RandomPrefabsIndex());
                 //SpawnTile(Random.Range(0, tilePrefabs.Length);//neu ko dung ham RandomPrefabsIndex()
             }
         }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > (zSpawn - amnTileOnScreen * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
    void SpawnTile(int index)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[index], transform.forward * zSpawn, transform.rotation);
        activeTile.Add(go);//them tilePrefabs go moi sinh ra vao list activeTile
        zSpawn += tileLength;

    }
    void DeleteTile()
    {
        Destroy(activeTile[0]);//xoa phan tu dau tien trong list
        activeTile.RemoveAt(0);//loai bo phan tu tai vi tri 0 trong list
    }
    int RandomPrefabsIndex() //lay random index
    {
        if (tilePrefabs.Length <= 1)
            return 0;
        int randomIndex = lastPrefabsIndex;
        while (randomIndex == lastPrefabsIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabsIndex = randomIndex;
        return randomIndex;
    }
}
