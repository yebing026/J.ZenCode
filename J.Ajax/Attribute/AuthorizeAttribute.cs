using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace J.Ajax
{
	/// <summary>
	/// 用于验证用户身份的修饰属性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class AuthorizeAttribute : Attribute
	{
		public virtual bool AuthenticateRequest(HttpContext context)
		{			
			return false;
		}
	}
}
