using System;
using System.Collections.Generic;
using System.IO;

namespace WOWSModelMod
{
	internal class OBJMODEL
	{
		private List<VERTS> ots;

		private List<FIDX> ofdx;

		public VERTS[] getvts()
		{
			return GenTBN();
		}

		public FIDX[] getfdx()
		{
			return ofdx.ToArray();
		}

		private byte[] GetWWWII(string curw)
		{
			if (curw == "0000000000")
			{
				return new byte[0];
			}
			byte[] array = new byte[5];
			for (int i = 0; i < 5; i++)
			{
				array[i] = Convert.ToByte(curw.Substring(i * 2, 2), 16);
			}
			return array;
		}

		public void LoadObjFile(string ofname, bool invFace)
		{
			StreamReader streamReader = new StreamReader(ofname);
			char[] separator = new char[1]
			{
				' '
			};
			char[] separator2 = new char[1]
			{
				'/'
			};
			List<VT3> list = new List<VT3>();
			List<VT3> list2 = new List<VT3>();
			List<VT2> list3 = new List<VT2>();
			List<FIDX> list4 = new List<FIDX>();
			List<FIDX> list5 = new List<FIDX>();
			List<FIDX> list6 = new List<FIDX>();
			string text = "00000000";
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			VT3 item = default(VT3);
			VT3 item2 = default(VT3);
			VT2 item3 = default(VT2);
			FIDX item4 = default(FIDX);
			FIDX item5 = default(FIDX);
			FIDX item6 = default(FIDX);
			while (!streamReader.EndOfStream)
			{
				string text2 = streamReader.ReadLine().ToLower();
				string[] array = text2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length <= 0)
				{
					continue;
				}
				if (array[0] == "v")
				{
					item.x = Convert.ToSingle(array[1]);
					item.y = Convert.ToSingle(array[2]);
					item.z = Convert.ToSingle(array[3]);
					list.Add(item);
				}
				else if (array[0] == "vn")
				{
					item2.x = Convert.ToSingle(array[1]);
					item2.y = Convert.ToSingle(array[2]);
					item2.z = Convert.ToSingle(array[3]);
					list2.Add(item2);
				}
				else if (array[0] == "vt")
				{
					item3.x = Convert.ToSingle(array[1]);
					item3.y = 1f - Convert.ToSingle(array[2]);
					list3.Add(item3);
				}
				else if (array[0] == "g")
				{
					text = array[1];
					if (text.Length != 10)
					{
						text = "0000000000";
						continue;
					}
					for (int i = 0; i < 10; i++)
					{
						if ((text[i] < '0' || text[i] > '9') && (text[i] < 'a' || text[i] > 'f') && (text[i] < 'A' || text[i] > 'F'))
						{
							text = "0000000000";
							break;
						}
					}
				}
				else if (array[0] == "f")
				{
					string[] array2 = array[1].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
					item4.x = int.Parse(array2[0]) - 1;
					item5.x = int.Parse(array2[1]) - 1;
					item6.x = int.Parse(array2[2]) - 1;
					array2 = array[2].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
					item4.y = int.Parse(array2[0]) - 1;
					item5.y = int.Parse(array2[1]) - 1;
					item6.y = int.Parse(array2[2]) - 1;
					array2 = array[3].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
					item4.z = int.Parse(array2[0]) - 1;
					item5.z = int.Parse(array2[1]) - 1;
					item6.z = int.Parse(array2[2]) - 1;
					dictionary.Add(list4.Count, text);
					list4.Add(item4);
					list5.Add(item5);
					list6.Add(item6);
				}
			}
			streamReader.Close();
			ots = new List<VERTS>();
			ofdx = new List<FIDX>();
			Dictionary<long, int> dictionary2 = new Dictionary<long, int>();
			VERTS item7 = default(VERTS);
			VERTS item8 = default(VERTS);
			VERTS item9 = default(VERTS);
			for (int j = 0; j < list4.Count; j++)
			{
				FIDX fIDX = default(FIDX);
				long key = (long)((ulong)(uint)list4[j].x << 32) | ((long)list6[j].x << 16) | (uint)list5[j].x;
				if (dictionary2.ContainsKey(key))
				{
					fIDX.x = dictionary2[key];
				}
				else
				{
					item7.wwstr = dictionary[j];
					item7.wwwii = GetWWWII(item7.wwstr);
					item7.binormal = default(VT3);
					item7.tangent = default(VT3);
					item7.tvert = list3[list5[j].x];
					item7.tvert2 = list3[list5[j].x];
					item7.vert = list[list4[j].x];
					item7.normal = list2[list6[j].x];
					dictionary2.Add(key, ots.Count);
					fIDX.x = ots.Count;
					ots.Add(item7);
				}
				key = (((long)list4[j].y << 32) | ((long)list6[j].y << 16) | (uint)list5[j].y);
				if (dictionary2.ContainsKey(key))
				{
					fIDX.y = dictionary2[key];
				}
				else
				{
					item8.wwstr = dictionary[j];
					item8.wwwii = GetWWWII(item8.wwstr);
					item8.binormal = default(VT3);
					item8.tangent = default(VT3);
					item8.tvert = list3[list5[j].y];
					item8.tvert2 = list3[list5[j].y];
					item8.vert = list[list4[j].y];
					item8.normal = list2[list6[j].y];
					dictionary2.Add(key, ots.Count);
					fIDX.y = ots.Count;
					ots.Add(item8);
				}
				key = (((long)list4[j].z << 32) | ((long)list6[j].z << 16) | (uint)list5[j].z);
				if (dictionary2.ContainsKey(key))
				{
					fIDX.z = dictionary2[key];
				}
				else
				{
					item9.wwstr = dictionary[j];
					item9.wwwii = GetWWWII(item9.wwstr);
					item9.binormal = default(VT3);
					item9.tangent = default(VT3);
					item9.tvert = list3[list5[j].z];
					item9.tvert2 = list3[list5[j].z];
					item9.vert = list[list4[j].z];
					item9.normal = list2[list6[j].z];
					dictionary2.Add(key, ots.Count);
					fIDX.z = ots.Count;
					ots.Add(item9);
				}
				FIDX item10 = fIDX;
				if (invFace)
				{
					item10.x = fIDX.z;
					item10.z = fIDX.x;
				}
				ofdx.Add(item10);
			}
		}

