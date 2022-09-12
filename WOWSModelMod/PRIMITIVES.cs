using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WOWSModelMod
{
	internal class PRIMITIVES
	{
		private struct PRIMChunkInfo
		{
			public int ChunkLen;

			public int NameLen;

			public byte[] NameBuf;

			public string ChunkName;

			public byte[] unk;

			public void Write(BinaryWriter w)
			{
				w.Write(ChunkLen);
				w.Write(unk);
				w.Write(NameLen);
				w.Write(NameBuf);
				w.Seek(4);
			}

			public PRIMChunkInfo(BinaryReader r)
			{
				ChunkLen = r.ReadInt32();
				unk = r.ReadBytes(16);
				NameLen = r.ReadInt32();
				NameBuf = r.ReadBytes(NameLen);
				ChunkName = Encoding.UTF8.GetString(NameBuf);
				r.Seek(4);
			}
		}

		private class PRIMINFO
		{
			public int infoNum;

			public PRIMChunkInfo[] fcifs;

			public PRIMINFO(BinaryReader r)
			{
				r.BaseStream.Position = r.BaseStream.Length - 4;
				int num = r.ReadInt32();
				r.BaseStream.Position = r.BaseStream.Length - num - 4;
				List<PRIMChunkInfo> list = new List<PRIMChunkInfo>();
				while (r.BaseStream.Position < r.BaseStream.Length - 4)
				{
					list.Add(new PRIMChunkInfo(r));
				}
				fcifs = list.ToArray();
				infoNum = list.Count;
			}
		}

		private struct PRIMMESH
		{
			public string name;

			public int vtInofId;

			public int idxInfoId;
		}

		private PRIMINFO pInfo;

		private IPrimitiveChunk[] pChunks;

		private int matidx = -1;

		private string bfname;

		private uint FileHead;

		private PRIMMESH[] pMesh;

		public int MatIdx => matidx;

		public PRIMITIVES(string fname)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(fname));
			bfname = fname;
			Init(binaryReader);
			binaryReader.Close();
		}

		private string GetSortName(string fname)
		{
			if (fname.EndsWith("vertices"))
			{
				fname = "root_" + fname.Replace("vertices", "");
			}
			else if (fname.EndsWith("indices"))
			{
				fname = "root_" + fname.Replace("indices", "");
			}
			if (fname.EndsWith("."))
			{
				fname = fname.Substring(0, fname.Length - 1);
			}
			return fname;
		}

		public string[] GetFChunkList(int idx)
		{
			int groupNUM = (pChunks[pMesh[idx].idxInfoId] as INDICES).groupNUM;
			string[] array = new string[groupNUM];
			for (int i = 0; i < groupNUM; i++)
			{
				array[i] = i.ToString();
			}
			return array;
		}

		private void Init(BinaryReader r)
		{
			pInfo = new PRIMINFO(r);
			pChunks = new IPrimitiveChunk[pInfo.infoNum];
			List<string> list = new List<string>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			r.BaseStream.Position = 0L;
			FileHead = r.ReadUInt32();
			for (int i = 0; i < pInfo.infoNum; i++)
			{
				if (pInfo.fcifs[i].ChunkName.EndsWith("vertices"))
				{
					list.Add(GetSortName(pInfo.fcifs[i].ChunkName));
					dictionary2.Add(GetSortName(pInfo.fcifs[i].ChunkName), i);
					pChunks[i] = new VERTICES();
					pChunks[i].Init(r.ReadBytes(pInfo.fcifs[i].ChunkLen));
					r.Seek(4);
				}
				else if (pInfo.fcifs[i].ChunkName.EndsWith("indices"))
				{
					dictionary.Add(GetSortName(pInfo.fcifs[i].ChunkName), i);
					pChunks[i] = new INDICES();
					pChunks[i].Init(r.ReadBytes(pInfo.fcifs[i].ChunkLen));
					r.Seek(4);
				}
				else if (pInfo.fcifs[i].ChunkName.EndsWith("_materials"))
				{
					pChunks[i] = new MATERIAL();
					pChunks[i].Init(r.ReadBytes(pInfo.fcifs[i].ChunkLen));
					r.Seek(4);
					matidx = i;
				}
				else
				{
					pChunks[i] = new COMMONCHUNK();
					pChunks[i].Init(r.ReadBytes(pInfo.fcifs[i].ChunkLen));
					r.Seek(4);
				}
			}
			pMesh = new PRIMMESH[list.Count];
			for (int j = 0; j < pMesh.Length; j++)
			{
				pMesh[j].name = list[j];
				pMesh[j].vtInofId = dictionary2[pMesh[j].name];
				pMesh[j].idxInfoId = dictionary[pMesh[j].name];
				(pChunks[pMesh[j].vtInofId] as VERTICES).Split((pChunks[pMesh[j].idxInfoId] as INDICES).GPIS);
			}
		}

		public void Save()
		{
			Save(bfname + ".imped");
		}

		public void SaveUp()
		{
			Save(bfname);
		}

		public void Save(string fname)
		{
			BinaryWriter binaryWriter = new BinaryWriter(File.Create(fname));
			binaryWriter.Write(FileHead);
			for (int i = 0; i < pChunks.Length; i++)
			{
				binaryWriter.Write(pChunks[i].GetData());
				pInfo.fcifs[i].ChunkLen = pChunks[i].DataLen;
				binaryWriter.Seek(4);
			}
			int num = (int)binaryWriter.BaseStream.Position;
			for (int j = 0; j < pInfo.infoNum; j++)
			{
				pInfo.fcifs[j].Write(binaryWriter);
			}
			num = (int)binaryWriter.BaseStream.Position - num;
			binaryWriter.Write(num);
			binaryWriter.Flush();
			binaryWriter.Close();
		}

		public string GetMatStr()
		{
			return (pChunks[matidx] as MATERIAL).GetStr();
		}

		public void UpdataMatStr(string ttstr)
		{
			(pChunks[matidx] as MATERIAL).UpdataStr(ttstr);
		}

		public float[] GetRenderVERT(int idx, int cid)
		{
			VERTS[] vts = (pChunks[pMesh[idx].vtInofId] as VERTICES).GetVts(cid);
			FIDX[] fdxs = (pChunks[pMesh[idx].idxInfoId] as INDICES).GetFdxs(cid);
			float[] array = new float[fdxs.Length * 3 * 8];
			int[] array2 = new int[3];
			for (int i = 0; i < fdxs.Length; i++)
			{
				array2[0] = fdxs[i].x;
				array2[1] = fdxs[i].y;
				array2[2] = fdxs[i].z;
				for (int j = 0; j < 3; j++)
				{
					array[i * 24 + 8 * j] = vts[array2[j]].vert.x;
					array[i * 24 + 8 * j + 1] = vts[array2[j]].vert.y;
					array[i * 24 + 8 * j + 2] = vts[array2[j]].vert.z;
					array[i * 24 + 8 * j + 3] = vts[array2[j]].normal.x;
					array[i * 24 + 8 * j + 4] = vts[array2[j]].normal.y;
					array[i * 24 + 8 * j + 5] = vts[array2[j]].normal.z;
					array[i * 24 + 8 * j + 6] = vts[array2[j]].tvert.x;
					array[i * 24 + 8 * j + 7] = vts[array2[j]].tvert.y;
				}
			}
			return array;
		}

		public void ExpObj(int idx, int cid)
		{
			string sname = bfname + "." + pMesh[idx].name + "." + cid.ToString() + ".obj";
			OBJMODEL oBJMODEL = new OBJMODEL();
			oBJMODEL.LoadFromMem((pChunks[pMesh[idx].vtInofId] as VERTICES).GetVts(cid), (pChunks[pMesh[idx].idxInfoId] as INDICES).GetFdxs(cid));
			oBJMODEL.SaveObjFile(sname);
		}

		public void ImpObj(string objfname, int idx, int cid, bool invFace)
		{
			OBJMODEL oBJMODEL = new OBJMODEL();
			oBJMODEL.LoadObjFile(objfname, invFace);
			VERTS[] array = oBJMODEL.getvts();
			(pChunks[pMesh[idx].vtInofId] as VERTICES).UpdataVts(oBJMODEL.getvts(), cid);
			(pChunks[pMesh[idx].idxInfoId] as INDICES).Updata(oBJMODEL.getfdx(), array.Length, cid);
		}

		public void ImpVert(string objfname, int idx, int cid, bool invFace)
		{
			OBJMODEL oBJMODEL = new OBJMODEL();
			oBJMODEL.LoadObjVert(objfname, invFace);
			oBJMODEL.getvts();
			(pChunks[pMesh[idx].vtInofId] as VERTICES).UpdataOnlyVts(oBJMODEL.getvts(), cid);
		}

		public void AddObj(string objfname, int idx, bool invFace)
		{
			OBJMODEL oBJMODEL = new OBJMODEL();
			oBJMODEL.LoadObjFile(objfname, invFace);
			VERTS[] array = oBJMODEL.getvts();
			(pChunks[pMesh[idx].vtInofId] as VERTICES).AddVts(oBJMODEL.getvts());
			(pChunks[pMesh[idx].idxInfoId] as INDICES).Add(oBJMODEL.getfdx(), array.Length);
		}

		public void RemoveObj(int idx, int cid)
		{
			(pChunks[pMesh[idx].vtInofId] as VERTICES).Remove(cid);
			(pChunks[pMesh[idx].idxInfoId] as INDICES).Remove(cid);
		}

		public string[] GetChunkList()
		{
			string[] array = new string[pMesh.Length];
			for (int i = 0; i < pMesh.Length; i++)
			{
				array[i] = GetSortName(pMesh[i].name);
			}
			return array;
		}
	}
}
