using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlashLigth : MonoBehaviour
{
    public GameObject Player;
    Animator playerAnim;
    SpriteRenderer playerSprite;
    RaycastHit2D hit;
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    private float power;

    private bool runningCoroutine;
    private bool hitLightSensor;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = Player.GetComponent<Animator>();
        playerSprite = Player.GetComponent<SpriteRenderer>();
        Light = this.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        power = 0f;
        runningCoroutine = false;
        hitLightSensor = false;
    }

    // Update is called once per frame
    private void Update()
    {       
        if (playerAnim.GetBool("LookUp")) {
            this.transform.eulerAngles = Vector3.forward;
            hit = handleLineCast(Vector3.up);

        } else if (playerSprite.flipX) {

            this.transform.eulerAngles = Vector3.forward *  90.0f;
            hit = handleLineCast(Vector3.left);

        } else {

            this.transform.eulerAngles = Vector3.forward * -90.0f;
            hit = handleLineCast(Vector3.right);
            
        }

        if(hit != false){
            //  Debug.Log(hit.transform.name);
             if(hit.transform.tag == "LightSensor"){
                //  Debug.Log("Hit Light");
                 hit.transform.GetComponent<LightSensor>().isCharging = true;
                 hitLightSensor = true;
             }
        } else {
            hitLightSensor = false;
        }
       
        this.transform.position = new Vector3(Player.transform.position.x, this.transform.position.y, this.transform.position.z);
        if(!runningCoroutine){
            Light.pointLightOuterRadius = power;
        }
        
    }

    public bool GethitLightSensor(){
        return this.hitLightSensor;
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
        runningCoroutine = true;
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
        runningCoroutine = false;
    }

    public IEnumerator TurnOffLight(){
        runningCoroutine = true;
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
        power  = 0;
        runningCoroutine = false;
    }

    private RaycastHit2D handleLineCast(Vector3 pos){
        
        if(power >= 10){

            Debug.DrawLine(this.transform.position, this.transform.position + pos * power/2);
            return Physics2D.Linecast(this.transform.position, this.transform.position + pos * power/2);
            
        } else if(power < 10 && power > 0){
            Debug.DrawLine(this.transform.position, this.transform.position + pos * power);
            return Physics2D.Linecast(this.transform.position, this.transform.position + pos * power);
        } else {
            Debug.DrawLine(this.transform.position, this.transform.position);
            return Physics2D.Linecast(this.transform.position, this.transform.position);
        }
        
    }
}
