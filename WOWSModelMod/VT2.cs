namespace WOWSModelMod
{
	internal struct VT2
	{
		public float x;

		public float y;

		public VT2(BinaryReader r)
		{
			x = r.ReadSingle();
			y = r.ReadSingle();
		}

		public void Write(BinaryWriter w)
		{
			w.Write(x);
			w.Write(y);
		}

		public VT2(float a, float b)
		{
			x = a;
			y = b;
		}

		public static VT2 operator +(VT2 a, VT2 b)
		{
			return new VT2(a.x + b.x, a.y + b.y);
		}

		public static VT2 operator -(VT2 a, VT2 b)
		{
			return new VT2(a.x - b.x, a.y - b.y);
		}
	}
}
