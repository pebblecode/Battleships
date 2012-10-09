namespace BattleShips.Attributes
{
	using System;
	using System.Reflection;

	using BattleShips.Enums;

	// Extention class to get ship description attribute out of the ShipTypeEnum value
	public static class ShipDescriptionAttributeExtentions
	{
		public static string GetShipDescription(this ShipTypeEnum en)
		{
			Type type = en.GetType();
			MemberInfo[] memInfo = type.GetMember(en.ToString());

			if (memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(ShipDescriptionAttribute), false);

				if (attrs.Length > 0)
				{
					return ((ShipDescriptionAttribute)attrs[0]).Description;
				}
			}

			return string.Empty;
		}
	}
}