using System;
using System.Web;

namespace HelloWorld.Web
{
	/// <summary>Represents a cookie that stores the last division visited by an user (ID).</summary>
	public class DivisionCodeCookie : WrappedCookie
	{
		/// <summary>The key for the division code.</summary>
		private const string DivisionCodeKey = "Division";

		/// <summary>Initializes a new division code cookie.</summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="code">The division code.</param>
		public DivisionCodeCookie(Guid userId, DivisionCode code)
			: base(userId)
		{
			this.Code = code;
		}

		/// <summary>Initializes a new division code cookie.</summary>
		private DivisionCodeCookie(HttpCookie cookie) : base(cookie) { }

		/// <summary>Gets and set the division code of the cookie.</summary>
		public DivisionCode Code
		{
			get { return DivisionCode.TryParse(UnderlingCookie.Values[DivisionCodeKey]); }
			set { UnderlingCookie.Values[DivisionCodeKey] = value.ToString(); }
		}

		/// <summary>Creates a copy of the division code cookie.</summary>
		public DivisionCodeCookie Copy() { return new DivisionCodeCookie(this.UserId, this.Code); }

		/// <summary>Casts an HTTP Cookie to a Division code cookie.</summary>
		/// <remarks>
		/// Making the cast implicit allows the use of wrapped cookie when a HTTP cookie is asked.
		/// </remarks>
		public static implicit operator DivisionCodeCookie(HttpCookie http) { return new DivisionCodeCookie(http); }
	}

	public class DivisionCode
	{
		public static DivisionCode TryParse(string value)
		{
			return new DivisionCode();
		}
	}
}