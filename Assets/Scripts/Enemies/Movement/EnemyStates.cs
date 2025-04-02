using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStates : MonoBehaviour {
    protected StatesController enemy;

    protected virtual void Awake() {
        enemy = GetComponent<StatesController>();
    }

    public virtual void Enter() { this.enabled = true; }
    public virtual void Exit() { this.enabled = false; }
}
