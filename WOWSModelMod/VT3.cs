using System;

namespace WOWSModelMod
{
	internal struct VT3
	{
		public float x;

		public float y;

		public float z;

		public VT3(BinaryReader r)
		{
			x = r.ReadSingle();
			y = r.ReadSingle();
			z = r.ReadSingle();
		}

		public VT3 Cross(VT3 b)
		{
			VT3 result = default(VT3);
			result.x = y * b.z - z * b.y;
			result.y = z * b.x - x * b.z;
			result.z = x * b.y - y * b.x;
			return result;
		}

		public void Normalize()
		{
			double num = Math.Sqrt(x * x + y * y + z * z);
			if (num != 0.0)
			{
				x = (float)((double)x / num);
				y = (float)((double)y / num);
				z = (float)((double)z / num);
			}
		}

		public float Dot(VT3 b)
		{
			return x * b.x + y * b.y + z * b.z;
		}

		public static VT3 operator +(VT3 a, VT3 b)
		{
			return new VT3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static VT3 operator -(VT3 a, VT3 b)
		{
			return new VT3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public VT3(float a, float b, float c)
		{
			x = a;
			y = b;
			z = c;
		}

		public void ReadUIntXYZ(BinaryReader r)
		{
			byte[] array = r.ReadBytes(4);
			int num = 0;
			num = (((array[1] & 7) << 29) | (array[0] << 21));
			num >>= 16;
			x = (float)num / 32767f;
			num = (((array[2] & 0x3F) << 26) | ((array[1] & 0xF8) << 18));
			num >>= 16;
			y = (float)num / 32767f;
			num = (((array[3] & 0xFF) << 24) | ((array[2] & 0xC0) << 16));
			num >>= 16;
			z = (float)num / 32767f;
		}

		public void WriteUintXYZ(BinaryWriter w)
		{
			int num = 0;
			short num2 = (short)(x * 32767f);
			num |= ((num2 >> 5) & 0x7FF);
			num2 = (short)(y * 32767f);
			num |= ((num2 << 6) & 0x3FF800);
			num2 = (short)(z * 32767f);
			num |= ((num2 << 16) & -4194304);
			w.Write(num);
		}

		public void Write(BinaryWriter w)
		{
			w.Write(x);
			w.Write(y);
			w.Write(z);
		}
	}
}
