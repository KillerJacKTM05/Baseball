using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class TapShoot : MonoBehaviour
{
    [Range(0, 20)] [SerializeField] private float turnSpeed = 6f;
    [Range(10f, 100f)] [SerializeField] private float bruteForce = 10f;
    private bool isFingerTouching = false;
    private Rigidbody rb;

    private float counter = 0;
    [SerializeField] Vector3 collisionPoint;
    private Vector3 fixedPoint;

    public LeanFinger fingerTouch;
    public GameObject player;
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Tap();
    }
    private void Tap()
    {
        var fingers = LeanTouch.Fingers;
        isFingerTouching = fingers.Count > 0;
        if (isFingerTouching)
        {
            fingerTouch = fingers[0];
            if (fingers[0].Down)
            {
                counter = 0;
                StartCoroutine(Shot(counter));  
            }
            else if (fingers[0].Set)
            {

            }
            else if (fingers[0].Up)
            {

            }
        }
    }
    private IEnumerator Shot(float count)
    {
        while (count <= 1)
        {
            count = count + (turnSpeed * 0.01f);
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rb.rotation.x, -90, rb.rotation.z), count);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.1f);
        count = 0;
        StartCoroutine(Release(count));
    }
    private IEnumerator Release(float count)
    {
        while (count <= 1)
        {
            count = count + (turnSpeed * Time.deltaTime);
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rb.rotation.x, 0, rb.rotation.z), count);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        collisionPoint = other.ClosestPoint(player.transform.position);
        if (other.transform.position.y - collisionPoint.y > 0.05f)
        {
            fixedPoint = new Vector3(-collisionPoint.x, 0.3f, 0);
        }
        else if (other.transform.position.y - collisionPoint.y < 0.05f || other.transform.position.y - collisionPoint.y > -0.05f)
        {
            fixedPoint = new Vector3(-collisionPoint.x, 0.1f, 0);
        }
        else if (other.transform.position.y - collisionPoint.y < -0.05f)
        {
            fixedPoint = new Vector3(-collisionPoint.x, -0.3f, 0);
        }


        other.gameObject.GetComponent<Rigidbody>().AddForce(fixedPoint * bruteForce, ForceMode.Impulse);
        Debug.Log(collisionPoint);
    }
}
