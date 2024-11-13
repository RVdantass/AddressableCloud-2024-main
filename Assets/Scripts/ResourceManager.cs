using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : MonoBehaviour
{

    public AssetReference assetReference;
    AsyncOperationHandle<GameObject> handle;
    List<GameObject> prefabs = new List<GameObject>();

    private int counterSpawn0 = 0;
    private int counterSpawn2 = 0;
    private int counterSpawn3 = 0;
    private int counterSpawn5 = 0;

    private int spawnNumber = 0;

    void Awake(){
        spawnNumber = this.gameObject.layer;
    }


    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            if(spawnNumber == 6) counterSpawn0++;
            if(spawnNumber == 7) counterSpawn2++;
            if(spawnNumber == 8) counterSpawn3++;
            if(spawnNumber == 9) counterSpawn5++;
            if(prefabs.Count <= 0) LoadAsset();
        }
        
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            if(spawnNumber == 6) counterSpawn0--;
            if(spawnNumber == 7) counterSpawn2--;
            if(spawnNumber == 8) counterSpawn3--;
            if(spawnNumber == 9) counterSpawn5--;
            if(prefabs.Count > 0){
                if(spawnNumber == 0) UnloadAsset();
                else if(spawnNumber == 6 && counterSpawn0 == 0) UnloadAsset();
                else if(spawnNumber == 7 && counterSpawn2 == 0) UnloadAsset();
                else if(spawnNumber == 8 && counterSpawn3 == 0) UnloadAsset();
                else if(spawnNumber == 9 && counterSpawn5 == 0) UnloadAsset();
            }
        }
    }

    void LoadAsset(){
        handle = assetReference.InstantiateAsync(transform.position, Quaternion.identity);
        handle.Completed += handle => {prefabs.Add(handle.Result);};
    }

    void UnloadAsset(){
        foreach(GameObject prefab in prefabs){
            Addressables.ReleaseInstance(prefab);
        }
        prefabs.Clear();
    }

   
}
