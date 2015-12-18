using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace J.Ajax
{
	internal class BaseDescription
	{
		
		public SessionModeAttribute SessionMode { get; protected set; }
		public AuthorizeAttribute Authorize { get; protected set; }

		protected BaseDescription(MemberInfo m)
		{
			
			this.SessionMode = m.GetMyAttribute<SessionModeAttribute>();
			this.Authorize = m.GetMyAttribute<AuthorizeAttribute>(true /* inherit */);
		}
	}



}
