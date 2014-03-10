using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Creature : MonoBehaviour {

	public string[] initialState;
	public string[] initialGoals;
	private Action[] actions;
	private SortedList<float, string> goals;
	private List<string> state;
	private NavMeshAgent agent;
	private ActionPath path;
	private Action currentAction;

	// Use this for initialization
	void Start () {
		this.agent = GetComponent<NavMeshAgent>();
		this.agent.SetDestination(new Vector3(30, .5f, 30));
		this.actions = GetComponents<Action>();

		this.state = new List<string>(this.initialState);
		parseGoals();
		print(this.goals.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		this.updateGoalAndActionPath();
	}

	private void updateGoalAndActionPath() {
		if(this.path == null) {
			foreach(KeyValuePair<float, string> de in this.goals) {
				if(this.state.Contains(de.Value))
					continue;
				else {
					this.path = new ActionPath(de.Value, this.state, this.actions);
					if(this.path != null)
						break;
				}
			}
			
			print(this.path.ToString());

			this.currentAction = this.path.Path.First;
			this.currentAction.Activate();
		} else {
			this.currentAction.Execute();
			if(this.currentAction.CheckDone()) {
				if(this.path.Path.Last == this.currentAction)
					this.path = null;
				else {
					this.currentAction = this.path.Path[this.path.Path.FindIndex(this.currentAction) + 1];
					this.currentAction.Activate();
				}
			}

		}

		/*List<string> goalList = goals.Keys.ToList();
		goalList.Sort();
		ActionPath path = new ActionPath(this.goals, this.state, this.actions);
		print(path.ToString());*/
	}

	private void parseGoals() {
		this.goals = new SortedList<float, string>();
		foreach(string s in this.initialGoals) {
			string[] parts = s.Split('=');
			this.goals.Add(float.Parse(parts[1]), parts[0]);
			//this.goals[parts[0]] = float.Parse(parts[1]);
		}

		print( "\t-KEY-\t-VALUE-" );
		for ( int i = 0; i < this.goals.Count; i++ )  {
			print("\t" + this.goals.Keys.ElementAt(i) + ":\t" + this.goals.ElementAt(i));
		}
	}
}
