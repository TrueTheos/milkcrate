using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    void Start()
    {
        
        if (!instance) {
            DontDestroyOnLoad(gameObject);

            instance = this;
        }else{
            Destroy(gameObject);
        }

    }
}
