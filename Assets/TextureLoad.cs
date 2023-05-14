using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureLoad : MonoBehaviour
{
    private int TextureID;
    // Start is called before the first frame update
    void Start()
    {
        GenerateTexture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTexture() {
        TextureID = Random.Range(0, 4); //change 4 to however many textures you have
        gameObject.GetComponent<MeshRenderer>().materials[0].mainTexture = Resources.Load<Texture>("FishTexture/Fish" + (TextureID + 3)); //The textireID + 3 is there because I was too lazy to change the texture filenames, cope and seethe
    }
}
