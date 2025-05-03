using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState{

    protected EnemyStateMachine stateMachine;
    protected Detection detection;
    protected GameObject enemy;

    //Construtor do estado
    public EnemyState(EnemyStateMachine stateMachine, GameObject enemy)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.detection = enemy.GetComponent<Detection>();

    }

    //Métodos padrões
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
