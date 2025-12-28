using System;
using System.Reflection;

namespace QuoterPlan
{
	public static class ReflectionHelper
	{
		private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
		{
			PropertyInfo property;
			do
			{
				property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				type = type.BaseType;
			}
			while (property == null && type != null);
			return property;
		}

		public static object GetPropertyValue(this object obj, string propertyName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Type type = obj.GetType();
			PropertyInfo propertyInfo = ReflectionHelper.GetPropertyInfo(type, propertyName);
			if (propertyInfo == null)
			{
				throw new ArgumentOutOfRangeException("propertyName", string.Format("Couldn't find property {0} in type {1}", propertyName, type.FullName));
			}
			return propertyInfo.GetValue(obj, null);
		}

		public static void SetPropertyValue(this object obj, string propertyName, object val)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Type type = obj.GetType();
			PropertyInfo propertyInfo = ReflectionHelper.GetPropertyInfo(type, propertyName);
			if (propertyInfo == null)
			{
				throw new ArgumentOutOfRangeException("propertyName", string.Format("Couldn't find property {0} in type {1}", propertyName, type.FullName));
			}
			propertyInfo.SetValue(obj, val, null);
		}
	}
}
