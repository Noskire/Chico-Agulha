using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public Transform player;
    public float suavisacaoCamera = 0.5f;
    public Vector2 velociade;

	// Use this for initialization
	void Start () {
        this.velociade = new Vector2(0.5f,0.5f);
	    
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 novaposicao = Vector2.zero;
        novaposicao.x = Mathf.SmoothDamp(this.transform.position.x, this.player.position.x, ref this.velociade.x, suavisacaoCamera);
        novaposicao.y = Mathf.SmoothDamp(this.transform.position.y, this.player.position.y + 3.8f, ref this.velociade.y, suavisacaoCamera);

        Vector3 novaposicaoCamera = new Vector3(novaposicao.x, novaposicao.y, this.transform.position.z);

        this.transform.position = Vector3.Slerp(this.transform.position,novaposicaoCamera,Time.time);

	}
}
