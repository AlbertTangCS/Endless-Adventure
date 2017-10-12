using System.Collections.Generic;
using System.Linq;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Battle {
	public class Battlefield {

		public List<Combatant> Protagonists { get; private set; }
		public List<Combatant> Antagonists { get; private set; }
		private List<Combatant> _antagonistQueue;

		public Battlefield() {
			Protagonists = new List<Combatant>();
			Antagonists = new List<Combatant>();
			_antagonistQueue = new List<Combatant>();
			AddProtagonist();
			AddAntagonistToQueue();
		}

		public void Update() {
			foreach (Combatant protagonist in Protagonists) {
				if (protagonist.Fallen) {
					protagonist.Heal();
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

			// if all protagonists have fallen, return
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

		private void AddProtagonist() {
			Combatant protagonist = new Combatant(CharacterFactory.CreateCharacter(StatType.Health, 5));
			Protagonists.Add(protagonist);
		}

		private void AddAntagonistToQueue() {
			Combatant antagonist = new Combatant(CharacterFactory.CreateCharacter(StatType.Health, 3));
			_antagonistQueue.Add(antagonist);
		}
	}
}