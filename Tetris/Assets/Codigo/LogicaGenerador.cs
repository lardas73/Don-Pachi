using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaGenerador : MonoBehaviour
{
    public GameObject[] tetrominos;
    
    // Start is called before the first frame update
    void Start()
    {
        NuevoTetronimo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NuevoTetronimo()
    {
        int indice =  Random.Range(0, tetrominos.Length);
        Instantiate(tetrominos[indice], transform.position, Quaternion.identity);
    }
}
