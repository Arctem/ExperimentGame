using UnityEngine;
using System.Collections;

public abstract class Action : MonoBehaviour {

	public abstract string[] preconditions();
	public abstract string[] postconditions();

	public override string ToString ()
	{
		return this.GetType().Name;
	}

	public abstract void Activate();
	public abstract void Execute();
	public abstract void CheckDone();
	public abstract void EndEarly();
}
