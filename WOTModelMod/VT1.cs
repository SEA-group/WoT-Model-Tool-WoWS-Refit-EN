using System;

namespace WOTModelMod
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
