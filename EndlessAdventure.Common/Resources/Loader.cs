using System.Collections.Generic;
using System.Linq;
using EndlessAdventure.Common.Battle;

namespace EndlessAdventure.Common.Resources {

	public class Loader {
		public static List<Combatant> GetProtagonists() {
			List<Combatant> protagonists = new List<Combatant>();
			Combatant protagonist = CombatantFactory.CreateCombatant(name: "Player", body: 2);
			protagonist.Equip(EquipmentFactory.CreateWeapon(1));
			protagonists.Add(protagonist);
			return protagonists;
		}

		public static List<Combatant> GetAntagonists() {
			List<Combatant> antagonists = new List<Combatant>();

			return antagonists;
		}

		public static List<CombatantData> GetEnemyData(string world) {
			return new List<CombatantData>(EnemyDatabase.Enemies.Where(data => data.World == world));
		}
	}

}