		public void LoadObjVert(string ofname, bool invFace)
		{
			StreamReader streamReader = new StreamReader(ofname);
			char[] separator = new char[1]
			{
				' '
			};
			char[] separator2 = new char[1]
			{
				'/'
			};
			List<VT3> list = new List<VT3>();
			List<VT3> list2 = new List<VT3>();
			List<VT2> list3 = new List<VT2>();
			List<FIDX> list4 = new List<FIDX>();
			List<FIDX> list5 = new List<FIDX>();
			List<FIDX> list6 = new List<FIDX>();
			VT3 item = default(VT3);
			VT3 item2 = default(VT3);
			VT2 item3 = default(VT2);
			FIDX item4 = default(FIDX);
			FIDX item5 = default(FIDX);
			FIDX item6 = default(FIDX);
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine().ToLower();
				string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length > 0)
				{
					if (array[0] == "v")
					{
						item.x = Convert.ToSingle(array[1]);
						item.y = Convert.ToSingle(array[2]);
						item.z = Convert.ToSingle(array[3]);
						list.Add(item);
					}
					else if (array[0] == "vn")
					{
						item2.x = Convert.ToSingle(array[1]);
						item2.y = Convert.ToSingle(array[2]);
						item2.z = Convert.ToSingle(array[3]);
						list2.Add(item2);
					}
					else if (array[0] == "vt")
					{
						item3.x = Convert.ToSingle(array[1]);
						item3.y = 1f - Convert.ToSingle(array[2]);
						list3.Add(item3);
					}
					else if (array[0] == "f")
					{
						string[] array2 = array[1].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
						item4.x = Convert.ToUInt16(array2[0]) - 1;
						item5.x = Convert.ToUInt16(array2[1]) - 1;
						item6.x = Convert.ToUInt16(array2[2]) - 1;
						array2 = array[2].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
						item4.y = Convert.ToUInt16(array2[0]) - 1;
						item5.y = Convert.ToUInt16(array2[1]) - 1;
						item6.y = Convert.ToUInt16(array2[2]) - 1;
						array2 = array[3].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
						item4.z = Convert.ToUInt16(array2[0]) - 1;
						item5.z = Convert.ToUInt16(array2[1]) - 1;
						item6.z = Convert.ToUInt16(array2[2]) - 1;
						list4.Add(item4);
						list5.Add(item5);
						list6.Add(item6);
					}
				}
			}
			streamReader.Close();
			ots = new List<VERTS>();
			ofdx = new List<FIDX>();
			VERTS item7 = default(VERTS);
			for (int i = 0; i < list.Count; i++)
			{
				item7.wwstr = "0000000000";
				item7.wwwii = GetWWWII(item7.wwstr);
				item7.binormal = default(VT3);
				item7.tangent = default(VT3);
				item7.tvert = list3[i];
				item7.tvert2 = list3[i];
				item7.vert = list[i];
				item7.normal = list2[i];
				ots.Add(item7);
			}
			ofdx.AddRange(list4.ToArray());
		}

		//private VT3 GenTangent(VT3 v1, VT3 v2, VT2 st1, VT2 st2)
		//{
		//	float num = st1.x * st2.y - st2.x * st1.y;
		//	if (num != 0f)
		//	{
		//		num = 1f / num;
		//	}
		//	VT3 result = default(VT3);
		//	result.x = num * (v1.x * st2.y + v2.x * (0f - st1.y));
		//	result.y = num * (v1.y * st2.y + v2.y * (0f - st1.y));
		//	result.z = num * (v1.z * st2.y + v2.z * (0f - st1.y));
		//	return result;
		//}

		private VT3 GenTangentPro(VT3 v1, VT3 v2, VT3 v3, VT2 st12, VT2 st13, VT3 norm)
		{
			float num1 = norm.x + norm.y + norm.z;
			if (num1 != 0f)
			{
				num1 = 1f / num1;
			}
			float t12 = num1 * (norm.x * (v1.x - v2.x) + norm.y * (v1.y - v2.y) + norm.z * (v1.z - v2.z));
			float v2px = v2.x + t12 * norm.x;
			float v2py = v2.y + t12 * norm.y;
			float v2pz = v2.z + t12 * norm.z;
			float t13 = num1 * (norm.x * (v1.x - v3.x) + norm.y * (v1.y - v3.y) + norm.z * (v1.z - v3.z));
			float v3px = v3.x + t13 * norm.x;
			float v3py = v3.y + t13 * norm.y;
			float v3pz = v3.z + t13 * norm.z;
			float num2 = st12.x * st13.y - st13.x * st12.y;
			if (num2 != 0f)
			{
				num2 = 1f / num2;
			}
			VT3 result = default(VT3);
			result.x = num2 * ((v2px - v1.x) * st13.y + (v3px - v1.x) * (0f - st12.y));
			result.y = num2 * ((v2py - v1.y) * st13.y + (v3py - v1.y) * (0f - st12.y));
			result.z = num2 * ((v2pz - v1.z) * st13.y + (v3pz - v1.z) * (0f - st12.y));
			return result;
		}

		//private VT3 GenBinormal(VT3 v1, VT3 v2, VT2 st1, VT2 st2)
		//{
		//	float num = st1.x * st2.y - st2.x * st1.y;
		//	if (num != 0f)
		//	{
		//		num = 1f / num;
		//	}
		//	VT3 result = default(VT3);
		//	result.x = num * (v1.x * (0f - st2.x) + v2.x * st1.x);
		//	result.y = num * (v1.y * (0f - st2.x) + v2.y * st1.x);
		//	result.z = num * (v1.z * (0f - st2.x) + v2.z * st1.x);
		//	return result;
		//}

		private VERTS[] GenTBN()
		{
			VERTS[] array = ots.ToArray();
			FIDX[] array2 = ofdx.ToArray();
			for (int i = 0; i < ofdx.Count; i++)
			{
				VT3 vert = array[array2[i].x].vert;
				VT3 vert2 = array[array2[i].y].vert;
				VT3 vert3 = array[array2[i].z].vert;
				VT2 tvert = array[array2[i].x].tvert;
				VT2 tvert2 = array[array2[i].y].tvert;
                VT2 tvert3 = array[array2[i].z].tvert;
                VT3 normal = array[array2[i].x].normal;
                VT3 normal2 = array[array2[i].y].normal;
                VT3 normal3 = array[array2[i].z].normal;
				//array[array2[i].x].tangent += GenTangent(vert2 - vert, vert3 - vert, tvert2 - tvert, tvert3 - tvert);
				//array[array2[i].y].tangent += GenTangent(vert3 - vert2, vert - vert2, tvert3 - tvert2, tvert - tvert2);
				//array[array2[i].z].tangent += GenTangent(vert - vert3, vert2 - vert3, tvert - tvert3, tvert2 - tvert3);
				array[array2[i].x].tangent += GenTangentPro(vert, vert2, vert3, tvert2 - tvert, tvert3 - tvert, normal);
				array[array2[i].y].tangent += GenTangentPro(vert2, vert3, vert, tvert3 - tvert2, tvert - tvert2, normal2);
				array[array2[i].z].tangent += GenTangentPro(vert3, vert, vert2, tvert - tvert3, tvert2 - tvert3, normal3);
				//array[array2[i].x].binormal += GenBinormal(vert2 - vert, vert3 - vert, tvert2 - tvert, tvert3 - tvert);
				//array[array2[i].y].binormal += GenBinormal(vert3 - vert2, vert - vert2, tvert3 - tvert2, tvert - tvert2);
				//array[array2[i].z].binormal += GenBinormal(vert - vert3, vert2 - vert3, tvert - tvert3, tvert2 - tvert3);
			}
            for (int j = 0; j < ots.Count; j++)
            {
                array[j].normal.Normalize();
                array[j].tangent.Normalize();
                array[j].binormal = array[j].normal.Cross(array[j].tangent);
                array[j].binormal.Normalize();
            }
            return array;
		}

		public void LoadFromMem(VERTS[] vts, FIDX[] fdx)
		{
			ots = new List<VERTS>(vts);
			ofdx = new List<FIDX>(fdx);
		}

		public void SaveObjFile(string sname)
		{
			StreamWriter streamWriter = new StreamWriter(sname);
			for (int i = 0; i < ots.Count; i++)
			{
				ots[i].WriteOBJVert(streamWriter);
			}
			for (int j = 0; j < ots.Count; j++)
			{
				ots[j].WriteOBJNormal(streamWriter);
			}
			for (int k = 0; k < ots.Count; k++)
			{
				ots[k].WriteOBJTvert(streamWriter);
			}
			streamWriter.WriteLine();
			Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
			for (int l = 0; l < ofdx.Count; l++)
			{
				string wwstr = ots[ofdx[l].x].wwstr;
				if (dictionary.TryGetValue(wwstr, out List<int> value))
				{
					value.Add(l);
					continue;
				}
				value = new List<int>();
				value.Add(l);
				dictionary.Add(wwstr, value);
			}
			foreach (string key in dictionary.Keys)
			{
				List<int> list = dictionary[key];
				streamWriter.WriteLine("g {0}", key);
				for (int m = 0; m < list.Count; m++)
				{
					ofdx[list[m]].WriteOBJFidx(streamWriter);
				}
				streamWriter.WriteLine();
			}
			streamWriter.Flush();
			streamWriter.Close();
		}
	}
}
