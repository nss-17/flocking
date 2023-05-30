using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coherence : MonoBehaviour
{
    public GameObject coherencePointVisual;

    private BoidsBrain brain;
    private Vector2 sumOfAllBoidPositions;

    private void Start()
    {
        brain = GetComponent<BoidsBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        sumOfAllBoidPositions = new Vector2();
        foreach (Boid boid in brain.boids)
        {
            sumOfAllBoidPositions += boid.position;
        }
    }


    public Vector2 GetCoherence(Boid currentBoid){

        Vector2 centerOfMassOfBoidsEceptCurrentBoid = (sumOfAllBoidPositions - currentBoid.position) / (brain.boids.Length - 1);
        coherencePointVisual.transform.position = centerOfMassOfBoidsEceptCurrentBoid;
        return (centerOfMassOfBoidsEceptCurrentBoid - currentBoid.position).normalized;

    }
}
