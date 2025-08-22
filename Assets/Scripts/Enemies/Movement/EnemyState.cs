using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Detection detection;
    protected GameObject enemy;
    protected EnemyConfig enemyConfig;
    protected Rigidbody2D rb; 

    protected EnemyCollisions collisions;

    public EnemyState(EnemyStateMachine stateMachine, GameObject enemy, EnemyConfig enemyConfig)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.detection = enemy.GetComponent<Detection>();
        this.enemyConfig = enemyConfig;
        this.rb = enemy.GetComponent<Rigidbody2D>();
        this.collisions = enemy.GetComponent<EnemyCollisions>();
    }

    //Métodos padrões
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate(){}

}