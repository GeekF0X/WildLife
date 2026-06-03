using UnityEngine;
using UnityEngine.Splines;

public class RiverFlow : MonoBehaviour
{
    public SplineContainer riverSpline;
    public float speed = 2f;

    float t;
    float entryElapsed = 0, entryDuration = 1.5f;

    Vector3 entryStartPos;
    bool following = false;
    bool entering = false;

    public Rigidbody rb;
    public Transform EndPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("River") && !rb.isKinematic)
        {
            EnterRiver(transform.position);
        }
    }

    public void EnterRiver(Vector3 entryPosition)
    {
        rb.isKinematic = true;
        rb.detectCollisions = false;

        Vector3 localPos = riverSpline.transform.InverseTransformPoint(entryPosition);
        SplineUtility.GetNearestPoint(riverSpline.Spline, localPos, out _, out t);

        entryStartPos = transform.position;

        entryElapsed = 0f;
        entering = true;
    }

    void Update()
    {
        if (entering)
        {
            entryElapsed += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, entryElapsed / entryDuration);

            t += (Time.deltaTime * speed/1.8f) / riverSpline.CalculateLength();
            t = Mathf.Clamp01(t);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 22, 0), 2f);
            transform.position = Vector3.Lerp(entryStartPos, riverSpline.EvaluatePosition(t), alpha);
            if (alpha >= 1f)
            {
                entering = false;
                following = true;
            }
        }
        else if (following)
        {
            t += (Time.deltaTime * speed) / riverSpline.CalculateLength();
            t = Mathf.Clamp01(t);

            Vector3 pos = riverSpline.EvaluatePosition(t);
            transform.position = pos;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 22, 0), 2f);

            if (t >= 1f)
            {
                following = false;
                rb.detectCollisions = true;
                transform.rotation = Quaternion.Euler(0, 22, 0);
                transform.position = EndPoint.position;
            }
        }
    }
}
