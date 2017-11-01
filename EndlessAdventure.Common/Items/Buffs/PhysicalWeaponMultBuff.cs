using System;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items.Effects {
	public class PhysicalWeaponMultBuff : ABuff {

		public PhysicalWeaponMultBuff(double value) : base("Physical Weapon Buff", StatType.PhysicalAttack, value) { }

		public override int Apply(int stat) {
			return (int)((1+Value)*stat);
		}

		public override int Unapply(int stat) {
			return (int)Math.Ceiling(stat/(1+Value));
		}
	}
}