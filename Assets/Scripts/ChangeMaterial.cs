using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    public GameObject modelo;
    //public Color color;
    public Material[] material;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMaterialColor_BTN()
    {
        modelo.GetComponent<Renderer>().material = material[i];
        i++;
        if (i >= material.Length)
        {
            i = 0;  // Reinicia el índice a 0
        }
    }
}
