using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items.Effects {
	public class PhysicalWeaponIncBuff : ABuff {

		public PhysicalWeaponIncBuff(double value) : base("Physical Weapon Buff", StatType.PhysicalAttack, value) { }

		public override int Apply(int stat) {
			return stat + (int)Value;
		}

		public override int Unapply(int stat) {
			return stat - (int)Value;
		}
	}
}