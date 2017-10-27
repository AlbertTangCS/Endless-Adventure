using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items.Effects {
	public class PhysicalWeaponBuff : ABuff {

		public PhysicalWeaponBuff(string name, double value) : base(name, StatType.PhysicalAttack, value) { }

		public override int Apply(int stat) {
			return stat + (int)Value;
		}

		public override int Unapply(int stat) {
			return stat - (int)Value;
		}
	}
}