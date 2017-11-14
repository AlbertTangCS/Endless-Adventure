using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Buffs.Effects {
	public class HealEffect : AEffect {

		public HealEffect(string pName, string pDescription, double pValue, int pDuration) :
				base(pName, pDescription, pValue, pDuration) { }

		public override void ApplyEffect(Character character) {
			character.Heal((int)Value);
		}
	}
}