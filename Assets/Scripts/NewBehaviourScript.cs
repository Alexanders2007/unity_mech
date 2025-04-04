using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public float gravity = 9.8f;  // ускорение свободного падения
    public float m = 1f;   // масса
    public GameObject otherBall; // другой шарик
    public float coefficientOfRestitution = 0.8f;  // коэффициент восстановления

    private Rigidbody rb;
    private int count;
    private Vector3 initialVelocity; // начальная скорость объекта

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        initialVelocity = new Vector3(0f, 0f, 0f); // начальная скорость
        rb.velocity = initialVelocity;
    }

    void FixedUpdate()
    {
        // Применяем силу тяжести
        rb.AddForce(Vector3.down * gravity * m);

        // Проверка на столкновение с другим шариком
        if (IsCollidingWithOtherBall())
        {
            // Моделируем отскок с заданным коэффициентом восстановления
            Vector3 relativeVelocity = rb.velocity - otherBall.GetComponent<Rigidbody>().velocity;
            Vector3 collisionNormal = (transform.position - otherBall.transform.position).normalized;
            float impulseMagnitude = -(1 + coefficientOfRestitution) * Vector3.Dot(relativeVelocity, collisionNormal) / (1 / m + 1 / otherBall.GetComponent<Rigidbody>().mass);
            Vector3 impulse = impulseMagnitude * collisionNormal;

            rb.AddForce(impulse, ForceMode.Impulse);
        }
    }

    bool IsCollidingWithOtherBall()
    {
        if (otherBall != null)
        {
            SphereCollider sphereCollider = otherBall.GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                Vector3 otherBallPosition = otherBall.transform.position;
                float otherBallRadius = sphereCollider.radius;
                float thisBallRadius = GetComponent<SphereCollider>().radius;

                // Проверяем, находится ли объект в пределах сферы
                if (Vector3.Distance(transform.position, otherBallPosition) <= otherBallRadius + thisBallRadius)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
