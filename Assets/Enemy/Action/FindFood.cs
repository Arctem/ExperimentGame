using UnityEngine;
using System.Collections;

public class FindFood : Action {

	// Use this for initialization
	void Start () {
	
	}

	public override string[] preconditions() {
		return new string[] {"hungry"};
	}
	public override string[] postconditions() {
		return new string[] {"-hungry", "full"};
	}
	
	public override void Activate() {

	}
	public override void Execute() {

	}
	public override void CheckDone() {

	}
	public override void EndEarly() {

	}
}
