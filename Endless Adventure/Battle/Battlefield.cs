using System.Collections.Generic;

namespace EndlessAdventure.Battle {
	public class Battlefield {

		public List<Combatant> Protagonists { get; private set; }
		public List<Combatant> Antagonists { get; private set; }

		public Battlefield() {
			Protagonists = new List<Combatant>();
			Protagonists.Add(new Combatant());
			Antagonists = new List<Combatant>();
			Antagonists.Add(new Combatant());
		}

		public void Update() {
			foreach (Combatant protagonist in Protagonists) {
				foreach (Combatant antagonist in Antagonists) {
					protagonist.AttackCombatant(antagonist);
					antagonist.AttackCombatant(protagonist);
				}
			}

			foreach (Combatant protagonist in Protagonists) {
				protagonist.ApplyPendingDamage();
			}

			foreach (Combatant antagonist in Antagonists) {
				antagonist.ApplyPendingDamage();
			}
		}
	}
}