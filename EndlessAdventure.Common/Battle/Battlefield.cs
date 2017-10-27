using System;
using System.Collections.Generic;
using System.Linq;
using EndlessAdventure.Common.Resources;

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
		}

		public void Update() {

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
						protagonist.AttackCombatant(antagonist);
						antagonist.AttackCombatant(protagonist);
					}
				}
			}

			// if all protagonists have fallen, tick has finished
			if (Protagonists.FirstOrDefault<Combatant>(x => x.Fallen == false) == null) {
				return;
			}

			// apply pending damage to all protagonists
			for (int i = 0; i < Protagonists.Count; i++) {
				Protagonists[i].ApplyPendingDamage();
				if (Protagonists[i].Fallen) {
					Antagonists.Clear();
				}
			}

			// apply pending damage to all antagonists
			for (int i = 0; i < Antagonists.Count; i++) {
				Antagonists[i].ApplyPendingDamage();
				if (Antagonists[i].Fallen) {
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
			_antagonistQueue.Add(antagonist);
		}
	}
}