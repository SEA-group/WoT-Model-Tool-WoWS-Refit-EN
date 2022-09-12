namespace WOWSModelMod
{
	internal interface IPrimitiveChunk
	{
		int DataLen
		{
			get;
		}

		void Init(byte[] data);

		byte[] GetData();
	}
}
