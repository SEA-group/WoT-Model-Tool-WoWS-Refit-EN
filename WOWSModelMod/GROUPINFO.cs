namespace WOWSModelMod
{
	internal class GROUPINFO
	{
		public int un0;

		public int faceNum;

		public int un1;

		public int vertNum;

		public int stidxf;

		public int stidxv;

		public GROUPINFO(BinaryReader r, int ldf, int ldv)
		{
			un0 = r.ReadInt32();
			faceNum = r.ReadInt32();
			un1 = r.ReadInt32();
			vertNum = r.ReadInt32();
			stidxf = ldf;
			stidxv = ldv;
		}

		public GROUPINFO(int fnum, int vnum)
		{
			un0 = 0;
			faceNum = fnum;
			un1 = 0;
			vertNum = vnum;
			stidxf = 0;
			stidxv = 0;
		}

		public void WriteFDX(BinaryWriter w, FIDX[] fdxs, bool list32)
		{
			for (int i = 0; i < faceNum; i++)
			{
				fdxs[i].Write(w, stidxv, list32);
			}
		}

		public void WriteInfo(BinaryWriter w)
		{
			w.Write(un0);
			w.Write(faceNum);
			w.Write(un1);
			w.Write(vertNum);
		}
	}
}
