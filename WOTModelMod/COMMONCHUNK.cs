using System;

namespace WOTModelMod
{
	internal class COMMONCHUNK : IPrimitiveChunk
	{
		private byte[] bData;

		public int DataLen => bData.Length;

		public void Init(byte[] data)
		{
			bData = new byte[data.Length];
			Array.Copy(data, bData, data.Length);
		}

		public byte[] GetData()
		{
			return bData;
		}
	}
}
