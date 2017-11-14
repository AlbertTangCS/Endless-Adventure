using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Buffs.Effects;

namespace EndlessAdventure.Common.Battle {
	public class Battlefield {

		public List<Combatant> Protagonists { get; private set; }
		public List<Combatant> Antagonists { get; private set; }
		private List<Combatant> _antagonistQueue;
		private Func<Combatant> _generateAntagonist;

		public Battlefield(List<Combatant> protagonists, Func<Combatant> generateAntagonist) {
			Protagonists = protagonists;
			Antagonists = new List<Combatant>();
			_antagonistQueue = new List<Combatant>();
			_generateAntagonist = generateAntagonist;

			AddAntagonistToQueue();
			Message = string.Empty;
		}

		private string _message;
		public string Message {
			get {
				string message = _message;
				_message = string.Empty;
				return message;
			}
			private set => _message = value;
		}

		public void Update() {
			Message = "";

			// all protagonists attack all antagonists
			foreach (Combatant protagonist in Protagonists) {
				if (protagonist.Fallen) {
					protagonist.AutoHeal();
					if (!protagonist.Fallen) {
						AddAntagonistToQueue();
					}
				}
				else {
					foreach (Combatant antagonist in Antagonists) {
						if (protagonist.TryAttackCombatant(antagonist, out int damage)) {
							Message += protagonist.Character.Name + " attacks " + antagonist.Character.Name + " for "+damage+" damage.\n";
						}
						else {
							Message += protagonist.Character.Name + " attacks " + antagonist.Character.Name + " and misses.\n";
						}
						if (antagonist.TryAttackCombatant(protagonist, out damage)) {
							Message += antagonist.Character.Name + " attacks " + protagonist.Character.Name + " for "+damage+" damage.\n";
						}
						else {
							Message += antagonist.Character.Name + " attacks " + protagonist.Character.Name + " and misses.\n";
						}
					}
				}
			}

			// apply active Effects on all combatants
			foreach (Combatant p in Protagonists) {
				foreach (AEffect e in p.Character.ActiveEffects) {
					e.ApplyEffect(p.Character);
					Message += e.Name + " applied on "+p.Character.Name+" for " + e.Value + " value!\n";
				}
			}
			foreach (Combatant a in Antagonists) {
				foreach (AEffect e in a.Character.ActiveEffects) {
					e.ApplyEffect(a.Character);
					Message += e.Name + " applied on " + a.Character.Name+" for " + e.Value + " value!\n";
				}
			}

			// apply pending damage to all protagonists
			foreach (Combatant p in Protagonists) {
					p.ApplyPendingDamage();
				if (p.Fallen) {
					Message += p.Character.Name + " has fallen!\n";
					Antagonists.Clear();
				}
				else {
				}
			}

			// apply pending damage to all antagonists
			for (int i = 0; i < Antagonists.Count; i++) {
				Antagonists[i].ApplyPendingDamage();
				if (Antagonists[i].Fallen) {
					Message += Antagonists[i].Character.Name + " has been defeated!\n";
					Protagonists.ForEach(p => p.DefeatCombatant(Antagonists[i]));
					Antagonists.RemoveAt(i);
					i--;
					AddAntagonistToQueue();
				}
			}

			// add antagonists in queue to active list
			foreach (Combatant antagonist in _antagonistQueue) {
				Antagonists.Add(antagonist);
			}
			_antagonistQueue.Clear();
		}

		private void AddAntagonistToQueue() {
			Combatant antagonist = _generateAntagonist();
			if (antagonist != null) {
				_antagonistQueue.Add(antagonist);
			}
		}

		public void Flee() {
			string enemyName = null;
			while (Antagonists.Count > 0) {
				enemyName = Antagonists[0].Character.Name;
				Antagonists.RemoveAt(0);
				_antagonistQueue.Add(_generateAntagonist());
			}
			if (enemyName != null) {
				Message += "Fled from " + enemyName + "!\n";
			}
		}

		public void SwitchWorlds(Func<Combatant> generateAntagonist) {
			_generateAntagonist = generateAntagonist;
			Flee();
			_antagonistQueue.Clear();
			AddAntagonistToQueue();
		}
	}
}