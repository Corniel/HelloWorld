using System;

namespace HelloWorld
{
	public class WTF002 : WTF002Base
	{
		public override void Redirect(PipelineContext pipelineContext)
		{
			string linkcp = "";
			if (linkcp == null || linkcp == "" || linkcp.Length == 0)
				linkcp = "default.aspx";

			pipelineContext.Response.Redirect(linkcp);
		}
	}

	public class WTF002_ : WTF002Base
	{
		public override void Redirect(PipelineContext pipelineContext)
		{
			string linkcp = string.Empty;
			// TODO: implement logic.
			if (string.IsNullOrEmpty(linkcp))
			{
				linkcp = "~/default.aspx";
			}
			pipelineContext.Response.Redirect(linkcp);
		}
	}

	public class WTF002_1 : WTF002Base
	{
		public override void Redirect(PipelineContext pipelineContext)
		{
			throw new NotImplementedException("TODO: implement logic.");
			//string linkcp = string.Empty;
			//if (string.IsNullOrEmpty(linkcp))
			//{
			//	linkcp = "~/default.aspx";
			//}
			//pipelineContext.Response.Redirect(linkcp);
		}
	}

	public abstract class WTF002Base
	{
		public abstract void Redirect(PipelineContext pipelineContext);
	}
	public class PipelineContext
	{
		public Response Response { get; set; }
	}
	public class Response
	{
		public void Redirect(string url) { }
	}
}
