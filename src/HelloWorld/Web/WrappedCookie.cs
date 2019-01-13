using System;
using System.Diagnostics;
using System.Web;

namespace HelloWorld.Web
{
	/// <summary>Represents a cookie.</summary>
	/// <remarks>
	/// It is a wrapper that allows to add custom logic to the cookie.
	/// </remarks>
	[DebuggerDisplay("{DebuggerDisplay}")]
	public abstract class WrappedCookie
	{
		/// <summary>Initials a new wrapped cookie based on an HTTP cookie.</summary>
		protected WrappedCookie(HttpCookie httpCookie)
		{
			UnderlingCookie = httpCookie ?? throw new ArgumentNullException("httpCookie");
		}

		/// <summary>Initials a new wrapped cookie based on an user ID.</summary>
		protected WrappedCookie(Guid userId) : this(GetCookieName(userId)) { }

		/// <summary>Initials a new wrapped cookie based on cookie name.</summary>
		protected WrappedCookie(string name): this(new HttpCookie(name)){}

		/// <summary>Gets or set the underlying HTTP cookie.</summary>
		protected HttpCookie UnderlingCookie { get; set; }

		/// <summary>Gets or set the user ID of the cookie.</summary>
		public Guid UserId
		{
			get
			{

				if (Name.StartsWith("ExactServer{") && Guid.TryParseExact(this.Name.Substring(11), "B", out Guid userid))
				{
					return userid;
				}
				return Guid.Empty;

			}
			set { this.Name = GetCookieName(value); }
		}

		/// <summary>Gets or set the name of the cookie.</summary>
		public string Name 
		{ 
			get { return UnderlingCookie.Name;  }
			set{ UnderlingCookie.Name = value;}
		}
		/// <summary>Gets or set the domain of the cookie.</summary>
		public string Domain
		{
			get { return UnderlingCookie.Domain; }
			set { UnderlingCookie.Domain = value; }
		}
		/// <summary>Gets or set the path of the cookie.</summary>
		public string Path
		{
			get { return UnderlingCookie.Path; }
			set { UnderlingCookie.Path = value; }
		}
		/// <summary>Gets or set the expiration date of the cookie.</summary>
		public DateTime Expires
		{
			get { return UnderlingCookie.Expires; }
			set { UnderlingCookie.Expires = value; }
		}
		
		/// <summary>Gets or set a value that specifies whatever a cookie is accessible by client-side script.</summary>
		public bool HttpOnly
		{
			get { return UnderlingCookie.HttpOnly; }
			set { UnderlingCookie.HttpOnly = value; }
		}
		/// <summary>Gets or set a value indicating specifies whatever to transmit the cookie Secure Sockets Layers (SSL)--that is, over HTTPS only.</summary>
		public bool Secure
		{
			get { return UnderlingCookie.Secure; }
			set { UnderlingCookie.Secure = value; }
		}
		/// <summary>Determines whatever the cookie is allowed to participate in output caching.</summary>
		public bool Shareable
		{
			get { return UnderlingCookie.Shareable; }
			set { UnderlingCookie.Shareable = value; }
		}

		/// <summary>Casts a wrapped cookie (back) to an HTTP cookie.</summary>
		/// <remarks>
		/// Making the cast implicit allows the use of wrapped cookie when an HTTP cookie is asked.
		/// </remarks>
		public static implicit operator HttpCookie(WrappedCookie wrapped) { return wrapped.UnderlingCookie; }

		/// <summary>Cleans the cookie up by clearing the value and set the expire date in the past.</summary>
		public void Cleanup()
		{
			UnderlingCookie.Expires = DateTime.Now.AddMinutes(-1);
			UnderlingCookie.Values.Clear();
		}

		/// <summary>Gets the name for the cookie based on the user ID.</summary>
		public static string GetCookieName(Guid userId)
		{
			return string.Format("ExactServer{0:B}", userId);
		}

		/// <summary>Gets a debugger display for the wrapped cookie.</summary>
		protected virtual string DebuggerDisplay { get { return string.Format("Cookie[{0}], Value: {1}, Expires: {2:yyyy-MM-dd HH:mm}", this.Name, this.UnderlingCookie.Value, this.Expires); } }
	}
}