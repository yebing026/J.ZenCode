using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace J.Ajax.Serializer
{
	internal interface IActionParametersProvider
	{
		object[] GetParameters(HttpRequest request, ActionDescription action);

	}
}
