using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Vector3 initialPosition; // Начальная позиция игрока
    public float gravityStrength = 9.8f; // Сила гравитационного поля
    public float mass = 1.0f; // Масса игрока
    public Vector3 centralForcePosition = new Vector3(0, 0, 0); // Позиция центральной силы (точка 0, 0, 0)
    public Vector3 initialVelocity = Vector3.zero; // Начальная скорость игрока

    private Rigidbody rb; // Компонент Rigidbody игрока

    void Start()
    {
        // Инициализация начальной позиции
        transform.position = initialPosition;
        rb = GetComponent<Rigidbody>(); // Получение компонента Rigidbody
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>(); // Добавление компонента Rigidbody, если он отсутствует
        }
        rb.useGravity = false; // Отключение гравитации Unity, так как мы используем свою
        rb.mass = mass; // Установка массы объекта
        rb.velocity = initialVelocity; // Установка начальной скорости
    }

    void FixedUpdate()
    {
        // Вычисление вектора направления от игрока к центральной силе
        Vector3 direction = centralForcePosition - transform.position;
        float distance = direction.magnitude;

        // Избегаем деления на ноль
        if (distance > 0)
        {
            // Вычисление гравитационной силы
            Vector3 gravitationalForce = direction.normalized * (gravityStrength * rb.mass * mass / (distance * distance));

            // Применение силы к Rigidbody
            rb.AddForce(gravitationalForce);
        }
    }
}
