using System.IO;

namespace WOTModelMod
{
	internal struct FIDX
	{
		public int x;

		public int y;

		public int z;

		public FIDX(BinaryReader r)
		{
			x = r.ReadUInt16();
			y = r.ReadUInt16();
			z = r.ReadUInt16();
		}

		public void Write(BinaryWriter w)
		{
			w.Write((ushort)x);
			w.Write((ushort)y);
			w.Write((ushort)z);
		}

		public void Write(BinaryWriter w, int Lvnum)
		{
			w.Write((ushort)(x + Lvnum));
			w.Write((ushort)(y + Lvnum));
			w.Write((ushort)(z + Lvnum));
		}

		public void WriteLFidx(BinaryWriter w)
		{
			w.Write(x + 1);
			w.Write(y + 1);
			w.Write(z + 1);
		}

		public void WriteOBJFidx(StreamWriter w)
		{
			w.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", x + 1, y + 1, z + 1);
		}
	}
}
