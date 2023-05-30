using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsBrain : MonoBehaviour
{
    public int numBoids;
    public float simulationSpeed;

    public float coherence_bias;
    public float collision_bias;
    public float velocity_bias;
    public float speed_bias;

    public Coherence coherence;
    public Separation separation;
    public Alignment alignment;
    
    public GameObject boidPrefab;
  
    internal Boid[] boids;

    void Start()
    {
        //Spawn random boids
        boids = new Boid[numBoids];
        for (int i = 0; i < numBoids; i++) {
            GameObject b = Instantiate(boidPrefab,transform);
            b.transform.position = new Vector3(Random.Range(CreateBounds.xMin, CreateBounds.xMax), Random.Range(CreateBounds.yMin, CreateBounds.yMax),0);
            boids[i] = b.GetComponent<Boid>();
            boids[i].direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            boids[i].speed = Random.Range(1,5);
        }
        
    }

    void LateUpdate()
    {
        //coherence
        Vector2 coherenceVector;

        //separation
        Vector2 collisionVector;
        Vector2 hardCollisionVector;
        float hardCollisionBias;

        //alignment
        Vector2 velocityMatcherVector;
        float speedMatch;

        // for each boid
        for (int i = 0; i < boids.Length; i++)
        {
            //calculate modifications
            coherenceVector = coherence.GetCoherence(boids[i]);
            collisionVector = separation.GetSoftColissionAvoidance(boids[i]);
            hardCollisionVector = separation.GetHardColissionAvoidance(boids[i], out hardCollisionBias);
            velocityMatcherVector = alignment.GetVelocityMatchVector(boids[i], out speedMatch);

            //add modifications to boid
            boids[i].AdjustVelocityBy(coherenceVector, coherence_bias);
            boids[i].AdjustVelocityBy(collisionVector, collision_bias);
            boids[i].AdjustVelocityBy(velocityMatcherVector, velocity_bias);
            boids[i].AdjustVelocityBy(hardCollisionVector, hardCollisionBias);
            boids[i].AdjustSpeedBy(speedMatch, speed_bias);

            //supply speed multiplier
            boids[i].speedMultiplier = simulationSpeed;
        }
    }


}
