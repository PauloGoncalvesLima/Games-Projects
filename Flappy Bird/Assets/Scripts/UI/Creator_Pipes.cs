using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator_Pipes : MonoBehaviour
{
    public GameObject Pipe_prefab;
    private float time;
    public float difficulty;
    public int CurrentValue;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        CurrentValue = 0;
        difficulty = 2f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Brid_Controller.points / 10 != CurrentValue)
        {
            CurrentValue++;
            difficulty -= 0.02f;
            if (difficulty <= 1.30f)
            {
                difficulty = 1.30f;
            }
        }

        if (Brid_Controller.life == false)
        {
            this.enabled = false;
        }

        if (time >= difficulty)
        {
            Instantiate(Pipe_prefab, new Vector3(3, Random.Range(1.2f, -0.8f), 5), Quaternion.identity);
            time = 0f;
        }
        time += Time.deltaTime;
    }
}
