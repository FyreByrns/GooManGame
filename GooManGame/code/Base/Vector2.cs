namespace GooManGame {
	public class Vector2 {
		public float x, y;

		public float Length2 => x * x + y * y;
		public float Length => (float)System.Math.Sqrt(Length2);

		public Vector2 RawNormal =>
			new Vector2(y, -x);
		public Vector2 Normal =>
			RawNormal.Normalized();

		public Vector2 Normalized() {
			Vector2 result = new Vector2();

			if (Length > 0) {
				result.x = x / Length;
				result.y = y / Length;
			}

			return result;
		}

		public float DotProduct(Vector2 other) =>
			DotProduct(this, other);

		public Vector2 Project(Vector2 onto) =>
			Project(this, onto);

		public static float DotProduct(Vector2 va, Vector2 vb) =>
			va.x * vb.x + va.y * vb.y;

		public static Vector2 Project(Vector2 v, Vector2 onto) {
			float dp = DotProduct(v, onto);
			return onto * dp;
		}

		public static bool operator ==(Vector2 va, Vector2 vb) =>
			va is object && vb is object &&
			va.x == vb.x &&
			va.y == vb.y;

		public static bool operator !=(Vector2 va, Vector2 vb) =>
			!(va == vb);

		public static Vector2 operator +(Vector2 va, Vector2 vb) =>
			new Vector2(va?.x + vb?.x,
						va?.y + vb?.y);
		public static Vector2 operator -(Vector2 va, Vector2 vb) =>
			new Vector2(va?.x - vb?.x,
						va?.y - vb?.y);

		public static Vector2 operator *(Vector2 va, Vector2 vb) =>
			new Vector2(va.x * vb.x, va.y * vb.y);
		public static Vector2 operator /(Vector2 va, Vector2 vb) =>
			new Vector2(va.x / vb.x, va.y / vb.y);

		public static Vector2 operator *(Vector2 v, float f) =>
			new Vector2(v?.x * f, v?.y * f);
		public static Vector2 operator /(Vector2 v, float f) =>
			new Vector2(v.x / f, v.y / f);

		public static implicit operator PixelEngine.Point(Vector2 v) =>
			new PixelEngine.Point((int)v?.x, (int)v?.y);
		public static implicit operator Vector2(PixelEngine.Point p) =>
			new Vector2(p.X, p.Y);

		public static implicit operator (float x, float y)(Vector2 v) =>
			(v.x, v.y);
		public static implicit operator Vector2((float x, float y) t) =>
			new Vector2(t.x, t.y);

		public Vector2() { }
		public Vector2(Vector2 v) {
			x = v.x;
			y = v.y;
		}
		public Vector2(float x, float y) {
			this.x = x;
			this.y = y;
		}
		public Vector2(float? x, float? y) {
			this.x = x ?? 0;
			this.y = y ?? 0;
		}

		public override int GetHashCode() =>
			base.GetHashCode();

		public override bool Equals(object obj) =>
			obj is Vector2 v && v == this;
	}
}
