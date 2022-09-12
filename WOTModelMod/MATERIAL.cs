using System;
using System.Text;

namespace WOTModelMod
{
	internal class MATERIAL : IPrimitiveChunk
	{
		private byte[] bData;

		public int DataLen => bData.Length;

		public void Init(byte[] data)
		{
			bData = new byte[data.Length];
			Array.Copy(data, bData, data.Length);
		}

		public void UpdataStr(string ttstr)
		{
			bData = Encoding.UTF8.GetBytes(ttstr);
		}

		public string GetStr()
		{
			return Encoding.UTF8.GetString(bData);
		}

		public byte[] GetData()
		{
			return bData;
		}
	}
}
