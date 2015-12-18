using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace J.Ajax.Serializer
{
	internal static class ActionParametersProviderFactory
	{
		public static IActionParametersProvider CreateActionParametersProvider(HttpRequest request)
		{
			if( request == null )
				throw new ArgumentNullException("request");


			string contentType = request.ContentType;

			if( contentType.IndexOf("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) >= 0 )
				return new FormDataProvider();

			if( contentType.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) >= 0 )
				return new JsonDataProvider();



			// 默认还是表单的 key = vlaue格式。
			return new FormDataProvider();
		}
	}
}
