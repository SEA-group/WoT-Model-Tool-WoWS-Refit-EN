using System.IO;

namespace WOWSModelMod
{
	internal struct FIDX
	{
		public int x;

		public int y;

		public int z;

		public FIDX(BinaryReader r, bool list32)
		{
			if (list32)
            {
				x = r.ReadInt32();
				y = r.ReadInt32();
				z = r.ReadInt32();
				if (x > 16777217)
                {
					x -= 16711680;
				}
				if (y > 16777217)
				{
					y -= 16711680;
				}
				if (z > 16777217)
				{
					z -= 16711680;
				}
			}
			else
            {
				x = r.ReadUInt16();
				y = r.ReadUInt16();
				z = r.ReadUInt16();
			}
		}

		public void Write(BinaryWriter w, bool list32)
		{
			if (list32)
			{
				//if (x> 65536)
				//{
				//	w.Write(x + 16711680);
				//}
				//else
				//{
				//	w.Write(x);
				//}
				//if (y > 65536)
				//{
				//	w.Write(y + 16711680);
				//}
				//else
				//{
				//	w.Write(y);
				//}
				//if (z > 65536)
				//{
				//	w.Write(z + 16711680);
				//}
				//else
				//{
				//	w.Write(z);
				//}
				w.Write(x);
				w.Write(y);
				w.Write(z);
			}
			else
			{
				w.Write((ushort)x);
				w.Write((ushort)y);
				w.Write((ushort)z);
			}
		}

		public void Write(BinaryWriter w, int Lvnum, bool list32)
		{
			if (list32)
            {
				//if (x + Lvnum > 65536)
				//{
				//	w.Write(x + Lvnum + 16711680);
				//}
				//else
				//{	
				//	w.Write(x + Lvnum); 
				//}
				//if (y + Lvnum > 65536)
				//{
				//	w.Write(y + Lvnum + 16711680);
				//}
				//else
				//{
				//	w.Write(y + Lvnum);
				//}
				//if (z + Lvnum > 65536)
				//{
				//	w.Write(z + Lvnum + 16711680);
				//}
				//else
				//{
				//	w.Write(z + Lvnum);
				//}
				w.Write(x + Lvnum);
				w.Write(y + Lvnum);
				w.Write(z + Lvnum);
			}
			else
            {
				w.Write((ushort)(x + Lvnum));
				w.Write((ushort)(y + Lvnum));
				w.Write((ushort)(z + Lvnum));
			}
		}

		//public void WriteLFidx(BinaryWriter w)
		//{
		//	w.Write(x + 1);
		//	w.Write(y + 1);
		//	w.Write(z + 1);
		//}

		public void WriteOBJFidx(StreamWriter w)
		{
			w.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", x + 1, y + 1, z + 1);
		}
	}
}
