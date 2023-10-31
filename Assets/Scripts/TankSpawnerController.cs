using System.Linq;
using UnityEngine;

public class TankSpawnerController : MonoBehaviour
{

    private float minX, maxX, minY, maxY;

    [SerializeField] private Transform[] spawners;

    [SerializeField] private GameObject[] enemigos;

    [SerializeField] private float Reaparicion;

    private float esperaReaparicion;

    private void Start()
    {
        maxX = spawners.Max(spawner => spawner.position.x);
        minX = spawners.Min(spawner => spawner.position.x);
        maxY = spawners.Max(spawner => spawner.position.y);
        minY = spawners.Min(spawner => spawner.position.y);

        for (int i = 0; i < 4; i++)
        {
            CrearEnemigo();
        }
    }

    private void Update()
    {
        esperaReaparicion += Time.deltaTime;

        if (esperaReaparicion >= Reaparicion)
        {
            esperaReaparicion = 0;
            for (int i = 0; i < 3; i++)
            {
                CrearEnemigo();
            }
        }

    }

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);

        Vector2 posicionAleatoria = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        if (enemigos[numeroEnemigo] != null)
        {
            Instantiate(enemigos[numeroEnemigo], posicionAleatoria, Quaternion.identity);
        }
    }

}
