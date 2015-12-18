using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace J.Ajax
{

	internal sealed class InvokeInfo
	{
		public ControllerDescription Controller;
		public ActionDescription Action;
		public object Instance;

		
		public SessionMode GetSessionMode()
		{
			if( this.Action != null && this.Action.SessionMode != null )
				return this.Action.SessionMode.SessionMode;
			if( this.Controller != null && this.Controller.SessionMode != null )
				return this.Controller.SessionMode.SessionMode;			
			return SessionMode.NotSupport;
		}
		public AuthorizeAttribute GetAuthorize()
		{
			if( this.Action != null && this.Action.Authorize != null )
				return this.Action.Authorize;
			if( this.Controller != null && this.Controller.Authorize != null )
				return this.Controller.Authorize;
			return null;
		}
	}


	
}
