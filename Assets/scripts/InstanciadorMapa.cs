using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System;

public class InstanciadorMapa : MonoBehaviour {

    public GameObject[] objetos;
    public GameObject[] cliparts;
    public TextAsset mapa;
    private const float tilePositionX_Inicial = -6.3f;
    private float tilePositionY = 4.5f;
    private float tilePositionX = tilePositionX_Inicial;
    private float tilePositionZ = 0f;
	// Use this for initialization
	void Start () {
        //this.mapa = (TextAsset)Resources.Load("Mapa1.txt");
        String[] lines = this.mapa.text.Split("\n"[0]);

        for (int i = 0; i < lines.Length;i++ )
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                /*
                Debug.Log(lines[i][j]);
                if (lines[i][j] == '#')
                {
                    Instantiate(objetos[0], new Vector3(tilePositionX, tilePositionY, tilePositionZ), objetos[0].transform.rotation);
                    tilePositionX += objetos[0].collider2D.bounds.size.x + 0.7f;
                }
                else
                    if (lines[i][j] == '#')
                        tilePositionX += (objetos[0].collider2D.bounds.size.x + 0.7f);
                */


                //Debug.Log(lines[i][j]);
                switch( (lines[i][j]) ){
                    case '#': 
                        Instantiate(objetos[0], new Vector3(tilePositionX, tilePositionY, tilePositionZ), objetos[0].transform.rotation);
                        tilePositionX += objetos[0].collider2D.bounds.size.x + 0.7f;
                        break;
                    case '0':
                        Instantiate(objetos[1], new Vector3(tilePositionX, tilePositionY, tilePositionZ), objetos[1].transform.rotation);
                        tilePositionX += objetos[1].collider2D.bounds.size.x + 0.7f;
                        break;
                    case '.':
                        tilePositionX += objetos[0].collider2D.bounds.size.x + 0.7f;
                        break;
                    case 'P':
                        int indice = UnityEngine.Random.Range(0,cliparts.Length);
                        Instantiate(cliparts[indice], new Vector3(tilePositionX, tilePositionY, tilePositionZ), objetos[1].transform.rotation);
                        tilePositionX += objetos[1].collider2D.bounds.size.x + 0.7f;
                        break;
                    default:
                        tilePositionX = tilePositionX_Inicial;
                        tilePositionY = (tilePositionY - (objetos[0].collider2D.bounds.size.y + 0.7f));
                        break;

                }


            }
        }

/*        for (int i = 0; i < 10; i++)
        {
            Instantiate(objetos[0], new Vector3(tilePositionX, tilePositionY, tilePositionZ), objetos[0].transform.rotation);
            tilePositionX += objetos[0].collider2D.bounds.size.x + 0.7f;
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
