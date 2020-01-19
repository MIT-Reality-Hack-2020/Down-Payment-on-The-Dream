using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pullTab_manager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public float maxPull = 2f;
    public GameObject pullTabObj;
    public GameObject houseObj;
    public GameObject mailObj;
    public GameObject EnvObj;
    public GameObject Palm;
    public bool isOpen = false;
    public float speed = 1f;
    public float houseSpeed = 1f;
    Vector3 currentPos;
    public Animator mailanim;
    public Animator envanim;
   


    // Start is called before the first frame update
    void Start()
    {
        currentPos = new Vector3(pullTabObj.transform.position.x, pullTabObj.transform.position.y, pullTabObj.transform.position.z);
        mailanim = mailObj.GetComponent<Animator>();
        envanim = EnvObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(isOpen == true)
        {
            Moving();
            
        } 

      


    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isOpen = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }


    /*
    private void OnMouseDown()
    {
        isOpen = true;
    }

    private void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    */
    void Moving()
    {
        Debug.Log("working");
       
            
           // Vector3 newPos = new Vector3(pullTabObj.transform.position.x, pullTabObj.transform.position.y, currentPos.z + maxPull);
           // transform.position = Vector3.Lerp(pullTabObj.transform.position, newPos, Time.deltaTime * speed);


        if(this.gameObject.name == "pulltab_house" || this.gameObject.name == "btn_orb_house")
        {
            /*
            float tiltingUp = Input.GetAxis("Vertical") * 90f;
            Quaternion target = Quaternion.Euler(tiltingUp, 0, 0);
            houseObj.transform.rotation = Quaternion.Slerp(houseObj.transform.rotation, target, Time.deltaTime * houseSpeed);
            //Palm.transform.rotation = Quaternion.Slerp(Palm.transform.rotation, target, Time.deltaTime * houseSpeed);
            Debug.Log("house");
            */
        }
         
        /*
        if(this.gameObject.name == "pulltab_mail")
        {
            mailanim.SetBool("isPulled", true);
            envanim.SetBool("mailPulled", true);
            Debug.Log("mail");
        }
        */

      

      

    }
}
