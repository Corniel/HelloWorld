using System.Collections.Generic;

namespace System.Web
{
	/// <summary>Mocks the .NET 4.5 version.</summary>
	public sealed class HttpCookie
	{
		public HttpCookie(string name)
		{
			Name = name;
			Values = new Dictionary<string, string>();
		}

		public string Domain { get; set; }
		public string Path { get; set; }
		public DateTime Expires { get; set; }
		public string Name { get; set; }
		public bool HttpOnly { get; set; }
		public bool Secure { get; set; }
		public bool Shareable { get; set; }

		public string Value { get; set; }

		public Dictionary<string, string> Values { get; }
	}
}