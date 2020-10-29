using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 MovementVector;
    [SerializeField] float period = 2f;

    [Range(0, 1)] [SerializeField] float movementFactor;
    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycle * tau);
        // print(rawSinWave);
        movementFactor = (rawSinWave / 2f) + .5f;
        Vector3 offset = movementFactor * MovementVector;
        transform.position = startingPos + offset;
    }
}
