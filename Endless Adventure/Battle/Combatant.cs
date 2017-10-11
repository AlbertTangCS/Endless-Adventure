using EndlessAdventure.Characters;
using EndlessAdventure.Equipments;

namespace EndlessAdventure.Battle {
	public class Combatant {

		public Character Character { get; private set; }
		public int Level { get; private set; }
		public int Experience { get; private set; }
		private Equipment _equipment;
		private int _pendingDamage;

		public Combatant() {
			Character = new Character();
			_equipment = new Equipment();
		}

		public void AttackCombatant(Combatant antagonist) {
			Stat attackStat;
			Character.Stats.TryGetValue(StatType.Attack, out attackStat);
			antagonist._pendingDamage = attackStat.Current;
		}

		public void ApplyPendingDamage() {
			Character.ApplyDamage(_pendingDamage);
			_pendingDamage = 0;
		}
	}
}