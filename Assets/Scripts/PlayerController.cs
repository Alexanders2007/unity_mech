using UnityEngine;
using UnityEngine.UI;
 
public class PlayerController : MonoBehaviour
{
    public float length = 5f;  // длина маятника
    public Text countText;    // UI text for the counter
    public Text winText;      // UI text for the win message
        // Начальные условия движения
    public float angle = Mathf.PI / 4; // начальный угол отклонения (45 градусов в радианах)
    public float angularVelocity = 0f; // начальная угловая скорость
    private Rigidbody rb;
    private int count;
    private float omega;      // угловая частота
    private Vector3 initialPosition; // начальная позиция объекта

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        initialPosition = transform.position; // сохраняем начальную позицию
        omega = Mathf.Sqrt(Physics.gravity.magnitude / length); // вычисляем угловую частоту
    }

    void FixedUpdate()
    {
        // Обновляем угловое ускорение
        float angularAcceleration = -(Physics.gravity.magnitude / length) * Mathf.Sin(angle);

        // Обновляем угловую скорость и угол отклонения с использованием метода Эйлера
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;
        angle += angularVelocity * Time.fixedDeltaTime;

        // Расчет положения объекта
        float x = length * Mathf.Sin(angle); // положение x(t)
        float y = -length * Mathf.Cos(angle); // положение y(t) (отрицательное, так как вниз)

        // Целевая позиция
        Vector3 targetPosition = initialPosition + new Vector3(x, y, 0f);

        // Плавное перемещение к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }
}


/*
using UnityEngine;
using UnityEngine.UI;
//матмаятник
public class PlayerController : MonoBehaviour
{
    public float k = 10f;  // коэффициент упругости
    public float m = 1f;   // масса
    public float amplitude = 5f;  // амплитуда колебаний
    public float phase = 0f;      // фаза колебаний
    public Text countText;  // текст для счётчика
    public Text winText;  // текст для победы

    private Rigidbody rb;
    private int count;
    private float omega;  // собственная частота

    private Vector3 initialPosition; // начальная позиция объекта

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        initialPosition = transform.position; // сохраняем начальную позицию
        omega = Mathf.Sqrt(k / m);  // вычисляем собственную частоту
    }
    void FixedUpdate()
    {
        // Текущее время в секундах
        float time = Time.time;

        // Расчитаем положение объекта по решению дифференциального уравнения
        float x = amplitude * Mathf.Cos(omega * time + phase); // положение x(t)
            

            // Целевая позиция
            Vector3 targetPosition = initialPosition + new Vector3(x, 0f, 0f);

        // Плавное перемещение к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
*/
