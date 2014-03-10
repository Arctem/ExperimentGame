using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionPath {

	private List<Action> path;
	public List<Action> Path {
		get { return path; }
	}

	private string goal;
	List<string> curState;

	public ActionPath(string goal, List<string> curState, Action[] actions) {
		this.goal = goal;
		this.curState = curState;
		//this.path = new List<Action>();

		this.path = MakePath(actions);
	}

	private List<Action> MakePath(Action[] actions) {
		List<List<Action>> path = new List<List<Action>>();
		path.Add(new List<Action>());

		while(path.Count != 0) {
			List<List<Action>> next = new List<List<Action>>();
			foreach(List<Action> p in path) {
				foreach(Action a in actions) {
					if(p.Contains(a))
						continue;
					List<Action> newElement = new List<Action>(p);
					newElement.Add(a);

					List<string> postState = getPathPostState(newElement);

					if(postState == null)
						//Not a valid path.
						continue;
					else if(postState.Contains(this.goal))
						//Leads to goal, let's use it.
						return newElement;
					else
						//Keep searching.
						next.Add(newElement);
				}
			}
			path = next;
		}

		return null;
	}

	private List<string> getPathPostState(List<Action> newElement) {
		List<string> state = new List<string>(curState);

		foreach(Action a in newElement) {
			foreach(string s in a.preconditions()) {
				if(!state.Contains(s))
					return null;
			}

			foreach(string s in a.postconditions()) {
				if(s.StartsWith("-"))
					state.Remove(s.Substring(1));
				else if(!state.Contains(s))
					state.Add(s);
			}
		}

		return state;
	}

	public override string ToString ()
	{
		string ret = curState.ToString() + " to " + goal + " via ";
		for(int i = 0; i < this.path.Count; i++) {
			if(i != 0)
				ret += ", ";
			ret += this.path[i].ToString();
		}

		return ret;
	}

	/*private Dictionary<Action, int> PrepareData(Action[] actions) {
		Dictionary<Action, int> heatmap = new Dictionary<Action, int>();
		List<Action> actionsLeft = new List<Action>(actions);

		//First, find the actions leading directly to the goal.
		foreach(Action a in actionsLeft) {
			if(System.Array.IndexOf(a.postconditions(), goal) != -1) {
				heatmap[a] = 1;
				actionsLeft.Remove(a);
			}
		}

		bool done = false;

		//Add each further layer of actions.
		for(int i = 2; true; i++) {
			foreach(Action a in actionsLeft) {
				foreach(Action b in heatmap) {
					if(heatmap[b] != i - 1)
						continue;
				}
			}
		}

		return heatmap;
	}*/
}
