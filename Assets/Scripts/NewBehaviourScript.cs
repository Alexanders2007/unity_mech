﻿using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public float k = 10f;  // коэффициент упругости
    public float m = 1f;   // масса
    public float amplitude = 5f;  // амплитуда колебаний
    public float phase = 0f;      // фаза колебаний
    public float damping = 0.1f; // коэффициент затухания
    public Text countText;  // текст для счётчика
    public Text winText;  // текст для победы
    public GameObject obstacle; // объект препятствия

    private Rigidbody rb;
    private int count;
    private float omega;  // собственная частота
    private float omegaDamped;  // затухающая частота
    private Vector3 initialPosition; // начальная позиция объекта
    public float springoobstacle;   //коэффициент упругости препятствия

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        initialPosition = transform.position; // сохраняем начальную позицию
        omega = Mathf.Sqrt(k / m);  // вычисляем собственную частоту
        omegaDamped = Mathf.Sqrt(omega * omega - (damping * damping) / (4 * m * m)); // вычисляем затухающую частоту
    }

    void FixedUpdate()
    {
        // Текущее время в секундах
        float time = Time.time;

        // Расчитаем положение объекта по решению дифференциального уравнения с затуханием
        float x = amplitude * Mathf.Exp(-damping * time / (2 * m)) * Mathf.Cos(omegaDamped * time + phase); // положение x(t)

        // Целевая позиция
        Vector3 targetPosition = initialPosition + new Vector3(x, 0f, 0f);

        // Плавное перемещение к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        // Проверка на столкновение с препятствием
        if (IsCollidingWithObstacle())
        {
            // Изменение фазы для моделирования столкновения
            phase += springoobstacle*Mathf.PI;
        }
    }
    
    bool IsCollidingWithObstacle()
    {
        if (obstacle != null)
        {
            BoxCollider boxCollider = obstacle.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Vector3 obstaclePosition = obstacle.transform.position;
                Vector3 obstacleSize = boxCollider.size;

                // Проверяем, находится ли объект в пределах параллелепипеда
                if (Mathf.Abs(transform.position.x - obstaclePosition.x) <= obstacleSize.x / 2 &&
                    Mathf.Abs(transform.position.y - obstaclePosition.y) <= obstacleSize.y / 2 &&
                    Mathf.Abs(transform.position.z - obstaclePosition.z) <= obstacleSize.z / 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
