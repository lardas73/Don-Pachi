using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocicaTetrominos : MonoBehaviour
{
    private float tiempoAnterior;

    public float tiempoCaida = 0.8f;
    public static int alto = 20, ancho = 10;
    public Vector3 puntoRotation;
    private static Transform[,] grid = new Transform[ancho, alto];
    private LogicaGenerador _logicaGenerador;
    public static int puntuacion = 0, nivelDeDificultad = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _logicaGenerador = FindObjectOfType<LogicaGenerador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            if (!Limites())
            {
                transform.position -= Vector3.left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            if (!Limites())
            {
                transform.position -= Vector3.right;
            }
        }

        if (Time.time - tiempoAnterior > (Input.GetKey(KeyCode.DownArrow) ? tiempoCaida /20: tiempoCaida))
        {
            transform.position += Vector3.down;
            if (!Limites())
            {
                transform.position -= Vector3.down;
                AnadirAlGrid();
                RevisarLineas();
                this.enabled = false;
                _logicaGenerador.NuevoTetronimo();
            }

            tiempoAnterior = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(puntoRotation), Vector3.forward, -90);
            if (!Limites())
            {
                transform.RotateAround(transform.TransformPoint(puntoRotation), Vector3.forward, 90);
            }
        }
    }

    bool Limites()
    {
        foreach (Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            if (enteroX < 0 || enteroX >= ancho || enteroY < 0 || enteroY >= alto)
            {
                return false;
            }

            if (grid[enteroX, enteroY] != null)
            {
                return false;
            }
        }
        return true;
    }

    void AnadirAlGrid()
    {
        foreach (Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            grid[enteroX, enteroY] = hijo;

            if (enteroY >= alto - 1)
            {
                tiempoCaida = 0.8f;
                nivelDeDificultad = 0;
                puntuacion = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void RevisarLineas()
    {
        for (int i = alto -1; i >= 0; i--)
        {
            if (TieneLinea(i))
            {
                BorrarLinea(i);
                BajarLinea(i);
            }
        }
    }

    bool TieneLinea(int linea)
    {
        for (int j = 0; j < ancho; j++)
        {
            if (grid[j, linea] == null)
            {
                return false;
            }
        }

        puntuacion += 100;
        AumentarNivel();
        AumentarDificultad();
        return true;
    }

    void BorrarLinea(int linea)
    {
        for (int j = 0; j < ancho; j++)
        {
            Destroy(grid[j, linea].gameObject);
            grid[j, linea] = null;
        }
    }

    void BajarLinea(int linea)
    {
        for (int y = linea; y < alto; y++)
        {
            for (int j = 0; j < ancho; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= Vector3.up;
                }
            }
        }
    }

    void AumentarNivel()
    {
        switch (puntuacion)
        {
            case 200:
                nivelDeDificultad = 1;
                break;
            case 200:
                nivelDeDificultad = 1;
                break;
        }
    }

    void AumentarDificultad()
    {
        switch (nivelDeDificultad)
        {
            case 1:
                tiempoCaida = 0.4f;
                break;
            case 2:
                tiempoCaida = 0.2f;
                break;
        }
    }
}
