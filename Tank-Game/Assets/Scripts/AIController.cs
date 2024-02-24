using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class AIController : Controller
{
    public GameManager manager;


    public enum AIPersonality { Aggressive, Cowardly, Passive, Greedy};
    public AIPersonality currentPersonality;
    public enum AIState { Guard, Chase, Flee, Attack, Patrol, GetPowerUp};
    public AIState currentState;
    private float lastStateChangeTime;
    public GameObject target;

    // FSM Values
    public float attackDistance;
    public float chaseDistance;
    public float fleeDistance;
    // FSM Values - Patrol
    public GameObject[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;

    public float hearingDistance;

    public float fieldOfView;
    public float raycastViewDistance;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        switch (currentPersonality)
        {
            case AIPersonality.Aggressive:
                currentState = AIState.Chase;
                break;
            case AIPersonality.Cowardly:
                currentState = AIState.Flee;
                break;
            case AIPersonality.Passive:
                currentState = AIState.Guard;
                break;
            case AIPersonality.Greedy:
                currentState = AIState.Chase;
                break;
        }
    }
    
    // Update is called once per frame
    // Start is called before the first frame update
    public override void Update()
    {
        // Call Function to make decision
        if (pawn != null)
        {
            if (IsHasTarget())
            {
                MakeDecision();
            }
            else
            {
                TargetNearestTank();
            }
            // run the inherited update
            base.Update();
        }
    }

    // -- [ States & Actions ] --

    public void MakeDecision()
    {
        Debug.Log("Make Decision");
        switch (currentState)
        {
            case AIState.Guard:
                // Guard State
                // Default State
                DoGuardState();
                if(IsDistanceLessThan(target, chaseDistance))
                {
                    ChangeState(AIState.Chase);
                }

                if (IsCanHear(target))
                {
                   ChangeState(AIState.Chase);
                }

                break;
            case AIState.Attack:
                // Shoot the player, if line of sight
                // if line of sight is broken either chase or flee
                DoAttackState();
                if (IsDistanceLessThan(target, attackDistance / 2) && currentPersonality != AIPersonality.Aggressive)
                {
                    ChangeState(AIState.Flee);
                }

                if (!IsDistanceLessThan(target, attackDistance))
                {

                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                // Chase State
                // When finding player go to Attack State
                DoChaseState();
                if(IsDistanceLessThan(target, attackDistance) && IsCanSee(target))
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Flee:
                // Flee State
                // Run Away, choose either to Guard, Patrol, or Chase
                DoFleeState();

                if (!IsDistanceLessThan(target, fleeDistance) || !IsCanSee(target))
                {
                    ChangeState((AIState)AIState.Patrol);
                }

                if (IsDistanceLessThan(target, attackDistance / 3) && currentPersonality != AIPersonality.Aggressive)
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Patrol:
                DoPatrolState();

                // On a random waypoint just do something
                if (currentWaypoint == Random.Range(0, waypoints.Length))
                {
                    switch (currentPersonality)
                    {
                        case AIPersonality.Cowardly:
                            currentState = AIState.Flee;
                            break;
                        default:
                            currentState = AIState.Patrol;
                            break;
                    }
                }

                if (IsDistanceLessThan(target, chaseDistance) && IsCanSee(target))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.GetPowerUp:
                // Prioritize getting Power Up unless it sees player
                DoGetPowerUp();
                break;

        }
    }

    protected void DoGuardState()
    {
        Debug.Log("Guarding");
    }

    protected void DoChaseState()
    {
        Debug.Log("Chasing");
        Seek(target);
    }

    protected void DoFleeState()
    {
        pawn.RotateAway(-(target.transform.position));

        pawn.MoveForward();

    }

    protected void DoPatrolState()
    {
        // If we have a enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            // If close, next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].transform.position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        // Set the index to 0
        currentWaypoint = 0;
    }

    protected void DoAttackState()
    {
        pawn.RotateTowards(target.transform.position);

        pawn.Shoot();
    }

    protected void DoGetPowerUp()
    {

    }

    public void Seek(GameObject target)
    {
        // Do Seek
        pawn.RotateTowards(target.transform.position);
        // Rotate towards target
        pawn.MoveForward();
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        Debug.Log("Distance: " + Vector3.Distance(pawn.transform.position, target.transform.position));
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            Debug.Log("False");
            return true;
        }
        else
        {
            Debug.Log("True");
            return false;
        }
    }

    protected bool IsCanHear(GameObject target)
    {
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();

        if (noiseMaker == null)
        {
            return false;
        }

        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }

        float totalDistance = noiseMaker.volumeDistance + hearingDistance;

        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool IsCanSee(GameObject target)
    {
        Vector3 agentTotTargetVector = target.transform.position - pawn.transform.position;

        float angleToTarget = Vector3.Angle(agentTotTargetVector, pawn.transform.forward);

        RaycastHit hit;
        GameObject hitObject;

        if (Physics.Raycast(Vector3.zero, Vector3.forward, out hit, raycastViewDistance))
        {
            hitObject = hit.collider.gameObject;
        }

        if (angleToTarget < fieldOfView)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }


    public virtual void ChangeState(AIState newState)
    {
        // Change current state
        currentState = newState;
        // Save time since changed states
        lastStateChangeTime = Time.time;
        
    }

    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            //Debug.Log("GameManager exists");
            // And the array of players exists
            if (GameManager.instance.players != null)
            {
                //Debug.Log("players exist");
                //Debug.Log("How many players exist: " + GameManager.instance.players.Count);
                // And there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    //Then target the gameObject of the pawn of the first player controller in the list
                    //Debug.Log("player 1 exist");
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
                else
                {
                    Debug.Log("player 1 does not exist");
                }
            }
        }
    }

    protected void TargetNearestTank()
    {
        // Get a list of all the tanks (pawns)
        Pawn[] allTanks = FindObjectsByType<Pawn>(FindObjectsSortMode.None);
        Debug.Log("Chosen Tank List: " + allTanks);
        // Assume that the first tank is closest
        Pawn closestTank = allTanks[1];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        // Iterate through them one at a time
        foreach (Pawn tank in allTanks)
        {
            // If this one is closer than the closest
            Debug.Log("Chosen pawn: " + pawn);
            Debug.Log("Chosen tank: " + tank);
            if (tank.gameObject != pawn.gameObject && Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                // It is the closest
                Debug.Log("Chosen closest tank: " + closestTank);
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
                target = closestTank.gameObject;
            }
            else
            {
                TargetPlayerOne();
            }
        }

        // Target the closest tank
        
        Debug.Log("Chosen target: " + closestTank);
    }


    protected bool IsHasTarget()
    {
        // return true if we have a target, false if we don't
        return (target != null || target == pawn);
    }
}

