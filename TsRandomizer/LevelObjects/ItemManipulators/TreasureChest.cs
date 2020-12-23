﻿using System;
using Timespinner.GameAbstractions.GameObjects;
using Timespinner.GameObjects.BaseClasses;
using Timespinner.GameObjects.Events;
using TsRandomizer.Extensions;
using TsRandomizer.IntermediateObjects;
using TsRandomizer.Randomisation;
using TsRandomizer.Screens;

namespace TsRandomizer.LevelObjects.ItemManipulators
{
	class TreasureChest : ItemManipulator<TreasureChestEvent>
	{
		static readonly Type ETreasureStatBoostType = TimeSpinnerType.Get("Timespinner.GameObjects.Events.TreasureChestEvent+ETreasureStatBoostType");

		bool hasDroppedLoot;

		public TreasureChest(TreasureChestEvent treasureChest, ItemLocation itemLocation) : base(treasureChest, itemLocation)
		{
		}

		protected override void Initialize(SeedOptions options)
		{
			if (ItemInfo == null)
				return;

			Object._treasureLootType = ItemInfo.TreasureLootType;

			switch (ItemInfo.Identifier.LootType)
			{
				case LootType.ConstUseItem:
					Object._lootUseItemType = ItemInfo.Identifier.UseItem;
					break;
				case LootType.ConstRelic:
					Object._lootRelicType = ItemInfo.Identifier.Relic;
					break;
				case LootType.ConstEquipment:
					Object._lootEquipmentType = ItemInfo.Identifier.Enquipment;
					break;
				case LootType.ConstOrb:
					Object._lootOrbType = ItemInfo.Identifier.OrbType;
					Object._lootOrbSlot = ItemInfo.Identifier.OrbSlot;
					break;
				case LootType.ConstFamiliar:
					Object._lootFamiliarType = ItemInfo.Identifier.Familiar;
					break;
				case LootType.ConstStat:
					switch (ItemInfo.Identifier.Stat)
					{
						case EItemType.MaxHP:
							Object._lootStatBoostType = ETreasureStatBoostType.GetEnumValue("HP");
							break;
						case EItemType.MaxAura:
							Object._lootStatBoostType = ETreasureStatBoostType.GetEnumValue("Aura");
							break;
						case EItemType.MaxSand:
							Object._lootStatBoostType = ETreasureStatBoostType.GetEnumValue("Sand");
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(ItemInfo.Identifier.LootType), ItemInfo.Identifier.LootType, $"lootType cannot be droppd by {nameof(TreasureChest)}");
			}

			var pickedUp = IsPickedUp;
			Object._isOpened = pickedUp;
			Object._hasDroppedLoot = pickedUp;
			hasDroppedLoot = pickedUp;
			if (pickedUp)
				((Appendage)Object._lidAppendage).AsDynamic().ChangeAnimation(Object._animationIndexOffset + 5, 1, 1f, EAnimationType.None);
		}

		protected override void OnUpdate(GameplayScreen gameplayScreen)
		{
			if (ItemInfo == null || hasDroppedLoot || !Object._hasDroppedLoot)
				return;
		
			// ReSharper disable once PossibleNullReferenceException
			if (ItemInfo.Identifier.LootType == LootType.Orb)
				Level.GameSave.AddItem(Level, ItemInfo.Identifier);

			OnItemPickup();

			hasDroppedLoot = true;
		}
	}
}
