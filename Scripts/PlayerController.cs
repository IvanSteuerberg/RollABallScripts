using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue){
       Vector2 movementVector = movementValue.Get<Vector2>();

       movementX = movementVector.x;
       movementY = movementVector.y;    
    }

    void SetCountText(){
        countText.text= "Puntuación: "+count.ToString();
        if (count >=10){
            winTextObject.SetActive(true);         
        }
    }
    
    private void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // recogemos los datos del acelerometro
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        
        dir *= Time.deltaTime;
        transform.Translate(dir * speed);    
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PickUp")){
        other.gameObject.SetActive(false);
        count++;
        SetCountText();
        }
        if(other.gameObject.CompareTag("Enemy")){
            if (count>0)
            {
            count--;
            SetCountText();
            }
            other.gameObject.transform.position=new Vector3(9,1,9);
            gameObject.transform.position=new Vector3(0,1,0);
        }

    }


}
