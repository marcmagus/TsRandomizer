﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Timespinner.GameAbstractions.Gameplay;
using Timespinner.GameObjects.BaseClasses;
using Timespinner.GameObjects.Events;
using TsRanodmizer.Extensions;
using TsRanodmizer.IntermediateObjects;

namespace TsRanodmizer.ReplacementObjects
{
	[TimeSpinnerType("Timespinner.GameObjects.Events.Relics.PyramidKeys")]
	// ReSharper disable once UnusedMember.Global
	class PyramidKeys : Replaces
	{
		protected override IEnumerable<Animate> Replace(Level level, Animate obj)
		{
			var reflected = obj.AsDynamic();

			return new[] {
				new TreasureChestEvent(level, new Point(296, 176), -1, reflected._objectSpec) //position based on room in the future
			};
		}
	}
}