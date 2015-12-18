using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace J.Ajax
{
	internal static class SerializerHelper
	{
		/// <summary>
		/// 将对象执行JSON序列化
		/// </summary>
		/// <param name="obj">要序列化的对象</param>
		/// <returns>JSON序列化的结果</returns>
		internal static string ToJson(this object obj)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			return jss.Serialize(obj);
		}


		/// <summary>
		/// 从JSON字符串中反序列化对象
		/// </summary>
		/// <typeparam name="T">反序列化的结果类型</typeparam>
		/// <param name="json">JSON字符串</param>
		/// <returns>反序列化的结果</returns>
		internal static T DeserializeFromJson<T>(this string json)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			return jss.Deserialize<T>(json);
		}		

	}
}
