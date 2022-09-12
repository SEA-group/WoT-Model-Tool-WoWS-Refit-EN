using System;

namespace WOWSModelMod
{
	internal struct VT1
	{
		public float trivial;

		public VT1(BinaryReader r)
		{
			trivial = r.ReadSingle();
		}

	}
}
