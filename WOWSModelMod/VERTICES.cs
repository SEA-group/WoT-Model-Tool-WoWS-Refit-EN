using System;
using System.Collections.Generic;
using System.IO;

namespace WOWSModelMod
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
			bool uv2 = false;
			if (header[6] == 50)    // check if the 7th letter is "2" behind "uv"
            {
				uv2 = true;
            }
			if (header[6] == 105)	// check if the 7th letter is "iiiww"
			{
				if (header[11] == 0)	// check if "tb" exists after "iiiww"
                {
					skinned = true;
					alpha = true;
                }
                else
                {
					skinned = true;
				}	
			}
			if (header[6] == 114)	// check if the 7th letter is "tb"
			{
				wire = true;
			}
			if (header[6] == 0)		// check if the header ends at "xyznuv"
			{
				alpha = true;
			}
			for (int i = 0; i < num; i++)
			{
				vts[i] = new VERTS(binaryReader, skinned, alpha, wire, uv2);
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
					array[i].tvert2 = nvts[i].tvert2;
					array[i].normal = nvts[i].normal;
					array[i].tangent = nvts[i].tangent;
					array[i].binormal = nvts[i].binormal;
					array[i].radius = nvts[i].radius;
				}
				spvts.Insert(Chunkid, array);
			}
		}

		public void Write(BinaryWriter w)
		{
			w.Write(header);
			bool wire = false;
			bool alpha = false;
			bool skinned = false;
			bool uv2 = false;
			if (header[6] == 50)
			{
				uv2 = true;
			}
			if (header[6] == 105)
			{
				if (header[11] == 0)
				{
					skinned = true;
					alpha = true;
				}
				else
				{
					skinned = true;
				}
			}
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
					spvts[j][k].Write(w, skinned, alpha, wire, uv2);
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
