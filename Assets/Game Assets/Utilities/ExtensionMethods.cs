using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace etc
{
	public static class ExtensionMethods
	{
		#region INT
		public static void Times ( this int max, Action action ) 
		{
			for ( int i = 0;i!=max;i++ )
				action ();
		}
		#endregion

		#region STRING
		public static string Format ( this string s ) 
		{
			return s
				.Replace ( "->", "\n" )
				.Replace ( "[", "<b><color=orange>" )
				.Replace ( "]", "</color></b>" );
		}
		#endregion

		#region MONO
		public static IEnumerator AsyncLerp ( this MonoBehaviour m, Type type, string value, float target, float duration, UnityEngine.Object parent = null )
		{
			// Reflection
			var param = type.GetProperty ( value );
			var original = ( float ) param.GetValue ( parent, null );

			var start = Time.time;
			var progress = 0f;

			while ( progress < 1f )
			{
				var newValue = Mathf.Lerp ( original, target, progress );
				param.SetValue ( parent, newValue, null );

				progress = ( Time.time - start ) / duration;
				yield return null;
			}
		}
		public static IEnumerator AsyncLerp ( this MonoBehaviour m, Type type, string value, Color target, float duration, UnityEngine.Object parent = null )
		{
			// Reflection
			var param = type.GetProperty ( value );
			var original = ( Color ) param.GetValue ( parent, null );

			var start = Time.time;
			var progress = 0f;

			while ( progress < 1f )
			{
				var newValue = Color.Lerp ( original, target, progress );
				param.SetValue ( parent, newValue, null );

				progress = ( Time.time - start ) / duration;
				yield return null;
			}
		}
		public static IEnumerator AsyncLerp ( this MonoBehaviour m, Type type, string value, Quaternion target, float duration, UnityEngine.Object parent = null ) 
		{
			// Reflection
			var param = type.GetProperty ( value );
			var original = (Quaternion) param.GetValue ( parent, null );

			var start = Time.time;
			var progress = 0f;

			while (progress < 1f)
			{
				var newValue = Quaternion.Lerp ( original, target, progress );
				param.SetValue ( parent, newValue, null );

				progress = (Time.time - start) / duration;
				yield return null;
			}
		}
		#endregion
	}
}
