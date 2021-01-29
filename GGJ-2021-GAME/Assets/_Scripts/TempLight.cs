using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLight : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private GameObject lightSource;




    private void Movement(Vector3 axis)
    {
        transform.position += axis * Time.deltaTime * speed;
    }

    private void LightMove ()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
       transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveAxis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
        Movement(moveAxis);


        LightMove();




    }
}
