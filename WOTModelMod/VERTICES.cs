using System;
using System.Collections.Generic;
using System.IO;

namespace WOTModelMod
{
	internal class VERTICES : IPrimitiveChunk
	{
		private byte[] bData;

		private byte[] header;

		private VERTS[] vts;

		private List<VERTS[]> spvts;

		public int DataLen => bData.Length;

		public VERTS[] GetVts(int cid)
		{
			return spvts[cid];
		}

		public void Init(byte[] data)
		{
			bData = new byte[data.Length];
			Array.Copy(data, bData, data.Length);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(data));
			header = binaryReader.ReadBytes(64);
			int num = binaryReader.ReadInt32();
			vts = new VERTS[num];
			bool skinned = false;
			bool wire = false;
			bool alpha = false;
			if (header[6] == 105)
			{
				skinned = true;
			}
			if (header[6] == 114)
			{
				wire = true;
			}
			if (header[6] == 0)
			{
				alpha = true;
			}
			for (int i = 0; i < num; i++)
			{
				vts[i] = new VERTS(binaryReader, skinned, alpha, wire);
			}
			binaryReader.Close();
		}

		public void Split(List<GROUPINFO> gips)
		{
			spvts = new List<VERTS[]>();
			for (int i = 0; i < gips.Count; i++)
			{
				VERTS[] array = new VERTS[gips[i].vertNum];
				Array.Copy(vts, gips[i].stidxv, array, 0, gips[i].vertNum);
				spvts.Add(array);
			}
		}

		public void AddVts(VERTS[] nvts)
		{
			spvts.Add(nvts);
		}

		public void Remove(int idx)
		{
			spvts.RemoveAt(idx);
		}

		public void UpdataVts(VERTS[] nvts, int Chunkid)
		{
			spvts.RemoveAt(Chunkid);
			spvts.Insert(Chunkid, nvts);
		}

		public void UpdataOnlyVts(VERTS[] nvts, int Chunkid)
		{
			if (nvts.Length == spvts[Chunkid].Length)
			{
				VERTS[] array = spvts[Chunkid];
				spvts.RemoveAt(Chunkid);
				for (int i = 0; i < nvts.Length; i++)
				{
					array[i].vert = nvts[i].vert;
					array[i].tvert = nvts[i].tvert;
					array[i].normal = nvts[i].normal;
					array[i].tangent = nvts[i].tangent;
					array[i].binormal = nvts[i].binormal;
				}
				spvts.Insert(Chunkid, array);
			}
		}

		public void Write(BinaryWriter w)
		{
			w.Write(header);
			bool wire = false;
			bool alpha = false;
			if (header[6] == 114)
			{
				wire = true;
			}
			if (header[6] == 0)
			{
				alpha = true;
			}
			int num = 0;
			for (int i = 0; i < spvts.Count; i++)
			{
				num += spvts[i].Length;
			}
			w.Write(num);
			for (int j = 0; j < spvts.Count; j++)
			{
				for (int k = 0; k < spvts[j].Length; k++)
				{
					spvts[j][k].Write(w, alpha, wire);
				}
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
