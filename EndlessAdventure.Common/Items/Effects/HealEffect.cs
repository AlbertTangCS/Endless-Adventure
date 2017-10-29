using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items.Consumables.Effects
{
	public class HealEffect : AEffect
	{
		public HealEffect(double value) : base("Heal Effect", StatType.Health, value) { }

		public override void ApplyEffect(Character character) {
			character.Heal((int)Value);
		}
	}
}