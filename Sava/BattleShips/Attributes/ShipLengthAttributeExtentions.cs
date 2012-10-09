namespace BattleShips.Attributes
{
	using System;
	using System.Reflection;

	using BattleShips.Enums;

	// Extention class to get ship length attribute out of the ShipTypeEnum value
	public static class ShipLengthAttributeExtentions
	{
		public static int GetShipLength(this ShipTypeEnum en)
		{
			Type type = en.GetType();
			MemberInfo[] memInfo = type.GetMember(en.ToString());

			if (memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(ShipLengthAttribute), false);

				if (attrs.Length > 0)
				{
					return ((ShipLengthAttribute)attrs[0]).Length;
				}
			}

			return 0;
		}
	}
}