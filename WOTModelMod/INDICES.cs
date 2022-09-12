using System;
using System.Collections.Generic;
using System.IO;

namespace WOTModelMod
{
	internal class INDICES : IPrimitiveChunk
	{
		private byte[] bData;

		private byte[] header;

		private int GroupNum;

		private List<FIDX[]> fdx;

        private List<GROUPINFO> gpis;

		public int DataLen => bData.Length;

		public int groupNUM => GroupNum;

		public List<GROUPINFO> GPIS => gpis;

		public FIDX[] GetFdxs(int Gid)
		{
			return fdx[Gid];
		}

		public void Init(byte[] data)
		{
			bData = new byte[data.Length];
			Array.Copy(data, bData, data.Length);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(data));
			header = binaryReader.ReadBytes(64);
			int num = binaryReader.ReadInt32() / 3;
			GroupNum = binaryReader.ReadInt32();
			gpis = new List<GROUPINFO>();
			fdx = new List<FIDX[]>();
			FIDX[] array = new FIDX[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new FIDX(binaryReader);
			}
			int j = 0;
			int num2 = 0;
			int num3 = 0;
			for (; j < GroupNum; j++)
			{
				GROUPINFO gROUPINFO = new GROUPINFO(binaryReader, num2, num3);
				FIDX[] array2 = new FIDX[gROUPINFO.faceNum];
				for (int k = 0; k < gROUPINFO.faceNum; k++)
				{
					array2[k].x = array[k + num2].x - num3;
					array2[k].y = array[k + num2].y - num3;
					array2[k].z = array[k + num2].z - num3;
				}
				fdx.Add(array2);
				num2 += gROUPINFO.faceNum;
				num3 += gROUPINFO.vertNum;
				gpis.Add(gROUPINFO);
			}
			binaryReader.Close();
		}

		public void Add(FIDX[] nfdx, int vertNum)
		{
			fdx.Add(nfdx);
			GROUPINFO item = new GROUPINFO(nfdx.Length, vertNum);
			gpis.Add(item);
			GroupNum = gpis.Count;
			UpINFO();
		}

		public void Remove(int idx)
		{
			fdx.RemoveAt(idx);
			gpis.RemoveAt(idx);
			GroupNum = gpis.Count;
			UpINFO();
		}

		private void UpINFO()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			for (; i < GroupNum; i++)
			{
				gpis[i].stidxf = num;
				gpis[i].stidxv = num2;
				gpis[i].un0 = num * 3;
				gpis[i].un1 = num2;
				num += gpis[i].faceNum;
				num2 += gpis[i].vertNum;
			}
		}

		public void Updata(FIDX[] nfdx, int vertNum, int gid)
		{
			fdx.RemoveAt(gid);
			fdx.Insert(gid, nfdx);
			gpis[gid].faceNum = nfdx.Length;
			gpis[gid].vertNum = vertNum;
			UpINFO();
		}

		public void Write(BinaryWriter w)
		{
			w.Write(header);
			int num = 0;
			for (int i = 0; i < GroupNum; i++)
			{
				num += gpis[i].faceNum;
			}
			w.Write(num * 3);
			w.Write(GroupNum);
			for (int j = 0; j < GroupNum; j++)
			{
				gpis[j].WriteFDX(w, fdx[j]);
			}
			for (int k = 0; k < GroupNum; k++)
			{
				gpis[k].WriteInfo(w);
			}
		}

		public byte[] GetData()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			Write(binaryWriter);
			binaryWriter.Flush();
			bData = memoryStream.ToArray();
			binaryWriter.Close();
			return bData;
		}
	}
}
