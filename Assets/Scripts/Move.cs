using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class Move : MonoBehaviour
{
    public GameObject modelo;
    public ObserverBehaviour[] imageTarget;
    public int actual = 0;
    public float speed = 1.0f;
    private bool isMoving = false;

    public void moveToNextMarker()
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

        //Si no encuentra el target
        if (target == null)
        {
            isMoving = false;
            yield break;
        }

        Vector3 startPosition = modelo.transform.position;
        Vector3 endPosition = target.transform.position;

        float journey = 0;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            modelo.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            yield return null;
        }

        // Avanza al siguiente target en la secuencia
        actual++;

        isMoving = false;
    }

    private ObserverBehaviour GetNextDetectedTarget()
    {
        if (actual < imageTarget.Length)
        {
            ObserverBehaviour target = imageTarget[actual];
            if (target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED))
            {
                return target;
            }
        }
        return null;
    }
}
