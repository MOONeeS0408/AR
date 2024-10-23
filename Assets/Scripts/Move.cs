using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class Move : MonoBehaviour
{
    public GameObject modelo;
    public ObserverBehaviour[] imageTarget;
    public int actual;
    public float speed = 1.0f;
    private bool isMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void moveToNextMarker ()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveModel());
        }
    }

    private IEnumerator MoveModel()
    {
        isMoving = true;
        ObserverBehaviour target = GetNextDetectedTarget();    
        if (target == null)
        {
            isMoving= false;
            yield break;
        }
        Vector3 startPosition = modelo.transform.position; //posicion inicial del modelo
        Vector3 endPosition = target.transform.position; //posicion del siguiente target

        float journey = 0;

        while(journey<=1f) {
            journey+=Time.deltaTime*speed;
            modelo.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            //posicion inicial, posicion final, velocidad
            yield return null;
        }

        actual = (actual + 1) % imageTarget.Length;
        isMoving= false;
    }

    private ObserverBehaviour GetNextDetectedTarget()
    {
        foreach (ObserverBehaviour target in imageTarget)
        {
            if(target !=null && (target.TargetStatus.Status==Status.TRACKED || target.TargetStatus.Status==Status.EXTENDED_TRACKED))
            {
                return target;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
