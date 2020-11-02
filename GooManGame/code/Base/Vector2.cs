namespace GooManGame {
	/// <summary>
	/// A 2D vector of structures. Only works properly with numeric types.
	/// </summary>
	public class Vector2<T> where T : struct {
		/// <summary>
		/// Values.
		/// </summary>
		public T x, y;

		#region properties
		/// <summary>
		/// Squared length.
		/// </summary>
		public T Length2 => (dynamic)x * x + (dynamic)y * y;
		/// <summary>
		/// Length.
		/// </summary>
		public T Length => (dynamic)System.Math.Sqrt((dynamic)Length2);

		/// <summary>
		/// Normal before normalization.
		/// </summary>
		public Vector2<T> RawNormal =>
			new Vector2<T>((dynamic)y, -(dynamic)x);
		/// <summary>
		/// Normalized normal.
		/// </summary>
		public Vector2<T> Normal =>
			RawNormal.Normalized();
		#endregion properties
		#region methods (instance)
		/// <summary>
		/// This in normal space.
		/// </summary>
		public Vector2<T> Normalized() {
			Vector2<T> result = new Vector2<T>();

			if ((dynamic)Length > 0) {
				result.x = (dynamic)x / Length;
				result.y = (dynamic)y / Length;
			}

			return result;
		}

		/// <summary>
		/// The dot product of this and another.
		/// </summary>
		public float DotProduct(Vector2<T> other) =>
				DotProduct(this, other);

		/// <summary>
		/// Project this onto another vector.
		/// </summary>
		public Vector2<T> Project(Vector2<T> onto) =>
				Project(this, onto);
		#endregion methods (instance)
		#region methods (static)
		/// <summary>
		/// Dot product of two vectors.
		/// </summary>
		public static float DotProduct(Vector2<T> va, Vector2<T> vb) =>
				(dynamic)va.x * vb.x + (dynamic)va.y * vb.y;

		/// <summary>
		/// Project a v onto onto
		/// </summary>
		public static Vector2<T> Project(Vector2<T> v, Vector2<T> onto) {
			float dp = DotProduct(v, onto);
			return (dynamic)onto * dp;
		}
		#endregion methods (static)
		#region operators
		public static bool operator ==(Vector2<T> va, Vector2<T> vb) =>
				va is object && vb is object &&
				(dynamic)va.x == vb.x &&
				(dynamic)va.y == vb.y;

		public static bool operator !=(Vector2<T> va, Vector2<T> vb) =>
				!(va == vb);

		public static Vector2<T> operator +(Vector2<T> va, Vector2<T> vb) =>
				new Vector2<T>((dynamic)va.x + vb.x,
							   (dynamic)va.y + vb.y);
		public static Vector2<T> operator -(Vector2<T> va, Vector2<T> vb) =>
				new Vector2<T>((dynamic)va.x - vb.x,
							   (dynamic)va.y - vb.y);

		public static Vector2<T> operator *(Vector2<T> va, Vector2<T> vb) =>
			new Vector2<T>((dynamic)va.x * vb.x, (dynamic)va.y * vb.y);
		public static Vector2<T> operator /(Vector2<T> va, Vector2<T> vb) =>
			new Vector2<T>((dynamic)va.x / vb.x, (dynamic)va.y / vb.y);

		public static Vector2<T> operator *(Vector2<T> v, float f) =>
			new Vector2<T>((dynamic)v.x * f, (dynamic)v.y * f);
		public static Vector2<T> operator /(Vector2<T> v, float f) =>
			new Vector2<T>((dynamic)v.x / f, (dynamic)v.y / f);

		public Vector2<TCast> Cast<TCast>() where TCast : struct =>
			new Vector2<TCast>((TCast)(dynamic)x, (TCast)(dynamic)y);
		#endregion operators
		#region constructors
		public Vector2() { }
		public Vector2(Vector2<T> v) {
			x = v.x;
			y = v.y;
		}
		public Vector2(T x, T y) {
			this.x = x;
			this.y = y;
		}
		public Vector2(T? x, T? y) {
			this.x = x ?? default;
			this.y = y ?? default;
		}
		#endregion constructors
		#region standard
		public override int GetHashCode() =>
				base.GetHashCode();

		public override bool Equals(object obj) =>
				obj is Vector2<T> v && v == this;
		#endregion standard
	}
}