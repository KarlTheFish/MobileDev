using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureLoad : MonoBehaviour
{
    private int TextureID;
    private string TexturePath;
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
        switch (gameObject.tag)
        {
            case "Fish1":
                TexturePath += "FishTexture"; 
                break;
            case "Fish2":
                TexturePath += "FishTexture1";
                break;
            case "Fish3":
                TexturePath += "FishTexture2";
                break;
        }
        TexturePath += "/Fish" + TextureID;
        
        gameObject.GetComponent<MeshRenderer>().materials[0].mainTexture = Resources.Load<Texture>(TexturePath);
    }
}
