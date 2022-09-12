using System.IO;

namespace WOTModelMod
{
	internal struct VERTS
	{
		public VT3 vert;

		public VT3 normal;

		public VT2 tvert;

		public VT3 tangent;

		public VT3 binormal;

		public VT3 reflect;

		public byte[] wwwii;

		public string wwstr;

		public bool isWire;

		public bool isAlpha;

		public VERTS(BinaryReader r, bool skinned, bool alpha, bool wire)
		{
			isAlpha = false;
			isWire = false;
			if(alpha)
			{
				vert = new VT3(r);
				normal = new VT3(r);
				tvert = new VT2(r);
				tangent = default(VT3);
				binormal = default(VT3);
				wwwii = new byte[0];
				wwstr = "0000000000";
				reflect = normal;
				isAlpha = true;
			}
			else if (wire)
			{
				vert = new VT3(r);
				normal = new VT3(r);
				tvert = new VT2(r);
				tangent = default(VT3);
				binormal = default(VT3);
				wwwii = new byte[0];
				wwstr = "0000000000";
				reflect = default(VT3);
				reflect.ReadUIntXYZ(r);
				isWire = true;
			}
			else
			{
				vert = new VT3(r);
				normal = default(VT3);
				normal.ReadUIntXYZ(r);
				tvert = new VT2(r);
				if (skinned)
				{
					wwwii = r.ReadBytes(5);
				}
				else
				{
					wwwii = new byte[0];
				}
				tangent = default(VT3);
				tangent.ReadUIntXYZ(r);
				binormal = default(VT3);
				binormal.ReadUIntXYZ(r);
				reflect = normal;
				if (skinned)
				{
					wwstr = string.Format("{0}{1}{2}{3}{4}", wwwii[0].ToString("X2"), wwwii[1].ToString("X2"), wwwii[2].ToString("X2"), wwwii[3].ToString("X2"), wwwii[4].ToString("X2"));
				}
				else
				{
					wwstr = "0000000000";
				}
			}
		}

		public void Write(BinaryWriter w, bool alpha, bool wire)
		{
			if (alpha)
            {
				vert.Write(w);
				normal.Write(w);
				tvert.Write(w);
			}
			else if (wire)
            {
				vert.Write(w);
				normal.Write(w);
				tvert.Write(w);
                //normal.WriteUintXYZ(w);
                float tempNum = 0.9991f;
				w.Write(tempNum);
			}
            else
            {
				vert.Write(w);
				normal.WriteUintXYZ(w);
				tvert.Write(w);
				w.Write(wwwii);
				tangent.WriteUintXYZ(w);
				binormal.WriteUintXYZ(w);
			}
		}

		public void WriteOBJVert(StreamWriter w)
		{
			w.WriteLine("v {0} {1} {2}", vert.x, vert.y, vert.z);
		}

		public void WriteOBJNormal(StreamWriter w)
		{
			w.WriteLine("vn {0} {1} {2}", normal.x, normal.y, normal.z);
		}

		public void WriteOBJTvert(StreamWriter w)
		{
			w.WriteLine("vt {0} {1}", tvert.x, 1f - tvert.y);
		}
	}
}
