using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlashLigth : MonoBehaviour
{
    public GameObject Player;
    Animator playerAnim;
    SpriteRenderer playerSprite;
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    private float power;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = Player.GetComponent<Animator>();
        playerSprite = Player.GetComponent<SpriteRenderer>();
        Light = this.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        power = 0f;
    }

    // Update is called once per frame
    private void Update()
    {   
        if (playerAnim.GetBool("LookUp")) {
            this.transform.eulerAngles = Vector3.forward;
        } else if (playerSprite.flipX) {
            this.transform.eulerAngles = Vector3.forward *  90f;
        } else {
            this.transform.eulerAngles = Vector3.forward * - 90f;
        }
        Vector3 pos = new Vector3(Player.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = pos;
        Light.pointLightOuterRadius = power;
    }

    public void setPower(float p) {
        power = p;
    }

    private void HandleLightRot(){
        if(playerSprite.flipX){
            this.transform.eulerAngles = Vector3.forward *  90f;
        } else {
            this.transform.eulerAngles = Vector3.forward * - 90f;
        }
    }

    public IEnumerator TurnOnLight(){
        HandleLightRot();
        Light.pointLightOuterRadius = power;
        yield return new WaitForSeconds(0.3f);
        HandleLightRot();
        Light.pointLightOuterRadius = 0;
        yield return new WaitForSeconds(0.5f);
        HandleLightRot();
        Light.pointLightOuterRadius = power;
        yield return new WaitForSeconds(0.2f);
        HandleLightRot();
        Light.pointLightOuterRadius = 0;
        yield return new WaitForSeconds(0.2f);
        HandleLightRot();
        Light.pointLightOuterRadius = power;
        yield return new WaitForSeconds(0.5f);
        HandleLightRot();
        Light.pointLightOuterRadius = 0;
        yield return new WaitForSeconds(0.2f);
        HandleLightRot();
        Light.pointLightOuterRadius = power;
    }

    public IEnumerator TurnOffLight(){
        HandleLightRot();
        Light.pointLightOuterRadius = 0;
        yield return new WaitForSeconds(0.3f);
        HandleLightRot();
        Light.pointLightOuterRadius = 10;
        yield return new WaitForSeconds(0.5f);
        HandleLightRot();
        Light.pointLightOuterRadius = 0;
        yield return new WaitForSeconds(0.2f);
        HandleLightRot();
        Light.pointLightOuterRadius = 10;
        yield return new WaitForSeconds(0.2f);
        HandleLightRot();
        Light.pointLightOuterRadius = 0;
        yield return new WaitForSeconds(0.5f);
        HandleLightRot();
        Light.pointLightOuterRadius = 10;
        yield return new WaitForSeconds(0.2f);
        HandleLightRot();
        Light.pointLightOuterRadius = 0;
    }
}
